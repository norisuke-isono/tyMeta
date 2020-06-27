using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Enums
{
    public enum MaterialType
    {
        [Display(Name = "画像")]
        Picture,

        [Display(Name = "新聞")]
        Newspaper,

        [Display(Name = "グラフィック")]
        graphics,

        [Display(Name = "その他")]
        Other,
    }
}