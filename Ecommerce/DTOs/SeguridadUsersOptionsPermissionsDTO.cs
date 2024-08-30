using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadUsersOptionsPermissionsDTO
    {
        [Key]
        public Guid UserOptionPermissionId { get; set; }
        public Guid UserOptionId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
