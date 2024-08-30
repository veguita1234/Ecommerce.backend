using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadUsersProfilesDTO
    {
        [Key]
        public Guid UsserProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
    }
}
