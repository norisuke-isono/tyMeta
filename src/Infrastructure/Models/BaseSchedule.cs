using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Interfaces;

namespace Infrastructure.Models
{
    public class BaseSchedule : BaseEntity, ISchedule
    {
        [Required]
        public int Sequence { get; set; }

        [Required]
        public int CornerId { get; set; }
        public Corner Corner { get; set; }
    }
}