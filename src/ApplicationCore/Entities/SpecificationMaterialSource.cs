using System.ComponentModel.DataAnnotations;
using ApplicationCore.Enums;

namespace ApplicationCore.Entites
{
    public class SpecificationMaterialSource : BaseEntity
    {
        [Required]
        public MaterialType Type { get; set; }

        public string CopyrightHolder { get; set; }

        public string ConditionsOfUse { get; set; }

        public string Note { get; set; }

        [Required]
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
    }
}