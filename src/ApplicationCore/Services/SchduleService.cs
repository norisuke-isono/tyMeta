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
                .OrderBy(x => x.Sequence)
                .ToListAsync();

            return schedules;
        }

        public async Task SortSchedulesAsync(int[] scheduleIds)
        {
            var schedules = await _context.Schedules
                .Where(x => scheduleIds.Contains(x.Id))
                .ToListAsync();

            if (scheduleIds.Count() != schedules.Count())
                throw new ArgumentException();

            schedules.ForEach(x => { x.Sequence = Array.IndexOf(scheduleIds, x.Id) + 1; });
            await _context.SaveChangesAsync();
        }
    }
}