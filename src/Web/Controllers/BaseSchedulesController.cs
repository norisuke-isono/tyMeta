using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class BaseSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaseSchedules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BaseSchedules.Include(b => b.Corner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BaseSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseSchedule = await _context.BaseSchedules
                .Include(b => b.Corner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseSchedule == null)
            {
                return NotFound();
            }

            return View(baseSchedule);
        }

        // GET: BaseSchedules/Create
        public IActionResult Create()
        {
            ViewData["CornerId"] = new SelectList(_context.Corners, "Id", "Name");
            return View();
        }

        // POST: BaseSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sequence,CornerId,Id,CreateUserId,UpdateUserId,CreatedAt,UpdatedAt")] BaseSchedule baseSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baseSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CornerId"] = new SelectList(_context.Corners, "Id", "Name", baseSchedule.CornerId);
            return View(baseSchedule);
        }

        // GET: BaseSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseSchedule = await _context.BaseSchedules.FindAsync(id);
            if (baseSchedule == null)
            {
                return NotFound();
            }
            ViewData["CornerId"] = new SelectList(_context.Corners, "Id", "Name", baseSchedule.CornerId);
            return View(baseSchedule);
        }

        // POST: BaseSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sequence,CornerId,Id,CreateUserId,UpdateUserId,CreatedAt,UpdatedAt")] BaseSchedule baseSchedule)
        {
            if (id != baseSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baseSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseScheduleExists(baseSchedule.Id))
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
            ViewData["CornerId"] = new SelectList(_context.Corners, "Id", "Name", baseSchedule.CornerId);
            return View(baseSchedule);
        }

        // GET: BaseSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseSchedule = await _context.BaseSchedules
                .Include(b => b.Corner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseSchedule == null)
            {
                return NotFound();
            }

            return View(baseSchedule);
        }

        // POST: BaseSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baseSchedule = await _context.BaseSchedules.FindAsync(id);
            _context.BaseSchedules.Remove(baseSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaseScheduleExists(int id)
        {
            return _context.BaseSchedules.Any(e => e.Id == id);
        }
    }
}