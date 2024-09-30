using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Request
{
    public class UpdateUserRequestDTO
    {
        [Key]

        public string? UserCode { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Correo { get; set; }
    }
}
