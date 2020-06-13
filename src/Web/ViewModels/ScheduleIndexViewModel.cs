using System;

namespace Web.ViewModels
{
    public class ScheduleIndexViewModel
    {
        public int Sequence { get; set; }
        public string CornerName { get; set; }
        public DateTime AirDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int ScheduleId { get; set; }
        public int BroadcastId { get; set; }
        public int CornerId { get; set; }
        public int SpecificationId { get; set; }
    }
}