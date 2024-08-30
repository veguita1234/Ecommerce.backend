using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadProfilesOptionsPermissionsDTO
    {
        [Key]
        public Guid ProfileOptionPermissionId { get; set; }
        public Guid ProfileOptionId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
