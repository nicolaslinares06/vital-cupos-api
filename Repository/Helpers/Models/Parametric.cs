using API.Helpers;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Utilities.Net;
using Repository.Configurations;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Helpers.Models
{
    public class ReqParametric
    {
        public int? code { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
        public string? description { get; set; }
        public decimal? estate { get; set; }
    }

    public class ReqIdParametric
    {
        public decimal id { get; set; }
    }

    public class Metodos
    {
        private readonly DBContext _context;
        private readonly LogManager logManager;

        public Metodos(DBContext context)
        {
            this._context = context;
            this.logManager = new LogManager(context);
        }
        public string GuardarArchivoFtp(SupportDocuments documento)
        {
            try
            {
                var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
                string urlAdjjunto = "";
                string Puerto = "";
                string usuraio = "";
                string clave = "";
                foreach (var c in query1)
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

                string eliminar = "data:" + documento.attachmentType + ";base64,";
                string SinData = documento.base64Attachment != null ? documento.base64Attachment.Replace(eliminar, String.Empty) : "";

                byte[] buffer = Convert.FromBase64String(SinData);

                bool esPdf = documento.attachmentName?.Contains(".pdf", StringComparison.CurrentCultureIgnoreCase) ?? false;
                bool esDocx = documento.attachmentName?.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                bool esJpeg = documento.attachmentName?.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                bool esPng = documento.attachmentName?.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                bool esJpg = documento.attachmentName?.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                bool esXlsx = documento.attachmentName?.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                string tipoAdjun = "";

                if (esPdf)
                {
                    tipoAdjun = ".pdf";
                }
                else if (esDocx)
                {
                    tipoAdjun = ".docx";
                }
                else if (esJpeg)
                {
                    tipoAdjun = ".jpeg";
                }
                else if (esPng)
                {
                    tipoAdjun = ".png";
                }
                else if (esJpg)
                {
                    tipoAdjun = ".jpg";
                }
                else if (esXlsx)
                {
                    tipoAdjun = ".xlsx";
                }

                var localDate = DateTime.Now;
                Random random = new Random();
                int numero = random.Next(100, 1000);

                string uri = "ftp://" + urlAdjjunto + ":" + Puerto + "/CUPOS/docs/doc" + localDate.Day.ToString() + localDate.Month.ToString() + localDate.Year.ToString() + localDate.Hour.ToString() + localDate.Minute.ToString() + localDate.Second.ToString() + numero + tipoAdjun;

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
            catch (Exception ex)
            {
                // Registra o notifica cualquier otra excepción
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return "";
            }

           
        }

        [ExcludeFromCodeCoverage]
        public  SupportDocuments CargarArchivoFtp(AdmintT009Documento doc, string usuraio, string clave)
        {
            try
            {

                bool esPdf = doc.A009url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                bool esDocx = doc.A009url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpeg = doc.A009url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esPng = doc.A009url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpg = doc.A009url.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esXlsx = doc.A009url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

#pragma warning disable SYSLIB0014
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(doc.A009url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(usuraio, clave);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                var base64 = ConvertToBase64(responseStream);

                string tipoAdjun = "";

                if (esPdf)
                {
                    tipoAdjun = "application/pdf";
                }
                else if (esDocx)
                {
                    tipoAdjun = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                }
                else if (esJpeg)
                {
                    tipoAdjun = "image/jpeg";
                }
                else if (esPng)
                {
                    tipoAdjun = "image/png";
                }
                else if (esJpg)
                {
                    tipoAdjun = "image/jpg";
                }
                else if (esXlsx)
                {
                    tipoAdjun = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }

                SupportDocuments docSoporte = new SupportDocuments();
                docSoporte.code = doc.PkT009codigo;
                docSoporte.base64Attachment = "data:" + tipoAdjun + ";base64," + base64;
                docSoporte.attachmentName = doc.A009documento;
                docSoporte.attachmentType = tipoAdjun;

                return docSoporte;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new SupportDocuments();
            }

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

        /// <summary>
        /// agregar cupos en lista
        /// </summary>
        /// <param name="quota"></param>
        /// <param name="inicial"></param>
        /// <param name="final"></param>
        /// <param name="inicialRepoblacion"></param>
        /// <param name="finalRepoblacion"></param>
        /// <returns></returns>
        public Quota addQuotaToList(CuposV001ResolucionCupos quota, decimal? inicial, decimal? final, decimal? inicialRepoblacion, decimal? finalRepoblacion)
        {
            Quota quotas = new Quota();
            quotas.Code = quota.codigoCupo;
            quotas.NumberResolution = quota.numeroResolucion;
            quotas.QuotasGrant = quota.cuposOtorgados;
            quotas.QuotasAdvantageCommercialization = Convert.ToDecimal(quota.cuposAprovechamientoComercializacion);
            quotas.QuotasRePoblation = quota.cuotaRepoblacion;
            quotas.QuotasAvailable = quota.cuposDisponibles;
            quotas.QuotasSold = quota.cuposDisponibles;
            quotas.YearProduction = decimal.Parse(quota.fechaProduccion.Year.ToString());
            quotas.SpeciesCode = Convert.ToDecimal(quota.codigoEspecie);
            quotas.SpeciesName = quota.NombreEspecieExportar;
            quotas.InitialNumeration = inicial;
            quotas.FinalNumeration = final;
            quotas.InitialNumerationRePoblation = inicialRepoblacion;
            quotas.FinalNumerationRePoblation = finalRepoblacion;

            return quotas;
        }

        /// <summary>
        /// agrega alista de inventario
        /// </summary>
        /// <param name="quota"></param>
        /// <param name="saleDocument"></param>
        /// <param name="companySeller"></param>
        /// <param name="quotaInvoice"></param>
        /// <param name="specieExport"></param>
        /// <param name="inicial"></param>
        /// <param name="final"></param>
        /// <param name="inicialRepoblacion"></param>
        /// <param name="finalRepoblacion"></param>
        /// <returns></returns>
        public Inventory addInventoryToList(CupostT002Cupo quota, CupostT004FacturacompraCartaventum saleDocument, CupostT001Empresa companySeller, CupostT026FacturaCompraCupo quotaInvoice, CupostT005Especieaexportar specieExport, decimal? inicial =null, decimal? final=null, decimal? inicialRepobalcion=null, decimal? finalRepoblacion = null)
        {
            Inventory inventory = new Inventory();
            inventory.quotaCode = quota.PkT002codigo;
            inventory.Code = quotaInvoice.Pk_T026Codigo;
            inventory.NumberSaleCarte = saleDocument.A004numeroCartaVenta;
            inventory.ReasonSocial = companySeller.A001nombre;
            inventory.SaleDate = saleDocument.A004fechaVenta;
            inventory.AvailabilityInventory = quotaInvoice.A026CantidadEspecimenesComprados;
            inventory.Year = quota.A002fechaProduccion != null ? Convert.ToString(quota.A002fechaProduccion.Value.Year) : "";
            inventory.SpeciesCode = Convert.ToDecimal(specieExport.A005codigoEspecie);
            inventory.SpeciesName = specieExport.A005nombreEspecie;
            inventory.AvailableInventory = quotaInvoice.A026CuposDisponibles;
            inventory.InitialNumerationSeal = quotaInvoice.A026NumeracionInicialPrecintos;
            inventory.FinalNumerationSeal = quotaInvoice.A026NumeracionFinalPrecintos;

            if(inicial!=null && final!=null && inicialRepobalcion !=null && finalRepoblacion != null)
            {
                inventory.InitialNumeration = inicial;
                inventory.FinalNumeration = final;
                inventory.InitialNumerationRePoblation = inicialRepobalcion;
                inventory.FinalNumerationRePoblation = finalRepoblacion;
                return inventory;
            }

            inventory.InitialNumeration = quotaInvoice.A026NumeracionInicial;
            inventory.FinalNumeration = quotaInvoice.A026NumeracionFinal;
            inventory.InitialNumerationRePoblation = quotaInvoice.A026NumeracionInicialRepoblacion;
            inventory.FinalNumerationRePoblation = quotaInvoice.A026NumeracionFinalRepoblacion;

            return inventory;
        }
    
        /// <summary>
        /// Auditoria
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="valorAnterior"></param>
        /// <param name="valorActual"></param>
        /// <param name="campos"></param>
        /// <param name="accion"></param>
        public void Auditoria(string ipAddress, int codigoUsuario,string moduloMs, List<string>? valorAnterior, List<string>? valorActual, List<string>? campos, int accion, object? eliminadoCreado, string? eliminadoCreadoCampo, bool esDocConsulta=false)
        {
            var modulo = _context.AdmintT010Modulos.Where(s => s.A010modulo == ModuleManager.smOtorgamientoCupos).FirstOrDefault();
            var rolesRl = _context.AdmintT015RlUsuarioRols.Where(p => p.A015codigoUsuario == codigoUsuario).FirstOrDefault();
            string nombresRoles = "";

            if (rolesRl != null)
            {
                var roles = rolesRl.A015codigoRol.Split("|");
                StringBuilder sb = new StringBuilder();
                foreach (var role in roles)
                {
                    var rl = _context.AdmintT011Rols.Where(p => p.PkT011codigo == Convert.ToInt32(role)).FirstOrDefault();
                    if(rl != null)
                        sb.Append(rl.A011nombre + ", ");
                }
                nombresRoles = sb.ToString();
                if(modulo != null)
                    nombresRoles += modulo.A010descripcion;
            }
            else
            {
                nombresRoles += "";
            }

            
            if (accion == 1)
            {
                if (esDocConsulta)
                {
                    logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), accion, moduloMs, "", "", "", "", "doc");
                }
                else
                {
                    logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), accion, moduloMs, "", "", "", "", "");
                }
            }
            if (accion==2){
                logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), accion, moduloMs, "", "", "", eliminadoCreado != null ? eliminadoCreado : "", eliminadoCreadoCampo != null ? eliminadoCreadoCampo : "");
            }
            else if (accion == 3)
            {
                logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), accion, moduloMs, valorAnterior != null ? valorAnterior.ToArray() : "", valorActual == null ? "" : valorActual.ToArray(), campos == null ? "" : campos.ToArray(), "", nombresRoles);
                _ = _context.SaveChanges();
            }else if(accion == 4)
            {
                logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), accion, moduloMs, "", "", "", eliminadoCreado != null ? eliminadoCreado : "", eliminadoCreadoCampo != null ? eliminadoCreadoCampo : "");
                _ = _context.SaveChanges();
            }
        }

        public void validarCampoEditadoAud(List<string>? valorAnterior, List<string>? valorActual, List<string>? campos, string anterior, string nuevo, string nombreCampo)
        {
            if (nuevo != anterior)
            {
                if(valorAnterior != null)
                    valorAnterior.Add(anterior.ToString());
                if(valorActual != null)
                    valorActual.Add(nuevo.ToString());
                if(campos != null)
                    campos.Add(nombreCampo);
            }
        }
    }
}
