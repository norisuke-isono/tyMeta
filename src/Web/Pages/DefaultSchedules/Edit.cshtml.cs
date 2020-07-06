using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages.Schedules
{
    public class EditModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public EditModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DefaultSchedule DefaultSchedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DefaultSchedule = await _context.DefaultSchedules
                .Include(b => b.Corner)
                .Include(b => b.TvProgram).FirstOrDefaultAsync(m => m.Id == id);

            if (DefaultSchedule == null)
            {
                return NotFound();
            }
            ViewData["CornerId"] = new SelectList(_context.Corners, "Id", "Name");
            ViewData["TvProgramId"] = new SelectList(_context.TvPrograms, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DefaultSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DefaultScheduleExists(DefaultSchedule.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DefaultScheduleExists(int id)
        {
            return _context.DefaultSchedules.Any(e => e.Id == id);
        }
    }
}
