using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AS.Core.Comparers;
using AS.Core.Data;
using AS.Core.Models;
using AS.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AS.Core.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;
        private readonly string _defaultPass = "qwe";
        private readonly string _photosDirectory = "/Photos/";

        public UserService(UserManager<User> userManager, IMemoryCache cache, ApplicationContext context, IWebHostEnvironment appEnvironment, IMapper mapper)
        {
            _userManager = userManager;
            _cache = cache;
            _context = context;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }
        
        public async Task<List<User>> GetUsers(char s)
        {
            var users = await _context.Users.Where(x => x.FirstName.ToLower().First() == char.ToLower(s)).ToListAsync();
            return users;
        }

        public async Task<List<User>> GetUsers(string firstName)
        {
            var users = await _context.Users.Where(x => x.FirstName.ToLower().StartsWith(firstName.ToLower()) 
                                                  || x.FirstName.ToLower().EndsWith(firstName.ToLower())).ToListAsync();
            return users;
        }

        public async Task<User> GetUser(string firstName, string lastName)
        {
            var users = _context.Users.Where(x => string.Equals(x.FirstName, firstName, 
                                                      StringComparison.CurrentCultureIgnoreCase) 
                                                  && string.Equals(x.LastName, lastName, StringComparison.CurrentCultureIgnoreCase));
            users.ToList().Sort(new UserBirthDateComparer());
            var user = await users.FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> AddUserJson(UserViewModel model)
        {
            model.PhotoPath = await SaveFile(model.Photo);
            var user = _mapper.Map<User>(model);
            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, _defaultPass);
            if (result.Succeeded) await _context.SaveChangesAsync();
            return await _userManager.FindByEmailAsync(user.Email);
        }
        
        public async Task<IdentityResult> AddUser(UserViewModel model)
        {
            model.PhotoPath = await SaveFile(model.Photo);
            var user = _mapper.Map<User>(model);
            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, _defaultPass);
            if (result.Succeeded) await _context.SaveChangesAsync();
            return result;
        }

        public async Task<User> GetUser(Guid id)
        {
            if (_cache.TryGetValue(id, out User user)) return user;
            user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                _cache.Set(user.Id, user,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }
            return user;
        }

        public async Task<IdentityResult> EditUser(EditUserViewModel model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            try
            {
                if (model.Photo != null)
                {
                    var path = await SaveFile(model.Photo);
                    DeleteFile(user.PhotoPath);
                    user.PhotoPath = path;
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded) await _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(model.Id))
                    return IdentityResult.Failed();
                
                else
                    throw;
            }
        }


        public async Task<IdentityResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return IdentityResult.Failed();
            DeleteFile(user.PhotoPath);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) await _context.SaveChangesAsync();
            return result;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private void DeleteFile(string path)
        {
            if (path != null || path != string.Empty)
            {
                if (File.Exists(path)) System.IO.File.Delete(path);
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var path = _appEnvironment.WebRootPath + _photosDirectory + file.FileName;
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return path;
        }
    }
}