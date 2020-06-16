using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages_ArticleSources
{
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public DeleteModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ArticleSource ArticleSource { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticleSource = await _context.ArticleSources.FirstOrDefaultAsync(m => m.Id == id);

            if (ArticleSource == null)
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

            ArticleSource = await _context.ArticleSources.FindAsync(id);

            if (ArticleSource != null)
            {
                _context.ArticleSources.Remove(ArticleSource);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
