using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadProfiles
    {
        [Key]
        public Guid ProfileId { get; set; }
        public string ProfileCode { get; set; }
        public string DescriptionProfile { get; set; }

        //NAVEGACION:
        public ICollection<SeguridadProfilesOptions> SeguridadProfilesOptions { get; set; }
        public ICollection<SeguridadUsersProfiles> SeguridadUsersProfiles { get; set; }
    }
}
