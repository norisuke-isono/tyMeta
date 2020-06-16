namespace ApplicationCore.Entites
{
    public class SpecificationArticleSource
    {
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public int ArticleSourceId { get; set; }
        public ArticleSource ArticleSource { get; set; }
    }
}