using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCApplicationToDo.Data;
using MVCApplicationToDo.Models;

namespace MVCApplicationToDo.Controllers
{
    public class MilestonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MilestonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Milestones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Milestones.Include(m => m.MilestoneChain);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Milestones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestones
                .Include(m => m.MilestoneChain)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestone == null)
            {
                return NotFound();
            }

            return View(milestone);
        }

        // GET: Milestones/Create
        public IActionResult Create()
        {
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title");
            return View();
        }

        // POST: Milestones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title,Value,Order,MilestoneChainId")] Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(milestone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title", milestone.MilestoneChainId);
            return View(milestone);
        }

        // GET: Milestones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestones.FindAsync(id);
            if (milestone == null)
            {
                return NotFound();
            }
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title", milestone.MilestoneChainId);
            return View(milestone);
        }

        // POST: Milestones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title,Value,Order,MilestoneChainId")] Milestone milestone)
        {
            if (id != milestone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(milestone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MilestoneExists(milestone.Id))
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
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title", milestone.MilestoneChainId);
            return View(milestone);
        }

        // GET: Milestones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestone = await _context.Milestones
                .Include(m => m.MilestoneChain)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestone == null)
            {
                return NotFound();
            }

            return View(milestone);
        }

        // POST: Milestones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var milestone = await _context.Milestones.FindAsync(id);
            if (milestone != null)
            {
                _context.Milestones.Remove(milestone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MilestoneExists(int id)
        {
            return _context.Milestones.Any(e => e.Id == id);
        }
    }
}
