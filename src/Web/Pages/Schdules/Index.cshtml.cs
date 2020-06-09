using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entites;
using Infrastructure.Data;

namespace Web.Pages_Schdules
{
    public class IndexModel : PageModel
    {
        private readonly Infrastructure.Data.ApplicationDbContext _context;

        public IndexModel(Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Schedule> Schedule { get;set; }

        public async Task OnGetAsync()
        {
            Schedule = await _context.Schedules
                .Include(s => s.Broadcast)
                .Include(s => s.Corner).ToListAsync();
        }
    }
}
