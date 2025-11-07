using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiLocal.Data;
using MiApiLocal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace MiApiLocal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InmueblesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InmueblesController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/inmuebles
        [HttpGet]
        [Authorize] // Requiere token
        public async Task<IActionResult> ObtenerInmuebles()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Token inválido o expirado.");

            var propietario = await _context.Propietarios
                .Include(p => p.Inmuebles)
                .FirstOrDefaultAsync(p => p.Email == email);

            if (propietario == null)
                return NotFound("Propietario no encontrado.");

            var inmuebles = propietario.Inmuebles.Select(i => new
            {
                i.IdInmueble,
                i.Direccion,
                i.Uso,
                i.Tipo,
                i.Ambientes,
                i.Superficie,
                i.Latitud,
                i.Longitud,
                i.Valor,
                i.Imagen,
                i.Disponible,
                i.IdPropietario
            }).ToList();

            return Ok(inmuebles);
        }

        // 🔹 POST: api/inmuebles/cargar
        [HttpPost("cargar")]
        [Authorize]
        public async Task<IActionResult> CargarInmueble([FromForm] IFormFile imagen, [FromForm] string inmueble)
        {
            if (string.IsNullOrEmpty(inmueble))
                return BadRequest("Debe enviar los datos del inmueble.");

            var nuevoInmueble = System.Text.Json.JsonSerializer.Deserialize<Inmueble>(inmueble);

            var email = User.Identity?.Name;
            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p => p.Email == email);

            if (propietario == null)
                return NotFound("Propietario no encontrado.");

            if (imagen != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imagen.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await imagen.CopyToAsync(stream);
                }

                nuevoInmueble.Imagen = "/images/" + fileName;
            }

            nuevoInmueble.IdPropietario = propietario.IdPropietario;

            nuevoInmueble.Disponible = false;

            _context.Inmuebles.Add(nuevoInmueble);
            await _context.SaveChangesAsync();

            var result = new
            {
                nuevoInmueble.IdInmueble,
                nuevoInmueble.Direccion,
                nuevoInmueble.Uso,
                nuevoInmueble.Tipo,
                nuevoInmueble.Ambientes,
                nuevoInmueble.Superficie,
                nuevoInmueble.Latitud,
                nuevoInmueble.Longitud,
                nuevoInmueble.Valor,
                nuevoInmueble.Imagen,
                nuevoInmueble.Disponible,
                nuevoInmueble.IdPropietario
            };

            return Ok(result);
        }



        // 🔹 PUT: api/inmuebles/actualizar
        [HttpPut("actualizar")]
        [Authorize]
        public async Task<IActionResult> ActualizarInmueble([FromBody] JsonElement data)
        {
            if (!data.TryGetProperty("idInmueble", out var idProp))
                return BadRequest("Debe enviar el Id del inmueble.");

            int idInmueble = idProp.GetInt32();
            var inmueble = await _context.Inmuebles.FirstOrDefaultAsync(i => i.IdInmueble == idInmueble);

            if (inmueble == null)
                return NotFound("Inmueble no encontrado.");

            // 🔹 Actualizar campos si existen en el JSON recibido
            if (data.TryGetProperty("direccion", out var direccionProp))
                inmueble.Direccion = direccionProp.GetString();

            if (data.TryGetProperty("uso", out var usoProp))
                inmueble.Uso = usoProp.GetString();

            if (data.TryGetProperty("tipo", out var tipoProp))
                inmueble.Tipo = tipoProp.GetString();

            if (data.TryGetProperty("ambientes", out var ambProp))
                inmueble.Ambientes = ambProp.GetInt32();

            if (data.TryGetProperty("superficie", out var supProp))
                inmueble.Superficie = supProp.GetDouble();

            if (data.TryGetProperty("latitud", out var latProp))
                inmueble.Latitud = latProp.GetDouble();

            if (data.TryGetProperty("longitud", out var lonProp))
                inmueble.Longitud = lonProp.GetDouble();

            if (data.TryGetProperty("valor", out var valorProp))
                inmueble.Valor = (double)valorProp.GetDecimal();

            if (data.TryGetProperty("imagen", out var imagenProp))
                inmueble.Imagen = imagenProp.GetString();

            if (data.TryGetProperty("disponible", out var disponibleProp))
                inmueble.Disponible = disponibleProp.GetBoolean();

            if (data.TryGetProperty("idPropietario", out var idPropietarioProp))
                inmueble.IdPropietario = idPropietarioProp.GetInt32();

            await _context.SaveChangesAsync();

            return Ok(new
            {
                inmueble.IdInmueble,
                inmueble.Direccion,
                inmueble.Uso,
                inmueble.Tipo,
                inmueble.Ambientes,
                inmueble.Superficie,
                inmueble.Latitud,
                inmueble.Longitud,
                inmueble.Valor,
                inmueble.Imagen,
                inmueble.Disponible,
                inmueble.IdPropietario
            });
        }


    }
}