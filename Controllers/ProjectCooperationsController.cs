using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_management_app.Models;

namespace project_management_app.Controllers
{
    public class ProjectCooperationsController : Controller
    {
        private readonly DB_COMPANYContext _context;

        public ProjectCooperationsController(DB_COMPANYContext context)
        {
            _context = context;
        }


        

        // GET: ProjectCooperations
        public async Task<IActionResult> Index()
        {
            var dB_COMPANYContext = _context.ProjectCooperation.Include(p => p.Project).Include(p => p.Team);
            return View(await dB_COMPANYContext.ToListAsync());
        }

       [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateCompositeKey(int ProjectId, int ? TeamId)
        {
            
            var ProjectCooperation = _context.ProjectCooperation.FirstOrDefault(
                pc => pc.ProjectId == ProjectId && pc.TeamId == TeamId );
            if (ProjectCooperation != null)
            {
                return Json($"This team already has assigned selected project.");
            }
            return Json(true);
        }
        // GET: ProjectCooperations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCooperation = await _context.ProjectCooperation
                .Include(p => p.Project)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectCooperation == null)
            {
                return NotFound();
            }

            return View(projectCooperation);
        }

        // GET: ProjectCooperations/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Set<Teams>(), "Id", "Name");
            return View();
        }

        // POST: ProjectCooperations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,TeamId,DateAssigned")] ProjectCooperation projectCooperation)
        {
            
            if (ModelState.IsValid)
            {
                
                _context.Add(projectCooperation);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Name", projectCooperation.ProjectId);
            ViewData["TeamId"] = new SelectList(_context.Set<Teams>(), "Id", "Name", projectCooperation.TeamId);
            return View(projectCooperation);
        }

        // GET: ProjectCooperations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCooperation = await _context.ProjectCooperation.FindAsync(id);
            if (projectCooperation == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Name", projectCooperation.ProjectId);
            ViewData["TeamId"] = new SelectList(_context.Set<Teams>(), "Id", "Name", projectCooperation.TeamId);
            return View(projectCooperation);
        }

        // POST: ProjectCooperations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProjectId,TeamId,DateAssigned")] ProjectCooperation projectCooperation)
        {
            if (id != projectCooperation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectCooperation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectCooperationExists(projectCooperation.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Name", projectCooperation.ProjectId);
            ViewData["TeamId"] = new SelectList(_context.Set<Teams>(), "Id", "Name", projectCooperation.TeamId);
            return View(projectCooperation);
        }

        // GET: ProjectCooperations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCooperation = await _context.ProjectCooperation
                .Include(p => p.Project)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectCooperation == null)
            {
                return NotFound();
            }

            return View(projectCooperation);
        }

        // POST: ProjectCooperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var projectCooperation = await _context.ProjectCooperation.FindAsync(id);
            _context.ProjectCooperation.Remove(projectCooperation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectCooperationExists(long id)
        {
            return _context.ProjectCooperation.Any(e => e.Id == id);
        }
    }
}
