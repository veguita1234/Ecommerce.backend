using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadUsers
    {
        [Key]
        public Guid UserId { get; set; }
        public string? UserCode { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

        public string? Correo { get; set; }
        public string UserName { get; set; }

        //NAVEGACION:
        public ICollection<SeguridadUsersOptions> SeguridadUsersOptions { get; set; }
        public ICollection<SeguridadUsersProfiles> SeguridadUsersProfiles { get; set; }
        public ICollection<SeguridadEmpresasUsers> SeguridadEmpresasUsers { get; set; }
    }
}
