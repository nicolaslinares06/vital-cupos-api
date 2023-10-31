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
    public interface IRoles
    {
        public Responses Consultar(ClaimsIdentity identity, int rol, string cargo, bool estado, string ipAddress);
        public Responses ConsultarRoles(ClaimsIdentity identity, string ipAddress);
        public Responses ConsultarRolesAsignar(ClaimsIdentity identity, string ipAddress);
        public Responses ActualizarFuncionalidades(ClaimsIdentity identity, RolModPermition permisos, string ipAddress);
        public Responses Crear(ClaimsIdentity identity, ReqRol rol, string ipAddress);
        public Responses Actualizar(ClaimsIdentity identity, ReqRol rol, string ipAddress);
        public Responses ConsultarTodos(ClaimsIdentity identity, string ipAddress, string? cadenaBusqueda, string? estado);
        public Responses ConsultarTodosRoles(ClaimsIdentity identity, string ipAddress, string parametro);
    }
}
