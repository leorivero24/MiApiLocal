using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiApiLocal.Models
{
    public class Propietario
    {
        [Key]
        public int IdPropietario { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Dni { get; set; }
        public string? Email { get; set; }
        public string? Clave { get; set; }
        public string? Telefono { get; set; }

        // Inicializamos la lista para que nunca sea null
        public List<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();
    }
}
