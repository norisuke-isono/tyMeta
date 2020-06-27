namespace ApplicationCore.Entites
{
    public class SpecificationCategory
    {
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}