using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entites
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