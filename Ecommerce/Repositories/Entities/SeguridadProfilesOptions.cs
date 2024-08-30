using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadProfilesOptions
    {
        [Key]
        public Guid ProfileOptionId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid OptionId { get; set; }

        //NAVEGACION:
        public SeguridadProfiles SeguridadProfile { get; set; }
        public SeguridadOptions SeguridadOption { get; set; }
        public ICollection<SeguridadProfilesOptionsPermissions> SeguridadProfilesOptionsPermissions { get; set; }
    }
}
