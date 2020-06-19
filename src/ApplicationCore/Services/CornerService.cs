using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class CornerService : ICornerService
    {
        private readonly IApplicationDbContext _context;

        public CornerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Corner>> GetCornersAsync()
        {
            var corners = await _context.Corners
                .OrderBy(x => x.Id)
                .ToListAsync();

            return corners;
        }
    }
}