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
                .Include(br => br.Schedules).ThenInclude(sc => sc.Corner)
                .Include(br => br.Schedules).ThenInclude(sc => sc.Specification)
                .SingleOrDefaultAsync(x => x.TvProgramId == tvProgramId && x.AirDate == airDate);

            return broadcast;
        }

        public async Task CreateBroadcastWithDefaultSchedulesAsync(int tvProgramId, DateTime airDate)
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

        public async Task UpdateBroadcastAsync(Broadcast broadcast)
        {
            var entity = await _context.Broadcasts
                .Include(x => x.Schedules)
                .SingleOrDefaultAsync(x => x.Id == broadcast.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Broadcast), broadcast.Id);

            // TODO: _context.Entry(entity).State = EntityState.Modified;
            entity.AirDate = broadcast.AirDate;
            entity.Schedules = broadcast.Schedules;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBroadcastAsync(int tvProgramId, DateTime airDate)
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