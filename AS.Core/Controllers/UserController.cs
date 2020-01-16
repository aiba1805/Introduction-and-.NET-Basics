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
using AS.Core.Services;
using AS.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AS.Core.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        
        private readonly UserService _userService;
        
        public UserController(ApplicationContext context,IWebHostEnvironment appEnvironment, IMapper mapper, UserService userService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
            _userService = userService;
        }

        [Route("users")]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsers();
            var models = (from user in users select _mapper.Map<UserViewModel>(user)).ToList();
            return View(models);
        }
        
        [Route("users/{s}")]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Index(char s)
        {
            var users = await _userService.GetUsers(s);
            var models = (from user in users select _mapper.Map<UserViewModel>(user));
            return View(models);
        }
        
        [Route("users/{firstname}")]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Index(string firstName)
        {
            var users = await _userService.GetUsers(firstName);
            var models = (from user in users select _mapper.Map<UserViewModel>(user));
            return View(models);
        }
        
        [Route("users/{firstname}_{lastname}")]
        public async Task<IActionResult> Index(string firstName, string lastname)
        {
            var user = await _userService.GetUser(firstName, lastname);
            return View("Details",_mapper.Map<UserViewModel>(user));
        }
        
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> GuestIndex()
        {
            var users = await _userService.GetUsers();
            var models = (from user in users select _mapper.Map<UserViewModel>(user)).ToList();
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
        
        [ResponseCache(CacheProfileName = "Default"),HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUser((Guid) id);
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
            var result = await _userService.AddUser(model);
            if (result.Succeeded) return RedirectToAction(nameof(Index));
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        
        [Route("user/{id}/edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUser((Guid)id);
            if (user == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<EditUserViewModel>(user));
        }
        
        [Route("user/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _userService.EditUser(model);
            if (result.Succeeded) return RedirectToAction(nameof(Index));
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
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

            var user = await _userService.GetUser((Guid) id);
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
            var result = await _userService.DeleteUser(id);
            if (result.Succeeded) return RedirectToAction(nameof(Index));
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        [Route("create-user-json"), Authorize(Roles = "admin"), HttpPost]
        public async Task<JsonResult> CreateJson(UserViewModel model)
        {
            if (!ModelState.IsValid) return Json(null);
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file == null) return Json(null);
            model.Photo = file;
            var user = await _userService.AddUserJson(model);
            return Json(_mapper.Map<UserViewModel>(user) ?? null);
        }

        [Route("user/{id}/delete-json"), HttpPost, ActionName("Delete"), Authorize(Roles = "admin")]
        public async Task<JsonResult> DeleteJson(string id)
        {
            var result = await _userService.DeleteUser(Guid.Parse(id));
            if (result.Succeeded) return Json(true);
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Json(false);
        }
        
        /*public async Task<JsonResult> EditFirstName(UserViewModel model)
        {
            
        }*/
    }
}
