using Repository.Helpers;
using Repository.Helpers.Models;
using System;
using System.Security.Claims;

namespace Repository.Persistence.IRepository
{
    public interface ISolicitudAsignacion
    {
        public Responses Consultar(ClaimsIdentity identity, string nombreUsuario, string ipAddress);
        public Responses Actualizar(ClaimsIdentity identity, ReqAssignment usuario, string ipAddress);
        public Responses ActualizarEstado(ClaimsIdentity identity, ReqAssignmentUpdate solicitud, string ipAddress);
        public Responses Eliminar(ClaimsIdentity identity, ReqId solicitud, string ipAddress);
        public Responses Assign(ClaimsIdentity identity, VitalReq solicitud, string ipAddress);
        public Responses VerificarDocumento(ClaimsIdentity identity, string documento, string ipAddress);
    }
}
