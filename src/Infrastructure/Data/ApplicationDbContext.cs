using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TvProgram> TvPrograms { get; set; }
        public DbSet<Corner> Corners { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<BaseSchedule> BaseSchedules { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<VideoSource> VideoSources { get; set; }
        public DbSet<ArticleSource> ArticleSources { get; set; }
        public DbSet<SpecificationVideoSource> SpecificationVideoSources { get; set; }
        public DbSet<SpecificationArticleSource> SpecificationArticleSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseSchedule>()
                .HasIndex(x => new { x.TvProgramId, x.Sequence }).IsUnique();

            // NOTE: EFcore3.1では多対多で中間テーブル用のEntityを手動で定義する必要がある。
            // https://docs.microsoft.com/ja-jp/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#many-to-many
            modelBuilder.Entity<SpecificationVideoSource>()
                .HasKey(x => new { x.SpecificationId, x.VideoSourceId });
            modelBuilder.Entity<SpecificationVideoSource>()
                .HasOne(x => x.Specification)
                .WithMany(x => x.SpecificationVideoSources)
                .HasForeignKey(x => x.SpecificationId);
            modelBuilder.Entity<SpecificationVideoSource>()
                .HasOne(x => x.VideoSource)
                .WithMany(x => x.SpecificationVideoSources)
                .HasForeignKey(x => x.VideoSourceId);

            modelBuilder.Entity<SpecificationArticleSource>()
                .HasKey(x => new { x.SpecificationId, x.ArticleSourceId });
            modelBuilder.Entity<SpecificationArticleSource>()
                .HasOne(x => x.Specification)
                .WithMany(x => x.SpecificationArticleSources)
                .HasForeignKey(x => x.SpecificationId);
            modelBuilder.Entity<SpecificationArticleSource>()
                .HasOne(x => x.ArticleSource)
                .WithMany(x => x.SpecificationArticleSources)
                .HasForeignKey(x => x.ArticleSourceId);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}