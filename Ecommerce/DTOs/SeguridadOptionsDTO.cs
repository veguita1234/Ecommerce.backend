using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadOptionsDTO
    {
        [Key]
        public Guid OptionId { get; set; }
        public string OptionCode { get; set; }
        public string DescriptionCode { get; set; }
    }
}
