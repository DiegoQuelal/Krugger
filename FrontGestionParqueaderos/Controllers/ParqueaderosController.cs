using FrontGestionParqueaderos.Models;
using FrontGestionParqueaderos.Servicio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System.Diagnostics;
namespace FrontGestionParqueaderos.Controllers
{
    public class ParqueaderosController : Controller
    {
        public IActionResult Index(int? Pagina, string FiltroBusqueda)
        {
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            IEnumerable<Parqueadero> lstParqueaderos = servicio.ObtenerParqueaderos().Result;
            if (!string.IsNullOrEmpty(FiltroBusqueda))
                lstParqueaderos = lstParqueaderos.Where(x => x.Estado.Contains(FiltroBusqueda) || x.Nomenclatura.Contains(FiltroBusqueda) || x.Piso.Contains(FiltroBusqueda));
            int? TamanioPagina = 1;
            Pagina = (Pagina ?? 1);
            @ViewData["FitroActual"] = FiltroBusqueda;
            return View(lstParqueaderos.ToPagedList(Pagina.Value,TamanioPagina.Value));
        }

        public IActionResult Edit(string Nomenclatura)
        {
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            Parqueadero Parqueadero = servicio.ObtenerParqueadero(Nomenclatura).Result;
            ViewBag.Estados = new SelectList(ListaEstados.Estados,"Value","Text");
            return View(Parqueadero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Nomenclatura,Piso,Estado")] Parqueadero Parqueadero)
        {
            Parqueadero objParqueadero = new Parqueadero();
            objParqueadero.Estado = Parqueadero.Estado;
            objParqueadero.Nomenclatura = Parqueadero.Nomenclatura;
            objParqueadero.Piso = Parqueadero.Piso;
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            await servicio.AtualizarParqueadero(objParqueadero);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            ViewBag.Estados = new SelectList(ListaEstados.Estados, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Nomenclatura,Piso,Estado")] Parqueadero Parqueadero)
        {
            Parqueadero objParqueadero = new Parqueadero();
            objParqueadero.Estado = Parqueadero.Estado;
            objParqueadero.Nomenclatura = Parqueadero.Nomenclatura;
            objParqueadero.Piso = Parqueadero.Piso;
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            await servicio.AtualizarParqueadero(objParqueadero);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string Nomenclatura)
        {
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            Parqueadero Parqueadero = servicio.ObtenerParqueadero(Nomenclatura).Result;
            return View(Parqueadero);
        }
        public async Task<IActionResult> Eliminar(string Nomenclatura)
        {
            ServicioAdministracionParqueadero servicio = new ServicioAdministracionParqueadero();
            await servicio.EliminarParqueadero(Nomenclatura);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}