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
    public interface ICoordinatorAssignRequestAnalystsGpnRepository
    {
        Task<Responses> ActualizarEstadoSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, string tipoEstado);
        Task<Responses> ActualizarIdAnalistaSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, decimal idAnalista);
        Task<Responses> ConsultarAnalistas(ClaimsIdentity identity, decimal codigoSolicitud);
        Task<Responses> ConsultarAnalistaSolicitudAsignado(ClaimsIdentity identity, decimal codigoSolicitud);      
        Task<Responses> ConsultarDatosDesistimientoSolicitud(ClaimsIdentity identity, decimal codigoSolicitud);
        Task<Responses> ConsultarDatosSolicitud(ClaimsIdentity identity, decimal codigoSolicitud);
        Task<Responses> ConsultarDatosSolicitudCupo(ClaimsIdentity identity, decimal codigoCupo);
        Task<Responses> ConsultarDocumentoSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, decimal tipoDocumento);
        Task<Responses> ConsultarDocumentosSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, decimal tipoDocumento);
        Task<Responses> ConsultarNumeracionesSolicitud(ClaimsIdentity identity, decimal codigoSolicitud);
        Task<Responses> ConsultarSolicitudes(ClaimsIdentity identity, string tipoEvaluacion);
        Task<Responses> ConsultarSolicitudesPagination(ClaimsIdentity identity, decimal tipoEvaluacion, PaginatioModels.ParamsPaginations parametrosPaginacion);
        Task<Responses> ConsultarSolicitudesPorFiltro(ClaimsIdentity identity, decimal tipoEvaluacion, PaginatioModels.ParamsPaginations parametrosPaginacion);
        Task<Responses> ConsultarTiposFraccionesSolicitud(ClaimsIdentity identity, decimal codigoSolicitud);
    }
}
