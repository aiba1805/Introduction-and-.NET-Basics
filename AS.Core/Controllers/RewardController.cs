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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AS.Core.Controllers
{
    [Authorize(Roles = "admin")]
    public class RewardController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        
        public RewardController(ApplicationContext context,IWebHostEnvironment appEnvironment, IMapper mapper)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [Route("rewards")]
        public async Task<IActionResult> Index()
        {
            var models = (from reward in await _context.Rewards.ToListAsync() select _mapper.Map<RewardViewModel>(reward)).ToList();
            return View(models);
        }
        
        [Route("rewards/{s}")]
        public async Task<IActionResult> Index(char s)
        {
            var rewards = _context.Rewards.Where(x => x.Title.ToLower().First() == char.ToLower(s));
            var models = (from reward in rewards select _mapper.Map<RewardViewModel>(reward));
            return View(models);
        }
        
        [Route("rewards/{title}")]
        public async Task<IActionResult> Index(string title)
        {
            var rewards = _context.Rewards.Where(x => x.Title.Contains(title,StringComparison.CurrentCultureIgnoreCase));
            var models = (from reward in rewards select _mapper.Map<RewardViewModel>(reward));
            return View(models);
        }

        // GET: Reward/Details/5
        [Route("reward/{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _context.Rewards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reward == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<RewardViewModel>(reward));
        }

        [Route("reward/{title}")]
        public async Task<IActionResult> Details(string title)
        {
            if (title == null)
            {
                return NotFound();
            }
            var reward = await _context.Rewards
                .FirstOrDefaultAsync(m => string.Equals(m.Title,title.Replace('_', ' '),StringComparison.InvariantCultureIgnoreCase));
            if (reward == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<RewardViewModel>(reward));
        }
        
        // GET: Reward/Create
        [Route("create-reward")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create-reward")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RewardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var path = _appEnvironment.WebRootPath + "/Images/" + model.Image.FileName;
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }
            model.ImagePath = path;
            var reward = _mapper.Map<Reward>(model);
            _context.Add(reward);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reward/Edit/5
        [Route("reward/{id}/edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _context.Rewards.FindAsync(id);
            if (reward == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<RewardViewModel>(reward));
        }

        
        [Route("reward/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RewardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var reward = await _context.Rewards.FindAsync(model.Id);
            try
            {
                var path = _appEnvironment.WebRootPath + "/Images/" + model.Image.FileName;
                await using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                if (reward.ImagePath != null || reward.ImagePath != string.Empty)
                {
                    if (System.IO.File.Exists(reward.ImagePath)) System.IO.File.Delete(reward.ImagePath);
                }
                reward.ImagePath = path;
                _context.Update(reward);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardExists(model.Id))
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

        [Route("reward/{id}/delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _context.Rewards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reward == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<RewardViewModel>(reward));
        }

        [Route("reward/{id}/delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reward = await _context.Rewards.FindAsync(id);
            if(reward.ImagePath != null || reward.ImagePath != string.Empty)
            {
                if(System.IO.File.Exists(reward.ImagePath)) System.IO.File.Delete(reward.ImagePath);
            }
            _context.Rewards.Remove(reward);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RewardExists(Guid id)
        {
            return _context.Rewards.Any(e => e.Id == id);
        }
    }
}
