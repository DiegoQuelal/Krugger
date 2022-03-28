using Microsoft.EntityFrameworkCore;
using SistemaParqueaderos.Modelos;

namespace SistemaParqueaderos.AccesoDatos
{
    public class AdministracionParqueaderosDBContext : DbContext
    {
        public AdministracionParqueaderosDBContext(DbContextOptions<AdministracionParqueaderosDBContext> options) : base(options)
        { }
        public DbSet<Vehiculo> Vehiculos{get;set;}
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Gestion> Gestion { get; set; }
        public DbSet<Parqueadero> Parqueadero { get; set; }
    }
}
