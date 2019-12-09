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

        // GET: Reward
        public async Task<IActionResult> Index()
        {
            var models = (from reward in await _context.Rewards.ToListAsync() select _mapper.Map<RewardViewModel>(reward)).ToList();
            return View(models);
        }

        // GET: Reward/Details/5
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

        // GET: Reward/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reward/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RewardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                ModelState.AddModelError(nameof(UserViewModel.PhotoPath), "Can't be empty");
            }
            var path = _appEnvironment.WebRootPath + "/Images/" + file.FileName;
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            model.ImagePath = path;
            var reward = _mapper.Map<Reward>(model);
            _context.Add(reward);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reward/Edit/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RewardViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var reward = await _context.Rewards.FindAsync(model.Id);
            try
            {
                var file = Request.Form.Files.FirstOrDefault();
                if (file != null)
                {
                    var path = _appEnvironment.WebRootPath + "/Images/" + file.FileName;
                    await using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    if (reward.ImagePath != null || reward.ImagePath != string.Empty)
                    {
                        if (System.IO.File.Exists(reward.ImagePath)) System.IO.File.Delete(reward.ImagePath);
                    }

                    reward.ImagePath = path;
                }

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

        // GET: Reward/Delete/5
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

        // POST: Reward/Delete/5
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
