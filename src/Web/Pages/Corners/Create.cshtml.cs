using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages.Corners
{
    public class CreateModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public CreateModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TvProgramId"] = new SelectList(_context.TvPrograms, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Corner Corner { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Corners.Add(Corner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
