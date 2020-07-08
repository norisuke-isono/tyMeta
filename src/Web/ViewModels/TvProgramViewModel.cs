using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class TvProgramViewModel : IMapping
    {
        public int TvProgramId { get; set; }

        [Display(Name = "番組名")]
        public string Name { get; set; }

        [Display(Name = "デフォルト番組構成")]
        public List<DefaultScheduleViewModel> DefaultScheduleViewModels { get; set; } = Enumerable.Empty<DefaultScheduleViewModel>().ToList();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TvProgram, TvProgramViewModel>()
                .ForMember(dest => dest.TvProgramId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DefaultScheduleViewModels, opt => opt.MapFrom(src => src.DefaultSchedules))
                .ReverseMap();
        }
    }
}