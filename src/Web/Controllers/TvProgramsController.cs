using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Models;

namespace Web.Controllers
{
    public class TvProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TvProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TvPrograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.TvPrograms.ToListAsync());
        }

        // GET: TvPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvProgram = await _context.TvPrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvProgram == null)
            {
                return NotFound();
            }

            return View(tvProgram);
        }

        // GET: TvPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TvPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreateUserId,UpdateUserId,CreatedAt,UpdatedAt")] TvProgram tvProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvProgram);
        }

        // GET: TvPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvProgram = await _context.TvPrograms.FindAsync(id);
            if (tvProgram == null)
            {
                return NotFound();
            }
            return View(tvProgram);
        }

        // POST: TvPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,CreateUserId,UpdateUserId,CreatedAt,UpdatedAt")] TvProgram tvProgram)
        {
            if (id != tvProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvProgramExists(tvProgram.Id))
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
            return View(tvProgram);
        }

        // GET: TvPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvProgram = await _context.TvPrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvProgram == null)
            {
                return NotFound();
            }

            return View(tvProgram);
        }

        // POST: TvPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tvProgram = await _context.TvPrograms.FindAsync(id);
            _context.TvPrograms.Remove(tvProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvProgramExists(int id)
        {
            return _context.TvPrograms.Any(e => e.Id == id);
        }
    }
}
