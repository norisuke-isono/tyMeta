using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entites
{
    public class SpecificationInterview : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Affiliation { get; set; }

        [MaxLength(255)]
        public string JobTitle { get; set; }

        [MaxLength(255)]
        public string Product { get; set; }

        [MaxLength(255)]
        public string ContactAddress { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Note { get; set; }

        public bool UseSearch { get; set; }

        [Required]
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
    }
}