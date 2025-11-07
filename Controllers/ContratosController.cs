using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiLocal.Data;
using MiApiLocal.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiApiLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContratosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContratosController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/Contratos/GetContratoVigente
        [HttpGet("GetContratoVigente")]
        public async Task<IActionResult> GetContratoVigente()
        {
            try
            {
                // ✅ Obtener el ID del propietario desde el token JWT
                var idClaim = User.FindFirst("IdPropietario");
                if (idClaim == null)
                    return Unauthorized("No se pudo obtener el ID del propietario desde el token.");

                int idPropietario = int.Parse(idClaim.Value);

                // ✅ Buscar inmuebles con contrato vigente del propietario autenticado
                var inmueblesConContrato = await _context.Inmuebles
                    .Include(i => i.Contratos)
                    .Where(i => i.IdPropietario == idPropietario &&
                                i.Contratos.Any(c => c.Estado == true &&
                                                     c.FechaInicio <= DateTime.Now &&
                                                     c.FechaFinalizacion >= DateTime.Now))
                    .Select(i => new
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
                        i.IdPropietario,
                        ContratoVigente = i.Contratos
                            .Where(c => c.Estado == true &&
                                        c.FechaInicio <= DateTime.Now &&
                                        c.FechaFinalizacion >= DateTime.Now)
                            .Select(c => new
                            {
                                c.IdContrato,
                                // 🔹 Formateamos las fechas sin horas/minutos/segundos
                                FechaInicio = c.FechaInicio.ToString("yyyy-MM-dd"),
                                FechaFinalizacion = c.FechaFinalizacion.ToString("yyyy-MM-dd"),
                                c.MontoAlquiler,
                                c.Estado,
                                c.IdInquilino
                            })
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                return Ok(inmueblesConContrato);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los inmuebles con contrato vigente: {ex.Message}");
            }
        }
        // 🔹 GET: api/contratos/inmueble/{id}
        [HttpGet("inmueble/{id}")]
        public async Task<IActionResult> GetContratoPorInmueble(int id)
        {
            try
            {
                // ✅ Obtener el ID del propietario desde el token JWT
                var idClaim = User.FindFirst("IdPropietario");
                if (idClaim == null)
                {
                    Console.WriteLine("🚫 No se encontró el claim IdPropietario en el token.");
                    return Unauthorized("No se pudo obtener el ID del propietario desde el token.");
                }

                int idPropietario = int.Parse(idClaim.Value);
                Console.WriteLine($"🧩 Propietario autenticado: {idPropietario}, buscando contrato para inmueble ID: {id}");

                // ✅ Buscar el inmueble con sus contratos
                var inmueble = await _context.Inmuebles
                    .Include(i => i.Contratos)
                    .ThenInclude(c => c.Inquilino)
                    .FirstOrDefaultAsync(i => i.IdInmueble == id);

                if (inmueble == null)
                {
                    Console.WriteLine($"❌ No se encontró el inmueble con ID {id}.");
                    return NotFound("Inmueble no encontrado.");
                }

                // ✅ Verificar que el inmueble pertenezca al propietario autenticado
                if (inmueble.IdPropietario != idPropietario)
                {
                    Console.WriteLine($"🚫 El inmueble con ID {id} no pertenece al propietario {idPropietario}.");
                    return Unauthorized("El inmueble no pertenece al propietario autenticado.");
                }

                Console.WriteLine($"🏠 Inmueble {id} pertenece al propietario {idPropietario}.");

                // ✅ Buscar el contrato vigente o más reciente
                var contrato = inmueble.Contratos
                    .Where(c => c.Estado == true)
                    .OrderByDescending(c => c.FechaInicio)
                    .Select(c => new
                    {
                        c.IdContrato,
                        FechaInicio = c.FechaInicio.ToString("yyyy-MM-dd"),
                        FechaFinalizacion = c.FechaFinalizacion.ToString("yyyy-MM-dd"),
                        c.MontoAlquiler,
                        c.Estado,
                        c.IdInquilino,
                        Inquilino = new
                        {
                            c.Inquilino.IdInquilino,
                            c.Inquilino.Nombre,
                            c.Inquilino.Apellido,
                            c.Inquilino.Email,
                            c.Inquilino.Telefono
                        },
                        Inmueble = new
                        {
                            inmueble.IdInmueble,
                            inmueble.Direccion,
                            inmueble.Tipo,
                            inmueble.Uso,
                            inmueble.Valor,
                            inmueble.Disponible
                        }
                    })
                    .FirstOrDefault();

                if (contrato == null)
                {
                    Console.WriteLine($"⚠️ No se encontró contrato vigente para el inmueble {id}.");
                    return NotFound("No hay contrato vigente asociado a este inmueble.");
                }

                Console.WriteLine($"✅ Contrato encontrado para el inmueble {id}, ID contrato: {contrato.IdContrato}");
                return Ok(contrato);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error al obtener contrato del inmueble {id}: {ex.Message}");
                return BadRequest($"Error al obtener el contrato del inmueble: {ex.Message}");
            }
        }

        
    }
}
