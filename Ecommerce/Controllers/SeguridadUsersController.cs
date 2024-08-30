using Ecommerce.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Ecommerce.DTOs.Request;
using Ecommerce.DTOs.Response;
using Ecommerce.Repositories;
using Ecommerce.Repositories.Entities;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadUsersController : ControllerBase // Usa ControllerBase en lugar de Controller para API
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public SeguridadUsersController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string Encripter(string texto)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] textoEnBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(texto));
                return string.Concat(textoEnBytes.Select(b => b.ToString("x2"))); // Uso más eficiente de StringBuilder
            }
        }

        private LoginResponseDTO GenerateToken(SeguridadUsersDTO seguridadUser)
        {
            try
            {
                if (string.IsNullOrEmpty(seguridadUser.UserName))
                {
                    throw new ArgumentNullException(nameof(seguridadUser.UserName), "El nombre de usuario no puede ser nulo o vacío.");
                }
                var expires = DateTime.UtcNow.AddHours(16);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, seguridadUser.UserName),
                    new Claim("Name", seguridadUser.Name ?? string.Empty),
                    new Claim("LastName", seguridadUser.LastName ?? string.Empty)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                var securityToken = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials
                );
                return new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Name = seguridadUser.Name,
                    LastName = seguridadUser.LastName,
                    UserName = seguridadUser.UserName
                };
            }
            catch (Exception e)
            {
                // Consider using logging here
                return new LoginResponseDTO
                {
                    Token = "ERROR: " + e.Message
                };
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO login)
        {
            try
            {
                var encryptedPassword = Encripter(login.Password);

                var user = await _context.SeguridadUsers
                    .Where(u => u.UserName == login.UserName && u.Password == encryptedPassword)
                    .Select(u => new SeguridadUsersDTO
                    {
                        UserId = u.UserId,
                        UserCode = u.UserCode,
                        Name = u.Name,
                        LastName = u.LastName,
                        Correo = u.Correo,
                        UserName = u.UserName
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Unauthorized("Usuario no encontrado o contraseña incorrecta."); // 401 Unauthorized es más apropiado
                }

                return Ok(GenerateToken(user));
            }
            catch (Exception e)
            {
                // Consider using logging here
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponseDTO>> Register([FromBody] SeguridadUsersDTO newUser)
        {
            try
            {
                if (string.IsNullOrEmpty(newUser.UserName) || string.IsNullOrEmpty(newUser.Password))
                {
                    return BadRequest("El nombre de usuario y la contraseña son obligatorios.");
                }

                var existingUser = await _context.SeguridadUsers
                    .AnyAsync(u => u.UserName == newUser.UserName);

                if (existingUser)
                {
                    return Conflict("El nombre de usuario ya está en uso."); // 409 Conflict para usuario existente
                }

                var encryptedPassword = Encripter(newUser.Password);

                var user = new SeguridadUsers
                {
                    UserId = Guid.NewGuid(),
                    UserCode = newUser.UserCode,
                    Password = encryptedPassword,
                    Name = newUser.Name,
                    LastName = newUser.LastName,
                    Correo = newUser.Correo,
                    UserName = newUser.UserName,
                };

                await _context.SeguridadUsers.AddAsync(user);
                await _context.SaveChangesAsync();

                var createdUser = await _context.SeguridadUsers
                    .Where(u => u.UserName == newUser.UserName)
                    .Select(u => new SeguridadUsersDTO
                    {
                        UserId = u.UserId,
                        UserCode = u.UserCode,
                        Name = u.Name,
                        LastName = u.LastName,
                        Correo = u.Correo,
                        UserName = u.UserName
                    })
                    .FirstOrDefaultAsync();

                if (createdUser == null)
                {
                    return BadRequest("Error al crear el usuario.");
                }

                return Ok(GenerateToken(createdUser));
            }
            catch (Exception e)
            {
                // Consider using logging here
                return BadRequest(e.Message);
            }
        }
    }
}
