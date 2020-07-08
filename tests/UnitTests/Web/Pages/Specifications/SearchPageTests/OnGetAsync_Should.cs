using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Web.Interfaces;
using Web.Pages_Specifications;
using Web.ViewModels;
using Xunit;

namespace UnitTests.Web.Pages.Specifications.SearchPageTests
{
    public class OnGetAsync_Should
    {
        private readonly Mock<ITvProgramService> _tvProgramService;
        private readonly Mock<ICornerService> _cornerService;
        private readonly Mock<IVideoSourceService> _videoSourceService;
        private readonly Mock<IArticleSourceService> _articleSourceService;
        private readonly Mock<ISpecificationViewModelService> _specificationViewModelService;

        public OnGetAsync_Should()
        {
            _tvProgramService = new Mock<ITvProgramService>();
            _cornerService = new Mock<ICornerService>();
            _videoSourceService = new Mock<IVideoSourceService>();
            _articleSourceService = new Mock<IArticleSourceService>();
            _specificationViewModelService = new Mock<ISpecificationViewModelService>();
        }

        [Fact]
        public async Task PopulatesThePageModel_WithASelectListOfTvPrograms()
        {
            var dummyTvPrograms = new List<TvProgram>
            {
                new TvProgram{Id = 1, Name="Dummy1"},
                new TvProgram{Id = 2, Name="Dummy2"},
            };
            var expected = new SelectList(dummyTvPrograms, nameof(TvProgram.Id), nameof(TvProgram.Name));
            _tvProgramService.Setup(x => x.GetTvProgramsAsync()).ReturnsAsync(dummyTvPrograms);
            _cornerService.Setup(x => x.GetCornersAsync()).ReturnsAsync(Enumerable.Empty<Corner>().ToList());
            _videoSourceService.Setup(x => x.GetVideoSourcesAsync()).ReturnsAsync(Enumerable.Empty<VideoSource>().ToList());
            _articleSourceService.Setup(x => x.GetArticleSourcesAsync()).ReturnsAsync(Enumerable.Empty<ArticleSource>().ToList());
            var pageModel = new SearchModel(_tvProgramService.Object,
                                            _cornerService.Object,
                                            _videoSourceService.Object,
                                            _articleSourceService.Object,
                                            _specificationViewModelService.Object);

            await pageModel.OnGetAsync();

            var actual = Assert.IsAssignableFrom<SelectList>(pageModel.TvProgramSelectList);
            Assert.Equal(expected.Select(x => x.Value), actual.Select(x => x.Value));
            Assert.Equal(expected.Select(x => x.Text), actual.Select(x => x.Text));
        }

        [Fact]
        public async Task PopulatesThePageModel_WithASelectListOfCorners()
        {
            var dummyCorners = new List<Corner>
            {
                new Corner{Id = 1, Name="Dummy1"},
                new Corner{Id = 2, Name="Dummy2"},
            };
            var expected = new SelectList(dummyCorners, nameof(Corner.Id), nameof(Corner.Name));
            _tvProgramService.Setup(x => x.GetTvProgramsAsync()).ReturnsAsync(Enumerable.Empty<TvProgram>().ToList());
            _cornerService.Setup(x => x.GetCornersAsync()).ReturnsAsync(dummyCorners);
            _videoSourceService.Setup(x => x.GetVideoSourcesAsync()).ReturnsAsync(Enumerable.Empty<VideoSource>().ToList());
            _articleSourceService.Setup(x => x.GetArticleSourcesAsync()).ReturnsAsync(Enumerable.Empty<ArticleSource>().ToList());
            var pageModel = new SearchModel(_tvProgramService.Object,
                                            _cornerService.Object,
                                            _videoSourceService.Object,
                                            _articleSourceService.Object,
                                            _specificationViewModelService.Object);

            await pageModel.OnGetAsync();

            var actual = Assert.IsAssignableFrom<SelectList>(pageModel.CornerSelectList);
            Assert.Equal(expected.Select(x => x.Value), actual.Select(x => x.Value));
            Assert.Equal(expected.Select(x => x.Text), actual.Select(x => x.Text));
        }

