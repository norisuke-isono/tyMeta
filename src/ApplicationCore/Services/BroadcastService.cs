using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class BroadcastService : IBroadcastService
    {
        private readonly IApplicationDbContext _context;
        public BroadcastService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Broadcast> FindBroadcastAsync(int tvProgramId, DateTime airDate)
        {
            var broadcast = await _context.Broadcasts
                .SingleOrDefaultAsync(x => x.TvProgramId == tvProgramId && x.AirDate == airDate);

            return broadcast;
        }

        public async Task CreateBroadcastWithDefaultSchedules(int tvProgramId, DateTime airDate)
        {
            var baseSchedules = await _context.BaseSchedules
                .Where(x => x.TvProgramId == tvProgramId)
                .AsNoTracking().ToListAsync();

            var broadcast = new Broadcast
            {
                AirDate = airDate,
                TvProgramId = tvProgramId,
                Schedules = baseSchedules.Select(x =>
                    new Schedule
                    {
                        Sequence = x.Sequence,
                        CornerId = x.CornerId,
                        Specification = new Specification(),
                    }).ToList(),
            };

            await _context.Broadcasts.AddAsync(broadcast);
            await _context.SaveChangesAsync();
        }
    }
}