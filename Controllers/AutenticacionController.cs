using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiApiLocal.Data;
using MiApiLocal.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiApiLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AutenticacionController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // 🔹 POST: api/autenticacion/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Propietario request)
        {
            // Solo usamos Email y Clave del request
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Clave))
                return BadRequest("Email y clave son requeridos.");

            // Buscar propietario por Email
            var propietario = await _context.Propietarios
                .FirstOrDefaultAsync(p => p.Email == request.Email);

            if (propietario == null)
                return Unauthorized("Usuario no encontrado.");

            // Comparar contraseña hasheada
            bool claveValida = BCrypt.Net.BCrypt.Verify(request.Clave, propietario.Clave);
            if (!claveValida)
                return Unauthorized("Contraseña incorrecta.");

            // Generar token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, propietario.Email),
                    new Claim("IdPropietario", propietario.IdPropietario.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
