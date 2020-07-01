using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class SpecificationIndexViewModel : IMapping
    {
        public int SpecificationId { get; set; }

        [Display(Name = "放送日")]
        [DataType(DataType.Date)]
        public DateTime AirDate { get; set; }

        [Display(Name = "番組")]
        public string TvProgramName { get; set; }

        [Display(Name = "コーナー名")]
        public string CornerName { get; set; }

        [Display(Name = "見出し")]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Specification, SpecificationIndexViewModel>()
                .ForMember(dest => dest.SpecificationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AirDate, opt => opt.MapFrom(src => src.Schedule.Broadcast.AirDate))
                .ForMember(dest => dest.TvProgramName, opt => opt.MapFrom(src => src.Schedule.Broadcast.TvProgram.Name))
                .ForMember(dest => dest.CornerName, opt => opt.MapFrom(src => src.Schedule.Corner.Name))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        }
    }
}