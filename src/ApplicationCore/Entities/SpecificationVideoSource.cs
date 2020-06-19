namespace ApplicationCore.Entites
{
    public class SpecificationVideoSource
    {
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }

        public int VideoSourceId { get; set; }
        public VideoSource VideoSource { get; set; }
    }
}