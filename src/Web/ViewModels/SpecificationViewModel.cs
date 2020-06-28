using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels
{
    public class SpecificationViewModel
    {
        public int SpecificationId { get; set; }

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
    }
}