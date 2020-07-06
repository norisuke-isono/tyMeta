using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface ISpecificationService
    {
        Task<Specification> FindSpecificationAsync(int specificationId);

        Task UpdateSpecificationAsync(Specification specification);

        Task<List<Specification>> SearchSpecificationsAsync(SpecificationFilter filter);

        Task<int> CountSpecificationsAsync(SpecificationFilter filter);
    }
}