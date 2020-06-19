using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
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
        private readonly IVideoSourceService _videoSourceService;
        private readonly IArticleSourceService _articleSourceService;

        public SpecificationViewModelService(
            ISpecificationService specificationService,
            IVideoSourceService videoSourceService,
            IArticleSourceService articleSourceService)
        {
            _specificationService = specificationService;
            _videoSourceService = videoSourceService;
            _articleSourceService = articleSourceService;
        }

        public async Task<SpecificationViewModel> GetSpecificationViewModel(int specificationId)
        {
            var specification = await _specificationService.FindSpecificationAsync(specificationId);
            if (specification == null) return null;

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
            };

            await _specificationService.UpdateSpecificationAsync(specification);
        }
    }
}
