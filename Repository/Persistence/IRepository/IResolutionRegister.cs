using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Persistence.IRepository
{
    public interface IResolutionRegister
    {
        /// <summary>
        /// consulta datos de la entidad o empresa
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public Responses ConsultEntityDates(ClaimsIdentity identity, decimal documentType, string nitBussines, decimal? entityType);

        /// <summary>
        /// consulta resolucion cupos
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        public Responses ConsultQuotas(ClaimsIdentity identity, string nitBussines, string ipAddress);

        /// <summary>
        /// consulta una resolucion por el numero de resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ResolutionNumbre"></param>
        /// <returns></returns>
        public Responses SearchQuotas(ClaimsIdentity identity, decimal? ResolutionNumbre, string ipAddress);

        /// <summary>
        /// consultar inventario
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultInventory(ClaimsIdentity identity, string ipAddress);

        /// <summary>
        /// consultar tipos de marcaje
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEntityTypes(ClaimsIdentity identity);
        /// <summary>
        /// consultar tipos de marcaje
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultMarkingType(ClaimsIdentity identity);

        /// <summary>
        /// consultar pago de repoblacion
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultRepoblationPay(ClaimsIdentity identity);

        /// <summary>
        /// consultar tipos de especimenes
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEspecimensTypes(ClaimsIdentity identity);

        /// <summary>
        /// consultar un cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        public Responses ConsultOneQuota(ClaimsIdentity identity, decimal quotaCode, string ipAddress);

        /// <summary>
        /// editar resolucion cupo 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public Responses EditDeleteResolutionQuota(ClaimsIdentity identity, SaveResolutionQuotas datas, string ipAddress);

        /// <summary>
        /// guardar nueva resolucion cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public Responses saveResolutionQuota(ClaimsIdentity identity, SaveResolutionQuotas datas, string ipAddress);

        /// <summary>
        /// deshabilitar resolucion cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        public Responses DisableResolution(ClaimsIdentity identity, decimal quotaCode, string ipAddress);
    }
}
