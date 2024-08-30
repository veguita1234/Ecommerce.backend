using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadPermissionsDTO
    {
        [Key]
        public Guid PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string DescriptionPermission { get; set; }
    }
}
