using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using static Repository.Helpers.Models.PermitResolution;

namespace Repository.Persistence.IRepository
{
    public interface IControlFishRequest
    {
        /// <summary>
        /// consultar datos de entidad
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        public Responses ConsultEntityDates(ClaimsIdentity identity, decimal documentType, decimal nitBussines);

        /// <summary>
        /// consultar resoluciones por empresa
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeBussines"></param>
        /// <returns></returns>
        public Responses ConsultPermitsReslution(ClaimsIdentity identity,decimal codeBussines);

        /// <summary>
        /// consultar resolucion por codigo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeReslution"></param>
        /// <returns></returns>
        public Responses ConsultOnePermitResolution(ClaimsIdentity identity, decimal codeReslution);

        /// <summary>
        /// guardar nueva resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public Responses SaveResolution(ClaimsIdentity identity, ResolucionPermisos resolution);

        /// <summary>
        /// editar resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public Responses EditResolution(ClaimsIdentity identity, ResolucionPermisos resolution);

        /// <summary>
        /// deshabiliar resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeResolution"></param>
        /// <returns></returns>
        public Responses DeleteResolution(ClaimsIdentity identity, decimal codeResolution);
    }
}
