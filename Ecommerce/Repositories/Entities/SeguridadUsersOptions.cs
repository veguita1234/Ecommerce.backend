using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadUsersOptions
    {
        [Key]
        public Guid UserOptionId { get; set; }
        public Guid UserId { get; set; }
        public Guid OptionId { get; set; }

        //NAVEGACION:
        public SeguridadUsers SeguridadUser { get; set; }
        public SeguridadOptions SeguridadOption { get; set; }
        public ICollection<SeguridadUsersOptionsPermissions> SeguridadUsersOptionsPermissions { get; set; }
    }
}
