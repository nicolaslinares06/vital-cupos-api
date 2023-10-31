using Repository.Helpers;
using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.IRepository
{
    public interface IUsuario
    {
        public Responses Consultar(ClaimsIdentity identity, string CorreoElectronico, string ipAddress);
        Responses Actualizar(ClaimsIdentity identity, ReqUser usuario, string ipAddress);
        Responses ConsultarTodos(ClaimsIdentity identity, string ipAddress, string cadenaBusqueda);
        Responses Create(ClaimsIdentity identity, ReqUser usuario, string ipAddress);
        Responses Authenticate(ReqLogin user, string ipAddress);
        Responses CambiaContrasena(ReqChangePassword user, string ipAddress);
        Responses enviaEmailRecuperarContrasena(ReqSimpleUser usuario, string ip);
        Responses ConsultarTerminos(string? login, string ipAddress);
        Responses ConsultarEdit(ClaimsIdentity identity, string ipAddress, decimal id);
    }
}
