using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiLocal.Models
{
    public class Contrato
    {
        [Key]
        public int IdContrato { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFinalizacion { get; set; }

        public double MontoAlquiler { get; set; }
        public bool Estado { get; set; }

        // FK con Inquilino
        public int IdInquilino { get; set; }
        [ForeignKey(nameof(IdInquilino))]
        public Inquilino Inquilino { get; set; }

        // FK con Inmueble
        public int IdInmueble { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        public Inmueble Inmueble { get; set; }

        // Relación con pagos
        public ICollection<Pago> Pagos { get; set; }
    }
}
