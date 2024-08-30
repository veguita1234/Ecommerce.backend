using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadOptions
    {
        [Key]
        public Guid OptionId { get; set; }
        public string OptionCode { get; set; }
        public string DescriptionCode { get; set; }

        //NAVEGACION:
        public ICollection<SeguridadProfilesOptions> SeguridadProfilesOptions { get; set; }
        public ICollection<SeguridadUsersOptions> SeguridadUsersOptions { get; set; }

    }
}
