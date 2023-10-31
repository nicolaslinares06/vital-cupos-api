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
    public interface IReportesPrecintosRepository
    {
        Task<Responses> ConsultarDatosPrecintos(ClaimsIdentity identity, ReportesPrecintosModels.SealFilters filtros);
        Task<Responses> ConsultarEstablecimientos(ClaimsIdentity identity);
    }
}