        [Fact]
        public async Task PopulatesThePageModel_WithASelectListOfVideoSources()
        {
            var dummyVideoSources = new List<VideoSource>
            {
                new VideoSource{Id = 1, Name="Dummy1"},
                new VideoSource{Id = 2, Name="Dummy2"},
            };
            var expected = new SelectList(dummyVideoSources, nameof(VideoSource.Id), nameof(VideoSource.Name));
            _tvProgramService.Setup(x => x.GetTvProgramsAsync()).ReturnsAsync(Enumerable.Empty<TvProgram>().ToList());
            _cornerService.Setup(x => x.GetCornersAsync()).ReturnsAsync(Enumerable.Empty<Corner>().ToList());
            _videoSourceService.Setup(x => x.GetVideoSourcesAsync()).ReturnsAsync(dummyVideoSources);
            _articleSourceService.Setup(x => x.GetArticleSourcesAsync()).ReturnsAsync(Enumerable.Empty<ArticleSource>().ToList());
            var pageModel = new SearchModel(_tvProgramService.Object,
                                            _cornerService.Object,
                                            _videoSourceService.Object,
                                            _articleSourceService.Object,
                                            _specificationViewModelService.Object);

            await pageModel.OnGetAsync();

            var actual = Assert.IsAssignableFrom<SelectList>(pageModel.VideoSourceSelectList);
            Assert.Equal(expected.Select(x => x.Value), actual.Select(x => x.Value));
            Assert.Equal(expected.Select(x => x.Text), actual.Select(x => x.Text));
        }

        [Fact]
        public async Task PopulatesThePageModel_WithASelectListOfArticleSources()
        {
            var dummyArticleSources = new List<ArticleSource>
            {
                new ArticleSource{Id = 1, Name="Dummy1"},
                new ArticleSource{Id = 2, Name="Dummy2"},
            };
            var expected = new SelectList(dummyArticleSources, nameof(ArticleSource.Id), nameof(ArticleSource.Name));
            _tvProgramService.Setup(x => x.GetTvProgramsAsync()).ReturnsAsync(Enumerable.Empty<TvProgram>().ToList());
            _cornerService.Setup(x => x.GetCornersAsync()).ReturnsAsync(Enumerable.Empty<Corner>().ToList());
            _videoSourceService.Setup(x => x.GetVideoSourcesAsync()).ReturnsAsync(Enumerable.Empty<VideoSource>().ToList());
            _articleSourceService.Setup(x => x.GetArticleSourcesAsync()).ReturnsAsync(dummyArticleSources);
            var pageModel = new SearchModel(_tvProgramService.Object,
                                            _cornerService.Object,
                                            _videoSourceService.Object,
                                            _articleSourceService.Object,
                                            _specificationViewModelService.Object);

            await pageModel.OnGetAsync();

            var actual = Assert.IsAssignableFrom<SelectList>(pageModel.ArticleSourceSelectList);
            Assert.Equal(expected.Select(x => x.Value), actual.Select(x => x.Value));
            Assert.Equal(expected.Select(x => x.Text), actual.Select(x => x.Text));
        }

        [Fact]
        public async Task InvokeSpecificationViewModelServiceSearchSpecifications_Once()
        {
            _tvProgramService.Setup(x => x.GetTvProgramsAsync()).ReturnsAsync(Enumerable.Empty<TvProgram>().ToList());
            _cornerService.Setup(x => x.GetCornersAsync()).ReturnsAsync(Enumerable.Empty<Corner>().ToList());
            _videoSourceService.Setup(x => x.GetVideoSourcesAsync()).ReturnsAsync(Enumerable.Empty<VideoSource>().ToList());
            _articleSourceService.Setup(x => x.GetArticleSourcesAsync()).ReturnsAsync(Enumerable.Empty<ArticleSource>().ToList());
            _specificationViewModelService.Setup(x => x.SearchSpecifications(It.IsAny<SpecificationFilterViewModel>()))
                .ReturnsAsync(It.IsAny<SpecificationIndexViewModel>());
            var pageModel = new SearchModel(_tvProgramService.Object,
                                            _cornerService.Object,
                                            _videoSourceService.Object,
                                            _articleSourceService.Object,
                                            _specificationViewModelService.Object);

            await pageModel.OnGetAsync();

            _specificationViewModelService.Verify(x => x.SearchSpecifications(It.IsAny<SpecificationFilterViewModel>()), Times.Once);
        }
    }
}