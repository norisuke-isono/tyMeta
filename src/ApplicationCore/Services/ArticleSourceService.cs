using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class ArticleSourceService : IArticleSourceService
    {
        private readonly IApplicationDbContext _context;

        public ArticleSourceService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArticleSource>> GetArticleSourcesAsync()
        {
            var articleSources = await _context.ArticleSources
                .OrderBy(x => x.Id)
                .ToListAsync();

            return articleSources;
        }
    }
}