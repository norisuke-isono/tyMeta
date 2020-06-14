using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entites
{
    public class TvProgram : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Corner> Corners { get; set; }

        public List<Broadcast> Broadcasts { get; set; }

        public List<BaseSchedule> BaseSchedules { get; set; }
    }
}