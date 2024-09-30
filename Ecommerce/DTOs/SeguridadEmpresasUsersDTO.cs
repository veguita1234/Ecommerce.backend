using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadEmpresasUsersDTO
    {
        [Key]
        public Guid EmpresaUserId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid UserId { get; set; }
    }
}
