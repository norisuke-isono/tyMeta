using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages.TvPrograms
{
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public DeleteModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TvProgram TvProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TvProgram = await _context.TvPrograms.FirstOrDefaultAsync(m => m.Id == id);

            if (TvProgram == null)
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

            TvProgram = await _context.TvPrograms.FindAsync(id);

            if (TvProgram != null)
            {
                _context.TvPrograms.Remove(TvProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
