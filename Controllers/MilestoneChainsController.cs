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
    public class MilestoneChainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MilestoneChainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MilestoneChains
        public async Task<IActionResult> Index()
        {
            return View(await _context.MilestoneChains.ToListAsync());
        }

        // GET: MilestoneChains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneChain = await _context.MilestoneChains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestoneChain == null)
            {
                return NotFound();
            }

            return View(milestoneChain);
        }

        // GET: MilestoneChains/Create
        public IActionResult Create()
        {
            var milestone = new Milestone() { Code = "M01", Title = "Milestone 01" };
            var milestoneChain = new MilestoneChain() { Code = "MC01", Title = "Milestone Chain 01", Milestones = [milestone] };
            return View(milestoneChain);

        }

        // POST: MilestoneChains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title")] MilestoneChain milestoneChain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(milestoneChain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(milestoneChain);
        }

        // GET: MilestoneChains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneChain = await _context.MilestoneChains.FindAsync(id);
            if (milestoneChain == null)
            {
                return NotFound();
            }
            return View(milestoneChain);
        }

        // POST: MilestoneChains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title")] MilestoneChain milestoneChain)
        {
            if (id != milestoneChain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(milestoneChain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MilestoneChainExists(milestoneChain.Id))
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
            return View(milestoneChain);
        }

        // GET: MilestoneChains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneChain = await _context.MilestoneChains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestoneChain == null)
            {
                return NotFound();
            }

            return View(milestoneChain);
        }

        // POST: MilestoneChains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var milestoneChain = await _context.MilestoneChains.FindAsync(id);
            if (milestoneChain != null)
            {
                _context.MilestoneChains.Remove(milestoneChain);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MilestoneChainExists(int id)
        {
            return _context.MilestoneChains.Any(e => e.Id == id);
        }
    }
}
