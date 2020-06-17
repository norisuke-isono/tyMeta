using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class VideoSourceService : IVideoSourceService
    {
        private readonly IApplicationDbContext _context;

        public VideoSourceService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideoSource>> GetVideoSourcesAsync()
        {
            var videoSources = await _context.VideoSources
                .OrderBy(x => x.Id)
                .ToListAsync();

            return videoSources;
        }
    }
}