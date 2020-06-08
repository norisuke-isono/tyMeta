using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entites
{
    public class Broadcast : BaseEntity
    {
        [Required]
        public DateTime AirDate { get; set; }

        [Required]
        public int TvProgramId { get; set; }
        public TvProgram TvProgram { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}