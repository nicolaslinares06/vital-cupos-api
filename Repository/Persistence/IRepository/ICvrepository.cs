using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.IRepository
{
    public interface ICvrepository
    {
        public Responses Buscar(ClaimsIdentity identity, decimal documentypecv, string documentid);
        public Responses Situacion(ClaimsIdentity identity, decimal documentypecv, string documentid);
        public Responses Resolucioncupos(ClaimsIdentity identity, string documentid);
        public Responses ConsultCertificateshj(ClaimsIdentity identity);
        public Responses ConsultPeces(ClaimsIdentity identity, decimal documentid);
        public Responses DocumentoVenta(ClaimsIdentity identity, decimal nit);
        public Responses ConsultOneQuota2(ClaimsIdentity identity, decimal quotaCode);
        public Responses ConsultDocument2(ClaimsIdentity identity, decimal docuid);
        public Responses ConsultDocumentid(ClaimsIdentity identity, decimal docuid);
        public Responses ConsultCertificateshj2(ClaimsIdentity identity, decimal idcertificado);
        public Responses ConsultSituacionid(ClaimsIdentity identity,decimal codigoEmpresa, decimal situacionid);
        public Responses ConsultSituacionnovedad(ClaimsIdentity identity,decimal codigoEmpresa);
        public Responses ConsultSituacionnovedadultima(ClaimsIdentity identity,decimal codigoEmpresa);
        public Responses ConsultSituacionpdf(ClaimsIdentity identity, decimal situacionid);
        public Responses Consultpecespdf(ClaimsIdentity identity, decimal idresolucionp);

    }
}
