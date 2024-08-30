using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadProfilesOptionsDTO
    {
        [Key]
        public Guid ProfileOptionId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid OptionId { get; set; }
    }
}
