namespace Ecommerce.DTOs.Response
{
    public class RegisterCompanyResponseDTO
    {
        public string Token { get; set; }
        public string? UserName { get; set; }
        public string RazonSocial { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
    }
}
