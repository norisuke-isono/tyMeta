using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class SpecificationCastViewModel : IMapping
    {
        [Display(Name = "名前")]
        public string Name { get; set; }

        [Display(Name = "所属")]
        public string Affiliation { get; set; }

        [Display(Name = "肩書き")]
        public string JobTitle { get; set; }

        [Display(Name = "連絡先")]
        public string ContactAddress { get; set; }

        [Display(Name = "備考")]
        public string Note { get; set; }

        public bool Dead { get; set; } = false;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SpecificationCast, SpecificationCastViewModel>().ReverseMap();
        }
    }
}