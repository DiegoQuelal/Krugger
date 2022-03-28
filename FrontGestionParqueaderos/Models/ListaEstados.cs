using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontGestionParqueaderos.Models
{
    static class ListaEstados
    {
        public static List<SelectListItem> Estados
        {
            get
            {
                List<SelectListItem> lstEstados = new()
                {
                    new SelectListItem { Value = "Libre", Text = "Libre" },
                    new SelectListItem { Value = "Ocupado", Text = "Ocupado" },
                    new SelectListItem { Value = "Reservado", Text = "Reservado" },
                    new SelectListItem { Value = "Mal Estado", Text = "Mal Estado" }
                };
                return lstEstados;
            }
        }
    }
}
