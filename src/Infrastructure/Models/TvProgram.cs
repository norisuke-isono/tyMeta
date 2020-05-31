using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class TvProgram : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Corner> Corners { get; set; }
    }
}