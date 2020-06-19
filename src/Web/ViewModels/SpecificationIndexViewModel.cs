using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels
{
    public class SpecificationIndexViewModel
    {
        public int SpecificationId { get; set; }

        [Display(Name = "放送日")]
        public DateTime AirDate { get; set; }

        [Display(Name = "番組")]
        public string TvProgramName { get; set; }

        [Display(Name = "コーナー名")]
        public string CornerName { get; set; }

        [Display(Name = "見出し")]
        public string Title { get; set; }
    }
}