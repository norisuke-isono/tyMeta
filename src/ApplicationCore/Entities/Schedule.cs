using System.ComponentModel.DataAnnotations;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entites
{
    public class Schedule : BaseEntity, ISchedule
    {
        [Required]
        public int Sequence { get; set; }

        public int? CornerId { get; set; }
        public Corner Corner { get; set; }

        [Required]
        public int BroadcastId { get; set; }
        public Broadcast Broadcast { get; set; }

        public Specification Specification { get; set; }
    }
}