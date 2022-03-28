using System.ComponentModel.DataAnnotations;

namespace FrontGestionParqueaderos.Models
{
    public class Gestion
    {
        [Key]
        public int IdRegistro { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public DateTime FechaIngreso { get; set;}
        [Required]
        public DateTime FechaSalida { get; set; }
        [Required]
        public decimal ValorPagar { get; set;}
        [Required]
        public string Estado { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Nomenclatura { get; set; }
    }
}
