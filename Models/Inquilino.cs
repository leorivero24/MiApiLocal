using System.ComponentModel.DataAnnotations;

namespace MiApiLocal.Models
{
    public class Inquilino
    {
        [Key]
        public int IdInquilino { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Relación con contratos
        public ICollection<Contrato> Contratos { get; set; }
    }
}
