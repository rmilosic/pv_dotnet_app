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
    public class WorkItemsController : Controller
    {
        private readonly DB_COMPANYContext _context;

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateCompositeKey(int ProjectId, int EmployeeId)
        {
            // TODO: fix this
            // get employee team
            // var Employee = _context.Employees.Where(e => e.Id == EmployeeId).ToList();
            // var TeamId = _context.Employees.Include(e => e.Projects).Where(e => e.Id == EmployeeId && e.Projects.Where(p => p.Id == ProjectId));

            System.Console.WriteLine("validation started");

            System.Console.WriteLine($"Project param {ProjectId}");
            System.Console.WriteLine($"Project selection {Project}");
            
        
            if (Project == null)
            {
                System.Console.WriteLine("validation failed");
                return Json($"This employee currently doesn't cooperate on the selected project");
            }
            System.Console.WriteLine("validation ok");
            return Json(true);
        }

        public WorkItemsController(DB_COMPANYContext context)
        {
            _context = context;
        }

        // GET: WorkItems
        public async Task<IActionResult> Index()
        {
            var dB_COMPANYContext = _context.WorkItems.Include(w => w.Employee).Include(w => w.Project);
            return View(await dB_COMPANYContext.ToListAsync());
        }

        // GET: WorkItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItems = await _context.WorkItems
                .Include(w => w.Employee)
                .Include(w => w.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workItems == null)
            {
                return NotFound();
            }

            return View(workItems);
        }

        // GET: WorkItems/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employees>(), "Id", "Email");
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Name");
            return View();
        }

        // POST: WorkItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ProjectId,EmployeeId,DateCreated,DateStarted,DateFinished,DateDue")] WorkItems workItems)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(workItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employees>(), "Id", "Email", workItems.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Name", workItems.ProjectId);
            return View(workItems);
        }

        // GET: WorkItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItems = await _context.WorkItems.FindAsync(id);
            if (workItems == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employees>(), "Id", "Id", workItems.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Id", workItems.ProjectId);
            return View(workItems);
        }

        // POST: WorkItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description,ProjectId,EmployeeId,DateCreated,DateStarted,DateFinished,DateDue")] WorkItems workItems)
        {
            if (id != workItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkItemsExists(workItems.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employees>(), "Id", "Id", workItems.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Projects>(), "Id", "Id", workItems.ProjectId);
            return View(workItems);
        }

        // GET: WorkItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItems = await _context.WorkItems
                .Include(w => w.Employee)
                .Include(w => w.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workItems == null)
            {
                return NotFound();
            }

            return View(workItems);
        }

        // POST: WorkItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var workItems = await _context.WorkItems.FindAsync(id);
            _context.WorkItems.Remove(workItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkItemsExists(long id)
        {
            return _context.WorkItems.Any(e => e.Id == id);
        }
    }
}
