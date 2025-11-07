using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiApiLocal.Data;
using MiApiLocal.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace MiApiLocal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropietariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PropietariosController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

   

        // 🔹 GET: api/propietarios
        [HttpGet]
        [Authorize] // Requiere token
        public async Task<IActionResult> ObtenerPerfil()
        {
            // Obtenemos el email del propietario desde el token
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Token inválido o expirado.");

            // Buscamos el propietario en la base de datos
            var propietario = await _context.Propietarios
                .Include(p => p.Inmuebles) // si querés incluir inmuebles
                .FirstOrDefaultAsync(p => p.Email == email);

            if (propietario == null)
                return NotFound("Propietario no encontrado.");

            // Retornamos los datos (sin devolver la clave)
            return Ok(new
            {
                propietario.IdPropietario,
                propietario.Nombre,
                propietario.Apellido,
                propietario.Dni,
                propietario.Email,
                propietario.Telefono,
                Inmuebles = propietario.Inmuebles
            });
        }
        [HttpPut("actualizar")]
        [Authorize]
        public async Task<IActionResult> ActualizarPerfil([FromBody] Propietario actualizarPropietario)
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Token inválido o expirado.");

            var propietario = await _context.Propietarios
                .FirstOrDefaultAsync(p => p.Email == email);

            if (propietario == null)
                return NotFound("Propietario no encontrado.");

            // Actualización parcial: solo se cambian los campos que no son null
            if (!string.IsNullOrEmpty(actualizarPropietario.Nombre))
                propietario.Nombre = actualizarPropietario.Nombre;

            if (!string.IsNullOrEmpty(actualizarPropietario.Apellido))
                propietario.Apellido = actualizarPropietario.Apellido;

            if (!string.IsNullOrEmpty(actualizarPropietario.Dni))
                propietario.Dni = actualizarPropietario.Dni;

            if (!string.IsNullOrEmpty(actualizarPropietario.Telefono))
                propietario.Telefono = actualizarPropietario.Telefono;

            await _context.SaveChangesAsync();

            // Retornamos el propietario actualizado sin la clave
            return Ok(new
            {
                propietario.IdPropietario,
                propietario.Nombre,
                propietario.Apellido,
                propietario.Dni,
                propietario.Email,
                propietario.Telefono
            });
        }


    }

  
}
