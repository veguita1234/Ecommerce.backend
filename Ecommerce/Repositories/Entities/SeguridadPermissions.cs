using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadPermissions
    {
        [Key]
        public Guid PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string DescriptionPermission { get; set; }

        //NAVEGACION:
        public ICollection<SeguridadProfilesOptionsPermissions> SeguridadProfilesOptionsPermissions { get; set; }
        public ICollection<SeguridadUsersOptionsPermissions> SeguridadUsersOptionsPermissions { get; set; }
    }
}
