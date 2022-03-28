using System.ComponentModel.DataAnnotations;

namespace SistemaParqueaderos.Modelos
{
    public class Parqueadero
    {
        [Key]
        public string Nomenclatura { get; set; }
        [Required]
        public string Piso { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}
