using Microsoft.AspNetCore.Mvc;
using SistemaParqueaderos.AccesoDatos;
using SistemaParqueaderos.Modelos;

namespace SistemaParqueaderos.Controllers
{
    [Route("[controller]")]
    public class VehiculosController : Controller
    {
        private readonly AdministracionParqueaderosDBContext context;
        public VehiculosController(AdministracionParqueaderosDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("Vehiculos")]
        public IEnumerable<Vehiculo> Get()
        {
            var Vehiculos = context.Vehiculos.ToList();
            return Vehiculos;
        }
        [HttpGet]
        [Route("Vehiculos/{Placa}")]
        public Vehiculo Get(string Placa)
        {
            var Vehiculo = context.Vehiculos.FirstOrDefault(x => x.Placa == Placa);
            if (Vehiculo == null)
                Vehiculo = new Vehiculo();
            return Vehiculo;
        }
        [HttpPost]
        [Route("Vehiculos")]
        public void Post(Vehiculo Vehiculo)
        {
            context.Add(Vehiculo);
            context.SaveChanges();
        }
        [HttpDelete]
        [Route("Vehiculos")]
        public void Delete(string Placa)
        {
            var Vehiculo = context.Vehiculos.FirstOrDefault(x => x.Placa == Placa);
            if(Vehiculo == null)
                return; 
            context.Vehiculos.Remove(Vehiculo);
            context.SaveChanges();
        }
    }
}
