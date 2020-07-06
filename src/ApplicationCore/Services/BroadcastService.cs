using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Common.Exceptions;
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
            var defaultSchedules = await _context.DefaultSchedules
                .Where(x => x.TvProgramId == tvProgramId)
                .AsNoTracking().ToListAsync();

            var broadcast = new Broadcast
            {
                AirDate = airDate,
                TvProgramId = tvProgramId,
                Schedules = defaultSchedules.Select(x =>
                    new Schedule
                    {
                        Sequence = x.Sequence,
                        CornerId = x.CornerId,
                        Specification = new Specification(),
                    }).ToList(),
            };

            _context.Broadcasts.Add(broadcast);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBroadcast(int tvProgramId, DateTime airDate)
        {
            var broadcast = await _context.Broadcasts
                .SingleOrDefaultAsync(x => x.TvProgramId == tvProgramId && x.AirDate == airDate);

            if (broadcast == null)
                throw new NotFoundException(nameof(Broadcast), tvProgramId, airDate);

            _context.Broadcasts.Remove(broadcast);
            await _context.SaveChangesAsync();
        }
    }
}