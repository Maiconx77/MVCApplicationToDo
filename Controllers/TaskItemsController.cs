using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCApplicationToDo.Data;
using MVCApplicationToDo.Models;

namespace MVCApplicationToDo.Controllers
{
    public class TaskItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskItems
        public async Task<IActionResult> Index()
        {
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            var selectedProjectTitle = HttpContext.Session.GetString("SelectedProjectTitle");

            ViewBag.SelectedProjectId = selectedProjectId;
            ViewBag.SelectedProjectTitle = selectedProjectTitle;

            var applicationDbContext = _context.TaskItems.Include(t => t.MilestoneChain).Include(t => t.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaskItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems
                .Include(t => t.MilestoneChain)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // GET: TaskItems/Create
        public IActionResult Create()
        {

            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            var selectedProjectTitle = HttpContext.Session.GetString("SelectedProjectTitle");

            ViewBag.SelectedProjectId = selectedProjectId;
            ViewBag.SelectedProjectTitle = selectedProjectTitle;

            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projects"); 
            }

            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains.Where(mc => mc.ProjectId == selectedProjectId.Value), "Id", "Title");
            return View();

        }

        // POST: TaskItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,TaskNumber,Weight,MilestoneChainId,IsCompleted")] TaskItem taskItem)
        {
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");

            if (ModelState.IsValid)
            {
                taskItem.ProjectId = selectedProjectId ?? 0; 
                _context.Add(taskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title", taskItem.MilestoneChainId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", taskItem.ProjectId);
            return View(taskItem);
        }

        // GET: TaskItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title", taskItem.MilestoneChainId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", taskItem.ProjectId);
            return View(taskItem);
        }

        // POST: TaskItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Weight,MilestoneChainId,ProjectId,IsCompleted")] TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.Id))
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
            ViewData["MilestoneChainId"] = new SelectList(_context.MilestoneChains, "Id", "Title", taskItem.MilestoneChainId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", taskItem.ProjectId);
            return View(taskItem);
        }

        // GET: TaskItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems
                .Include(t => t.MilestoneChain)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // POST: TaskItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
