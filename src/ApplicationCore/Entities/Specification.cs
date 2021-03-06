using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entites
{
    public class Specification : BaseEntity
    {
        [MaxLength(40)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Text { get; set; }

        [MaxLength(255)]
        public string Director { get; set; }

        [MaxLength(255)]
        public string Desk { get; set; }

        [MaxLength(1000)]
        public string Tag { get; set; }

        [MaxLength(1000)]
        public string Keyword { get; set; }

        [MaxLength(1000)]
        public string VideoSourceNote { get; set; }

        [MaxLength(1000)]
        public string ArticleSourceNote { get; set; }

        public bool DeskCheck { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public List<SpecificationCategory> SpecificationCategories { get; set; }

        public List<SpecificationVideoSource> SpecificationVideoSources { get; set; }

        public List<SpecificationArticleSource> SpecificationArticleSources { get; set; }

        public List<SpecificationMaterialSource> SpecificationMaterialSources { get; set; }

        public List<SpecificationInterview> SpecificationInterviews { get; set; }

        public List<SpecificationCast> SpecificationCasts { get; set; }
    }
}