using Repository.Helpers;
using Repository.Helpers.Models;
using System.Security.Claims;

namespace Repository.Persistence.IRepository
{
    public interface IEmpresa
    {
        public Responses Actualizar(ClaimsIdentity identity, EntityRequest empresa, string ipAddress);
        public Responses ConsultaNovedades(ClaimsIdentity identity, decimal codigoEmpresa, decimal? idNovedad);
        public Responses RegistroNovedad(ClaimsIdentity identity, NoveltiesRequest novedad, string ipAddress);
        public Responses ElimiarDocumentoNovedad(ClaimsIdentity identity, decimal idNovedad, decimal idArchivo);
        public Responses ConsultarCupos(ClaimsIdentity identity, decimal idEmpresa);
    }
}