using API.Helpers;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;
using Web.Models;
using static Repository.Helpers.Models.PermitResolution;

namespace Repository.Persistence.Repository
{
    public class Cvrepository : ICvrepository
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public Cvrepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public Responses Buscar(ClaimsIdentity identity, decimal documentypecv, string documentid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from empresa in _context.CupostT001Empresas
                        join parametrica in _context.AdmintT008Parametricas
                        on empresa.A001codigoParametricaTipoEntidad equals parametrica.PkT008codigo
                        join ciudad in _context.AdmintT004Ciudads
                        on empresa.A001codigoCiudad equals ciudad.PkT004codigo
                        join departamento in _context.AdmintT003Departamentos
                        on ciudad.A004codigoDepartamento equals departamento.PkT003codigo
                        join pais in _context.AdmintT002Pais
                        on departamento.A003codigoPais equals pais.PkT002codigo
                        join parametricaEstado in _context.AdmintT008Parametricas
                        on empresa.A001estadoRegistro equals parametricaEstado.PkT008codigo
                        from persona in _context.CitestT003Personas.DefaultIfEmpty()
                        where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                        where (documentypecv == 95 && empresa.A001nit == Convert.ToDecimal(documentid)) || (persona.A003codigoParametricaTipoIdentificacion == documentypecv && persona.A003identificacion == documentid)
                        select new
                        {
                            codigoEmpresa = empresa.PkT001codigo,
                            idtipoEntidadhj = parametrica.PkT008codigo,
                            nombreEntidadhj = parametrica.A008valor,
                            nombreEmpresahj = empresa.A001nombre,
                            nithj = empresa.A001nit,
                            telefonohj = empresa.A001telefono,
                            correohj = empresa.A001correo,
                            idciudadhj = ciudad.PkT004codigo,
                            ciudadhj = ciudad.A004nombre,
                            iddepartamentohj = departamento.PkT003codigo,
                            departamentohj = departamento.A003nombre,
                            idpaishj = pais.PkT002codigo,
                            paishj = pais.A002nombre,
                            direccionhj = empresa.A001direccion,

                        };



            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }
        public Responses Situacion(ClaimsIdentity identity, decimal documentypecv, string documentid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query2 = from empresa in _context.CupostT001Empresas
                         join parametrica in _context.AdmintT008Parametricas
                         on empresa.A001codigoParametricaTipoEntidad equals parametrica.PkT008codigo
                         join ciudad in _context.AdmintT004Ciudads
                         on empresa.A001codigoCiudad equals ciudad.PkT004codigo
                         join departamento in _context.AdmintT003Departamentos
                         on ciudad.A004codigoDepartamento equals departamento.PkT003codigo
                         join pais in _context.AdmintT002Pais
                         on departamento.A003codigoPais equals pais.PkT002codigo
                         join parametricaEstado in _context.AdmintT008Parametricas
                         on empresa.A001estadoRegistro equals parametricaEstado.PkT008codigo
                         from persona in _context.CitestT003Personas.DefaultIfEmpty()
                         where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                         where (documentypecv == 95 && empresa.A001nit == Convert.ToDecimal(documentid)) || (persona.A003codigoParametricaTipoIdentificacion == documentypecv && persona.A003identificacion == documentid)
                         select new
                         {


                             id = empresa.PkT001codigo,
                             nitsitu = empresa.A001nit,
                             nombresitu = empresa.A001nombre,
                             empresasitu = parametrica.A008valor,
                             estadositu = parametricaEstado.A008valor
                         };

            return ResponseManager.generaRespuestaGenerica("", query2, token, false);

        }
        public Responses Resolucioncupos(ClaimsIdentity identity, string documentid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var bs = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == documentid).FirstOrDefault();

            if(bs != null)
            {
                var query = from cupos in _context.CuposV001ResolucionCupos
                            select cupos;

                var qry = from cupos in query
                          where cupos.codigoEmpresa == bs.PkT001codigo
                          select new
                          {
                              cupos.codigoCupo,
                              cupos.autoridadEmiteResolucion,
                              cupos.codigoZoocriadero,
                              cupos.numeroResolucion,
                              cupos.fechaResolucion,
                              cupos.fechaRegistroResolucion,
                              cupos.fechaRadicado,
                              cupos.cuposOtorgados,
                              cupos.cuposPorAnio,
                              cupos.cuposTotal,
                              cupos.fechaProduccion,
                              cupos.cuposAprovechamientoComercializacion,
                              cupos.cuotaRepoblacion,
                              cupos.cuposDisponibles,
                              cupos.codigoEspecie,
                              cupos.numeroInternoFinalCuotaRepoblacion,
                              cupos.numeroInternoFinal
                          };
                return ResponseManager.generaRespuestaGenerica("", qry, token, false);
            }

