using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadUsersOptionsPermissions
    {
        [Key]
        public Guid UserOptionPermissionId { get; set; }
        public Guid UserOptionId { get; set; }
        public Guid PermissionId { get; set; }

        //NAVEGACION:
        public SeguridadUsersOptions SeguridadUserOption { get; set; }
        public SeguridadPermissions SeguridadPermission { get; set; }
    }
}
