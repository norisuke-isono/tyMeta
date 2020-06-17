using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Common.Exceptions;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
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
                .Include(x => x.SpecificationVideoSources)
                .Include(x => x.SpecificationArticleSources)
                .SingleOrDefaultAsync(x => x.Id == specificationId);

            return specification;
        }

        public async Task UpdateSpecificationAsync(Specification Specification)
        {
            var entity = await _context.Specifications
                .Include(x => x.SpecificationVideoSources)
                .Include(x => x.SpecificationArticleSources)
                .SingleOrDefaultAsync(x => x.Id == Specification.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Specification), Specification.Id);

            entity.Title = Specification.Title;
            entity.Text = Specification.Text;
            entity.SpecificationVideoSources = Specification.SpecificationVideoSources;
            entity.SpecificationArticleSources = Specification.SpecificationArticleSources;

            await _context.SaveChangesAsync();
        }
    }
}