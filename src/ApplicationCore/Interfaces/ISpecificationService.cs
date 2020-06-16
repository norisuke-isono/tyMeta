using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;

namespace ApplicationCore.Interfaces
{
    public interface ISpecificationService
    {
        Task<Specification> FindSpecificationAsync(int specificationId);

        Task UpdateSpecificationAsync(Specification specification);
    }
}