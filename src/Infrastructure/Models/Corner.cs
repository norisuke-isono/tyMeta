using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Corner : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public int TvProgramId { get; set; }
        public TvProgram TvProgram { get; set; }
    }
}