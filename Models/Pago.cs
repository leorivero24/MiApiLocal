using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiLocal.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        public double Monto { get; set; }
        public string Detalle { get; set; }
        public bool Estado { get; set; }

        // FK con Contrato
        public int IdContrato { get; set; }
        [ForeignKey(nameof(IdContrato))]
        public Contrato Contrato { get; set; }
    }
}
