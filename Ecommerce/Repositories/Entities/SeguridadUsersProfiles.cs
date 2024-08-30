using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadUsersProfiles
    {
        [Key]
        public Guid UsserProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }

        //NAVEGACION:
        public SeguridadUsers SeguridadUser { get; set; }
        public SeguridadProfiles SeguridadProfile { get; set; }
    }
}
