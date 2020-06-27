using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class SpecificationViewModelService : ISpecificationViewModelService
    {
        private readonly ISpecificationService _specificationService;
        private readonly ICategoryService _categoryService;
        private readonly IVideoSourceService _videoSourceService;
        private readonly IArticleSourceService _articleSourceService;

        public SpecificationViewModelService(
            ISpecificationService specificationService,
            ICategoryService categoryService,
            IVideoSourceService videoSourceService,
            IArticleSourceService articleSourceService)
        {
            _specificationService = specificationService;
            _categoryService = categoryService;
            _videoSourceService = videoSourceService;
            _articleSourceService = articleSourceService;
        }

        public async Task<SpecificationViewModel> GetSpecificationViewModel(int specificationId)
        {
            var specification = await _specificationService.FindSpecificationAsync(specificationId);
            if (specification == null) return null;

            var categories = await _categoryService.GetCategoriesAsync();
            var videoSources = await _videoSourceService.GetVideoSourcesAsync();
            var articleSources = await _articleSourceService.GetArticleSourcesAsync();

            var viewModel = new SpecificationViewModel
            {
                SpecificationId = specification.Id,
                TvProgramName = specification.Schedule.Broadcast.TvProgram.Name,
                CornerName = specification.Schedule.Corner.Name,
                AirDate = specification.Schedule.Broadcast.AirDate,
                Title = specification.Title,
                Text = specification.Text,
                Director = specification.Director,
                Desk = specification.Desk,
                CategorySelectItems = categories.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = specification.SpecificationCategories
                            .Any(sv => sv.CategoryId == x.Id)
                    }).ToList(),
                VideoSourceSelectItems = videoSources.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = specification.SpecificationVideoSources
                            .Any(sv => sv.VideoSourceId == x.Id)
                    }).ToList(),
                ArticleSourceSelectItems = articleSources.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = specification.SpecificationArticleSources
                            .Any(sa => sa.ArticleSourceId == x.Id)
                    }).ToList(),
                MaterialSourceViewModels = specification.SpecificationMaterialSources
                    .Select(x => new SpecificationMaterialSourceViewModel
                    {
                        Type = x.Type,
                        CopyrightHolder = x.CopyrightHolder,
                        ConditionsOfUse = x.ConditionsOfUse,
                        Note = x.Note,
                        Dead = false
                    }).ToList(),
                InterviewViewModels = specification.SpecificationInterviews
                    .Select(x => new SpecificationInterviewViewModel
                    {
                        Name = x.Name,
                        Affiliation = x.Affiliation,
                        JobTitle = x.JobTitle,
                        Product = x.Product,
                        ContactAddress = x.ContactAddress,
                        Note = x.Note,
                        Address = x.Address,
                        UseSearch = x.UseSearch,
                        Dead = false
                    }).ToList(),
                CastViewModels = specification.SpecificationCasts
                    .Select(x => new SpecificationCastViewModel
                    {
                        Name = x.Name,
                        Affiliation = x.Affiliation,
                        JobTitle = x.JobTitle,
                        ContactAddress = x.ContactAddress,
                        Note = x.Note,
                        Dead = false
                    }).ToList()
            };

            return viewModel;
        }

        public async Task<List<SpecificationIndexViewModel>> SearchSpecifications(SpecificationFilterViewModel filterViewModel)
        {
            var specifications = await _specificationService
                .SearchSpecificationsAsync(new SpecificationFilter
                {
                    Keyword = filterViewModel.Keyword,
                    TvProgramId = filterViewModel.TvProgramId,
                    CornerId = filterViewModel.CornerId,
                    VideoSourceId = filterViewModel.VideoSourceId,
                    ArticleSourceId = filterViewModel.ArticleSourceId,
                    Director = filterViewModel.Director,
                    Desk = filterViewModel.Desk,
                    AirDateFrom = filterViewModel.AirDateFrom,
                    AirDateTo = filterViewModel.AirDateTo,
                    pageIndex = 1, // TODO:
                    pageSize = 20, // TODO:
                });

            var indexViewModels = specifications.Select(x => new SpecificationIndexViewModel
            {
                SpecificationId = x.Id,
                AirDate = x.Schedule.Broadcast.AirDate,
                TvProgramName = x.Schedule.Broadcast.TvProgram.Name,
                CornerName = x.Schedule.Corner.Name,
                Title = x.Title,
            }).ToList();

            return indexViewModels;
        }

        public async Task UpdateSpecificationFrom(SpecificationViewModel viewModel)
        {
            var specification = new Specification
            {
                Id = viewModel.SpecificationId,
                Title = viewModel.Title,
                Text = viewModel.Text,
                Director = viewModel.Director,
                Desk = viewModel.Desk,
                SpecificationCategories = viewModel.CategorySelectItems
                    .Where(x => x.Selected)
                    .Select(x => new SpecificationCategory
                    {
                        CategoryId = int.Parse(x.Value),
                    }).ToList(),
                SpecificationVideoSources = viewModel.VideoSourceSelectItems
                    .Where(x => x.Selected)
                    .Select(x => new SpecificationVideoSource
                    {
                        VideoSourceId = int.Parse(x.Value),
                    }).ToList(),
                SpecificationArticleSources = viewModel.ArticleSourceSelectItems
                    .Where(x => x.Selected)
                    .Select(x => new SpecificationArticleSource
                    {
                        ArticleSourceId = int.Parse(x.Value),
                    }).ToList(),
                SpecificationMaterialSources = viewModel.MaterialSourceViewModels
                    .Select(x => new SpecificationMaterialSource
                    {
                        Type = (MaterialType)x.Type,
                        CopyrightHolder = x.CopyrightHolder,
                        ConditionsOfUse = x.ConditionsOfUse,
                        Note = x.Note
                    }).ToList(),
                SpecificationInterviews = viewModel.InterviewViewModels
                    .Select(x => new SpecificationInterview
                    {
                        Name = x.Name,
                        Affiliation = x.Affiliation,
                        JobTitle = x.JobTitle,
                        Product = x.Product,
                        ContactAddress = x.ContactAddress,
                        Note = x.Note,
                        Address = x.Address,
                        UseSearch = x.UseSearch,
                    }).ToList(),
                SpecificationCasts = viewModel.CastViewModels
                    .Select(x => new SpecificationCast
                    {
                        Name = x.Name,
                        Affiliation = x.Affiliation,
                        JobTitle = x.JobTitle,
                        ContactAddress = x.ContactAddress,
                        Note = x.Note,
                    }).ToList()
            };

            await _specificationService.UpdateSpecificationAsync(specification);
        }
    }
}
