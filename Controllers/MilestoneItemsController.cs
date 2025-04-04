using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCApplicationToDo.Data;
using MVCApplicationToDo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            var selectedProjectTitle = HttpContext.Session.GetString("SelectedProjectTitle");

            ViewBag.SelectedProjectId = selectedProjectId;
            ViewBag.SelectedProjectTitle = selectedProjectTitle;

            if (selectedProjectId == null)
            {
                return View(new List<MilestoneItem>());
            }

            var milestoneItems = await _context.MilestoneItems
                .Where(m => m.ProjectId == selectedProjectId)
                .ToListAsync();

            return View(milestoneItems);
        }

        // GET: MilestoneItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneItems = await _context.MilestoneItems
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (milestoneItems == null)
            {
                return NotFound();
            }

            return View(milestoneItems);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneItems = await _context.MilestoneItems
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (milestoneItems == null)
            {
                return NotFound();
            }
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Title");
            return View(milestoneItems);
        }

        // POST: Projects/Edit/5
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
                    // Atualiza o item no banco de dados
                    _context.Update(milestoneItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MilestoItemExist(milestoneItem.Id))
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

            // Caso o ModelState não seja válido, recarrega o ViewBag para o dropdown
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Name", milestoneItem.ProjectId);
            return View(milestoneItem);
        }

        //GET: MilestoneItems/Create
        public IActionResult Create()
            {
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            var selectedProjectTitle = HttpContext.Session.GetString("SelectedProjectTitle");

            ViewBag.SelectedProjectId = selectedProjectId;
            ViewBag.SelectedProjectTitle = selectedProjectTitle;

            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projects"); // Redirecionar caso não haja um projeto selecionado
            }

            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Title");
            return View(); 
            }

        // POST: MilestoneItems/Create
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
            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Title", milestoneItem.ProjectId);
            return View(milestoneItem);
        }

        // GET: MilestoneItems/CreateMultiple
        public async Task<IActionResult> CreateMultiple()
        {
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            var selectedProjectTitle = HttpContext.Session.GetString("SelectedProjectTitle");

            ViewBag.SelectedProjectId = selectedProjectId;
            ViewBag.SelectedProjectTitle = selectedProjectTitle;

            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projects"); // Redirecionar caso não haja um projeto selecionado
            }

            var milestoneItems = await _context.MilestoneItems
                .Where(m => m.ProjectId == selectedProjectId)
                .ToListAsync();

            return View(milestoneItems);
        }

        // POST: MilestoneItems/CreateMultiple
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMultiple(int projectId, List<MilestoneItem> milestoneItems)
        {
            if (ModelState.IsValid)
            {
                foreach (var milestoneItem in milestoneItems)
                {
                    if (milestoneItem.Id == 0)
                    {
                        milestoneItem.ProjectId = projectId;
                        _context.MilestoneItems.Add(milestoneItem);
                    }
                    else
                    {
                        milestoneItem.ProjectId = projectId;
                        _context.MilestoneItems.Update(milestoneItem);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ProjectId = new SelectList(_context.Projects, "Id", "Title", projectId);
            ViewBag.SelectedProjectId = projectId;
            return View(milestoneItems);

        }

        // GET: Projects/Delete/5
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

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var milestoneItem = await _context.MilestoneItems
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            var projectId = milestoneItem.ProjectId; 

            if (milestoneItem != null)
            {
                _context.MilestoneItems.Remove(milestoneItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = projectId });
        }

        private bool MilestoItemExist(int id)
        {
            return _context.MilestoneItems.Any(e => e.Id == id);
        }
    }
}

