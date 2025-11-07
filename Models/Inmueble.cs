using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiLocal.Models
{
    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }

        [Required]
        public string Direccion { get; set; }

        public string Uso { get; set; }
        public string Tipo { get; set; }
        public int Ambientes { get; set; }
        public double Superficie { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Valor { get; set; }
        public string Imagen { get; set; }
        public bool Disponible { get; set; }

        [NotMapped]
        public bool TieneContratoVigente { get; set; }

        // FK con Propietario
        public int IdPropietario { get; set; }
        [ForeignKey(nameof(IdPropietario))]
        public Propietario Duenio { get; set; }

        // Relación con contratos
        public ICollection<Contrato> Contratos { get; set; }
    }
}
