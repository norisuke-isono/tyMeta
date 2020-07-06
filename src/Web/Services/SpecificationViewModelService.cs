using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class SpecificationViewModelService : ISpecificationViewModelService
    {
        private readonly IMapper _mapper;
        private readonly ISpecificationService _specificationService;
        private readonly ICategoryService _categoryService;
        private readonly IVideoSourceService _videoSourceService;
        private readonly IArticleSourceService _articleSourceService;

        public SpecificationViewModelService(
            IMapper mapper,
            ISpecificationService specificationService,
            ICategoryService categoryService,
            IVideoSourceService videoSourceService,
            IArticleSourceService articleSourceService)
        {
            _mapper = mapper;
            _specificationService = specificationService;
            _categoryService = categoryService;
            _videoSourceService = videoSourceService;
            _articleSourceService = articleSourceService;
        }

        public async Task<SpecificationViewModel> GetSpecificationViewModel(int specificationId)
        {
            var specification = await _specificationService.FindSpecificationAsync(specificationId);
            if (specification == null) return null;

            var viewModel = _mapper.Map<Specification, SpecificationViewModel>(specification);

            var categories = await _categoryService.GetCategoriesAsync();
            viewModel.CategorySelectItems = categories.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = specification.SpecificationCategories
                        .Any(sv => sv.CategoryId == x.Id)
                }).ToList();

            var videoSources = await _videoSourceService.GetVideoSourcesAsync();
            viewModel.VideoSourceSelectItems = videoSources.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = specification.SpecificationVideoSources
                        .Any(sv => sv.VideoSourceId == x.Id)
                }).ToList();

            var articleSources = await _articleSourceService.GetArticleSourcesAsync();
            viewModel.ArticleSourceSelectItems = articleSources.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = specification.SpecificationArticleSources
                    .Any(sa => sa.ArticleSourceId == x.Id)
                }).ToList();

            return viewModel;
        }

        public async Task<SpecificationIndexViewModel> SearchSpecifications(SpecificationFilterViewModel filterViewModel)
        {
            var filter = _mapper.Map<SpecificationFilterViewModel, SpecificationFilter>(filterViewModel);

            var specifications = await _specificationService.SearchSpecificationsAsync(filter);
            var totalItems = await _specificationService.CountSpecificationsAsync(filter);
            var totalPages = int.Parse(Math.Ceiling(((decimal)totalItems / filter.pageSize)).ToString());

            return new SpecificationIndexViewModel()
            {
                Items = _mapper.Map<IEnumerable<Specification>, List<SpecificationIndexItemViewModel>>(specifications),
                PaginationViewModel = new PaginationViewModel()
                {
                    TotalItems = totalItems,
                    PageSize = filter.pageSize,
                    PageIndex = filter.pageIndex,
                    TotalPages = totalPages,
                }
            };
        }

        public async Task UpdateSpecificationFrom(SpecificationViewModel viewModel)
        {
            var specification = _mapper.Map<SpecificationViewModel, Specification>(viewModel);

            await _specificationService.UpdateSpecificationAsync(specification);
        }
    }
}