            return ResponseManager.generaRespuestaGenerica("", "", token, false);
        }
        public Responses ConsultCertificateshj(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var count = (from a in _context.CitestT001Certificados

                         select a).Count();
            int count2 = count;

            var queryyy = from certificates in _context.CupostT021CertificadoFloraNoMaderable
                          join empresa in _context.CupostT001Empresas
                          on certificates.A021CodigoEmpresa equals empresa.PkT001codigo
                          where certificates.A021TipoCertificado == StringHelper.tipoCetificadosFloraNoMaderable
                          where certificates.A021EstadoRegistro == StringHelper.estadoActivo
                          select new
                          {
                              codigoCertificado = certificates.Pk_T021Codigo,
                              numeroCertificacion = certificates.A021NumeroCertificado,
                              fechaCertificacion = certificates.A021FechaCertificacion,
                              nit = empresa.A001nit,
                              vigenciaCertificacion = certificates.A021VigenciaCertificacion,
                              totalcertificados = count2.ToString()
                          };


            return ResponseManager.generaRespuestaGenerica("", queryyy, token, false);

        }
        public Responses ConsultPeces(ClaimsIdentity identity, decimal documentid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var resoluciones = _context.CupostT014Resolucion.Where(p => p.A014codigoEmpresa == documentid && p.A014estadoRegistro == StringHelper.estadoActivo).ToList();

            List<ResolucionPermisos> resolucionesPermiso = new List<ResolucionPermisos>();
            foreach (var resolucion in resoluciones)
            {
                resolucionesPermiso.Add(new ResolucionPermisos
                {
                    resolutionCode = resolucion.PkT014codigo,
                    resolutionNumber = resolucion.A014numeroResolucion,
                    resolutionDate = resolucion.A014fechaResolucion,
                    startDate = resolucion.A014fechaInicio,
                    endDate = resolucion.A014fechaFin,
                    resolutionObject = resolucion.A014objetoResolucion
                });
            }


            return ResponseManager.generaRespuestaGenerica("", resolucionesPermiso, token, false);
        }
        public Responses DocumentoVenta(ClaimsIdentity identity, decimal nit)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from saleDocument in _context.CupostT004FacturacompraCartaventa
                        join companySeller in _context.CupostT001Empresas
                        on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                        join companyShopper in _context.CupostT001Empresas
                        on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                        where saleDocument.A004estadoRegistro == StringHelper.estadoActivo 
                              && companySeller.A001nit == nit
                        select new
                        {
                            Code = saleDocument.PkT004codigo,
                            Numeration = saleDocument.PkT004codigo,
                            CarteNumber = saleDocument.A004numeroCartaVenta,
                            NitCompanySeller = companySeller.A001nit,
                            ReasonSocial = companySeller.A001nombre,
                            NitCompanyShopper = companyShopper.A001nit,
                            ReasonSocialShopper = companyShopper.A001nombre,
                            SaleDate = saleDocument.A004fechaVenta,
                            QuotasSold = saleDocument.A004totalCuposVendidos
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        [ExcludeFromCodeCoverage]
        public Responses ConsultDocument2(ClaimsIdentity identity, decimal docuid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var documents = from saleDocumentsAttachments in _context.CupostT025FacturaCompraCartaVentaDocumento
                            join saleDocument in _context.CupostT004FacturacompraCartaventa
                            on saleDocumentsAttachments.A025CodigoFacturaCompraCartaVenta equals saleDocument.PkT004codigo
                            join document in _context.AdmintT009Documentos
                            on saleDocumentsAttachments.A025CodigoDocumento equals document.PkT009codigo
                            where saleDocument.PkT004codigo == docuid
                            where saleDocument.A004estadoRegistro == StringHelper.estadoActivo
                            where document.A009estadoRegistro == StringHelper.estadoActivo
                            select new
                            {
                                document.PkT009codigo,
                                document.A009documento,
                                document.A009url
                            };

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

            List<SupportDocuments> docSoporte = new List<SupportDocuments>();

            foreach (var docs in documents)
            {
                bool esPdf = docs.A009url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                bool esDocx = docs.A009url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpeg = docs.A009url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esPng = docs.A009url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpg = docs.A009url.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esXlsx = docs.A009url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

                #pragma warning disable SYSLIB0014
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(docs.A009url);
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

                docSoporte.Add(new SupportDocuments
                {
                    code = docs.PkT009codigo,
                    base64Attachment = "data:" + tipoAdjun + ";base64," + base64,
                    attachmentName = docs.A009documento,
                    attachmentType = tipoAdjun,
                });
            }

            CertificateData certificadoRetornar = new CertificateData();

            certificadoRetornar.supportingDocuments = docSoporte;

            return ResponseManager.generaRespuestaGenerica("", certificadoRetornar, token, false);

        }

        public Responses ConsultDocumentid(ClaimsIdentity identity, decimal docuid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var documentinfo = from saleDocument in _context.CupostT004FacturacompraCartaventa
                               join companySeller in _context.CupostT001Empresas
                               on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                               join companyShopper in _context.CupostT001Empresas
                               on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                               where saleDocument.PkT004codigo == decimal.Parse(docuid.ToString())
                               && saleDocument.A004estadoRegistro == StringHelper.estadoActivo
                               select new
                               {
                                   Code = saleDocument.PkT004codigo,
                                   Numeration = saleDocument.PkT004codigo,
                                   CarteNumber = saleDocument.A004numeroCartaVenta,
                                   SaleDate = saleDocument.A004fechaVenta,
                                   NumberSold = saleDocument.A004totalCuposVendidos,
                                   BusinessSale = companySeller.A001codigoParametricaTipoEntidad,
                                   TypeDocumentSeller = Convert.ToInt32(StringHelper.tipoDocumentoNit), //QUEMADO SELECT
                                   DocumentNumberSeller = companySeller.A001nit,
                                   ReasonSocial = companySeller.A001nombre,
                                   InitialBalanceBusiness = saleDocument.A004saldoEntidadVendeInicial,
                                   FinalBalanceBusiness = saleDocument.A004saldoEntidadVendeFinal,
                                   Observations = saleDocument.A004observacionesVenta,
                                   InventoryAvailability = saleDocument.A004disponibilidadInventario,
                                   BusinessShopper = companyShopper.A001codigoParametricaTipoEntidad,
                                   TypeDocumentShopper = Convert.ToInt32(StringHelper.tipoDocumentoNit), //QUEMADO SELECT
                                   DocumentNumberShopper = companyShopper.A001nit,
                                   ReasonSocialShopper = companyShopper.A001nombre,
                                   InitialBalanceBusinessShopper = saleDocument.A004saldoEntidadCompraInicial,
                                   FinalBalanceBusinessShopper = saleDocument.A004saldoEntidadCompraFinal,
                                   ObservationsShopper = saleDocument.A004observacionesCompra,
                                   CompanySellerCode = saleDocument.A004codigoEntidadVende,
                                   CompanyShopperCode = saleDocument.A004codigoEntidadCompra,

                               };
            return ResponseManager.generaRespuestaGenerica("", documentinfo, token, false);


        }

        [ExcludeFromCodeCoverage]
        public Responses ConsultOneQuota2(ClaimsIdentity identity, decimal quotaCode)
        {
            try
            {
                var token = jwtAuthenticationManager.generarJWT(identity);
                
                var cupos = _context.CupostT002Cupos.Where(p => p.PkT002codigo == quotaCode && p.A002estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                
                if(cupos != null)
                {
                    CupoGuardar resolucionInfo = new CupoGuardar();

                    resolucionInfo.issuingAuthority = cupos.A002AutoridadEmiteResolucion;
                    resolucionInfo.zoocriaderoCode = cupos.A002CodigoZoocriadero;
                    resolucionInfo.resolutionNumber = cupos.A002numeroResolucion;
                    resolucionInfo.resolutionDate = cupos.A002fechaResolucion;
                    resolucionInfo.resolutionRegistrationDate = cupos.A002fechaRegistroResolucion;
                    resolucionInfo.observations = cupos.A002observaciones;


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
                    ExportSpecimens especiesExportar = new ExportSpecimens();
                    List<SupportDocuments> documentos = new List<SupportDocuments>();
                    var rlDocCupo = _context.CupostT023RlCupoDocumento.Where(p => p.A023CodigoCupo == quotaCode && p.A023EstadoRegistro == StringHelper.estadoActivo).ToList();
                    foreach (var docCode in rlDocCupo)
                    {
                        var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == docCode.A023CodigoDocuemento && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                        if (doc != null)
                        {
                            bool esPdf = doc.A009url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                            bool esDocx = doc.A009url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                            bool esJpeg = doc.A009url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
                            bool esPng = doc.A009url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);

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

                            SupportDocuments docSoporte = new SupportDocuments();
                            docSoporte.code = doc.PkT009codigo;
                            docSoporte.base64Attachment = "data:" + tipoAdjun + ";base64," + base64;
                            docSoporte.attachmentName = doc.A009documento;
                            docSoporte.attachmentType = tipoAdjun;

                            documentos.Add(docSoporte);
                        }
                    }

                    var esp = _context.CupostT005Especieaexportars.Where(p => p.A005codigoCupo == quotaCode && p.A005estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    if (esp != null)
                    {
                        especiesExportar.quotaCode = esp.A005codigoCupo;
                        especiesExportar.year = esp.A005añoProduccion;
                        especiesExportar.quotas = Convert.ToDecimal(esp.A005cupoAprovechamientoOtorgados);
                        especiesExportar.id = esp.PkT005codigo;
                        especiesExportar.totalQuotas = cupos.A002cuposTotal;
                        especiesExportar.repopulationQuota = cupos.A002cuotaRepoblacion;
                        especiesExportar.markingTypeParametricCode = esp.A005codigoParametricaTipoMarcaje;
                        especiesExportar.speciesCode = esp.A005codigoEspecie;
                        especiesExportar.repopulationQuotaPaymentParametricCode = esp.A005codigoParametricaPagoCuotaDerepoblacion;
                        especiesExportar.filingDate = esp.A005fechaRadicado;
                        especiesExportar.specimenType = esp.A005nombreEspecie;
                        especiesExportar.productionYear = esp.A005añoProduccion;
                        especiesExportar.batchCode = esp.A005marcaLote;
                        especiesExportar.markingConditions = esp.A005condicionesMarcaje;
                        especiesExportar.parentalPopulationMale = esp.A005poblacionParentalMacho;
                        especiesExportar.parentalPopulationFemale = esp.A005poblacionParentalHembra;
                        especiesExportar.totalParentalPopulation = esp.A005poblacionParentalTotal;
                        especiesExportar.populationFromIncubator = esp.A005poblacionSalioDeIncubadora;
                        especiesExportar.populationAvailableForQuotas = esp.A005poblacionDisponibleParaCupos;
                        especiesExportar.individualsForRepopulation = esp.A005individuosDestinadosARepoblacion;
                        especiesExportar.grantedUtilizationQuotas = Convert.ToDecimal(esp.A005cupoAprovechamientoOtorgados);
                        especiesExportar.replacementRate = esp.A005tasaReposicion;
                        especiesExportar.mortalityNumber = esp.A005numeroMortalidad;
                        especiesExportar.repopulationQuotaForUtilization = esp.A005cuotaRepoblacionParaAprovechamiento;
                        especiesExportar.grantedPaidUtilizationQuotas = esp.A005cupoAprovechamientoOtorgadosPagados;
                        especiesExportar.observations = esp.A005observaciones;
                        especiesExportar.speciesRegisteredForCommercialization = true;
                        especiesExportar.repopulationQuotaPaid = especiesExportar.repopulationQuotaPaymentParametricCode != null;
                        especiesExportar.initialRepopulationQuotaInternalNumber = esp.A005NumeroInternoInicialCuotaRepoblacion.ToString();
                        especiesExportar.finalRepopulationQuotaInternalNumber = esp.A005NumeroInternoFinalCuotaRepoblacion.ToString();
                        especiesExportar.initialInternalNumber = cupos.A002rangoCodigoInicial;
                        especiesExportar.finalInternalNumber = cupos.A002rangoCodigoFin;
                        especiesExportar.supportingDocuments = documentos;

                        var qry3 = new
                        {
                            resolucionInfo,
                            especiesExportar
                        };
                        return ResponseManager.generaRespuestaGenerica("", qry3, token, false);
                    }
                }
                return ResponseManager.generaRespuestaGenerica("", "", token, true);

            }
            catch (Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }

        public Responses ConsultCertificateshj2(ClaimsIdentity identity, decimal idcertificado)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            
            var certificado = _context.CupostT021CertificadoFloraNoMaderable.Where(p => p.Pk_T021Codigo == idcertificado && p.A021EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

            var documentos = from certifica in _context.CupostT021CertificadoFloraNoMaderable
                             join rl in _context.CupostT022RlCertificadoFloraNoMaderableDocumento
                             on certifica.Pk_T021Codigo equals rl.A022CodigoCertificadoFloraNoMaderable
                             join doc in _context.AdmintT009Documentos
                             on rl.A022CodigoDocuemento equals doc.PkT009codigo
                             where certifica.Pk_T021Codigo == idcertificado
                             where certifica.A021EstadoRegistro == StringHelper.estadoActivo
                             where rl.A022EstadoRegistro == StringHelper.estadoActivo
                             where doc.A009estadoRegistro == StringHelper.estadoActivo
                             select new
                             {
                                 doc.PkT009codigo,
                                 doc.A009documento,
                                 doc.A009url
                             };

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

            List<SupportDocuments> docSoporte = new List<SupportDocuments>();

            foreach (var docs in documentos)
            {
                bool esPdf = docs.A009url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                bool esDocx = docs.A009url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpeg = docs.A009url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esPng = docs.A009url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpg = docs.A009url.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esXlsx = docs.A009url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

                #pragma warning disable SYSLIB0014
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(docs.A009url);
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

                docSoporte.Add(new SupportDocuments
                {
                    code = docs.PkT009codigo,
                    base64Attachment = "data:" + tipoAdjun + ";base64," + base64,
                    attachmentName = docs.A009documento,
                    attachmentType = tipoAdjun,
                });
            }

            if(certificado != null)
            {
                CertificateData certificadoRetornar = new CertificateData();

                certificadoRetornar.certificationValidity = certificado.A021VigenciaCertificacion;
                certificadoRetornar.issuingAuthority = certificado.A021AutoridadEmiteCertificado;
                certificadoRetornar.certificateRemarks = certificado.A021Observaciones;
                certificadoRetornar.certificationDate = certificado.A021FechaCertificacion;
                certificadoRetornar.certificateNumber = certificado.A021NumeroCertificado;
                certificadoRetornar.permissionType = certificado.A021TipoPermiso;
                certificadoRetornar.specimenProductImpExpType = certificado.A021TipoEspecimenProductoImpExp;
                certificadoRetornar.supportingDocuments = docSoporte;

                return ResponseManager.generaRespuestaGenerica("", certificadoRetornar, token, false);
            }

            return ResponseManager.generaRespuestaGenerica("", "", token, true);
        }

        public Responses ConsultSituacionpdf(ClaimsIdentity identity, decimal situacionid)

        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            
            var documentos = from docNovedad in _context.CupostT012RlNovedadDocumentos
                             join doc in _context.AdmintT009Documentos
                             on docNovedad.A012codigoDocumento equals doc.PkT009codigo
                             where docNovedad.A012codigoNovedad == situacionid
                             where docNovedad.A012estadoRegistro == StringHelper.estadoActivo
                             where doc.A009estadoRegistro == StringHelper.estadoActivo
                             select new
                             {
                                 doc.PkT009codigo,
                                 doc.A009documento,
                                 doc.A009url
                             };

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

            List<SupportDocuments> docSoporte = new List<SupportDocuments>();

            foreach (var docs in documentos)
            {
                bool esPdf = docs.A009url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                bool esDocx = docs.A009url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpeg = docs.A009url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esPng = docs.A009url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);
                bool esJpg = docs.A009url.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase);
                bool esXlsx = docs.A009url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

                #pragma warning disable SYSLIB0014
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(docs.A009url);
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

                docSoporte.Add(new SupportDocuments
                {
                    code = docs.PkT009codigo,
                    base64Attachment = "data:" + tipoAdjun + ";base64," + base64,
                    attachmentName = docs.A009documento,
                    attachmentType = tipoAdjun,
                });
            }

            CertificateData certificadoRetornar = new CertificateData();


            certificadoRetornar.supportingDocuments = docSoporte;

            return ResponseManager.generaRespuestaGenerica("", certificadoRetornar, token, false);
        }

        public Responses ConsultSituacionid(ClaimsIdentity identity, decimal codigoEmpresa, decimal situacionid)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from novedad in _context.CupostT003Novedads
                        join parametrica in _context.AdmintT008Parametricas
                        on novedad.A003codigoParametricaTiponovedad equals parametrica.PkT008codigo
                        join parametricaEstado in _context.AdmintT008Parametricas
                        on novedad.A003estadoRegistro equals parametricaEstado.PkT008codigo
                        join parametricaEstadoCITES in _context.AdmintT008Parametricas
                        on novedad.A003estadoEmisionPermisosCITES equals parametricaEstadoCITES.PkT008codigo
                        join empresa in _context.CupostT001Empresas
                        on novedad.A003codigoEmpresa equals empresa.PkT001codigo
                        where (empresa.A001nit == codigoEmpresa && (novedad.PkT003codigo == situacionid))
                        orderby novedad.A003fechaRegistroNovedad ascending
                        select new
                        {
                            codigo = novedad.PkT003codigo,

                            idTipoNovedad = novedad.A003codigoParametricaTiponovedad,
                            nombreTipoNovedad = parametrica.A008valor,
                            fechaRegistroNovedad = novedad.A003fechaRegistroNovedad,
                            idEstado = parametricaEstado.PkT008codigo,
                            estadositu = parametricaEstado.A008valor,
                            idEstadoEmisionCITES = novedad.A003estadoEmisionPermisosCITES,
                            estadoEmisionCITES = parametricaEstadoCITES.A008valor,
                            observacionesitu = novedad.A003observaciones,
                            saldoProduccionDisponible = novedad.A003saldoProduccionDisponible,
                            cuposDisponibles = novedad.A003cuposDisponibles,
                            inventarioDisponible = novedad.A003inventarioDisponible,
                            numeroCupospendientesportramitar = novedad.A003numeroCupospendientesportramitar,
                            idDisposicionEspecimen = novedad.A003codigoParametricaDisposicionEspecimen,
                            idEmpresaZoo = novedad.A003codigoEmpresaTraslado,
                            otroCual = novedad.A003otroCual,
                            observacionesDetalle = novedad.A003observacionesDetalle
                        };
            List<object> nov = new List<object>();

            foreach (var novedad in query)
            {
                decimal nitEmpresaZoocriadero = 0;

                var zoo = _context.CupostT001Empresas.Where(p => p.PkT001codigo == novedad.idEmpresaZoo).FirstOrDefault();

                if (zoo != null)
                {
                    nitEmpresaZoocriadero = zoo.A001nit;
                }

                var obj = new 
                {
                    codigo = novedad.codigo,

                    idTipoNovedad = novedad.idTipoNovedad,
                    nombreTipoNovedad = novedad.nombreTipoNovedad,
                    fechaRegistroNovedad = novedad.fechaRegistroNovedad,
                    idEstado = novedad.idEstado,
                    estadositu = novedad.estadositu,
                    idEstadoEmisionCITES = novedad.idEstadoEmisionCITES,
                    estadoEmisionCITES = novedad.estadoEmisionCITES,
                    observacionesitu = novedad.observacionesitu,
                    saldoProduccionDisponible = novedad.saldoProduccionDisponible,
                    cuposDisponibles = novedad.cuposDisponibles,
                    inventarioDisponible = novedad.inventarioDisponible,
                    numeroCupospendientesportramitar = novedad.numeroCupospendientesportramitar,
                    idDisposicionEspecimen = novedad.idDisposicionEspecimen,
                    idEmpresaZoo = novedad.idEmpresaZoo,
                    NitEmpresaZoo = nitEmpresaZoocriadero,
                    otroCual = novedad.otroCual,
                    observacionesDetalle = novedad.observacionesDetalle
                };
                nov.Add(obj);
            }

            return ResponseManager.generaRespuestaGenerica("", nov, token, false);
        }
        public Responses ConsultSituacionnovedad(ClaimsIdentity identity, decimal codigoEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            var query = from novedad in _context.CupostT003Novedads
                        join parametrica in _context.AdmintT008Parametricas
                        on novedad.A003codigoParametricaTiponovedad equals parametrica.PkT008codigo
                        join parametricaEstado in _context.AdmintT008Parametricas
                        on novedad.A003estadoEmpresa equals parametricaEstado.PkT008codigo
                        join parametricaEstadoCITES in _context.AdmintT008Parametricas
                        on novedad.A003estadoEmisionPermisosCITES equals parametricaEstadoCITES.PkT008codigo
                        join empresa in _context.CupostT001Empresas
                        on novedad.A003codigoEmpresa equals empresa.PkT001codigo
                        //codigo empresa de donde sale 
                        where (empresa.A001nit == codigoEmpresa)
                        orderby novedad.A003fechaRegistroNovedad ascending
                        select new
                        {
                            codigo = novedad.PkT003codigo,

                            idTipoNovedad = novedad.A003codigoParametricaTiponovedad,
                            nombreTipoNovedad = parametrica.A008valor,
                            fechaRegistroNovedad = novedad.A003fechaRegistroNovedad,
                            idEstado = parametricaEstado.PkT008codigo,
                            estadositu = parametricaEstado.A008valor,
                            idEstadoEmisionCITES = novedad.A003estadoEmisionPermisosCITES,
                            estadoEmisionCITES = parametricaEstadoCITES.A008valor,
                            observacionesitu = novedad.A003observaciones,
                            ultimomodificado = novedad.A003fechaModificacion,




                            observacionesDetalle = novedad.A003observacionesDetalle,

                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
         public Responses ConsultSituacionnovedadultima(ClaimsIdentity identity, decimal codigoEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from novedad in _context.CupostT003Novedads
                        join parametrica in _context.AdmintT008Parametricas
                        on novedad.A003codigoParametricaTiponovedad equals parametrica.PkT008codigo
                        join parametricaEstado in _context.AdmintT008Parametricas
                        on novedad.A003estadoRegistro equals parametricaEstado.PkT008codigo
                        join parametricaEstadoCITES in _context.AdmintT008Parametricas
                        on novedad.A003estadoEmisionPermisosCITES equals parametricaEstadoCITES.PkT008codigo
                        //codigo empresa de donde sale 
                        where (novedad.A003codigoEmpresa == codigoEmpresa)
                        orderby novedad.A003fechaModificacion descending
                        select new
                        {
                            estadositu = parametricaEstado.A008valor,
                           
                            estadoEmisionCITES = parametricaEstadoCITES.A008valor,
                           
                            ultimomodificado = novedad.A003fechaModificacion,

                            observacionesDetalle = novedad.A003observacionesDetalle,

                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        public Responses Consultpecespdf(ClaimsIdentity identity, decimal idresolucionp)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            
            var resolucion = _context.CupostT014Resolucion.Where(p => p.PkT014codigo == idresolucionp && p.A014estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

            if(resolucion != null)
            {
                var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == resolucion.A014codigoDocumentoSoporte && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                if(doc != null)
                {
                    var docuemnto = GetFile(doc);

                    ResolucionPermisos resolucionPermiso = new ResolucionPermisos();
                    resolucionPermiso.resolutionCode = resolucion.PkT014codigo;
                    resolucionPermiso.resolutionNumber = resolucion.A014numeroResolucion;
                    resolucionPermiso.resolutionDate = resolucion.A014fechaResolucion;
                    resolucionPermiso.startDate = resolucion.A014fechaInicio;
                    resolucionPermiso.endDate = resolucion.A014fechaFin;
                    resolucionPermiso.attachment = docuemnto;
                    resolucionPermiso.resolutionObject = resolucion.A014objetoResolucion;

                    return ResponseManager.generaRespuestaGenerica("", resolucionPermiso, token, false);
                }
            }

            return ResponseManager.generaRespuestaGenerica("", "", token, true);
        }

        [ExcludeFromCodeCoverage]
        public SupportDocuments GetFile(AdmintT009Documento doc)
        {
            SupportDocuments docuemnto = new SupportDocuments();
            var query = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            
            string usuraio = "";
            string clave = "";
            foreach (var c in query)
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
            docuemnto.code = doc.PkT009codigo;
            docuemnto.base64Attachment = "data:" + tipoAdjun + ";base64," + base64;
            docuemnto.attachmentName = doc.A009documento;
            docuemnto.attachmentType = tipoAdjun;

            return docuemnto;
        }

        public static string ConvertToBase64(Stream stream)
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

    }
}