using System.ComponentModel.DataAnnotations;
using ApplicationCore.Enums;

namespace Web.ViewModels
{
    public class SpecificationMaterialSourceViewModel
    {
        [Required]
        [Display(Name = "種類")]
        public MaterialType? Type { get; set; }

        [Display(Name = "権利元")]
        public string CopyrightHolder { get; set; }

        [Display(Name = "使用条件・期間")]
        public string ConditionsOfUse { get; set; }

        [Display(Name = "連絡先・備考")]
        public string Note { get; set; }

        public bool Dead { get; set; } = false;
    }
}