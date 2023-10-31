using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.IRepository
{
    public interface IPrecintosMarquillas
    {
        Responses Consultar(ClaimsIdentity identity, string ipAddress, string? tipoDocumento,
            string? fechaInicial, string? numero, string? numeroDocumento, string? fechaFinal, string? color,
            string? nombreEmpresa, string? vigencia, int? pagina);
    }
}
