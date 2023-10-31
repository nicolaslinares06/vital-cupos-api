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
    public interface IGestionDocumental
    {
        public Responses ConsultarDocumentos(ClaimsIdentity identity, string ipAddress, string? cadenaBusqueda);
        public Responses GetFile(ClaimsIdentity identity, string ipAddress, decimal id);
        public Responses SaveDocument(ClaimsIdentity identity, SaveDocumentRequest documento);
        public Responses updateDocument(ClaimsIdentity identity, string ipAddress, UpdateDocument datosDoc);
    }
}
