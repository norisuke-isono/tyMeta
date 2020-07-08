using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}