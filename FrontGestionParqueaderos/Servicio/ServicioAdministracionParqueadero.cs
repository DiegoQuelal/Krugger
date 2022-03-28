using Newtonsoft.Json;
using FrontGestionParqueaderos.Models;
using System.Text;

namespace FrontGestionParqueaderos.Servicio
{
    public class ServicioAdministracionParqueadero
    {
        public async Task<IEnumerable<Parqueadero>> ObtenerParqueaderos()
        {
            IEnumerable<Parqueadero> Parqueaderos;
            var url = "https://localhost:7082/Parqueaderos/Parqueadero";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetStringAsync(url);
                Parqueaderos = JsonConvert.DeserializeObject<IEnumerable<Parqueadero>>(respuesta);
            }
            return Parqueaderos;
        }
        public async Task<Parqueadero> ObtenerParqueadero(string Nomenclatura)
        {
            Parqueadero Parqueaderos;
            var url = "https://localhost:7082/Parqueaderos/Parqueadero/" + Nomenclatura;
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetStringAsync(url);
                Parqueaderos = JsonConvert.DeserializeObject<Parqueadero>(respuesta);
            }
            return Parqueaderos;
        }
        public async Task AtualizarParqueadero(Parqueadero Parqueadero)
        {
            var url = "https://localhost:7082/Parqueaderos/Parqueadero/";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.PostAsJsonAsync(url, Parqueadero);
            }
        }
        public async Task EliminarParqueadero(string Nomenclatura)
        {
            var url = "https://localhost:7082/Parqueaderos/Parqueadero?Nomenclatura=" + Nomenclatura;
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.DeleteAsync(url);
            }
        }
    }
}
