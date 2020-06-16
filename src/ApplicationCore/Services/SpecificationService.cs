using System;
using System.Linq;
using System.Threading.Tasks;
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
                .SingleOrDefaultAsync(x => x.Id == specificationId);

            return specification;
        }

        public async Task UpdateSpecificationAsync(Specification Specification)
        {
            var entity = await _context.Specifications
                .SingleOrDefaultAsync(x => x.Id == Specification.Id);

            if (entity == null)
                return;

            entity.Title = Specification.Title;
            entity.Text = Specification.Text;

            await _context.SaveChangesAsync();
        }
    }
}