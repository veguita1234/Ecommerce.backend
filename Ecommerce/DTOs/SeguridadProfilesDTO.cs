using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadProfilesDTO
    {
        [Key]
        public Guid ProfileId { get; set; }
        public string ProfileCode { get; set; }
        public string DescriptionProfile { get; set; }
    }
}
