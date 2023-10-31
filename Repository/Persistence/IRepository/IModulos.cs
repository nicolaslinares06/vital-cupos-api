using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.IRepository
{
    public interface IModulos
    {
        public Responses ConsultarTodos(ClaimsIdentity identity, string ipAddress);
        public Responses ConsultarRol(ClaimsIdentity identity, string ipAddress);
    }
}