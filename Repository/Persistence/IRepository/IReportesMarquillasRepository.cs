using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ReportesMarquillasModels;

namespace Repository.Persistence.IRepository
{
    public interface IReportesMarquillasRepository
    {
        Responses ConsultarMarquillas(ClaimsIdentity identity, TagsFilters filtros);
    }
}
