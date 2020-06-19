using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ISpecificationViewModelService
    {
        Task<SpecificationViewModel> GetSpecificationViewModel(int specificationId);

        Task<List<SpecificationIndexViewModel>> SearchSpecifications(SpecificationFilterViewModel filterViewModel);

        Task UpdateSpecificationFrom(SpecificationViewModel viewModel);
    }
}