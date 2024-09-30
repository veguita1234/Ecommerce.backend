using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Repositories.Entities
{
    public class SeguridadEmpresasUsers
    {
        [Key]
        public Guid EmpresaUserId {  get; set; }
        public Guid EmpresaId { get; set; }
        public Guid UserId { get; set; }

        //NAVEGACION:
        public SeguridadEmpresas SeguridadEmpresas { get; set; }
        public SeguridadUsers SeguridadUsers { get; set; }
    }
}
