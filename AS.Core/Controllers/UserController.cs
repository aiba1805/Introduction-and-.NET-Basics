using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AS.Core.Data;
using AS.Core.Models;
using AS.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AS.Core.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        
        public UserController(ApplicationContext context,IWebHostEnvironment appEnvironment, IMapper mapper)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var models = (from user in await _context.Users.ToListAsync() select _mapper.Map<UserViewModel>(user)).ToList();
            return View(models);
        }
        
        // GET: User/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                ModelState.AddModelError(nameof(UserViewModel.PhotoPath), "Can't be empty");
            }
            var path = _appEnvironment.WebRootPath + "/Photos/" + file.FileName;
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            model.PhotoPath = path;
            var user = _mapper.Map<User>(model);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<UserViewModel>(user));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _context.Users.FindAsync(model.Id);
            try
            {
                var file = Request.Form.Files.FirstOrDefault();
                if (file != null)
                {
                    var path = _appEnvironment.WebRootPath + "/Photos/" + file.FileName;
                    await using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    if(user.PhotoPath != null || user.PhotoPath != string.Empty)
                    {
                        if(System.IO.File.Exists(user.PhotoPath)) System.IO.File.Delete(user.PhotoPath);
                    }
                    user.PhotoPath = path;
                }
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
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user.PhotoPath != null || user.PhotoPath != string.Empty)
            {
                if(System.IO.File.Exists(user.PhotoPath)) System.IO.File.Delete(user.PhotoPath);
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
