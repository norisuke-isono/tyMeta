using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Common.Exceptions;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class TvProgramService : ITvProgramService
    {
        private readonly IApplicationDbContext _context;
        public TvProgramService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TvProgram> FindTvProgramAsync(int tyProgramId)
        {
            return await _context.TvPrograms
                .Include(x => x.DefaultSchedules)
                .SingleOrDefaultAsync(x => x.Id == tyProgramId);
        }

        public async Task<List<TvProgram>> GetTvProgramsAsync()
        {
            var programs = await _context.TvPrograms
                .OrderBy(x => x.Id)
                .ToListAsync();

            return programs;
        }

        public async Task UpdateTvProgramAsync(TvProgram tvProgram)
        {
            var entity = await _context.TvPrograms
                .Include(x => x.DefaultSchedules)
                .SingleOrDefaultAsync(x => x.Id == tvProgram.Id);

            if (entity == null)
                throw new NotFoundException(nameof(TvProgram), tvProgram.Id);

            // TODO: _context.Entry(entity).State = EntityState.Modified;
            entity.Name = tvProgram.Name;
            entity.DefaultSchedules = tvProgram.DefaultSchedules;

            await _context.SaveChangesAsync();
        }
    }
}