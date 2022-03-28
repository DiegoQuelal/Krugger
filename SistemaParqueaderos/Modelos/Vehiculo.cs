using System.ComponentModel.DataAnnotations;

namespace SistemaParqueaderos.Modelos
{
    public class Vehiculo
    {
        [Key]
        public string Placa { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Propietario { get; set; }
    }
}
