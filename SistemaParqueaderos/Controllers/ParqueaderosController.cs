using Microsoft.AspNetCore.Mvc;
using SistemaParqueaderos.AccesoDatos;
using SistemaParqueaderos.Modelos;

namespace SistemaParqueaderos.Controllers
{
    [Route("[controller]")]
    public class ParqueaderosController : Controller
    {
        private readonly AdministracionParqueaderosDBContext context;
        public ParqueaderosController(AdministracionParqueaderosDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("Parqueadero")]
        public IEnumerable<Parqueadero> Get()
        {
            var Parqueadero = context.Parqueadero.ToList();
            return Parqueadero;
        }
        [HttpGet]
        [Route("Parqueadero/{Nomenclatura}")]
        public Parqueadero Get(string Nomenclatura)
        {
            var Parqueadero = context.Parqueadero.FirstOrDefault(x => x.Nomenclatura == Nomenclatura);
            if (Parqueadero == null)
                Parqueadero = new Parqueadero();
            return Parqueadero;
        }
        [HttpPost]
        [Route("Parqueadero")]
        public void Post([FromBody]Parqueadero Parqueadero)
        {
            Parqueadero ParqueaderoFitrado = context.Parqueadero.FirstOrDefault(x => x.Nomenclatura == Parqueadero.Nomenclatura);
            if(ParqueaderoFitrado?.Nomenclatura == null)
            {
                context.Add(Parqueadero);
                context.SaveChanges();
            }
            else
            {
                ParqueaderoFitrado.Nomenclatura = Parqueadero.Nomenclatura;
                ParqueaderoFitrado.Piso = Parqueadero.Piso;
                ParqueaderoFitrado.Estado = Parqueadero.Estado;
                context.Parqueadero.Update(ParqueaderoFitrado);
                context.SaveChanges();
            }
        }
        [HttpDelete]
        [Route("Parqueadero")]
        public void Delete(string Nomenclatura)
        {
            var Parqueadero = context.Parqueadero.FirstOrDefault(x => x.Nomenclatura == Nomenclatura);
            if (Parqueadero == null)
                return;
            context.Parqueadero.Remove(Parqueadero);
            context.SaveChanges();
        }
    }
}
