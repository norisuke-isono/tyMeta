using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entites
{
    public class ArticleSource : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<SpecificationArticleSource> SpecificationArticleSources { get; set; }
    }
}