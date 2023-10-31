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
    public interface IParametricas
    {
        public Responses Consultar(ClaimsIdentity identity, string tabla, string ipAddress);
        public Responses Crear(ClaimsIdentity identity, ReqParametric parametrica, string ipAddress);
        public Responses Actualizar(ClaimsIdentity identity, ReqParametric parametrica, string ipAddress);
        public Responses ConsultarTipoDocumento(ClaimsIdentity identity, string ipAddress);
        public Responses ConsultarParametrica(ClaimsIdentity identity, string parametrica, string ipAddress);
        public Responses ConsultarPaises(ClaimsIdentity identity, string ipAddress);
        public Responses ConsultarDepartamentos(ClaimsIdentity identity, int idpais, string ipAddress);
        public Responses ConsultarCiudades(ClaimsIdentity identity, int iddepartamento, string ipAddress);
        public Responses ConsultarEstadoCertificado(ClaimsIdentity identity, string ipAddress);
        public Responses ConsultarDependencia(ClaimsIdentity identity, string ipAddress);
    }
}
