using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class SpecificationInterviewViewModel
    {
        [Display(Name = "名前")]
        public string Name { get; set; }

        [Display(Name = "所属")]
        public string Affiliation { get; set; }

        [Display(Name = "肩書き")]
        public string JobTitle { get; set; }

        [Display(Name = "商品・サービス名")]
        public string Product { get; set; }

        [Display(Name = "連絡先")]
        public string ContactAddress { get; set; }

        [Display(Name = "住所")]
        public string Address { get; set; }

        [Display(Name = "備考")]
        public string Note { get; set; }

        [Display(Name = "検索対象")]
        public bool UseSearch { get; set; }

        public bool Dead { get; set; } = false;
    }
}