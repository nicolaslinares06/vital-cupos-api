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
    public interface IReportesEmpresasMarcaje
    {
        Responses ConsultarDatosReportes(ClaimsIdentity identity);
        Task<Responses> ConsultarDatosEmpresas(ClaimsIdentity identity, ReportesEmpresasMarcajeModels.BusinessFilters filtros);
        Task<Responses> ConsultarEstablecimientosPorTipo(ClaimsIdentity identity, decimal tipoEstablecimiento);
    }
}
