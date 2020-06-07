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

namespace Web.Pages.Corners
{
    public class EditModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public EditModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Corner Corner { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Corner = await _context.Corners
                .Include(c => c.TvProgram).FirstOrDefaultAsync(m => m.Id == id);

            if (Corner == null)
            {
                return NotFound();
            }
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

            _context.Attach(Corner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CornerExists(Corner.Id))
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

        private bool CornerExists(int id)
        {
            return _context.Corners.Any(e => e.Id == id);
        }
    }
}
