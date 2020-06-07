using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages.TvPrograms
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
            return Page();
        }

        [BindProperty]
        public TvProgram TvProgram { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TvPrograms.Add(TvProgram);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
