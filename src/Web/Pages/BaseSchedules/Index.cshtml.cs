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
    public class IndexModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public IndexModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BaseSchedule> BaseSchedule { get;set; }

        public async Task OnGetAsync()
        {
            BaseSchedule = await _context.BaseSchedules
                .Include(b => b.Corner)
                .Include(b => b.TvProgram).ToListAsync();
        }
    }
}
