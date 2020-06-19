using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class SpecificationFilterViewModel
    {
        [Display(Name = "キーワード")]
        public string Keyword { get; set; }

        [Display(Name = "番組")]
        public int? TvProgramId { get; set; }

        [Display(Name = "コーナー")]
        public int? CornerId { get; set; }

        [Display(Name = "映像ソース")]
        public int? VideoSourceId { get; set; }

        [Display(Name = "原稿ソース")]
        public int? ArticleSourceId { get; set; }

        [Display(Name = "担当ディレクター")]
        public string Director { get; set; }

        [Display(Name = "担当デスク")]
        public string Desk { get; set; }

        [Display(Name = "放送日")]
        public DateTime AirDateFrom { get; set; } = DateTime.Now;

        [Display(Name = "放送日")]
        public DateTime AirDateTo { get; set; } = DateTime.Now;

        public int pageIndex { get; set; }

        public int pageSize { get; set; }

    }
}