using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages_Specification
{
    public class SearchModel : PageModel
    {
        private readonly ITvProgramService _tvProgramService;
        private readonly ICornerService _cornerService;
        private readonly IVideoSourceService _videoSourceService;
        private readonly IArticleSourceService _articleSourceService;
        private readonly ISpecificationViewModelService _specificationViewModelService;

        public SearchModel(ITvProgramService tvProgramService,
                          ICornerService cornerService,
                          IVideoSourceService videoSourceService,
                          IArticleSourceService articleSourceService,
                          ISpecificationViewModelService specificationViewModelService)
        {
            _tvProgramService = tvProgramService;
            _cornerService = cornerService;
            _videoSourceService = videoSourceService;
            _articleSourceService = articleSourceService;
            _specificationViewModelService = specificationViewModelService;
        }

        [BindProperty(SupportsGet = true)]
        public SpecificationFilterViewModel Filter { get; set; }

        public SelectList TvPrograms { get; set; }
        public SelectList Corners { get; set; }
        public SelectList VideoSources { get; set; }
        public SelectList ArticleSources { get; set; }

        public SpecificationIndexViewModel SpecificationIndexViewModel { get; set; }

        public async Task OnGetAsync()
        {
            TvPrograms = new SelectList(await _tvProgramService.GetTvProgramsAsync(),
                                        nameof(TvProgram.Id), nameof(TvProgram.Name));

            VideoSources = new SelectList(await _videoSourceService.GetVideoSourcesAsync(),
                                        nameof(VideoSource.Id), nameof(VideoSource.Name));

            ArticleSources = new SelectList(await _articleSourceService.GetArticleSourcesAsync(),
                                        nameof(ArticleSource.Id), nameof(ArticleSource.Name));

            Corners = new SelectList(await _cornerService.GetCornersAsync(),
                                        nameof(Corner.Id), nameof(Corner.Name));

            SpecificationIndexViewModel = await _specificationViewModelService.SearchSpecifications(Filter);
        }
    }
}
