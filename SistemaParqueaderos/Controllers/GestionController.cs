using Microsoft.AspNetCore.Mvc;
using SistemaParqueaderos.AccesoDatos;
using SistemaParqueaderos.Modelos;

namespace SistemaParqueaderos.Controllers
{
    [Route("[controller]")]
    public class GestionController : Controller
    {
        private readonly AdministracionParqueaderosDBContext context;
        public GestionController(AdministracionParqueaderosDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("Gestion")]
        public IEnumerable<Gestion> Get()
        {
            var Gestion = context.Gestion.ToList();
            return Gestion;
        }
        [HttpGet]
        [Route("Gestion/{IdRegistro}")]
        public Gestion Get(int IdRegistro)
        {
            var Gestion = context.Gestion.FirstOrDefault(x => x.IdRegistro == IdRegistro);
            if (Gestion == null)
                Gestion = new Gestion();
            return Gestion;
        }
        [HttpPost]
        [Route("Gestion")]
        public void Post([FromBody]Gestion Gestion)
        {
            Gestion GestionFitrado = context.Gestion.FirstOrDefault(x => x.IdRegistro == Gestion.IdRegistro);
            if (string.IsNullOrEmpty(GestionFitrado?.Nomenclatura))
            {
                context.Add(Gestion);
                context.SaveChanges();
            }
            else
            {
                GestionFitrado.Nomenclatura = Gestion.Nomenclatura;
                GestionFitrado.IdRegistro = Gestion.IdRegistro;
                GestionFitrado.Estado = Gestion.Estado;
                GestionFitrado.Placa = Gestion.Placa;
                GestionFitrado.NombreUsuario = Gestion.NombreUsuario;
                GestionFitrado.ValorPagar = Gestion.ValorPagar;
                GestionFitrado.FechaIngreso = Gestion.FechaIngreso;
                GestionFitrado.FechaSalida = Gestion.FechaSalida;
                context.Gestion.Update(GestionFitrado);
                context.SaveChanges();
            }

        }
        [HttpDelete]
        [Route("Gestion")]
        public void Delete(int IdRegistro)
        {
            var Gestion = context.Gestion.FirstOrDefault(x => x.IdRegistro == IdRegistro);
            if (Gestion == null)
                return;
            context.Gestion.Remove(Gestion);
            context.SaveChanges();
        }
    }
}
