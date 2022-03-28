using Microsoft.AspNetCore.Mvc;
using SistemaParqueaderos.AccesoDatos;
using SistemaParqueaderos.Modelos;

namespace SistemaParqueaderos.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AdministracionParqueaderosDBContext context;
        public UsuariosController(AdministracionParqueaderosDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("Usuarios")]
        public Usuario Get(string NombreUsuario, string Clave)
        {
            var Usuario = context.Usuarios.FirstOrDefault(x => x.NombreUsuario == NombreUsuario && x.Clave== Clave);
            if (Usuario == null)
                Usuario =new Usuario();

            return Usuario;
        }
        [HttpPost]
        [Route("Usuarios")]
        public void Post(Usuario Usuario)
        {
            context.Add(Usuario);
            context.SaveChanges();
        }
    }
}
