using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Common.Exceptions;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class SpecificationService : ISpecificationService
    {
        private readonly IApplicationDbContext _context;
        public SpecificationService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Specification> FindSpecificationAsync(int specificationId)
        {
            var specification = await _context.Specifications
                .Include(spec => spec.Schedule)
                    .ThenInclude(schedule => schedule.Broadcast)
                        .ThenInclude(Broadcast => Broadcast.TvProgram)
                .Include(spec => spec.Schedule)
                    .ThenInclude(Schedule => Schedule.Corner)
                .Include(spec => spec.SpecificationCategories)
                .Include(spec => spec.SpecificationVideoSources)
                .Include(spec => spec.SpecificationArticleSources)
                .Include(spec => spec.SpecificationMaterialSources)
                .Include(spec => spec.SpecificationInterviews)
                .Include(spec => spec.SpecificationCasts)
                .SingleOrDefaultAsync(spec => spec.Id == specificationId);

            return specification;
        }

        public async Task UpdateSpecificationAsync(Specification Specification)
        {
            var entity = await _context.Specifications
                .Include(x => x.SpecificationCategories)
                .Include(x => x.SpecificationVideoSources)
                .Include(x => x.SpecificationArticleSources)
                .Include(x => x.SpecificationMaterialSources)
                .Include(x => x.SpecificationInterviews)
                .Include(x => x.SpecificationCasts)
                .SingleOrDefaultAsync(x => x.Id == Specification.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Specification), Specification.Id);

            // TODO: _context.Entry(entity).State = EntityState.Modified;
            entity.Title = Specification.Title;
            entity.Text = Specification.Text;
            entity.Director = Specification.Director;
            entity.Desk = Specification.Desk;
            entity.Tag = Specification.Tag;
            entity.Keyword = Specification.Keyword;
            entity.VideoSourceNote = Specification.VideoSourceNote;
            entity.ArticleSourceNote = Specification.ArticleSourceNote;
            entity.DeskCheck = Specification.DeskCheck;
            entity.SpecificationCategories = Specification.SpecificationCategories;
            entity.SpecificationVideoSources = Specification.SpecificationVideoSources;
            entity.SpecificationArticleSources = Specification.SpecificationArticleSources;
            entity.SpecificationMaterialSources = Specification.SpecificationMaterialSources;
            entity.SpecificationInterviews = Specification.SpecificationInterviews;
            entity.SpecificationCasts = Specification.SpecificationCasts;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Specification>> SearchSpecificationsAsync(SpecificationFilter filter)
        {
            IQueryable<Specification> source = _context.Specifications
                .Include(spec => spec.Schedule)
                    .ThenInclude(schedule => schedule.Broadcast)
                        .ThenInclude(broadcast => broadcast.TvProgram)
                .Include(spec => spec.Schedule)
                    .ThenInclude(Schedule => Schedule.Corner);

            if (filter.TvProgramId != null)
            {
                source = source.Where(spec => spec.Schedule.Broadcast.TvProgramId == filter.TvProgramId);
            }

            if (filter.AirDateFrom != null || filter.AirDateTo != null)
            {
                source = source.Where(spec => filter.AirDateFrom <= spec.Schedule.Broadcast.AirDate
                                && spec.Schedule.Broadcast.AirDate <= filter.AirDateTo);
            }

            if (filter.CornerId != null)
            {
                source = source.Where(spec => spec.Schedule.CornerId == filter.CornerId);
            }

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                source = source.Where(spec => spec.Title.Contains(filter.Keyword)
                                           || spec.Text.Contains(filter.Keyword));
            }

            if (!string.IsNullOrEmpty(filter.Director))
            {
                source = source.Where(spec => spec.Director.Contains(filter.Director));
            }

            if (!string.IsNullOrEmpty(filter.Desk))
            {
                source = source.Where(spec => spec.Desk.Contains(filter.Desk));
            }

            if (filter.ArticleSourceId != null)
            {
                source = source.Include(spec => spec.SpecificationArticleSources)
                    .Where(spec => spec.SpecificationArticleSources
                        .Any(sa => sa.ArticleSourceId == filter.ArticleSourceId));
            }

            if (filter.VideoSourceId != null)
            {
                source = source.Include(spec => spec.SpecificationVideoSources)
                    .Where(spec => spec.SpecificationVideoSources
                        .Any(sa => sa.VideoSourceId == filter.VideoSourceId));
            }

            var specs = await source
                .OrderBy(spec => spec.Schedule.Broadcast.AirDate)
                .ThenBy(spec => spec.Schedule.Broadcast.TvProgramId)
                .ThenBy(spec => spec.Schedule.Sequence)
                .Skip((filter.pageIndex - 1) * filter.pageSize)
                .Take(filter.pageSize)
                .ToListAsync();

            return specs;
        }
    }
}