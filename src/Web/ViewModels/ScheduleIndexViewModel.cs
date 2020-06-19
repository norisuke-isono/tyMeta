using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ScheduleIndexViewModel
    {
        [Display(Name = "放送順")]
        public int Sequence { get; set; }

        [Display(Name = "コーナー名")]
        public string CornerName { get; set; }

        [Display(Name = "分類")]
        public string CornerType { get; set; }

        [Display(Name = "放送日")]
        public DateTime AirDate { get; set; }

        [Display(Name = "見出し")]
        public string Title { get; set; }

        [Display(Name = "担当ディレクター")]
        public string Director { get; set; }

        [Display(Name = "担当デスク")]

        public string Desk { get; set; }

        public int ScheduleId { get; set; }

        public int BroadcastId { get; set; }

        public int CornerId { get; set; }

        public int SpecificationId { get; set; }
    }
}