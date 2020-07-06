using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages.Schedules
{
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public DeleteModel(Infrastructure.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DefaultSchedule = await _context.DefaultSchedules.FindAsync(id);

            if (DefaultSchedule != null)
            {
                _context.DefaultSchedules.Remove(DefaultSchedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
