
using Repository.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Web.Models;

namespace Repository.Helpers
{
    public class DocumentManager
    {
        private readonly DBContext _context;

        public DocumentManager(DBContext context)
        {
            this._context = context;
        }

        public string GuardarArchivoFTP(Archivo documento)
        {
            var query = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            string urlAdjjunto = "";
            string Puerto = "";
            string usuraio = "";
            string clave = "";
            foreach (var c in query)
            {
                switch (c.A008descripcion)
                {
                    case "URL":
                        urlAdjjunto = c.A008valor;
                        break;
                    case "PUERTO":
                        Puerto = c.A008valor;
                        break;
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }
            Stream? requestStream = null;

            string eliminar = "data:" + documento.tipoAdjunto + ";base64,";

            if(documento.adjuntoBase64 != null)
            {
                string SinData = documento.adjuntoBase64.Replace(eliminar, String.Empty);

                byte[] buffer = Convert.FromBase64String(SinData);

                string uri = "ftp://" + urlAdjjunto + ":" + Puerto + "/CUPOS/docs/" + documento.nombreAdjunto;

                #pragma warning disable SYSLIB0014
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.ContentLength = buffer.Length;
                request.EnableSsl = false;
                request.Credentials = new NetworkCredential(usuraio, clave);

                requestStream = request.GetRequestStream();

                requestStream.Write(buffer, 0, buffer.Length);

                #pragma warning disable S2589
                if (requestStream != null)
                    requestStream.Close();

                return uri;
            }

            return "";
        }

        [ExcludeFromCodeCoverage]
        public string ConvertirArchivoToBase64(string urlFTP)
        {
            var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            string usuraio = "";
            string clave = "";
            foreach (var c in query1)
            {
                switch (c.A008descripcion)
                {
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }

            #pragma warning disable SYSLIB0014
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlFTP);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(usuraio, clave);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            var base64 = ConvertToBase64(responseStream);
        
            return "data:" + getTipoData(urlFTP) + ";base64," + base64;
        }

        public string ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public AdmintT009Documento GuardarDocumento(string ipAddress, decimal codigoUsuario, string nombreAdjunto, string uri){
            AdmintT009Documento docNuevo = new AdmintT009Documento
            {
                A009fechaCreacion = DateTime.Now,
                A009codigoUsuarioCreacion = codigoUsuario,
                A009estadoRegistro = StringHelper.estadoActivo,
                A009codigoParametricaTipoDocumento = StringHelper.tipoDocumentoAdjuntoOtro,
                A009firmaDigital = "firma",
                A009codigoPlantilla = 1,
                A009documento = nombreAdjunto,
                A009descripcion = nombreAdjunto,
                A009url = uri
            };

            _context.AdmintT009Documentos.Add(docNuevo);
            _context.SaveChanges();
            
            return docNuevo;
        }

        public String getTipoData(String url)
        {
            bool esPdf = url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
            bool esDocx = url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
            bool esJpeg = url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
            bool esPng = url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);
            bool esJpg = url.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase);
            bool esXlsx = url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

            string dataAdjun = "application/pdf";

            if (esPdf)
            {
                dataAdjun = "application/pdf";
            }
            else if (esDocx)
            {
                dataAdjun = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else if (esJpeg)
            {
                dataAdjun = "image/jpeg";
            }
            else if (esPng)
            {
                dataAdjun = "image/png";
            }
            else if (esJpg)
            {
                dataAdjun = "image/jpg";
            }
            else if (esXlsx)
            {
                dataAdjun = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            return dataAdjun;
        }
    }

    public class Archivo
    {
        public decimal? codigo { get; set; }
        public string? adjuntoBase64 { get; set; }
        public string? nombreAdjunto { get; set; }
        public string? tipoAdjunto { get; set; }
        public string? urlFTP { get; set; }
    }
}
