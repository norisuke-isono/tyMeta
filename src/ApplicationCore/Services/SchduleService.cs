using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IApplicationDbContext _context;
        public ScheduleService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetSchedulesAsync(int tvProgramId, DateTime airDate)
        {
            var schedules = await _context.Schedules
                .Include(x => x.Corner)
                .Include(x => x.Broadcast)
                .Include(x => x.Specification)
                .Where(x => x.Corner.TvProgramId == tvProgramId && x.Broadcast.AirDate == airDate)
                .ToListAsync();

            return schedules;
        }
    }
}