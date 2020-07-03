using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entites;
using ApplicationCore.Enums;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class SpecificationMaterialSourceViewModel : IMapping
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SpecificationMaterialSource, SpecificationMaterialSourceViewModel>()
                .ForMember(dest => dest.Dead, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}