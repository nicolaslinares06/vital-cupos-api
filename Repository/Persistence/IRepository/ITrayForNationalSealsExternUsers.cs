using Repository.Helpers;
using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace Repository.Persistence.IRepository
{
    public interface ITrayForNationalSealsExternUsers
    {
        /// <summary>
        /// consulta tipos solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultRequestTypes(ClaimsIdentity identity);

        /// <summary>
        /// consultar empresas
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultBussiness(ClaimsIdentity identity);

        /// <summary>
        /// consultar empresa y representante legar ligados a el usuario externo
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultBussinesAndLegalRepresentant(ClaimsIdentity identity, decimal codeBussines);

        /// <summary>
        /// consultar ciudades
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultCities(ClaimsIdentity identity);

        /// <summary>
        /// guardar solicitud nueva
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Responses RegisterRequest(ClaimsIdentity identity, Requests request, string ipAddress);

        /// <summary>
        /// consultar solicitudes radicadas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultRegisteredRecuest(ClaimsIdentity identity, decimal codigoEmpresa);

        /// <summary>
        /// consultar solicitudes en requerimeinto
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultRequirements(ClaimsIdentity identity, decimal codigoEmpresa);

        /// <summary>
        /// consultar solicitudes aprobadas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultApproved(ClaimsIdentity identity, decimal codigoEmpresa);

        /// <summary>
        /// consultar solicitudes desistidas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultDesisted(ClaimsIdentity identity, decimal codigoEmpresa);

        /// <summary>
        /// radicar solicitud pendiente 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="solicitudPendiente"></param>
        /// <returns></returns>
        public Responses RegisterPending(ClaimsIdentity identity, RegisterPending solicitudPendiente);

        /// <summary>
        /// consultar una solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoSolicitud"></param>
        /// <returns></returns>
        public Responses ConsultOnePendientRegister(ClaimsIdentity identity, decimal codigoSolicitud);

        /// <summary>
        /// edicion de la solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Responses EditRequest(ClaimsIdentity identity, Requests request, string ipAddress);

        /// <summary>
        /// obtiene cupos
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        Responses GetQuotas(ClaimsIdentity identity, string documentNumber, string especie);

        /// <summary>
        /// obtiene inventario
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        Responses GetInventory(ClaimsIdentity identity, string documentNumber, string especie);

        /// <summary>
        /// Obtiene Ciudades por Departamento
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="idDepartment"></param>
        /// <returns></returns>
        Task<Responses> ConsultCitiesByIdDepartment(ClaimsIdentity identity, decimal idDepartment);

        /// <summary>
        /// Obtiene departamentos
        /// </summary>     
        /// <returns></returns>
        Task<Responses> consultDepartments(ClaimsIdentity identity);

        /// <summary>
        /// Consultar numeraciones solicitudes no disponibles
        /// </summary>     
        /// <returns></returns>
        Responses getNumbersRequest(ClaimsIdentity identity, ConsultUnableNumbersModel data);

        /// <summary>
        /// Valida si estan disponibvles las numeraciones
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        Responses ValidateNumbers(ClaimsIdentity identity, ValidateNumbersModel numbers);

        /// <summary>
        /// Obtener actas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        Responses getActaData(ClaimsIdentity identity, string documentNumber);

        /// <summary>
        /// obtener fracciones
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="cuttingCode"></param>
        /// <returns></returns>
        Responses getFractions(ClaimsIdentity identity, int cuttingCode);

        /// <summary>
        /// obtener salvoconducto
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="reportCode"></param>
        /// <returns></returns>
        Responses getSafeguard(ClaimsIdentity identity, int reportCode);
    }
}
