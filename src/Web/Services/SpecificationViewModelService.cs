using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
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
            IArticleSourceService articleSourceService
            )
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
                Title = specification.Title,
                Text = specification.Text,
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

        public async Task UpdateSpecificationFrom(SpecificationViewModel viewModel)
        {
            var specification = new Specification
            {
                Id = viewModel.SpecificationId,
                Title = viewModel.Title,
                Text = viewModel.Text,
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
