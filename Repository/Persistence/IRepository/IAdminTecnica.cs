using Repository.Helpers;
using Repository.Helpers.Models;
using System.Security.Claims;

namespace Repository.Persistence.IRepository
{
    public interface IAdminTecnica
    {
        public Responses Consultar(ClaimsIdentity identity, string? valor, string ipAddress);
        public Responses Listar(ClaimsIdentity identity, string ipAddress);
        public Responses Actualizar(ClaimsIdentity identity, TechnicalAdminReq req, string ipAddress);
        public Responses Eliminar(ClaimsIdentity identity, ReqId req, string ipAddress);
        public Responses ConsultarValoresTecnicos(ClaimsIdentity identity, string ipAddress, string parametro);
    }
}