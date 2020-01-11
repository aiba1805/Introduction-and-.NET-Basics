using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AS.Core.Comparers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AS.Core.Data;
using AS.Core.Models;
using AS.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AS.Core.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly string _defaultPass = "#123Aa";
        
        public UserController(ApplicationContext context,IWebHostEnvironment appEnvironment, IMapper mapper
        ,UserManager<User> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Route("users"), Authorize(Roles = "authorized,admin")]
        public async Task<IActionResult> Index()
        {
            var models = (from user in await _context.Users.ToListAsync() select _mapper.Map<UserViewModel>(user)).ToList();
            return View(models);
        }
        
        [Route("users/{s}")]
        [Authorize(Roles="authorized,admin")]
        public async Task<IActionResult> Index(char s)
        {
            var users = _context.Users.Where(x => x.FirstName.ToLower().First() == char.ToLower(s));
            var models = (from user in users select _mapper.Map<UserViewModel>(user));
            return View(models);
        }
        
        [Route("users/{firstname}")]
        [Authorize(Roles="authorized,admin")]
        public async Task<IActionResult> Index(string firstName)
        {
            var users = _context.Users.Where(x => x.FirstName.ToLower().StartsWith(firstName.ToLower()) || x.FirstName.ToLower().EndsWith(firstName.ToLower()));
            var models = (from user in users select _mapper.Map<UserViewModel>(user));
            return View(models);
        }
        
        [Route("users/{firstname}_{lastname}")]
        [Authorize(Roles="authorized,admin")]
        public async Task<IActionResult> Index(string firstName, string lastname)
        {
            var users = _context.Users.Where(x => string.Equals(x.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase) && string.Equals(x.LastName, lastname, StringComparison.CurrentCultureIgnoreCase));
            users.ToList().Sort(new UserBirthDateComparer());
            var user = await users.FirstOrDefaultAsync();
            return View("Details",_mapper.Map<UserViewModel>(user));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GuestIndex()
        {
            var models = (from user in await _context.Users.ToListAsync() select _mapper.Map<UserViewModel>(user)).ToList();
            return View(models);
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> AddReward(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var userRewards = user.Rewards.Where(x=>x.UserId == user.Id).Select(x=>x.Reward).ToList();
            var rewards = _context.Rewards.ToList();
            foreach (var ur in userRewards.Where(ur => rewards.Exists(x => x.Id == ur.Id)))
            {
                rewards.Remove(ur);
            }
            
            var model = new AddRewardViewModel()
            {
                Rewards = rewards,
                UserId = user.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> AddReward(AddRewardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.UserRewards.Add(new UserReward()
            {
                UserId = model.UserId,
                RewardId = model.RewardId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        [Route("reward-user/{userId}_{rewardId}")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> AddReward(Guid userId, Guid rewardId)
        {
            if (_context.UserRewards.Any(x => x.UserId == userId && x.RewardId == rewardId)) return BadRequest();
            _context.UserRewards.Add(new UserReward()
            {
                UserId = userId,
                RewardId = rewardId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<UserViewModel>(user));
        }
        
        [Route("create-user")]
        [Authorize(Roles="admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create-user"), Authorize(Roles = "admin"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var path = _appEnvironment.WebRootPath + "/Photos/" + model.Photo.FileName;
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.Photo.CopyToAsync(fileStream);
            }
            model.PhotoPath = path;
            var user = _mapper.Map<User>(model);
            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, _defaultPass);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        
        [Route("user/{id}/edit")]
        [Authorize(Roles="admin,authorized")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<EditUserViewModel>(user));
        }
        
        [Route("user/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin,authorized")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _context.Users.FindAsync(model.Id);
            try
            {
                if (model.Photo != null)
                {
                    var path = _appEnvironment.WebRootPath + "/Photos/" + model.Photo.FileName;
                    await using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await model.Photo.CopyToAsync(fileStream);
                    }

                    if (user.PhotoPath != null || user.PhotoPath != string.Empty)
                    {
                        if (System.IO.File.Exists(user.PhotoPath)) System.IO.File.Delete(user.PhotoPath);
                    }
                    user.PhotoPath = path;
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        
        [Route("user/{id}/delete")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<UserViewModel>(user));
        }

        [Route("user/{id}/delete"), HttpPost, ActionName("Delete"), Authorize(Roles = "admin"),
         ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user.PhotoPath != null || user.PhotoPath != string.Empty)
            {
                if(System.IO.File.Exists(user.PhotoPath)) System.IO.File.Delete(user.PhotoPath);
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
