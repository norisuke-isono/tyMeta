using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationCore.Entites;
using ApplicationCore.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;

namespace Web.ViewModels
{
    public class SpecificationViewModel : IMapping
    {
        public int SpecificationId { get; set; }

        public int TvProgramId { get; set; }

        [Display(Name = "番組名")]
        public string TvProgramName { get; set; }

        [Display(Name = "コーナー名")]
        public string CornerName { get; set; }

        [Display(Name = "放送日")]
        [DataType(DataType.Date)]
        public DateTime AirDate { get; set; }

        [Display(Name = "見出し")]
        public string Title { get; set; }

        [Display(Name = "本文")]
        public string Text { get; set; }

        [Display(Name = "担当ディレクター")]
        public string Director { get; set; }

        [Display(Name = "担当デスク")]
        public string Desk { get; set; }

        [Display(Name = "タグ")]
        public string Tag { get; set; }

        [Display(Name = "キーワード")]
        public string Keyword { get; set; }

        [Display(Name = "映像ソース備考")]
        public string VideoSourceNote { get; set; }

        [Display(Name = "原稿ソース備考")]
        public string ArticleSourceNote { get; set; }

        [Display(Name = "デスクチェック")]
        public bool DeskCheck { get; set; }

        [Display(Name = "最終更新日時")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "カテゴリー")]
        public List<SelectListItem> CategorySelectItems { get; set; }

        [Display(Name = "映像ソース")]
        public List<SelectListItem> VideoSourceSelectItems { get; set; }

        [Display(Name = "原稿ソース")]
        public List<SelectListItem> ArticleSourceSelectItems { get; set; }

        [Display(Name = "素材ソース")]
        public List<SpecificationMaterialSourceViewModel> MaterialSourceViewModels { get; set; }

        [Display(Name = "取材先")]
        public List<SpecificationInterviewViewModel> InterviewViewModels { get; set; }

        [Display(Name = "出演者")]
        public List<SpecificationCastViewModel> CastViewModels { get; set; }

        public List<SpecificationCategory> SpecificationCategories
        {
            get
            {
                if (CategorySelectItems == null) return null;

                return CategorySelectItems.Where(x => x.Selected)
                    .Select(x => new SpecificationCategory
                    {
                        CategoryId = int.Parse(x.Value),
                    }).ToList();
            }
        }

        public List<SpecificationVideoSource> SpecificationVideoSources
        {
            get
            {
                if (VideoSourceSelectItems == null) return null;

                return VideoSourceSelectItems.Where(x => x.Selected)
                    .Select(x => new SpecificationVideoSource
                    {
                        VideoSourceId = int.Parse(x.Value),
                    }).ToList();
            }
        }

        public List<SpecificationArticleSource> SpecificationArticleSources
        {
            get
            {
                if (ArticleSourceSelectItems == null) return null;

                return ArticleSourceSelectItems.Where(x => x.Selected)
                    .Select(x => new SpecificationArticleSource
                    {
                        ArticleSourceId = int.Parse(x.Value),
                    }).ToList();
            }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Specification, SpecificationViewModel>()
                .ForMember(dest => dest.SpecificationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TvProgramId, opt => opt.MapFrom(src => src.Schedule.Broadcast.TvProgram.Id))
                .ForMember(dest => dest.TvProgramName, opt => opt.MapFrom(src => src.Schedule.Broadcast.TvProgram.Name))
                .ForMember(dest => dest.CornerName, opt => opt.MapFrom(src => src.Schedule.Corner.Name))
                .ForMember(dest => dest.AirDate, opt => opt.MapFrom(src => src.Schedule.Broadcast.AirDate))
                .ForMember(dest => dest.MaterialSourceViewModels, opt => opt.MapFrom(src => src.SpecificationMaterialSources))
                .ForMember(dest => dest.InterviewViewModels, opt => opt.MapFrom(src => src.SpecificationInterviews))
                .ForMember(dest => dest.CastViewModels, opt => opt.MapFrom(src => src.SpecificationCasts))
                .ForMember(dest => dest.CategorySelectItems, opt => opt.Ignore())
                .ForMember(dest => dest.VideoSourceSelectItems, opt => opt.Ignore())
                .ForMember(dest => dest.ArticleSourceSelectItems, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}