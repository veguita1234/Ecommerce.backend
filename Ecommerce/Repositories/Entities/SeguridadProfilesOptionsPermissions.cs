using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadProfilesOptionsPermissions
    {
        [Key]
        public Guid ProfileOptionPermissionId { get; set; }
        public Guid ProfileOptionId { get; set; }
        public Guid PermissionId { get; set; }

        //NAVEGACION:
        public SeguridadProfilesOptions SeguridadProfileOption { get; set; }
        public SeguridadPermissions SeguridadPermission { get; set; }
    }
}
