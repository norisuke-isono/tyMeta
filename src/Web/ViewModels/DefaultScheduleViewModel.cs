using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class DefaultScheduleViewModel : IMapping
    {
        [Display(Name = "放送順")]
        public int Sequence { get; set; }

        [Display(Name = "表示")]
        public bool DisplayFlag { get; set; }

        [Display(Name = "コーナー")]
        public int? CornerId { get; set; }

        public bool Dead { get; set; } = false;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DefaultSchedule, DefaultScheduleViewModel>()
                .ForMember(dest => dest.Dead, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}