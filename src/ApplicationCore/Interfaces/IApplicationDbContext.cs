using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TvProgram> TvPrograms { get; set; }
        DbSet<Corner> Corners { get; set; }
        DbSet<Broadcast> Broadcasts { get; set; }
        DbSet<DefaultSchedule> DefaultSchedules { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<VideoSource> VideoSources { get; set; }
        DbSet<ArticleSource> ArticleSources { get; set; }
        DbSet<SpecificationCategory> SpecificationCategories { get; set; }
        DbSet<SpecificationVideoSource> SpecificationVideoSources { get; set; }
        DbSet<SpecificationArticleSource> SpecificationArticleSources { get; set; }
        DbSet<SpecificationMaterialSource> SpecificationMaterialSources { get; set; }
        DbSet<SpecificationInterview> SpecificationInterviews { get; set; }
        DbSet<SpecificationCast> SpecificationCasts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}