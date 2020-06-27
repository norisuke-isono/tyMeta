using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entites
{
    public class SpecificationCast : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Affiliation { get; set; }

        [MaxLength(255)]
        public string JobTitle { get; set; }

        [MaxLength(255)]
        public string ContactAddress { get; set; }

        [MaxLength(255)]
        public string Note { get; set; }

        [Required]
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
    }
}