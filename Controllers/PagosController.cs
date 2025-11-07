using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiLocal.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiApiLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PagosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagosController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/pagos/contrato/{id}
        [HttpGet("contrato/{id}")]
        public async Task<IActionResult> GetPagosPorContrato(int id)
        {
            try
            {
                Console.WriteLine($"🔍 Buscando pagos para contrato ID: {id}");

                // Obtener el ID del propietario desde el token JWT
                var idClaim = User.FindFirst("IdPropietario");
                if (idClaim == null)
                {
                    Console.WriteLine("🚫 No se encontró el claim IdPropietario en el token.");
                    return Unauthorized("No se pudo obtener el ID del propietario desde el token.");
                }

                int idPropietario = int.Parse(idClaim.Value);
                Console.WriteLine($"🧩 Propietario autenticado según JWT: {idPropietario}");

                // Buscar el contrato con su inmueble
                var contrato = await _context.Contratos
                    .Include(c => c.Inmueble)
                    .FirstOrDefaultAsync(c => c.IdContrato == id);

                if (contrato == null)
                {
                    Console.WriteLine($"❌ No se encontró el contrato con ID {id} en la base de datos.");
                    return NotFound("Contrato no encontrado.");
                }

                Console.WriteLine($"🏠 Contrato encontrado. IdInmueble: {contrato.IdInmueble}, IdPropietario del inmueble: {contrato.Inmueble.IdPropietario}");

                // Verificar que el inmueble pertenezca al propietario autenticado
                if (contrato.Inmueble.IdPropietario != idPropietario)
                {
                    Console.WriteLine($"🚫 El inmueble con ID {contrato.IdInmueble} no pertenece al propietario {idPropietario} según JWT.");
                    return Unauthorized("El contrato no pertenece a un inmueble del propietario autenticado.");
                }

                // Obtener los pagos asociados al contrato
                var pagos = await _context.Pagos
                    .Where(p => p.IdContrato == id)
                    .OrderBy(p => p.FechaPago)
                    .Select(p => new
                    {
                        p.IdPago,
                        FechaPago = p.FechaPago.ToString("yyyy-MM-dd"),
                        p.Monto,
                        p.Detalle,
                        p.Estado,
                        p.IdContrato
                    })
                    .ToListAsync();

                if (pagos.Count == 0)
                {
                    Console.WriteLine($"⚠️ No se encontraron pagos para el contrato ID {id}.");
                    return NotFound("No se encontraron pagos para este contrato.");
                }

                Console.WriteLine($"✅ Se encontraron {pagos.Count} pagos para el contrato ID {id}.");
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error al obtener pagos del contrato {id}: {ex.Message}");
                return BadRequest($"Error al obtener los pagos: {ex.Message}");
            }
        }
    }
}
