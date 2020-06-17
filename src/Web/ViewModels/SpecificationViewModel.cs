using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels
{
    public class SpecificationViewModel
    {
        public int SpecificationId { get; set; }
        [Display(Name = "見出し")]
        public string Title { get; set; }
        [Display(Name = "本文")]
        public string Text { get; set; }
        [Display(Name = "担当ディレクター")]
        public string Director { get; set; }
        [Display(Name = "担当デスク")]
        public string Desk { get; set; }
        [Display(Name = "映像ソース")]
        public List<SelectListItem> VideoSourceSelectItems { get; set; }
        [Display(Name = "原稿ソース")]
        public List<SelectListItem> ArticleSourceSelectItems { get; set; }
    }
}