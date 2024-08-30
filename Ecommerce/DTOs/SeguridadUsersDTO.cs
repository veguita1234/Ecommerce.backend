using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs
{
    public class SeguridadUsersDTO
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Correo { get; set; }
        public string UserName { get; set; }
    }
}
