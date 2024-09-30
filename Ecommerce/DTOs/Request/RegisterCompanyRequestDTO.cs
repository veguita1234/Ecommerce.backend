using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Request
{
    public class RegisterCompanyRequestDTO
    {
        [Required]
        public string RUC { get; set; }

        [Required]
        public string RazonSocial { get; set; }

        [Required]
        public string Departamento { get; set; }

        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Distrito { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }

}
