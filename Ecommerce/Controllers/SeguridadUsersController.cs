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
    public class SeguridadUsersController : ControllerBase
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
                return string.Concat(textoEnBytes.Select(b => b.ToString("x2")));
            }
        }

        private LoginResponseDTO GenerateToken(SeguridadUsersDTO seguridadUser, string razonSocial = null)
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
            new Claim("LastName", seguridadUser.LastName ?? string.Empty),
            new Claim("RazonSocial", razonSocial ?? string.Empty) // Agrega RazonSocial aquí
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
                    UserName = seguridadUser.UserName,
                    RazonSocial = razonSocial 
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
                    .Select(u => new
                    {
                        User = new SeguridadUsersDTO
                        {
                            UserId = u.UserId,
                            UserCode = u.UserCode,
                            Name = u.Name,
                            LastName = u.LastName,
                            Correo = u.Correo,
                            UserName = u.UserName
                        },
                        RazonSocial = _context.SeguridadEmpresas
                            .Where(e => _context.SeguridadEmpresasUsers
                                .Any(ue => ue.UserId == u.UserId && ue.EmpresaId == e.EmpresaId))
                            .Select(e => e.CompanyName)
                            .FirstOrDefault()
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Unauthorized("Usuario no encontrado o contraseña incorrecta.");
                }

                return Ok(GenerateToken(user.User, user.RazonSocial));
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
                    return Conflict("El nombre de usuario ya está en uso.");
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
                    UserName = newUser.UserName
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

        [HttpPost("registerCompany")]
        
        public async Task<ActionResult<RegisterCompanyResponseDTO>> RegisterCompany([FromBody] RegisterCompanyRequestDTO request)
        {
            try
            {
                if (request == null ||
                    string.IsNullOrEmpty(request.RUC) ||
                    string.IsNullOrEmpty(request.RazonSocial) ||
                    string.IsNullOrEmpty(request.Departamento) ||
                    string.IsNullOrEmpty(request.Provincia) ||
                    string.IsNullOrEmpty(request.Distrito) ||
                    string.IsNullOrEmpty(request.Direccion) ||
                    string.IsNullOrEmpty(request.Telefono) ||
                    string.IsNullOrEmpty(request.Celular) ||
                    string.IsNullOrEmpty(request.UserName) ||
                    string.IsNullOrEmpty(request.Password) ||
                    string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Datos incompletos para el registro de la empresa y el usuario.");
                }

                var existingUser = await _context.SeguridadUsers
                    .AnyAsync(u => u.UserName == request.UserName);

                if (existingUser)
                {
                    return Conflict("El nombre de usuario ya está en uso.");
                }

                var empresa = new SeguridadEmpresas
                {
                    EmpresaId = Guid.NewGuid(),
                    RUC = request.RUC,
                    CompanyName = request.RazonSocial,
                    Department = request.Departamento,
                    Province = request.Provincia,
                    District = request.Distrito,
                    Address = request.Direccion,
                    Telefono = request.Telefono,
                    Celular = request.Celular,
                    Email = request.Email,
                };

                await _context.SeguridadEmpresas.AddAsync(empresa);
                await _context.SaveChangesAsync();

                var encryptedPassword = Encripter(request.Password);

                var user = new SeguridadUsers
                {
                    UserId = Guid.NewGuid(),
                    UserCode = null,
                    Password = encryptedPassword,
                    Name = null,
                    LastName = null,
                    //Correo = request.Email,
                    UserName = request.UserName
                };

                await _context.SeguridadUsers.AddAsync(user);
                await _context.SaveChangesAsync();

                var empresaUser = new SeguridadEmpresasUsers
                {
                    EmpresaUserId = Guid.NewGuid(),
                    EmpresaId = empresa.EmpresaId,
                    UserId = user.UserId
                };

                await _context.SeguridadEmpresasUsers.AddAsync(empresaUser);
                await _context.SaveChangesAsync();

                var createdUserDTO = new RegisterCompanyResponseDTO
                {
                    Token = GenerateToken(new SeguridadUsersDTO
                    {
                        UserId = user.UserId,
                        UserCode = user.UserCode,
                        Name = user.Name,
                        LastName = user.LastName,
                        Correo = user.Correo,
                        UserName = user.UserName,
                       
                    }).Token,
                    UserName = user.UserName,
                    RazonSocial = empresa.CompanyName,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = empresa.Email,
                };

                return Ok(createdUserDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("updateUser/{userId}")]
        public async Task<ActionResult> UpdateUser( Guid userId, [FromBody] UpdateUserRequestDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Datos incompletos.");
            }

            var user = await _context.SeguridadUsers.FindAsync(userDto.UserId);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            user.UserCode = userDto.UserCode;
            user.Name = userDto.Name;
            user.LastName = userDto.LastName;
            user.Correo = userDto.Correo;
            

            _context.SeguridadUsers.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Usuario actualizado correctamente.");
        }

        // PUT: api/SeguridadUsers/updateCompany
        [HttpPut("updateCompany/{empresaId}")]
        public async Task<ActionResult> UpdateCompany(Guid empresaId, [FromBody] UpdateCompanyRequestDTO empresaDto)
        {
            if (empresaDto == null)
            {
                return BadRequest("Datos incompletos.");
            }

            var empresa = await _context.SeguridadEmpresas.FindAsync(empresaDto.EmpresaId);
            if (empresa == null)
            {
                return NotFound("Empresa no encontrada.");
            }

            empresa.RUC = empresaDto.RUC;
            empresa.CompanyName = empresaDto.CompanyName;
            empresa.Department = empresaDto.Department;
            empresa.Province = empresaDto.Province;
            empresa.District = empresaDto.District;
            empresa.Address = empresaDto.Address;
            empresa.Telefono = empresaDto.Telefono;
            empresa.Celular = empresaDto.Celular;
            empresa.Email = empresaDto.Email;

            _context.SeguridadEmpresas.Update(empresa);
            await _context.SaveChangesAsync();

            return Ok("Empresa actualizada correctamente.");
        }
    }
}