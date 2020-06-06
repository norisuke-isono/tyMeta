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
    public class CornersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CornersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Corners
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Corners.Include(c => c.TvProgram);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Corners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corner = await _context.Corners
                .Include(c => c.TvProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (corner == null)
            {
                return NotFound();
            }

            return View(corner);
        }

        // GET: Corners/Create
        public IActionResult Create()
        {
            ViewData["TvProgramId"] = new SelectList(_context.TvPrograms, "Id", "Name");
            return View();
        }

        // POST: Corners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type,TvProgramId,Id,CreateUserId,UpdateUserId,CreatedAt,UpdatedAt")] Corner corner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(corner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TvProgramId"] = new SelectList(_context.TvPrograms, "Id", "Name", corner.TvProgramId);
            return View(corner);
        }

        // GET: Corners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corner = await _context.Corners.FindAsync(id);
            if (corner == null)
            {
                return NotFound();
            }
            ViewData["TvProgramId"] = new SelectList(_context.TvPrograms, "Id", "Name", corner.TvProgramId);
            return View(corner);
        }

        // POST: Corners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Type,TvProgramId,Id,CreateUserId,UpdateUserId,CreatedAt,UpdatedAt")] Corner corner)
        {
            if (id != corner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CornerExists(corner.Id))
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
            ViewData["TvProgramId"] = new SelectList(_context.TvPrograms, "Id", "Name", corner.TvProgramId);
            return View(corner);
        }

        // GET: Corners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corner = await _context.Corners
                .Include(c => c.TvProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (corner == null)
            {
                return NotFound();
            }

            return View(corner);
        }

        // POST: Corners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corner = await _context.Corners.FindAsync(id);
            _context.Corners.Remove(corner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CornerExists(int id)
        {
            return _context.Corners.Any(e => e.Id == id);
        }
    }
}