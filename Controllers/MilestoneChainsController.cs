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
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            var selectedProjectTitle = HttpContext.Session.GetString("SelectedProjectTitle");

            ViewBag.SelectedProjectId = selectedProjectId;
            ViewBag.SelectedProjectTitle = selectedProjectTitle;

            var milestoneChains = await _context.MilestoneChains
                .Where(m => m.ProjectId == selectedProjectId)
                .ToListAsync();

            return View(milestoneChains);
        }

        // GET: MilestoneChains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneChain = await _context.MilestoneChains
                .Include(m => m.Milestones)
                .ThenInclude(m => m.MilestoneItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (milestoneChain == null)
            {
                return NotFound();
            }

            return View(milestoneChain);
        }

        // GET: MilestoneChains/Create
        public async Task<IActionResult> Create(int? stepCount)
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

            ViewBag.MilestoneItems = new SelectList(milestoneItems, "Id", "Code");

            // Se o stepCount não for passado, use um valor padrão (1)
            int count = stepCount ?? 1;

            // Gere os milestones dinamicamente
            var milestoneChain = new MilestoneChain
            {
                Code = "MC01",
                Title = "Nova Cadeia de Marcos",
                Milestones = Enumerable.Range(1, count).Select(i => new Milestone
                {
                    Code = $"MS0{i}",
                    Title = $"Milestone {i}",
                    Value = 0,
                    Order = i
                }).ToList()
            };

            return View(milestoneChain);


        }

        // POST: MilestoneChains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title,Milestones")] MilestoneChain milestoneChain)
        {
            var selectedProjectId = HttpContext.Session.GetInt32("SelectedProjectId");
            ViewBag.SelectedProjectId = selectedProjectId;

            if (selectedProjectId == null)
            {
                return RedirectToAction("Index", "Projects"); // Redirecionar caso não haja projeto selecionado
            }

            var mscode = _context.MilestoneChains
                .Where(m => m.Code == milestoneChain.Code)
                .FirstOrDefault();
            if (mscode != null)
            {
                ModelState.AddModelError("Code", "Já existe uma cadeia de marcos com esse código.");
            }

                if (ModelState.IsValid)
            {
                try
                {
                    milestoneChain.ProjectId = selectedProjectId.Value;
                    _context.MilestoneChains.Add(milestoneChain);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Erro ao salvar os dados: {ex.Message}");
                }
            }

            var milestoneItems = await _context.MilestoneItems
                .Where(m => m.ProjectId == selectedProjectId)
                .ToListAsync();

            ViewBag.MilestoneItems = new SelectList(milestoneItems, "Id", "Code");
            return View(milestoneChain);
        }

        // GET: MilestoneChains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var milestoneChain = await _context.MilestoneChains
                .Include(m => m.Milestones)
                .FirstOrDefaultAsync(m => m.Id == id);

            var milestoneItems = await _context.MilestoneItems
                .Where(m => m.ProjectId == milestoneChain.ProjectId)
                .ToListAsync();

            ViewBag.MilestoneItems = new SelectList(milestoneItems, "Id", "Code");

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title,Milestones")] MilestoneChain milestoneChain)
        {
            if (id != milestoneChain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMilestoneChain = await _context.MilestoneChains
                        .Include(m => m.Milestones)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existingMilestoneChain == null)
                    {
                        return NotFound();
                    }

                    // Atualize as propriedades da MilestoneChain
                    existingMilestoneChain.Code = milestoneChain.Code;
                    existingMilestoneChain.Title = milestoneChain.Title;

                    // Atualize os Milestones
                    foreach (var milestone in milestoneChain.Milestones)
                    {
                        var existingMilestone = existingMilestoneChain.Milestones
                            .FirstOrDefault(m => m.Id == milestone.Id);

                        if (existingMilestone != null)
                        {
                            existingMilestone.MilestoneItemId = milestone.MilestoneItemId;
                            existingMilestone.Value = milestone.Value;
                        }
                        else
                        {
                            existingMilestoneChain.Milestones.Add(milestone);
                        }
                    }

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