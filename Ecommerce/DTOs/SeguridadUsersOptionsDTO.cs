using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadUsersOptionsDTO
    {
        [Key]
        public Guid UserOptionId { get; set; }
        public Guid UserId { get; set; }
        public Guid OptionId { get; set; }
    }
}
