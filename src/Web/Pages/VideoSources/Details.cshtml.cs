using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages_VideoSources
{
    public class DetailsModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public DetailsModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public VideoSource VideoSource { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VideoSource = await _context.VideoSources.FirstOrDefaultAsync(m => m.Id == id);

            if (VideoSource == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
