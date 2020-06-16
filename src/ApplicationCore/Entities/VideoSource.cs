using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entites
{
    public class VideoSource : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<SpecificationVideoSource> SpecificationVideoSources { get; set; }
    }
}