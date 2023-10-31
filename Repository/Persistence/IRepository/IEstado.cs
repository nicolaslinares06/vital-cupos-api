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
    public interface IEstado
    {
        public Responses Consultar(ClaimsIdentity identity, string? nombre, string? estadoReg, int? codigoEstado, string ipAddress);
        public Responses Actualizar(ClaimsIdentity identity, ReqEstado estado, string ipAddress);
        public Responses Eliminar(ClaimsIdentity identity, ReqId estado, string ipAddress);
        public Responses Crear(ClaimsIdentity identity, AdminStatesReq req, string ipAddress);
        public Responses ConsultarEstado(ClaimsIdentity identity, string ipAddress, string parametro);
    }
}
