using System.ComponentModel.DataAnnotations;

namespace SistemaParqueaderos.Modelos
{
    public class Usuario
    {
        [Key]
        public string NombreUsuario { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Nombres { get; set; }
    }
}
