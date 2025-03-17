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
    public class MilestoneItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MilestoneItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MilestoneItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MilestoneItems.Include(m => m.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MilestoneItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneItem = await _context.MilestoneItems
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestoneItem == null)
            {
                return NotFound();
            }

            return View(milestoneItem);
        }

        // GET: MilestoneItems/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title");
            return View();
        }

        // POST: MilestoneItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title,ProjectId")] MilestoneItem milestoneItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(milestoneItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", milestoneItem.ProjectId);
            return View(milestoneItem);
        }

        // GET: MilestoneItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneItem = await _context.MilestoneItems.FindAsync(id);
            if (milestoneItem == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", milestoneItem.ProjectId);
            return View(milestoneItem);
        }

        // POST: MilestoneItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title,ProjectId")] MilestoneItem milestoneItem)
        {
            if (id != milestoneItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(milestoneItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MilestoneItemExists(milestoneItem.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", milestoneItem.ProjectId);
            return View(milestoneItem);
        }

        // GET: MilestoneItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneItem = await _context.MilestoneItems
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestoneItem == null)
            {
                return NotFound();
            }

            return View(milestoneItem);
        }

        // POST: MilestoneItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var milestoneItem = await _context.MilestoneItems.FindAsync(id);
            if (milestoneItem != null)
            {
                _context.MilestoneItems.Remove(milestoneItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MilestoneItemExists(int id)
        {
            return _context.MilestoneItems.Any(e => e.Id == id);
        }
    }
}
