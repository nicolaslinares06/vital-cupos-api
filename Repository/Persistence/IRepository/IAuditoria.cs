using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.IRepository
{
    public interface IAuditoria
    {
        public Responses Consultar(ClaimsIdentity identity, DateTime fechaInicio, DateTime fechaFinal, string ipAddress, int? pagina);

        public Responses ConsultarDetalle(ClaimsIdentity identity, DateTime fecha, string ipAddress);
    }
}
