using System;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class ScheduleViewModel : IMapping
    {
        [Display(Name = "放送順")]
        public int Sequence { get; set; }

        [Display(Name = "コーナー名")]
        public string CornerName { get; set; }

        [Display(Name = "分類")]
        public string CornerType { get; set; }

        [Display(Name = "見出し")]
        public string Title { get; set; }

        [Display(Name = "担当ディレクター")]
        public string Director { get; set; }

        [Display(Name = "担当デスク")]

        public string Desk { get; set; }

        [Display(Name = "デスクチェック")]
        public bool DeskCheck { get; set; }

        public int ScheduleId { get; set; }

        public int BroadcastId { get; set; }

        public int CornerId { get; set; }

        public int SpecificationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Schedule, ScheduleViewModel>()
                .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence))
                .ForMember(dest => dest.CornerName, opt => opt.MapFrom(src => src.Corner.Name))
                .ForMember(dest => dest.CornerType, opt => opt.MapFrom(src => src.Corner.Type))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Specification.Title))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Specification.Director))
                .ForMember(dest => dest.Desk, opt => opt.MapFrom(src => src.Specification.Desk))
                .ForMember(dest => dest.DeskCheck, opt => opt.MapFrom(src => src.Specification.DeskCheck))
                .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BroadcastId, opt => opt.MapFrom(src => src.BroadcastId))
                .ForMember(dest => dest.CornerId, opt => opt.MapFrom(src => src.Corner.Id))
                .ForMember(dest => dest.SpecificationId, opt => opt.MapFrom(src => src.Specification.Id));
        }
    }
}