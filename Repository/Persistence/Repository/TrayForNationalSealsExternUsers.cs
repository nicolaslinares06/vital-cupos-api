using API.Helpers;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text.pdf.codec.wmf;
using Microsoft.Extensions.Options;
using NHibernate.Linq.Functions;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities.Net;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using System.Security.Principal;
using Web.Models;
using static Repository.Helpers.Models.ModelsAppSettings;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace Repository.Persistence.Repository
{
    public class TrayForNationalSealsExternUsers : ITrayForNationalSealsExternUsers
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly IGenericsMethodsHelper genericsMethodsHelper;
        private readonly EstadosCuposSettings _estadosCuposSettings;

        public TrayForNationalSealsExternUsers(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, IGenericsMethodsHelper genericsMethodsHelper,
                                                IOptions<EstadosCuposSettings> estadosCuposSettings)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.genericsMethodsHelper = genericsMethodsHelper;
            _estadosCuposSettings = estadosCuposSettings.Value;
        }

        /// <summary>
        /// consulta tipos solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultRequestTypes(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var qry = from tipoSolicitud in _context.AdmintT008Parametricas
                      where tipoSolicitud.A008parametrica == "TIPO SOLICITUD CUPOS"
                      select new
                      {
                          code = tipoSolicitud.PkT008codigo,
                          name = tipoSolicitud.A008valor
                      };

            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// consulta las empresas
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultBussiness(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var empresas = from empresa in _context.CupostT001Empresas
                           where empresa.A001estadoRegistro == StringHelper.estadoActivo
                           select new
                           {
                               code = empresa.PkT001codigo,
                               name=empresa.A001nombre
                           };

            return ResponseManager.generaRespuestaGenerica("", empresas, token, false);
        }

        /// <summary>
        /// consulta la empresa y representante legal que esta ligada a el usuario externo
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultBussinesAndLegalRepresentant(ClaimsIdentity identity, decimal codeBussines)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            RepresentaeLegalEmpresas representaeLegalEmpresas = new RepresentaeLegalEmpresas();
            
            var empresa = _context.CupostT001Empresas.Where(p => p.PkT001codigo == codeBussines && p.A001estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
            var usuario = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (empresa != null)
            {
                var representante = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo== usuario).FirstOrDefault();
                if (representante != null)
                {

                    representaeLegalEmpresas.primerApellido = representante.A012primerApellido ?? "";
                    representaeLegalEmpresas.segundoApellido = representante.A012segundoApellido ?? "";
                    representaeLegalEmpresas.codigoEmpresa = empresa.PkT001codigo;
                    representaeLegalEmpresas.ciudad = empresa.A001codigoCiudad;
                    representaeLegalEmpresas.primerNombre = representante.A012primerNombre;
                    representaeLegalEmpresas.segundoNombre = representante.A012segundoNombre;
                    representaeLegalEmpresas.tipoIdentificacion = representante.A012codigoParametricaTipoDocumento;
                    representaeLegalEmpresas.numeroIdentificacion = representante.A012identificacion.ToString();
                    representaeLegalEmpresas.telefono = representante.A012telefono.ToString();
                    representaeLegalEmpresas.fax = representante.A012correoElectronico;

                }
            }
            return ResponseManager.generaRespuestaGenerica("", representaeLegalEmpresas, token, false);

        }

        /// <summary>
        /// consulta las ciudades
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultCities(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var qry = from ciudades in _context.AdmintT004Ciudads
                      select new
                      {
                          id = ciudades.PkT004codigo,
                          text = ciudades.A004nombre
                      };

            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }



        /// <summary>
        /// consulta las ciudades por id Departamento
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultCitiesByIdDepartment(ClaimsIdentity identity, decimal idDepartment)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var ciudades = await genericsMethodsHelper.ObtenerCiudadesPorIdDepartamento(idDepartment);


            var qry = ciudades.Select(e => new
                                        {
                                            id = e.PkT004codigo,
                                            text = e.A004nombre
                                        });   
            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }



        /// <summary>
        /// consulta los departamentos
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<Responses> consultDepartments(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var departamentos = await genericsMethodsHelper.ObtenerDepartamentos();

            var qry = departamentos.Select(e => new
            {
                id = e.PkT003codigo,
                text = e.A003nombre
            });

            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// guarda la solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Responses RegisterRequest(ClaimsIdentity identity, Requests request, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {

                Metodos met = new Metodos(_context);
                var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                CupostT019Solicitudes solicitud = new CupostT019Solicitudes();

                solicitud.A019Cantidad = request.quantity;
                solicitud.A019FechaCreacion = DateTime.Now;
                solicitud.A019FechaCambioEstado = DateTime.Now;
                solicitud.A019EstadoRegistro = StringHelper.estadoActivo;
                solicitud.A019EstadoSolicitud = Convert.ToDecimal(_estadosCuposSettings.Enviada.IdEstado);
                solicitud.A019DireccionEntrega = request.deliveryAddress;
                solicitud.A019CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                solicitud.A019CodigoCiudad = request.representativeCity;
                solicitud.A019FechaConsignacion = request.representativeDate;
                solicitud.A019TipoSolicitud = request.requestType;
                solicitud.A019FechaSolicitud = request.date;
                solicitud.A019LongitudMayor = request.majorLength;
                solicitud.A019LongitudMenor = request.minorLength;
                solicitud.A019CodigoEspecieExportar = request.specimens;
                solicitud.A019CodigoEmpresa = request.companyCode;
                solicitud.A019Observaciones = request.observations;

                _context.CupostT019Solicitudes.Add(solicitud);
                _context.SaveChanges();

                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, solicitud, solicitud.Pk_T019Codigo.ToString());
                if (request.requestType == StringHelper.tipoSolictudPrecintos || request.requestType == 10200)
                {
                    if(request.numerations != null)
                    {
                        foreach (var numeracion in request.numerations)
                        {
                            CupostT027NumeracionesSolicitud num = new CupostT027NumeracionesSolicitud();

                            num.A027CodigoSolicitud = solicitud.Pk_T019Codigo;
                            num.A027NumeroInternoInicial = numeracion.initial;
                            num.A027NumeroInternoFinal = numeracion.final;
                            num.A027OrigenSolicitud = numeracion.origen;
                            num.A027FechaCreacion = DateTime.Now;
                            num.A027CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                            num.A027EstadoRegistro = StringHelper.estadoActivo;
                            if (numeracion.origen == StringHelper.origenInventario)
                            {
                                var facturaNumeracion = _context.CupostT026FacturaCompraCupo.Where(p => p.Pk_T026Codigo == numeracion.code).FirstOrDefault();
                                if (facturaNumeracion != null)
                                {
                                    var factura = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == facturaNumeracion.A026CodigoFacturaCompra && p.A004estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                                    num.A027CodigoEmpresaOrigenNumeraciones = factura != null ? factura.A004codigoEntidadVende : 0;
                                }
                            }
                            else
                            {
                                num.A027CodigoEmpresaOrigenNumeraciones = request.companyCode;
                            }

                            _context.CupostT027NumeracionesSolicitud.Add(num);
                            _context.SaveChanges();
                            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, num, num.Pk_T027Codigo.ToString());
                        }
                    }
                }
                else if (request.requestType == StringHelper.tipoSolictudMarquillasMinisterio)
                {
                    if(request.cuttingSave != null)
                    {
                        foreach (var cortes in request.cuttingSave)
                        {

                            CupostT029CortesPielSolicitud cort = new CupostT029CortesPielSolicitud();
                            cort.A029CodigoCortePiel = cortes.id;
                            cort.A029CodigoSolicitud = solicitud.Pk_T019Codigo;
                            cort.A029Cantidad = cortes.amountSelected;
                            cort.A029AreaTotal = cortes.totalAreaSelected;
                            cort.A029CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                            cort.A029FechaCreacion = DateTime.Now;
                            cort.A029EstadoRegistro = StringHelper.estadoActivo;

                            _context.CupostT029CortesPielSolicitud.Add(cort);
                            _context.SaveChanges();
                            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, cort, cort.Pk_T029Codigo.ToString());

                        }
                    }

                    if(request.safeGuardNumbers != null)
                    {
                        foreach (var salvo in request.safeGuardNumbers)
                        {

                            CupostT028SalvoconductosSolicitud safe = new CupostT028SalvoconductosSolicitud();
                            safe.A028CodigoActaVisitaSalvoconducto = salvo.id;
                            safe.A028CodigoSolicitud = solicitud.Pk_T019Codigo;
                            safe.A028CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                            safe.A028FechaCreacion = DateTime.Now;
                            safe.A028EstadoRegistro = StringHelper.estadoActivo;

                            _context.CupostT028SalvoconductosSolicitud.Add(safe);
                            _context.SaveChanges();
                            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, safe, safe.Pk_T028Codigo.ToString());

                        }
                    }
                }

                Metodos metodo = new Metodos(_context);

                if (request.attachedAnnexes != null && request.attachedAnnexes.Count > 0)
                {
                    GuardarAdjuntoAnexosRespuestas(request.attachedAnnexes, solicitud.Pk_T019Codigo, Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value), ipAddress, true);
                }
                var url = metodo.GuardarArchivoFtp(request.invoiceAttachment);
                if(!String.IsNullOrEmpty(url))
                {
                    AdmintT009Documento docNuevoFactura = new AdmintT009Documento();
                    docNuevoFactura.A009fechaCreacion = DateTime.Now;
                    docNuevoFactura.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    docNuevoFactura.A009estadoRegistro = StringHelper.estadoActivo;
                    docNuevoFactura.A009codigoParametricaTipoDocumento = StringHelper.tipoDocumentoAdjuntoFactura;
                    docNuevoFactura.A009firmaDigital = "firma";
                    docNuevoFactura.A009codigoPlantilla = 1;
                    docNuevoFactura.A009documento = request.invoiceAttachment.attachmentName != null ? request.invoiceAttachment.attachmentName : "";
                    docNuevoFactura.A009descripcion = request.invoiceAttachment.attachmentName != null ? request.invoiceAttachment.attachmentName : "";
                    docNuevoFactura.A009url = url;

                    _context.AdmintT009Documentos.Add(docNuevoFactura);
                    _context.SaveChanges();
                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, docNuevoFactura, docNuevoFactura.PkT009codigo.ToString());

                    CupostT020RlSolicitudesDocumento rlDocNuev = new CupostT020RlSolicitudesDocumento();
                    rlDocNuev.A020FechaCreacion = DateTime.Now;
                    rlDocNuev.A020EstadoRegistro = StringHelper.estadoActivo;
                    rlDocNuev.A020CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    rlDocNuev.A020CodigoDocumento = docNuevoFactura.PkT009codigo;
                    rlDocNuev.A020CodigoSolicitud = solicitud.Pk_T019Codigo;

                    _context.CupostT020RlSolicitudesDocumento.Add(rlDocNuev);
                    _context.SaveChanges();
                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, rlDocNuev, rlDocNuev.Pk_T020Codigo.ToString());
                }

                return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// conuslta las solicitudes radicadas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultRegisteredRecuest(ClaimsIdentity identity, decimal codigoEmpresa)
        {
            string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var sol = _context.CupostT019Solicitudes.Where(p => (p.A019EstadoSolicitud == _estadosCuposSettings.Radicada.IdEstado) || (p.A019EstadoSolicitud == _estadosCuposSettings.Enviada.IdEstado) 
            && p.A019CodigoEmpresa == codigoEmpresa && p.A019EstadoRegistro == StringHelper.estadoActivo).ToList();
            List<RequestsOther> qry = new List<RequestsOther>();
            foreach (var s in sol)
            {
                if (s.A019TipoSolicitud != null && s.A019CodigoUsuarioCreacion == Convert.ToDecimal(codigoUsuario))
                {
                    var tipoSol = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == s.A019TipoSolicitud && p.A008estadoRegistro == StringHelper.estadoActivo).First();
                    var representante = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == s.A019CodigoUsuarioCreacion && p.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    qry.Add(new RequestsOther
                    {
                        code = s.Pk_T019Codigo,
                        filingNumber = s.A019NumeroRadicacion,
                        sealLabelRequest = "Solicitud " + tipoSol.A008valor,
                        requestingEntityName = representante != null ? representante.A012primerNombre + " " + representante.A012segundoNombre + " " + representante.A012primerApellido + " " + representante.A012segundoApellido : "",
                        requestDate = s.A019FechaSolicitud,
                        filingDate = s.A019FechaRadicacion,
                        status = s.A019EstadoSolicitud == _estadosCuposSettings.Enviada.IdEstado ? _estadosCuposSettings.Enviada.ValorEstado : _estadosCuposSettings.Radicada.ValorEstado
                    });
                }
                
            }

            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// consulta solicitudes en requerimiento
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultRequirements(ClaimsIdentity identity, decimal codigoEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<RequestsOther> qry = new List<RequestsOther>();

            var sol = _context.CupostT019Solicitudes.Where(p => p.A019EstadoSolicitud == _estadosCuposSettings.EnRequerimiento.IdEstado && p.A019CodigoEmpresa == codigoEmpresa && p.A019EstadoRegistro == StringHelper.estadoActivo).ToList();
            
            foreach (var s in sol)
            {
                if (s.A019TipoSolicitud != null)
                {
                    
                    var tipoSol = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == s.A019TipoSolicitud && p.A008estadoRegistro == StringHelper.estadoActivo).First();
                    var representante = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == s.A019CodigoUsuarioCreacion && p.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    RequestsOther solicitud = new RequestsOther();
                    solicitud.code = s.Pk_T019Codigo;
                    solicitud.sealLabelRequest = "Solicitud " + tipoSol.A008valor;

                    if (representante != null)
                        solicitud.requestingEntityName = representante.A012primerNombre + " " + representante.A012segundoNombre + " " + representante.A012primerApellido + " " + representante.A012segundoApellido;
                    else
                        solicitud.requestingEntityName = "";

                    solicitud.requestDate = s.A019FechaSolicitud;
                    solicitud.filingNumber = s.A019NumeroRadicacion;
                    solicitud.filingDate = s.A019FechaRadicacion;
                    solicitud.status = _estadosCuposSettings.EnRequerimiento.ValorEstado;
                    solicitud.observations = s.A019Observaciones;
                    qry.Add(solicitud);
                }
            }


            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// consulta solicitudes aprovadas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultApproved(ClaimsIdentity identity, decimal codigoEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<RequestsOther> qry = new List<RequestsOther>();

            var sol = _context.CupostT019Solicitudes.Where(p => p.A019EstadoSolicitud == _estadosCuposSettings.Aprobado.IdEstado && p.A019CodigoEmpresa == codigoEmpresa && p.A019EstadoRegistro == StringHelper.estadoActivo).ToList();
            
            foreach (var s in sol)
            {
                if (s.A019TipoSolicitud != null)
                {
                    var tipoSol = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == s.A019TipoSolicitud && p.A008estadoRegistro == StringHelper.estadoActivo).First();
                    var representante = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == s.A019CodigoUsuarioCreacion && p.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    RequestsOther solicitud = new RequestsOther();
                    solicitud.code = s.Pk_T019Codigo;
                    solicitud.sealLabelRequest = "Solicitud " + tipoSol.A008valor;

                    if (representante != null)
                        solicitud.requestingEntityName = representante.A012primerNombre + " " + representante.A012segundoNombre + " " + representante.A012primerApellido + " " + representante.A012segundoApellido;
                    else
                        solicitud.requestingEntityName = "";

                    solicitud.requestDate = s.A019FechaSolicitud;
                    solicitud.filingNumber = s.A019NumeroRadicacion;
                    solicitud.outgoingFilingNumber = s.A019NumeroRadicacionSalida;
                    solicitud.outgoingFilingDate = s.A019FechaRadicacionSalida;
                    solicitud.filingDate = s.A019FechaRadicacion;
                    solicitud.status = _estadosCuposSettings.Aprobado.ValorEstado;
                    solicitud.observations = s.A019Observaciones;
                    qry.Add(solicitud);
                }
            }


            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// consulta solicitudes desistidas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoEmpresa"></param>
        /// <returns></returns>
        public Responses ConsultDesisted(ClaimsIdentity identity, decimal codigoEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<RequestsOther> qry = new List<RequestsOther>();

            var sol = _context.CupostT019Solicitudes.Where(p => p.A019EstadoSolicitud == _estadosCuposSettings.Desistido.IdEstado && p.A019CodigoEmpresa==codigoEmpresa && p.A019EstadoRegistro == StringHelper.estadoActivo).ToList();
            
            foreach (var s in sol)
            {
                if (s.A019TipoSolicitud != null)
                {
                    var tipoSol = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == s.A019TipoSolicitud && p.A008estadoRegistro == StringHelper.estadoActivo).First();
                    var representante = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == s.A019CodigoUsuarioCreacion && p.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    RequestsOther solicitud = new RequestsOther();
                    solicitud.code = s.Pk_T019Codigo;
                    solicitud.sealLabelRequest = "Solicitud " + tipoSol.A008valor;

                    if (representante != null)
                        solicitud.requestingEntityName = representante.A012primerNombre + " " + representante.A012segundoNombre + " " + representante.A012primerApellido + " " + representante.A012segundoApellido;
                    else
                        solicitud.requestingEntityName = "";

                    solicitud.requestDate = s.A019FechaSolicitud;
                    solicitud.filingNumber = s.A019NumeroRadicacion;
                    solicitud.filingDate = s.A019FechaRadicacion;
                    solicitud.status = _estadosCuposSettings.Desistido.ValorEstado;
                    solicitud.observations = s.A019Observaciones;
                    qry.Add(solicitud);
                }
            }


            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// radicar solicitud pendiente 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="solicitudPendiente"></param>
        /// <returns></returns>
        public Responses RegisterPending(ClaimsIdentity identity, RegisterPending solicitudPendiente)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var sol = _context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == solicitudPendiente.requestCode && p.A019EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
            if (sol != null)
            {
                sol.A019EstadoSolicitud = Convert.ToDecimal(_estadosCuposSettings.Radicada.IdEstado);
                sol.A019FechaRadicacion = solicitudPendiente.fechaRadicado;
                sol.A019NumeroRadicacion = solicitudPendiente.numeroRadicado;
                sol.A019FechaCambioEstado= DateTime.Now;
                sol.A019FechaModificacion = DateTime.Now;
                sol.A019CodigoUsuarioModificacion= Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }

            _context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
        }

        /// <summary>
        /// consultar una solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoSolicitud"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses ConsultOnePendientRegister(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                Requests solicitud = new Requests();

                List<SupportDocuments> DocsAnexos = new List<SupportDocuments>();

                SupportDocuments DocFactura = new SupportDocuments();

                SupportDocuments DocCarta = new SupportDocuments();

                List<SupportDocuments> DocsRespuesta = new List<SupportDocuments>();

                var sol = _context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == codigoSolicitud && p.A019EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                if (sol != null)
                {

                    var Rlfactura = _context.CupostT020RlSolicitudesDocumento.Where(p => p.A020CodigoSolicitud == sol.Pk_T019Codigo && p.A020EstadoRegistro == StringHelper.estadoActivo).ToList();

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
                    Metodos metodo = new Metodos(_context);

                    if (Rlfactura != null && Rlfactura.Count>0) {

                        for (int i = 0; i < Rlfactura.Count; i++)
                        {
                            CupostT020RlSolicitudesDocumento? rl = Rlfactura[i];
                            var factura = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == rl.A020CodigoDocumento && p.A009codigoParametricaTipoDocumento == StringHelper.tipoDocumentoAdjuntoFactura && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                            if (factura != null)
                            {
                                var doc = metodo.CargarArchivoFtp(factura, usuraio, clave);

                                DocFactura.code = doc.code;
                                DocFactura.attachmentName = doc.attachmentName;
                                DocFactura.attachmentType = doc.attachmentType;
                                DocFactura.base64Attachment = doc.base64Attachment;
                            }

                            var Anexo = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == rl.A020CodigoDocumento && p.A009codigoParametricaTipoDocumento == StringHelper.tipoDocumentoAdjuntoAnexos && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                            if (Anexo != null)
                            {
                                var doc = metodo.CargarArchivoFtp(Anexo, usuraio, clave);

                                DocsAnexos.Add(doc);
                            }

                            var Respuesta = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == rl.A020CodigoDocumento && p.A009codigoParametricaTipoDocumento == StringHelper.tipoDocumentoAdjuntoSoporteRespuesta && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                            if (Respuesta != null)
                            {
                                var doc = metodo.CargarArchivoFtp(Respuesta, usuraio, clave);

                                DocsRespuesta.Add(doc);
                            }

                            var Carta = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == rl.A020CodigoDocumento && p.A009codigoParametricaTipoDocumento == StringHelper.tipoDocumentoAdjuntoCartaSolicitud && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                            if (Carta != null)
                            {
                                var doc = metodo.CargarArchivoFtp(Carta, usuraio, clave);

                                DocCarta.code = doc.code;
                                DocCarta.attachmentName = doc.attachmentName;
                                DocCarta.attachmentType = doc.attachmentType;
                                DocCarta.base64Attachment = doc.base64Attachment;
                            }
                        }
             
                    }

                    solicitud.date = sol.A019FechaSolicitud;
                    solicitud.representativeCity = sol.A019CodigoCiudad;
                    solicitud.representativeDepartment = _context.AdmintT004Ciudads
                                                                .Where(d => d.PkT004codigo == sol.A019CodigoCiudad)
                                                                .Select(d => d.A004codigoDepartamento).FirstOrDefault();
                    solicitud.deliveryAddress = sol.A019DireccionEntrega;
                    solicitud.quantity = sol.A019Cantidad;
                    solicitud.specimens = sol.A019CodigoEspecieExportar;
                    solicitud.minorLength = sol.A019LongitudMenor;
                    solicitud.majorLength = sol.A019LongitudMayor;
                    solicitud.representativeDate = sol.A019FechaConsignacion;
                    solicitud.observations = sol.A019Observaciones;
                    solicitud.response = sol.A019Respuesta;
                    solicitud.invoiceAttachment = DocFactura;
                    solicitud.attachedAnnexes = DocsAnexos;
                    solicitud.letterAttachment = DocCarta;
                    solicitud.responseAttachments = DocsRespuesta;
                    solicitud.registrationDate = sol.A019FechaRadicacion;
                    solicitud.statusChangeDate = sol.A019FechaCambioEstado;
                    solicitud.withdrawalObservations = sol.A019ObservacionesDesistimiento;
                    solicitud.requestType = sol.A019TipoSolicitud;


                    if (solicitud.requestType == StringHelper.tipoSolictudPrecintos || solicitud.requestType == 10200)
                    {
                        var numeraciones = _context.CupostT027NumeracionesSolicitud.Where(p => p.A027CodigoSolicitud == codigoSolicitud && p.A027EstadoRegistro == StringHelper.estadoActivo).ToList();

                        List<Numeration> numer = new List<Numeration>();

                        foreach (var num in numeraciones)
                        {
                            Numeration n = new Numeration();

                            n.initial = num.A027NumeroInternoInicial;
                            n.final = num.A027NumeroInternoFinal;

                            numer.Add(n);
                        }

                        solicitud.numerations = numer;
                    }
                    if (solicitud.requestType == StringHelper.tipoSolictudMarquillasMinisterio)
                    {
                        var salvoConducto = _context.CupostT028SalvoconductosSolicitud.Where(p => p.A028CodigoSolicitud == codigoSolicitud && p.A028EstadoRegistro == StringHelper.estadoActivo).ToList();

                        List<SafeGuardNumbersModel> safe = new List<SafeGuardNumbersModel>();

                        foreach (var saf in salvoConducto)
                        {

                            var numero = _context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.PK_T017Codigo == saf.A028CodigoActaVisitaSalvoconducto).FirstOrDefault();
                            SafeGuardNumbersModel n = new SafeGuardNumbersModel();

                            n.id = Convert.ToInt32(saf.Pk_T028Codigo);
                            n.number = numero != null ? numero.A017SalvoConductoNumero.ToString() : "";

                            safe.Add(n);
                        }

                        solicitud.safeGuardNumbers = safe;

                        var cortes = _context.CupostT029CortesPielSolicitud.Where(p => p.A029CodigoSolicitud == codigoSolicitud && p.A029EstadoRegistro == StringHelper.estadoActivo).ToList();

                        List<CuttingSaveModel> cort = new List<CuttingSaveModel>();

                        foreach (var cor in cortes)
                        {
                            var fraccion = _context.CupostT008CortePiels.Where(p => p.A008codigo == cor.A029CodigoCortePiel).FirstOrDefault();
                            CuttingSaveModel n = new CuttingSaveModel();

                            n.totalAreaSelected = Convert.ToInt32(cor.A029AreaTotal);
                            n.amountSelected = Convert.ToInt32(cor.A029Cantidad);
                            n.fractionType = fraccion != null ? fraccion.A008tipoParte : "";

                            cort.Add(n);
                        }

                        solicitud.cuttingSave = cort;


                        if (sol.A019EstadoSolicitud == _estadosCuposSettings.Desistido.IdEstado)
                        {
                            solicitud.requestStatus = _estadosCuposSettings.Desistido.ValorEstado;
                        }
                    }
                }

                return ResponseManager.generaRespuestaGenerica("", solicitud, token, false);
            }
            catch (Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }

        /// <summary>
        /// edicion de la solicitud
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Responses EditRequest(ClaimsIdentity identity, Requests request, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var sol = _context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == request.requestCode && p.A019EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
            if (sol != null)
            {
                #region auditoria

                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019FechaSolicitud.ToString(), request.date.ToString(), "A019FechaSolicitud");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019DireccionEntrega.ToString(), request.deliveryAddress.ToString(), "A019DireccionEntrega");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019LongitudMenor.ToString(), request.minorLength.ToString(), "A019LongitudMenor");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019LongitudMayor.ToString(), request.majorLength.ToString(), "A019LongitudMayor");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019FechaConsignacion.ToString(), request.representativeDate.ToString(), "A019FechaConsignacion");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019Observaciones != null ? sol.A019Observaciones.ToString() : "", request.observations.ToString(), "A019Observaciones");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019Respuesta != null ? sol.A019Respuesta.ToString() : "", request.response.ToString(), "A019Respuesta");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019CodigoUsuarioModificacion.ToString(), codigoUsuario.ToString(), "A019CodigoUsuarioModificacion");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019FechaModificacion.ToString(), DateTime.Now.ToString(), "A019FechaModificacion");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019CodigoCiudad.ToString(), DateTime.Now.ToString(), "A019CodigoCiudad");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019TipoSolicitud.ToString(), request.requestType.ToString(), "A019TipoSolicitud");
                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, sol.A019EstadoSolicitud.ToString(), _estadosCuposSettings.Radicada.IdEstado.ToString(), "A019EstadoSolicitud");

                #endregion

                sol.A019FechaSolicitud = request.date;
                sol.A019DireccionEntrega = request.deliveryAddress;
                sol.A019LongitudMenor = request.minorLength;
                sol.A019LongitudMayor = request.majorLength;
                sol.A019FechaConsignacion = request.representativeDate;
                sol.A019Observaciones = request.observations;
                sol.A019Respuesta = request.response;
                sol.A019CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                sol.A019FechaModificacion = DateTime.Now;
                sol.A019CodigoCiudad = request.representativeCity;
                sol.A019FechaCambioEstado = DateTime.Now;
                sol.A019TipoSolicitud = request.requestType;
                sol.A019EstadoSolicitud = Convert.ToDecimal(_estadosCuposSettings.Radicada.IdEstado);

                _context.SaveChanges();

                Metodos metodo = new Metodos(_context);

                if (request.attachedAnnexes != null && request.attachedAnnexes.Count > 0)
                {
                    GuardarAdjuntoAnexosRespuestas(request.attachedAnnexes, sol.Pk_T019Codigo, Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value), ipAddress, true);
                }

                if (request.responseAttachments != null && request.responseAttachments.Count > 0)
                {
                    GuardarAdjuntoAnexosRespuestas(request.responseAttachments, sol.Pk_T019Codigo, Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value), ipAddress);
                }

                if (request.invoiceAttachment.code == 0)
                {
                    var rlDoc = _context.CupostT020RlSolicitudesDocumento.Where(p => p.A020CodigoSolicitud == request.requestCode && p.A020EstadoRegistro == StringHelper.estadoActivo).ToList();

                    foreach (var doc in rlDoc)
                    {
                        var docElim = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == doc.A020CodigoDocumento && p.A009codigoParametricaTipoDocumento == StringHelper.tipoDocumentoAdjuntoFactura && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                        if (docElim != null)
                        {
                            docElim.A009estadoRegistro = StringHelper.estadoInactivo;
                            docElim.A009codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                            docElim.A009fechaModificacion = DateTime.Now;
                            _context.SaveChanges();

                            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 4, docElim, docElim.A009documento);

                            var rlDocElim = _context.CupostT020RlSolicitudesDocumento.Where(p => p.A020CodigoSolicitud == doc.A020CodigoSolicitud && p.A020CodigoDocumento == docElim.PkT009codigo && p.A020EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                            if(rlDocElim != null)
                            {
                                rlDocElim.A020EstadoRegistro = StringHelper.estadoInactivo;
                                rlDocElim.A020FechaModificacion = DateTime.Now;
                                rlDocElim.A020CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 4, rlDocElim, rlDocElim.Pk_T020Codigo.ToString());
                            }

                            _context.SaveChanges();
                        }
                    }

                    var url = metodo.GuardarArchivoFtp(request.invoiceAttachment);
                    if(!String.IsNullOrEmpty(url))
                    {
                        AdmintT009Documento docNuevoFactura = new AdmintT009Documento();
                        docNuevoFactura.A009fechaCreacion = DateTime.Now;
                        docNuevoFactura.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        docNuevoFactura.A009estadoRegistro = StringHelper.estadoActivo;
                        docNuevoFactura.A009codigoParametricaTipoDocumento = StringHelper.tipoDocumentoAdjuntoFactura;
                        docNuevoFactura.A009firmaDigital = "firma";
                        docNuevoFactura.A009codigoPlantilla = 1;
                        docNuevoFactura.A009documento = request.invoiceAttachment.attachmentName != null ? request.invoiceAttachment.attachmentName : "";
                        docNuevoFactura.A009descripcion = request.invoiceAttachment.attachmentName != null ? request.invoiceAttachment.attachmentName : "";
                        docNuevoFactura.A009url = url;

                        _context.AdmintT009Documentos.Add(docNuevoFactura);
                        _context.SaveChanges();
                        met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, docNuevoFactura, docNuevoFactura.PkT009codigo.ToString());

                        CupostT020RlSolicitudesDocumento rlDocNuev = new CupostT020RlSolicitudesDocumento();
                        rlDocNuev.A020FechaCreacion = DateTime.Now;
                        rlDocNuev.A020EstadoRegistro = StringHelper.estadoActivo;
                        rlDocNuev.A020CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        rlDocNuev.A020CodigoDocumento = docNuevoFactura.PkT009codigo;
                        rlDocNuev.A020CodigoSolicitud = sol.Pk_T019Codigo;

                        _context.CupostT020RlSolicitudesDocumento.Add(rlDocNuev);
                        _context.SaveChanges();
                        met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, rlDocNuev, rlDocNuev.Pk_T020Codigo.ToString());
                    }
                }

                if (request.attachedAnnexesToDelete != null && request.attachedAnnexesToDelete.Count > 0)
                {
                    DesactivarAdjuntos(request.attachedAnnexesToDelete, sol.Pk_T019Codigo, Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value), ipAddress);
                }

                if (request.responseAttachmentsToDelete != null && request.responseAttachmentsToDelete.Count > 0)
                {
                    DesactivarAdjuntos(request.responseAttachmentsToDelete, sol.Pk_T019Codigo, Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value), ipAddress);
                }
                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, valorAnterior, valorActual, campos, 3, null, null);
            }

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
        }

        /// <summary>
        /// guarda adjuntos 
        /// </summary>
        /// <param name="adjuntos"></param>
        /// <param name="codigoSolicitud"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="esAnexo"></param>
        public void GuardarAdjuntoAnexosRespuestas(List<SupportDocuments> adjuntos, decimal codigoSolicitud, decimal codigoUsuario, string ipAddress, bool esAnexo=false)
        {
            Metodos metodo = new Metodos(_context);

            foreach (var doc in adjuntos)
            {
                var uri = metodo.GuardarArchivoFtp(doc);

                if (String.IsNullOrEmpty(uri))
                    continue;

                AdmintT009Documento docNuevo = new AdmintT009Documento();
                docNuevo.A009fechaCreacion = DateTime.Now;
                docNuevo.A009codigoUsuarioCreacion = codigoUsuario;
                docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
                docNuevo.A009codigoParametricaTipoDocumento = esAnexo ? StringHelper.tipoDocumentoAdjuntoAnexos : StringHelper.tipoDocumentoAdjuntoSoporteRespuesta;
                docNuevo.A009firmaDigital = "firma";
                docNuevo.A009codigoPlantilla = 1;
                docNuevo.A009documento = doc.attachmentName !=null ? doc.attachmentName : "";
                docNuevo.A009descripcion = doc.attachmentName != null ? doc.attachmentName : "";
                docNuevo.A009url = uri;

                _context.AdmintT009Documentos.Add(docNuevo);
                _context.SaveChanges();
                metodo.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, docNuevo, docNuevo.PkT009codigo.ToString());

                CupostT020RlSolicitudesDocumento rlDocNuevo = new CupostT020RlSolicitudesDocumento();
                rlDocNuevo.A020FechaCreacion = DateTime.Now;
                rlDocNuevo.A020EstadoRegistro = StringHelper.estadoActivo;
                rlDocNuevo.A020CodigoUsuarioCreacion = codigoUsuario;
                rlDocNuevo.A020CodigoDocumento = docNuevo.PkT009codigo;
                rlDocNuevo.A020CodigoSolicitud = codigoSolicitud;

                _context.CupostT020RlSolicitudesDocumento.Add(rlDocNuevo);
               
                metodo.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 2, rlDocNuevo, rlDocNuevo.Pk_T020Codigo.ToString());
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// desactivar adjuntos
        /// </summary>
        /// <param name="adjuntos"></param>
        /// <param name="codigoSolicitud"></param>
        /// <param name="codigoUsuario"></param>
        [ExcludeFromCodeCoverage]
        public void DesactivarAdjuntos(List<SupportDocuments> adjuntos, decimal codigoSolicitud, decimal codigoUsuario, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            foreach (var el in adjuntos)
            {
                var docEl = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == el.code && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                if(docEl != null)
                {
                    docEl.A009estadoRegistro = StringHelper.estadoInactivo;
                    docEl.A009codigoUsuarioModificacion = codigoUsuario;
                    docEl.A009fechaModificacion = DateTime.Now;
                
                    _context.SaveChanges();
                    met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 4, docEl, docEl.A009documento);

                    var rlDocElim = _context.CupostT020RlSolicitudesDocumento.Where(p => p.A020CodigoSolicitud == codigoSolicitud && p.A020CodigoDocumento == docEl.PkT009codigo && p.A020EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    if(rlDocElim != null)
                    {
                        rlDocElim.A020EstadoRegistro = StringHelper.estadoInactivo;
                        rlDocElim.A020FechaModificacion = DateTime.Now;
                        rlDocElim.A020CodigoUsuarioModificacion = codigoUsuario;
                        met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smBandejaSolicitudPrecintosNacionalesUsuarioExterno, null, null, null, 4, rlDocElim, rlDocElim.Pk_T020Codigo.ToString());
                    }

                    _context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// onbitne cupos
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetQuotas(ClaimsIdentity identity, string documentNumber, string especie)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var company = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == documentNumber).FirstOrDefault();

            List<Quota>? quotaList = new List<Quota>();
            Metodos metodo = new Metodos(_context);
            if (company != null)
            {
                var quotas = _context.CuposV001ResolucionCupos.Where(p => p.codigoEmpresa == company.PkT001codigo && p.codigoEspecie==especie).ToList();
                foreach (var quota in quotas)
                {
                    quotaList.Add(metodo.addQuotaToList(quota, quota.NumeroInternoInicial, quota.numeroInternoFinal, quota.numeroInternoInicialCuotaRepoblacion, quota.numeroInternoFinalCuotaRepoblacion));
                }
            }

            return ResponseManager.generaRespuestaGenerica("", quotaList, token, false);
        }

        /// <summary>
        /// Obtiene el inventario
        /// </summary>
        /// <returns></returns>
        public Responses GetInventory(ClaimsIdentity identity, string documentNumber, string especie)
        {
            try
            {
                var token = jwtAuthenticationManager.generarJWT(identity);
                if (token == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
                }
                List<Inventory>? inventoryList = new List<Inventory>();
                var companySeller = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == documentNumber && p.A001estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                var sale = _context.CupostT004FacturacompraCartaventa.Where(p => p.A004codigoEntidadCompra == companySeller.PkT001codigo && p.A004estadoRegistro == StringHelper.estadoActivo).ToList();
                Metodos metodo = new Metodos(_context);
                foreach (var saleDocument in sale)
                {
                    var invoice = _context.CupostT026FacturaCompraCupo.Where(p => p.A026CodigoFacturaCompra == saleDocument.PkT004codigo).ToList();

                    foreach (var quotaInvoice in invoice)
                    {
                        var quotas = _context.CupostT002Cupos.Where(p => p.PkT002codigo == quotaInvoice.A026CodigoCupo && p.A002estadoRegistro == StringHelper.estadoActivo).ToList();
                        foreach (var quota in quotas)
                        {
                            var specieExport = _context.CupostT005Especieaexportars.Where(p => p.A005codigoCupo == quota.PkT002codigo && p.A005codigoEspecie== especie && p.A005estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                            if(specieExport != null)
                            {
                                inventoryList.Add(metodo.addInventoryToList(quota, saleDocument, companySeller, quotaInvoice, specieExport));
                            }

                        }
                    }
                }
                return ResponseManager.generaRespuestaGenerica("", inventoryList, token, false);
            }
            catch (Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }

        /// <summary>
        /// Consultar numeraciones solicitudes no disponibles
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Responses getNumbersRequest(ClaimsIdentity identity, ConsultUnableNumbersModel data)
        {
            try
            {
                var token = jwtAuthenticationManager.generarJWT(identity);
                if (token == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
                }

                var company = _context.CupostT001Empresas.Where(p => p.A001nit == data.companyNit  && p.A001estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                List<Numbers> numerosSolicitudes = new List<Numbers>();

                if (data.quota)
                {
                    var cupo = _context.CupostT002Cupos.Where(p => p.PkT002codigo == data.code && p.A002estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    var especieExport = _context.CupostT005Especieaexportars.Where(p => p.A005codigoCupo == data.code && p.A005estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    if (cupo != null)
                    {
                        var solicitudesNumber = _context.CupostT027NumeracionesSolicitud.Where(p => cupo.A002rangoCodigoInicial <= p.A027NumeroInternoInicial && p.A027NumeroInternoFinal <= cupo.A002rangoCodigoFin && p.A027EstadoRegistro == StringHelper.estadoActivo && (company == null || p.A027CodigoEmpresaOrigenNumeraciones == company.PkT001codigo)).ToList();

                        if (solicitudesNumber != null && solicitudesNumber.Count > 0)
                        {
                            foreach (var solicitudNumber in solicitudesNumber)
                            {
                                Numbers numNuevo = new Numbers();
                                numNuevo.initial = Convert.ToInt32(solicitudNumber.A027NumeroInternoInicial);
                                numNuevo.final = Convert.ToInt32(solicitudNumber.A027NumeroInternoFinal);
                                numerosSolicitudes.Add(numNuevo);
                            }
                        }


                        var numbers = _context.CupostT026FacturaCompraCupo.Where(p=> cupo.A002rangoCodigoInicial <= p.A026NumeracionInicial && p.A026NumeracionFinal <= cupo.A002rangoCodigoFin).ToList();
                        if(numbers != null && numbers.Count > 0)
                        {
                            foreach(var num in numbers)
                            {
                                var factura = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == num.A026CodigoFacturaCompra && (company == null || p.A004codigoEntidadVende == company.PkT001codigo) && p.A004estadoRegistro==StringHelper.estadoActivo).FirstOrDefault();
                                if(factura != null)
                                {
                                    Numbers numNuevo = new Numbers();
                                    numNuevo.initial = Convert.ToInt32(num.A026NumeracionInicial);
                                    numNuevo.final = Convert.ToInt32(num.A026NumeracionFinal);
                                    numerosSolicitudes.Add(numNuevo);
                                }
                            }
                        }

                        if (especieExport != null)
                        {
                            var solicitudesNumberRep = _context.CupostT027NumeracionesSolicitud.Where(p => especieExport.A005NumeroInternoInicialCuotaRepoblacion <= p.A027NumeroInternoInicial && p.A027NumeroInternoFinal <= especieExport.A005NumeroInternoFinalCuotaRepoblacion && p.A027EstadoRegistro == StringHelper.estadoActivo && (company == null || p.A027CodigoEmpresaOrigenNumeraciones == company.PkT001codigo)).ToList();
                            if (solicitudesNumberRep != null && solicitudesNumberRep.Count > 0)
                            {
                                foreach (var solicitudNumber in solicitudesNumberRep)
                                {
                                    Numbers numNuevo = new Numbers();
                                    numNuevo.initial = Convert.ToInt32(solicitudNumber.A027NumeroInternoInicial);
                                    numNuevo.final = Convert.ToInt32(solicitudNumber.A027NumeroInternoFinal);
                                    numerosSolicitudes.Add(numNuevo);
                                }
                            }

                            var numbersRep = _context.CupostT026FacturaCompraCupo.Where(p => especieExport.A005NumeroInternoInicialCuotaRepoblacion <= p.A026NumeracionInicial && p.A026NumeracionFinal <= especieExport.A005NumeroInternoFinalCuotaRepoblacion).ToList();
                            if (numbersRep != null && numbersRep.Count > 0)
                            {
                                foreach (var num in numbersRep)
                                {
                                    var factura = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == num.A026CodigoFacturaCompra && p.A004codigoEntidadVende == company.PkT001codigo && p.A004estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                                    if (factura != null)
                                    {
                                        Numbers numNuevo = new Numbers();
                                        numNuevo.initial = Convert.ToInt32(num.A026NumeracionInicial);
                                        numNuevo.final = Convert.ToInt32(num.A026NumeracionFinal);
                                        numerosSolicitudes.Add(numNuevo);
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    var factura = _context.CupostT026FacturaCompraCupo.Where(p => p.Pk_T026Codigo == data.code).FirstOrDefault();
                    List<int> ocupados = new List<int>();
                    List<OcupadosRangos> ocupadosRangos = new List<OcupadosRangos>();
                    if (factura != null)
                    {
                        for(var i = factura.A026NumeracionInicial; i <= factura.A026NumeracionFinal; i++)
                        {
                            var solicitudesNumber = _context.CupostT027NumeracionesSolicitud.Where(p => i >= p.A027NumeroInternoInicial && p.A027NumeroInternoFinal >= i && p.A027EstadoRegistro == StringHelper.estadoActivo && (company == null || p.A027CodigoEmpresaOrigenNumeraciones == company.PkT001codigo)).FirstOrDefault();
                            if (solicitudesNumber != null)
                            {
                                ocupados.Add(Convert.ToInt32(i));
                            }
                        }
                        if (ocupados.Count > 0)
                        {
                            var cont = 0;
                            var cont2 = 0;
                            int[] ocupadosArray = ocupados.ToArray();
                            for(var i=0; i< ocupadosArray.Length; i++)
                            {
                                if((ocupadosRangos.Count==0) || (ocupadosArray[i]-1 != cont2))
                                {
                                    cont++;
                                    OcupadosRangos ocpRangoTemp = new OcupadosRangos();
                                    List<int> ocpTemp = new List<int>();
                                    ocpTemp.Add(ocupadosArray[i]);
                                    ocpRangoTemp.numbers = ocpTemp;
                                    ocpRangoTemp.code = cont;
                                    cont2 = ocupadosArray[i];
                                    ocupadosRangos.Add(ocpRangoTemp);
                                }
                                else if(ocupadosArray[i] - 1 == cont2)
                                {
                                    var rango = (ocupadosRangos.Where(p => Convert.ToInt32(p.code) == Convert.ToInt32(cont))).FirstOrDefault();

                                    if (rango != null)
                                        rango.numbers.Add(ocupadosArray[i]);
                                    cont2 = ocupadosArray[i];
                                }
                            }
                            
                        }
                        foreach (var ocupado in ocupadosRangos.Select(x => x.numbers))
                        {
                            Numbers numNuevo = new Numbers();
                            numNuevo.initial = ocupado.Min();
                            numNuevo.final = ocupado.Max();
                            numerosSolicitudes.Add(numNuevo);
                        }
                    }
                }


                return ResponseManager.generaRespuestaGenerica("", numerosSolicitudes, token, false);
            }
            catch (Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }
        /// <summary>
        /// Valida si estan disponibvles las numeraciones
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses ValidateNumbers(ClaimsIdentity identity, ValidateNumbersModel numbers)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var validacion = true;
            var company = _context.CupostT001Empresas.Where(p => p.A001nit == numbers.codeCompany && p.A001estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
            var noDisp = _context.CupostT027NumeracionesSolicitud.Where(p => ((p.A027NumeroInternoInicial <= numbers.numbers.initial && p.A027NumeroInternoFinal >= numbers.numbers.initial) || (p.A027NumeroInternoInicial <= numbers.numbers.final && p.A027NumeroInternoFinal >= numbers.numbers.final)) && (company == null || p.A027CodigoEmpresaOrigenNumeraciones == company.PkT001codigo)).ToList();

            if (noDisp.Count > 0)
            {
                validacion = false;
            }
            else if(numbers.origin==StringHelper.origenCupos)
            {
                var noDispVendidos = _context.CupostT026FacturaCompraCupo.Where(p => (p.A026NumeracionInicial <= numbers.numbers.initial && p.A026NumeracionFinal >= numbers.numbers.initial) || (p.A026NumeracionInicial <= numbers.numbers.final && p.A026NumeracionFinal >= numbers.numbers.final)).ToList();
                if (noDispVendidos.Count>0)
                {
                    var factura = _context.CupostT004FacturacompraCartaventa.Where(p => p.A004codigoEntidadVende==company.PkT001codigo && p.A004estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    if (factura != null)
                    {
                        validacion = false;
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("", validacion, token, false);
        }

        public Responses getActaData(ClaimsIdentity identity, string documentNumber)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var empresa = _context.CupostT001Empresas.Where(p => p.A001nit == Convert.ToDecimal(documentNumber)).FirstOrDefault();

            var actas = _context.CupostT007ActaVisita.Where(p => p.A007codigoEntidad == empresa.PkT001codigo && p.A007TipoActa == StringHelper.tipoActaFraccionesIrregulares && p.A007estadoRegistro == StringHelper.estadoActivo).ToList();

            List<CuttingReport> actasCort = new List<CuttingReport>();

            foreach (var acta in actas)
            {
                var cortes = _context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == Convert.ToDecimal(acta.PkT007codigo) && p.A008total != 0 && p.A008total != null && p.A008areaPromedio != "" && p.A008areaPromedio != null).ToList();

                if (cortes.Count > 0)
                {
                    CuttingReport act = new CuttingReport();
                    act.code = acta.PkT007codigo;
                    act.dateVisit = acta.A007fechaActa;
                    act.dateRegister = FormatearFechaDecimal(acta.A007fechaCreacion);
                    act.visitNumber = ObtenerVisitasString(acta.A007VisitaNumero1, acta.A007VisitaNumero2);
                    actasCort.Add(act);
                }
            }

            return ResponseManager.generaRespuestaGenerica("", actasCort, token, false);
        }

        public Responses getFractions(ClaimsIdentity identity, int cuttingCode)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {

            var cortes = _context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == Convert.ToDecimal(cuttingCode) && p.A008total!=0 && p.A008total != null && p.A008cantidad != 0 && p.A008cantidad != null && p.A008TipoCorteParteCode == 4).ToList();

            List<Cutting> corte = new List<Cutting>();

            foreach (var cort in cortes)
            {
                var tabCortes = _context.CupostT029CortesPielSolicitud.Where(p => p.A029CodigoCortePiel == cort.A008codigo).ToList();

                var amountCutting = cort.A008cantidad - tabCortes.Sum(p=>p.A029Cantidad);
                var totalCutting = cort.A008total - tabCortes.Sum(p=>p.A029AreaTotal);
                
                
                if(totalCutting > 0 && amountCutting > 0)
                {
                    Cutting cor = new Cutting();

                    cor.code = cort.A008codigo;
                    cor.fractionsType = cort.A008tipoParte;
                    cor.amount = amountCutting;
                    cor.totalArea = totalCutting.ToString();
                    corte.Add(cor);
                }

            }

            return ResponseManager.generaRespuestaGenerica("", corte, token, false);
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message, "", token, false);
            }
        }


        private DateTime FormatearFechaDecimal(decimal fecha)
        {
            string fechaString = fecha.ToString();
            string fechaSietePrimerosNUmeros = fechaString.Substring(0, 8);
            DateTime fechaFormat = DateTime.ParseExact(fechaSietePrimerosNUmeros, "yyyyMMdd", CultureInfo.InvariantCulture);
            return fechaFormat;
        }

        private string ObtenerVisitasString(bool primeraVisita, bool SegundaVisita)
        {
            var visitas = "";

            if (primeraVisita)
                visitas = "1 -";
            if (SegundaVisita)
                visitas += " 2";

            if (visitas.Length > 0)
            {
                var ultimoCaracter = visitas.Substring(visitas.Length - 1);
                if (ultimoCaracter == "-")
                    visitas = visitas.Remove(visitas.Length - 1);
            }

            return visitas;

        }

        public Responses getSafeguard(ClaimsIdentity identity, int reportCode)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {

                var safes = _context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.A017CodigoActaVisita == Convert.ToDecimal(reportCode)).ToList();

                List<Safeguard> reporte = new List<Safeguard>();

                foreach (var safe in safes)
                {
                    Safeguard repo = new Safeguard();
                    repo.code = safe.PK_T017Codigo;
                    repo.codSafeguard = safe.A017SalvoConductoNumero;
                    reporte.Add(repo);
                }

                return ResponseManager.generaRespuestaGenerica("", reporte, token, false);
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message, "", token, false);
            }
        }

    }
}
