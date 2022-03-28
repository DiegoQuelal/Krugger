using FrontGestionParqueaderos.Models;
using FrontGestionParqueaderos.Servicio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System.Diagnostics;
namespace FrontGestionParqueaderos.Controllers
{
    public class GestionController : Controller
    {
        public IActionResult Index(int? Pagina, string FiltroBusqueda)
        {
            ServicioGestion servicio = new ServicioGestion();
            IEnumerable<Gestion> lstGestion = servicio.ObtenerGestion().Result;
            if (!string.IsNullOrEmpty(FiltroBusqueda))
                lstGestion = lstGestion.Where(x => x.Estado.Contains(FiltroBusqueda) || x.Nomenclatura.Contains(FiltroBusqueda) || x.Placa.Contains(FiltroBusqueda));
            int? TamanioPagina = 1;
            Pagina = (Pagina ?? 1);
            @ViewData["FitroActual"] = FiltroBusqueda;
            return View(lstGestion.ToPagedList(Pagina.Value,TamanioPagina.Value));
        }

        public IActionResult Edit(int IdRegistro)
        {
            ServicioGestion servicio = new ServicioGestion();
            Gestion Gestion = servicio.ObtenerGestion(IdRegistro).Result;
            Gestion.FechaSalida = System.DateTime.Now;
            TimeSpan result = (Gestion.FechaSalida).Subtract(Gestion.FechaIngreso);
            int TotalHoras = Convert.ToInt32(result.TotalHours);
            if ((result.TotalHours - TotalHoras) > 0 && (result.TotalHours - TotalHoras) < 0.5)
                TotalHoras = TotalHoras + 1;
            Gestion.ValorPagar = (decimal)(TotalHoras * 0.5);
            ViewBag.TotalHoras = TotalHoras;
            return View(Gestion);
        }

        public async Task<IActionResult> Editar(int IdRegistro, decimal ValorPagar)
        {
            ServicioGestion servicio = new ServicioGestion();
            Gestion objGestion = servicio.ObtenerGestion(IdRegistro).Result;
            objGestion.ValorPagar = ValorPagar;
            objGestion.Estado = "Pagado";
            objGestion.FechaSalida = System.DateTime.Now;
            await servicio.AtualizarGestion(objGestion);

            ServicioAdministracionParqueadero servicioParqueadero = new ServicioAdministracionParqueadero();
            Parqueadero objParqueadero = servicioParqueadero.ObtenerParqueadero(objGestion.Nomenclatura).Result; ;
            objParqueadero.Estado = "Libre";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            IEnumerable<Parqueadero> lstParqueaderos = servicio.ObtenerParqueaderos().Result;
            lstParqueaderos = lstParqueaderos.Where(x => x.Estado == "Libre");
            ViewBag.ParqueaderosDisponibles = new SelectList(lstParqueaderos, "Nomenclatura", "Nomenclatura");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("IdRegistro,ValorPagar,Estado,Nomenclatura,FechaSalida,FechaIngreso,NombreUsuario,Placa")] Gestion Gestion)
        {
            ServicioGestion servicio = new ServicioGestion();
            Gestion objGestion = new Gestion();
            objGestion.IdRegistro = Gestion.IdRegistro;
            objGestion.ValorPagar = 0;
            objGestion.Estado = "Pendiente";
            objGestion.Nomenclatura = Gestion.Nomenclatura;
            objGestion.FechaSalida = System.DateTime.Now;
            objGestion.FechaIngreso = System.DateTime.Now;
            objGestion.NombreUsuario = "moni123";
            objGestion.Placa = Gestion.Placa;
            await servicio.AtualizarGestion(objGestion);

            ServicioAdministracionParqueadero servicioParqueadero = new ServicioAdministracionParqueadero();
            Parqueadero objParqueadero = servicioParqueadero.ObtenerParqueadero(Gestion.Nomenclatura).Result;;
            objParqueadero.Estado = "Ocupado";
            await servicioParqueadero.AtualizarParqueadero(objParqueadero);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int IdRegistro)
        {
            ServicioGestion servicio = new ServicioGestion();
            Gestion Gestion = servicio.ObtenerGestion(IdRegistro).Result;
            return View(Gestion);
        }
        public async Task<IActionResult> Eliminar(int IdRegistro)
        {
            ServicioGestion servicio = new ServicioGestion();
            Gestion Gestion = servicio.ObtenerGestion(IdRegistro).Result;
            await servicio.EliminarGestion(IdRegistro);

            ServicioAdministracionParqueadero servicioParqueadero = new ServicioAdministracionParqueadero();
            Parqueadero objParqueadero = servicioParqueadero.ObtenerParqueadero(Gestion.Nomenclatura).Result; ;
            objParqueadero.Estado = "Libre";
            await servicioParqueadero.AtualizarParqueadero(objParqueadero);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}