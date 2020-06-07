using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages.Corners
{
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public DeleteModel(Infrastructure.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Corner = await _context.Corners.FindAsync(id);

            if (Corner != null)
            {
                _context.Corners.Remove(Corner);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
