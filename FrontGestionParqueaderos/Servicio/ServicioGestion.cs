using FrontGestionParqueaderos.Models;
using Newtonsoft.Json;

namespace FrontGestionParqueaderos.Servicio
{
    public class ServicioGestion
    {
        public async Task<IEnumerable<Gestion>> ObtenerGestion()
        {
            IEnumerable<Gestion> Gestion;
            var url = "https://localhost:7082/Gestion/Gestion";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetStringAsync(url);
                Gestion = JsonConvert.DeserializeObject<IEnumerable<Gestion>>(respuesta);
            }
            return Gestion;
        }
        public async Task<Gestion> ObtenerGestion(int IdRegistro)
        {
            Gestion Gestion;
            var url = "https://localhost:7082/Gestion/Gestion/" + IdRegistro;
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetStringAsync(url);
                Gestion = JsonConvert.DeserializeObject<Gestion>(respuesta);
            }
            return Gestion;
        }
        public async Task AtualizarGestion(Gestion Gestion)
        {
            var url = "https://localhost:7082/Gestion/Gestion/";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.PostAsJsonAsync(url, Gestion);
            }
        }
        public async Task EliminarGestion(int IdRegistro)
        {
            var url = "https://localhost:7082/Gestion/Gestion?IdRegistro=" + IdRegistro;
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.DeleteAsync(url);
            }
        }
    }
}
