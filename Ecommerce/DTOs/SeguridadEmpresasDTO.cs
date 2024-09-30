using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadEmpresasDTO
    {
        [Key]
        public Guid EmpresaId { get; set; }
        public string RUC { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}
