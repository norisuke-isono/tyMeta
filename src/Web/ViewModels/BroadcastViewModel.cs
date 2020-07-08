using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class BroadcastViewModel : IMapping
    {
        public int TvProgramId { get; set; }

        [Display(Name = "放送日")]
        public DateTime AirDate { get; set; }

        [Display(Name = "番組構成")]
        public List<ScheduleViewModel> ScheduleViewModels { get; set; } = Enumerable.Empty<ScheduleViewModel>().ToList();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Broadcast, BroadcastViewModel>()
                .ForMember(dest => dest.ScheduleViewModels, opt => opt.MapFrom(src => src.Schedules));
        }
    }
}