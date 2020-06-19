using System;

namespace ApplicationCore.Models
{
    public class SpecificationFilter
    {
        public string Keyword { get; set; }
        public int? TvProgramId { get; set; }
        public int? CornerId { get; set; }
        public int? VideoSourceId { get; set; }
        public int? ArticleSourceId { get; set; }
        public string Director { get; set; }
        public string Desk { get; set; }
        public DateTime AirDateFrom { get; set; }
        public DateTime AirDateTo { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}