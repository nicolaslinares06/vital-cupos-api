using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.IRepository
{
    public interface IGestionPrencintosNacionales
    {
        Responses GetNumbersSeals(ClaimsIdentity identity, string ipAddress, string tipoSolicitud);
        Task<Responses> GetTypesFractionsRequest(ClaimsIdentity identity, decimal requestCode);
    }
}
