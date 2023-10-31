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
    public interface IQuotasReports
    {
        /// <summary>
        /// consultar resoluciones
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resolutionNumber"></param>
        /// <param name="BussinesName"></param>
        /// <param name="BussinesNit"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Responses ConsultResolutions(ClaimsIdentity identity, string? resolutionNumber, string? BussinesName, string? BussinesNit, string? fromDate, string? toDate);
       
    }
}
