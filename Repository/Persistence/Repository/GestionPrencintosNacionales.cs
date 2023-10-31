using API.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Configuration;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace Repository.Persistence.Repository
{
    public class GestionPrencintosNacionales : IGestionPrencintosNacionales
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly IGenericsMethodsHelper genericsMethodsHelper;
        private static List<Files>? documentoEnviar;
        private readonly IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        public GestionPrencintosNacionales(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, IGenericsMethodsHelper genericsMethodsHelper)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.genericsMethodsHelper = genericsMethodsHelper;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultPendingAnalyst(ClaimsIdentity identity, string ipAddress)
        {
            string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Enviada:ValorEstado")
            || x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Radicada:ValorEstado"));

            query = query.Where(x => x.ANALISTA == Convert.ToDecimal(codigoUsuario));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// Ima
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultRequirementAnalyst(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:EnRequerimiento:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// Ima
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultNumbers(ClaimsIdentity identity, string ipAddress, int codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var solicitud = _context.CupostT019Solicitudes.FirstOrDefault(x => x.Pk_T019Codigo == codigoSolicitud);
            var codigosInternos = _context.CuposV004NumeracionesPrecintos.Where(x => x.ID == codigoSolicitud).ToList();
            
            List<CodigosInternos> codigos = new List<CodigosInternos>();

            if (solicitud != null)
            {

                foreach (var cod in codigosInternos)
                {
                    CodigosInternos n = new CodigosInternos();

                    CupostT002Cupo cupos = new CupostT002Cupo();

                    if (cod.A027CODIGO_PARAMETRICA_ORIGEN_SOLICITUD == StringHelper.origenInventario)
                    {
                        cupos = _context.CupostT002Cupos.FirstOrDefault(x => cod.NUMEROINTERNOINICIAL >= x.A002rangoCodigoInicial && cod.NUMEROINTERNOFINAL <= x.A002rangoCodigoFin && x.A002estadoRegistro == StringHelper.estadoActivo && x.A002codigoEmpresa == cod.A027CODIGO_EMPRESA_ORIGEN_NUMERACIONES) ?? new CupostT002Cupo();
                    }
                    else
                    {
                        cupos = _context.CupostT002Cupos.FirstOrDefault(x => cod.NUMEROINTERNOINICIAL >= x.A002rangoCodigoInicial && cod.NUMEROINTERNOFINAL <= x.A002rangoCodigoFin && x.A002estadoRegistro == StringHelper.estadoActivo && x.A002codigoEmpresa == solicitud.A019CodigoEmpresa) ?? new CupostT002Cupo();
                    }


                    if (cupos != null)
                    {
                        var cartaVenta = _context.CupostT026FacturaCompraCupo.FirstOrDefault(x => x.A026CodigoCupo == cupos.PkT002codigo && cod.NUMEROINTERNOINICIAL >= x.A026NumeracionInicial && cod.NUMEROINTERNOFINAL <= x.A026NumeracionFinal);

                        if (cartaVenta != null)
                        {
                            var cartaVentaNumero = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == cartaVenta.A026CodigoFacturaCompra).FirstOrDefault();

                            if (cartaVentaNumero != null)
                            {
                                n.carta = Convert.ToString(cartaVentaNumero.A004numeroCartaVenta);
                            }
                        }
                        else
                        {
                            n.carta = "No Aplica";
                        }

                        n.inicial = cod.NUMEROINTERNOINICIAL;
                        n.final = cod.NUMEROINTERNOFINAL;
                        n.resolucion = cupos.A002numeroResolucion;
                        n.zoocriadero = cupos.A002CodigoZoocriadero;
                        n.subtotal = cod.IDNUMERACION;

                        codigos.Add(n);
                    }
                    else
                    {
                        var especiesExportar = _context.CupostT005Especieaexportars.FirstOrDefault(x => cod.NUMEROINTERNOINICIAL >= x.A005NumeroInternoInicialCuotaRepoblacion && cod.NUMEROINTERNOFINAL <= x.A005NumeroInternoFinalCuotaRepoblacion && x.A005estadoRegistro == StringHelper.estadoActivo);

                        if (especiesExportar != null)
                        {
                            var cartaVenta = _context.CupostT026FacturaCompraCupo.FirstOrDefault(x => x.A026CodigoCupo == especiesExportar.A005codigoCupo);

                            if (cartaVenta != null)
                            {
                                n.carta = Convert.ToString(cartaVenta.A026CodigoFacturaCompra);
                            }
                            else
                            {
                                n.carta = "No Aplica";
                            }

                            var cuposEspecies = _context.CupostT002Cupos.FirstOrDefault(x => x.PkT002codigo == especiesExportar.A005codigoCupo && x.A002estadoRegistro == StringHelper.estadoActivo && x.A002codigoEmpresa == solicitud.A019CodigoEmpresa);

                            if (cuposEspecies != null)
                            {
                                n.inicial = cod.NUMEROINTERNOINICIAL;
                                n.final = cod.NUMEROINTERNOFINAL;
                                n.resolucion = cuposEspecies.A002numeroResolucion;
                                n.zoocriadero = cupos != null ? cupos.A002CodigoZoocriadero  : "";
                                n.subtotal = cod.IDNUMERACION;

                                codigos.Add(n);
                            }
                        }
                    }
                }
            }
            return ResponseManager.generaRespuestaGenerica("", codigos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultApprovedAnalyst(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Aprobado:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultWithdrawalAnalyst(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Desistido:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultAssignedAnalyst(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() != configuration.GetValue<string>("EstadosCupos:Desistido:ValorEstado") &&
            x.ESTADO.ToUpper() != configuration.GetValue<string>("EstadosCupos:Aprobado:ValorEstado") &&
            x.ESTADO.ToUpper() != configuration.GetValue<string>("EstadosCupos:AprobadoParaFirma:ValorEstado") &&
            x.ESTADO.ToUpper() != configuration.GetValue<string>("EstadosCupos:Negado:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultPendingDirector(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;
            //APROBADO PARA FIRMAR POR DIRECTOR
            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:AprobadoParaFirma:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsulSignedDirector(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Aprobado:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultApprovedDirector(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Aprobado:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ConsultWithdrawalDirector(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from gpn in _context.CuposV002GestionPrecintosNacionales
                        select gpn;

            query = query.Where(x => x.ESTADO.ToUpper() == configuration.GetValue<string>("EstadosCupos:Desistido:ValorEstado"));

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datos"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses FileApplication(ClaimsIdentity identity, SettledNationalSealsManagement datos, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (datos != null)
            {
                var result = _context.CupostT019Solicitudes.FirstOrDefault(x => x.Pk_T019Codigo == datos.code && x.A019EstadoRegistro == StringHelper.estadoActivo);

                if (result != null)
                {
                    result.A019FechaModificacion = now;
                    result.A019CodigoUsuarioModificacion = Convert.ToInt32(codigoUsuario);

                    if (result.A019NumeroRadicacion != datos.codeSettled)
                    {
                        valorAnterior.Add(Convert.ToString(result.A019NumeroRadicacion) ?? "");
                        valorActual.Add(Convert.ToString(datos.codeSettled) ?? "");
                        campos.Add("A019NUMERO_RADICACION");
                    }

                    result.A019NumeroRadicacion = datos.codeSettled != null ? datos.codeSettled.ToUpper() : "";

                    if (result.A019FechaRadicacion != datos.date)
                    {
                        valorAnterior.Add(Convert.ToString(result.A019FechaRadicacion) ?? "");
                        valorActual.Add(Convert.ToString(datos.date) ?? "");
                        campos.Add("A019FECHA_RADICACION");
                    }

                    result.A019FechaRadicacion = datos.date;
                    result.A019EstadoSolicitud = configuration.GetValue<int>("EstadosCupos:Radicada:IdEstado");

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaTrabajoValidacionSolicitud, valorAnterior, valorActual, campos, 3, null, null);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, true, token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datos"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses FileExitRequest(ClaimsIdentity identity, SettledNationalSealsManagement datos, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (datos != null)
            {
                var result = _context.CupostT019Solicitudes.FirstOrDefault(x => x.Pk_T019Codigo == datos.code && x.A019EstadoRegistro == StringHelper.estadoActivo);

                if (result != null)
                {
                    result.A019FechaModificacion = now;
                    result.A019CodigoUsuarioModificacion = Convert.ToInt32(codigoUsuario);

                    if (result.A019NumeroRadicacionSalida != datos.codeSettled)
                    {
                        valorAnterior.Add(Convert.ToString(result.A019NumeroRadicacionSalida) ?? "");
                        valorActual.Add(Convert.ToString(datos.codeSettled) ?? "");
                        campos.Add("A019NUMERO_RADICACION_SALIDA");
                    }

                    result.A019NumeroRadicacionSalida = datos.codeSettled != null ? datos.codeSettled.ToUpper() : "";

                    if (result.A019FechaRadicacionSalida != datos.date)
                    {
                        valorAnterior.Add(Convert.ToString(result.A019FechaRadicacionSalida) ?? "");
                        valorActual.Add(Convert.ToString(datos.date) ?? "");
                        campos.Add("A019FECHA_RADICACION_SALIDA");
                    }

                    result.A019FechaRadicacionSalida = datos.date;
                    result.A019EstadoSolicitud = configuration.GetValue<int>("EstadosCupos:PreAprobado:IdEstado");

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaTrabajoValidacionSolicitud, valorAnterior, valorActual, campos, 3, null, null);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, true, token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses Species(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var query = (from a in _context.AdmintT005Especimen
                         join b in _context.CupostT005Especieaexportars
                         on a.PkT005codigo equals Convert.ToDecimal(b.A005codigoEspecie)
                         where a.A005estadoRegistro == StringHelper.estadoActivo
                         select new { b.PkT005codigo, a.A005nombre }).Distinct();


            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datos"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses GeneratePrecitosCodes(ClaimsIdentity identity, GenerateSealCodes datos, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var now = DateTime.UtcNow;
            var year = now.Year;
            int initialNumber = 0;
            int finalNumber = 0;
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var solicitudes = _context.CupostT019Solicitudes.SingleOrDefault(x => x.Pk_T019Codigo == Convert.ToDecimal(datos.code) && x.A019EstadoRegistro == StringHelper.estadoActivo);

            if (solicitudes != null)
            {
                var especimenes = _context.AdmintT005Especimen.FirstOrDefault(x => x.PkT005codigo == datos.codeSpecies);

                if ((datos.initialNumber != null) && (datos.finalNumber != null))
                {
                    CupostT006Precintosymarquilla numeraciones = new CupostT006Precintosymarquilla();
                    if (datos.tipoSolicitud == "Precintos")
                    {
                        numeraciones = _context.CupostT006Precintosymarquillas.OrderByDescending(x => x.A006numeroFinalNumerico).Where(p => p.A006codigoPrecintoymarquilla.Contains("MMA")).FirstOrDefault() ?? new CupostT006Precintosymarquilla();
                    }
                    else
                    {
                        numeraciones = _context.CupostT006Precintosymarquillas.OrderByDescending(x => x.A006numeroFinalNumerico).Where(p => p.A006codigoPrecintoymarquilla.Contains("CO")).FirstOrDefault() ?? new CupostT006Precintosymarquilla();
                    }

                    if (numeraciones != null)
                    {
                        initialNumber = Convert.ToInt32(numeraciones.A006numeroFinalNumerico + 1);
                        finalNumber = Convert.ToInt32(initialNumber + solicitudes.A019Cantidad - 1);
                    }
                    else
                    {
                        initialNumber = 1;
                        finalNumber = Convert.ToInt32(solicitudes.A019Cantidad);
                    }

                    for (int i = initialNumber; i <= finalNumber; i++)
                    {
                        CupostT006Precintosymarquilla pym = new CupostT006Precintosymarquilla();

                        pym.A006codigoEspecieExportar = datos.codeSpecies != 0 ? datos.codeSpecies : null;
                        pym.A006codigoUsuarioCreacion = codigoUsuario;
                        pym.A006codigoUsuarioModificacion = null;
                        pym.A006codigoParametricaTipoPrecintomarquilla = 1;
                        pym.A006codigoParametricaColorPrecintosymarquillas = datos.color;
                        pym.A006estadoRegistro = StringHelper.estadoActivo;
                        pym.A006fechaCreacion = now;
                        pym.A006fechaModificacion = null;
                        pym.A006observacion = "SIN OBSERVACION";
                        pym.A006codigoSolicitud = Convert.ToDecimal(datos.code);

                        if (datos.tipoSolicitud == "Precintos")
                        {
                            if (especimenes != null)
                            {
                                if (especimenes.A005nombreCientifico.Contains("fuscus") || especimenes.A005nombreCientifico.ToUpper() == "CAIMAN CROCODILUS")
                                {
                                    pym.A006codigoPrecintoymarquilla = "MMAFUS" + year + "" + i;
                                    pym.A006numeroInicial = "MMAFUS" + year + "" + datos.initialNumber;
                                    pym.A006numeroFinal = "MMAFUS" + year + "" + datos.finalNumber;
                                }
                                else if (especimenes.A005nombreCientifico.Contains("apaporiensis"))
                                {
                                    pym.A006codigoPrecintoymarquilla = "MMAAPA" + year + "" + i;
                                    pym.A006numeroInicial = "MMAAPA" + year + "" + datos.initialNumber;
                                    pym.A006numeroFinal = "MMAAPA" + year + "" + datos.finalNumber;
                                }
                                else if (especimenes.A005nombreCientifico.Contains("amazonicus"))
                                {
                                    pym.A006codigoPrecintoymarquilla = "MMAAMA" + year + "" + i;
                                    pym.A006numeroInicial = "MMAAMA" + year + "" + datos.initialNumber;
                                    pym.A006numeroFinal = "MMAAMA" + year + "" + datos.finalNumber;
                                }
                                else if (especimenes.A005nombreCientifico.Contains("acutus"))
                                {
                                    pym.A006codigoPrecintoymarquilla = "MMAACU" + year + "" + i;
                                    pym.A006numeroInicial = "MMAACU" + year + "" + datos.initialNumber;
                                    pym.A006numeroFinal = "MMAACU" + year + "" + datos.finalNumber;
                                }
                                else
                                {
                                    pym.A006codigoPrecintoymarquilla = "MMAINT" + year + "" + i;
                                    pym.A006numeroInicial = "MMAINT" + year + "" + datos.initialNumber;
                                    pym.A006numeroFinal = "MMAINT" + year + "" + datos.finalNumber;
                                }
                            }
                        }
                        else
                        {
                            pym.A006codigoPrecintoymarquilla = "CO" + i;
                            pym.A006numeroInicial = "CO" + datos.initialNumber;
                            pym.A006numeroFinal = "CO" + datos.finalNumber;
                        }


                        pym.A006numeroInicialNumerico = Convert.ToDecimal(datos.initialNumber);
                        pym.A006numeroFinalNumerico = Convert.ToDecimal(datos.finalNumber);

                        _context.CupostT006Precintosymarquillas.Add(pym);
                        _context.SaveChanges();
                    }

                    solicitudes.A019FechaModificacion = now;
                    solicitudes.A019CodigoUsuarioModificacion = codigoUsuario;
                    solicitudes.A019EstadoSolicitud = configuration.GetValue<int>("EstadosCupos:PreAprobado:IdEstado");
                    solicitudes.A019ValorConsignacion = datos.worth;
                    solicitudes.A019Observaciones = datos.observations;

                    _context.Update(solicitudes);
                    _ = _context.SaveChanges();

                    //Numero inicial
                    var nuevoNumeroInicial = 0;
                    //Consultar numeraciones de la solicitud
                    var numeracionesPrecintos = _context.CupostT027NumeracionesSolicitud.Where(x => x.A027CodigoSolicitud == Convert.ToDecimal(datos.code) && x.A027EstadoRegistro == StringHelper.estadoActivo).ToList();
                    //Validar si esta vacio 
                    if (numeracionesPrecintos != null)
                    {
                        foreach (var value in numeracionesPrecintos)
                        {
                            //Consultar la numeracion de precintos
                            var numeracionPrecintos = _context.CupostT006Precintosymarquillas.FirstOrDefault(x => x.A006codigoSolicitud == Convert.ToDecimal(datos.code) && x.A006estadoRegistro == StringHelper.estadoActivo);
                            //Validar si numeracion precintos es nulo
                            if (numeracionPrecintos != null)
                            {
                                //Consultar para actualizar registro de numeraciones
                                var numeracion = _context.CupostT027NumeracionesSolicitud.FirstOrDefault(x => x.Pk_T027Codigo == value.Pk_T027Codigo && x.A027EstadoRegistro == StringHelper.estadoActivo);

                                if (numeracion != null)
                                {
                                    var cantidad = (numeracion.A027NumeroInternoFinal - numeracion.A027NumeroInternoInicial);

                                    if (nuevoNumeroInicial == 0)
                                    {
                                        if (datos.tipoSolicitud == "Precintos")
                                        {
                                            numeracion.A027NumeroInicialPrecintos = Convert.ToDecimal(numeracionPrecintos.A006numeroInicialNumerico);
                                            numeracion.A027NumeroFinalPrecintos = Convert.ToDecimal(numeracionPrecintos.A006numeroInicialNumerico + cantidad);
                                        }
                                        else
                                        {
                                            numeracion.A027NumeroInicialMarquillas = Convert.ToDecimal(numeracionPrecintos.A006numeroInicialNumerico);
                                            numeracion.A027NumeroFinalMarquillas = Convert.ToDecimal(numeracionPrecintos.A006numeroInicialNumerico + cantidad);
                                        }

                                        nuevoNumeroInicial = Convert.ToInt32(numeracionPrecintos.A006numeroInicialNumerico + cantidad);
                                    }
                                    else
                                    {
                                        if (datos.tipoSolicitud == "Precintos")
                                        {
                                            numeracion.A027NumeroInicialPrecintos = nuevoNumeroInicial;
                                            numeracion.A027NumeroFinalPrecintos = nuevoNumeroInicial + cantidad;
                                        }
                                        else
                                        {
                                            numeracion.A027NumeroInicialMarquillas = nuevoNumeroInicial;
                                            numeracion.A027NumeroFinalMarquillas = nuevoNumeroInicial + cantidad;
                                        }
                                    }

                                    _context.Update(numeracion);
                                    _ = _context.SaveChanges();
                                }
                            }
                        }
                    }

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaTrabajoValidacionSolicitud, null, null, null, 2, solicitudes, null);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgIntenteNuevamente, "", token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgIntenteNuevamente, "", token, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Responses ConsultDetail(ClaimsIdentity identity, string ipAddress, int? id)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var query = from spn in _context.CuposV003SolicitudPrecintosNacionales
                        select spn;

            query = query.Where(x => x.ID == id);

            return ResponseManager.generaRespuestaGenerica("", query.ToList(), token, false);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datos"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses ReturnRequest(ClaimsIdentity identity, ReturnSettledNationalSealsManagement datos, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (datos != null)
            {
                var solicitudes = _context.CupostT019Solicitudes.SingleOrDefault(x => x.Pk_T019Codigo == datos.code);

                if (solicitudes != null)
                {
                    solicitudes.A019FechaModificacion = now;
                    solicitudes.A019CodigoUsuarioModificacion = Convert.ToInt32(codigoUsuario);
                    solicitudes.A019EstadoSolicitud = configuration.GetValue<int>("EstadosCupos:EnRequerimiento:IdEstado");

                    if (solicitudes.A019Observaciones != datos.observations)
                    {
                        valorAnterior.Add(Convert.ToString(solicitudes.A019Observaciones) ?? "");
                        valorActual.Add(Convert.ToString(datos.observations));
                        campos.Add("A019Observaciones");
                    }

                    solicitudes.A019Observaciones = datos.observations;

                    _context.Update(solicitudes);
                    _ = _context.SaveChanges();

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaTrabajoValidacionSolicitud, valorAnterior, valorActual, campos, 3, null, null);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, true, token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <param name="code"></param>
        /// <param name="amount"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses GenerateDocument(ClaimsIdentity identity, string ipAddress, int? code, int? amount, string? color, decimal? codeIni, decimal? codeFin, string tipoCarta, string? tipoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }
            try
            {
                string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
                var codigoInicial = "";
                var codigoFinal = "";
                var now = DateTime.UtcNow;
                var year = now.Year;

                var detalleSolicitud = _context.CuposV003SolicitudPrecintosNacionales.Where(x => x.ID == code).FirstOrDefault();

                if (detalleSolicitud != null)
                {
                    var usuarioElabora = _context.AdmintT012Usuarios.Where(x => x.PkT012codigo == Convert.ToDecimal(codigoUsuario) && x.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    var coordinador = _context.AdmintT012Usuarios.Where(x => x.A012login.ToUpper() == "COORDINADORPRECINTOS" && x.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    var director = _context.AdmintT012Usuarios.Where(x => x.A012login.ToUpper() == "DIRECTORPRECINTOS" && x.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    var especie = _context.AdmintT005Especimen.FirstOrDefault(x => x.PkT005codigo == detalleSolicitud.CODIGOESPECIE && x.A005estadoRegistro == StringHelper.estadoActivo);
                    string nombreCompleto = (detalleSolicitud.PRIMERNOMBRE + " " + detalleSolicitud.SEGUNDONOMBRE + " " + detalleSolicitud.PRIMERAPELLIDO + " " + detalleSolicitud.SEGUNDOAPELLIDO).ToUpper();
                    
                    if (tipoSolicitud == "Precintos")
                    {
                        if(especie != null)
                        {
                            if (especie.A005nombreCientifico.Contains("fuscus") || especie.A005nombreCientifico.ToUpper() == "CAIMAN CROCODILUS")
                            {
                                codigoInicial = "MMAFUS" + year + "" + codeIni;
                                codigoFinal = "MMAFUS" + year + "" + codeFin;
                            }
                            else if (especie.A005nombreCientifico.Contains("apaporiensis"))
                            {
                                codigoInicial = "MMAAPA" + year + "" + codeIni;
                                codigoFinal = "MMAAPA" + year + "" + codeFin;
                            }
                            else if (especie.A005nombreCientifico.Contains("amazonicus"))
                            {
                                codigoInicial = "MMAAMA" + year + "" + codeIni;
                                codigoFinal = "MMAAMA" + year + "" + codeFin;
                            }
                            else if (especie.A005nombreCientifico.Contains("acutus"))
                            {
                                codigoInicial = "MMAACU" + year + "" + codeIni;
                                codigoFinal = "MMAACU" + year + "" + codeFin;
                            }
                            else
                            {
                                codigoInicial = "MMAINT" + year + "" + codeIni;
                                codigoFinal = "MMAINT" + year + "" + codeFin;
                            }
                        }
                    }
                    else
                    {
                        codigoInicial = "CO" + codeIni;
                        codigoFinal = "CO" + codeFin;
                    }

                    iTextSharp.text.Document doc = new iTextSharp.text.Document();
                    MemoryStream mem = new MemoryStream();
                    doc.SetMargins(80f, 80f, 10f, 10f);

                    PdfWriter writer = PdfWriter.GetInstance(doc, mem);
                    doc.Open();

                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"..\..\vital-cupos-app\CUPOS_FRONT\wwwroot\images\minambiente-logo.png");
                    imagen.BorderWidth = 0;
                    imagen.Alignment = Element.ALIGN_RIGHT;
                    float percentage = 0.0f;
                    percentage = 100 / imagen.Width;
                    imagen.ScalePercent(percentage * 250);

                    doc.Add(imagen);

                    Chunk texto_1 = new Chunk("Bogotá D.C.\r\n" + "\r\nDoctor(a) o Señor(a)");
                    texto_1.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    doc.Add(new Paragraph(texto_1));

                    Chunk nombre = new Chunk(nombreCompleto);
                    nombre.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    doc.Add(new Paragraph(nombre));

                    Chunk espacio = new Chunk("\r\n");
                    espacio.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

                    var solicitud = _context.CupostT019Solicitudes.FirstOrDefault(x => x.Pk_T019Codigo == code);

                    if (tipoSolicitud == "Precintos")
                    {
                        // Crear un Chunk para el texto antes de "detalleSolicitud.ESPECIE"
                        Chunk textoAntesEspecie = new Chunk("Representante Legal:\r\n" + detalleSolicitud.ESTABLECIMIENTO + "\r\n" + detalleSolicitud.DIRECCIONENTREGA + "\r\n" + detalleSolicitud.CIUDADENTREGA + "\r\n\r\nAsunto:\r\n" + detalleSolicitud.TIPOSOLICITUD + "\r\n\r\nSeñor(a):" + "\r\n" + detalleSolicitud.PRIMERNOMBRE + " " + detalleSolicitud.SEGUNDONOMBRE + " " + detalleSolicitud.PRIMERAPELLIDO + " " + detalleSolicitud.SEGUNDOAPELLIDO + "\r\n\r\nDe manera atenta procedemos a informarle que han sido asignados " + amount +
                            " precintos " + color + " de movilización nacional con la siguiente numeración: " +
                            codigoInicial + " a " + codigoFinal + " para pieles enteras crudas saladas de la especie ", FontFactory.GetFont(FontFactory.HELVETICA, 10));

                        // Crear un Chunk para "detalleSolicitud.ESPECIE" en cursiva
                        Chunk especieEnCursiva = new Chunk(detalleSolicitud.ESPECIE, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC));

                        // Crear un Chunk para el texto después de "detalleSolicitud.ESPECIE"
                        Chunk textoDespuesEspecie = new Chunk(" de acuerdo con la solicitud realizada " +
                            "con el radicado " + detalleSolicitud.NUMERORADICACION + ", y que a su vez ha sido informada a la empresa Alphex para su elaboración.", FontFactory.GetFont(FontFactory.HELVETICA, 10));

                        // Agregar los Chunks al párrafo
                        Paragraph paragraph = new Paragraph();
                        paragraph.Add(textoAntesEspecie);
                        paragraph.Add(especieEnCursiva);
                        paragraph.Add(textoDespuesEspecie);

                        // Agregar el párrafo al documento
                        doc.Add(paragraph);

                        doc.Add(new Paragraph(espacio));

                        PdfPTable table = new PdfPTable(5);
                        // Esta es la primera fila
                        table.AddCell("Numeración Interna Inicial");
                        table.AddCell("Numeración Interna Final");
                        table.AddCell("Código de zoocriadero de origen");
                        table.AddCell("Numero Resolución");
                        table.AddCell("Número de carta de venta y/o factura");

                        if (solicitud != null)
                        {
                            var codigosInternos = _context.CuposV004NumeracionesPrecintos.Where(x => x.ID == code).ToList();

                            if (codigosInternos != null)
                            {
                                List<CodigosInternos> codigos = new List<CodigosInternos>();

                                foreach (var cod in codigosInternos)
                                {
                                    CodigosInternos n = new CodigosInternos();

                                    CupostT002Cupo cupos = new CupostT002Cupo();

                                    if (cod.A027CODIGO_PARAMETRICA_ORIGEN_SOLICITUD == StringHelper.origenInventario)
                                    {
                                        cupos = _context.CupostT002Cupos.FirstOrDefault(x => cod.NUMEROINTERNOINICIAL >= x.A002rangoCodigoInicial && cod.NUMEROINTERNOFINAL <= x.A002rangoCodigoFin && x.A002estadoRegistro == StringHelper.estadoActivo && x.A002codigoEmpresa == cod.A027CODIGO_EMPRESA_ORIGEN_NUMERACIONES) ?? new CupostT002Cupo();
                                    }
                                    else
                                    {
                                        cupos = _context.CupostT002Cupos.FirstOrDefault(x => cod.NUMEROINTERNOINICIAL >= x.A002rangoCodigoInicial && cod.NUMEROINTERNOFINAL <= x.A002rangoCodigoFin && x.A002estadoRegistro == StringHelper.estadoActivo && x.A002codigoEmpresa == solicitud.A019CodigoEmpresa) ?? new CupostT002Cupo();
                                    }


                                    if (cupos != null)
                                    {
                                        var cartaVenta = _context.CupostT026FacturaCompraCupo.FirstOrDefault(x => x.A026CodigoCupo == cupos.PkT002codigo && cod.NUMEROINTERNOINICIAL >= x.A026NumeracionInicial && cod.NUMEROINTERNOFINAL <= x.A026NumeracionFinal);

                                        if (cartaVenta != null)
                                        {
                                            var cartaVentaNumero = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == cartaVenta.A026CodigoFacturaCompra).FirstOrDefault();

                                            if (cartaVentaNumero != null)
                                            {
                                                n.carta = Convert.ToString(cartaVentaNumero.A004numeroCartaVenta);
                                            }
                                        }
                                        else
                                        {
                                            n.carta = "No Aplica";
                                        }

                                        n.inicial = cod.NUMEROINTERNOINICIAL;
                                        n.final = cod.NUMEROINTERNOFINAL;
                                        n.resolucion = cupos.A002numeroResolucion;
                                        n.zoocriadero = cupos.A002CodigoZoocriadero;

                                        codigos.Add(n);
                                    }
                                    else
                                    {
                                        var especiesExportar = _context.CupostT005Especieaexportars.FirstOrDefault(x => cod.NUMEROINTERNOINICIAL >= x.A005NumeroInternoInicialCuotaRepoblacion && cod.NUMEROINTERNOFINAL <= x.A005NumeroInternoFinalCuotaRepoblacion && x.A005estadoRegistro == StringHelper.estadoActivo);

                                        if (especiesExportar != null)
                                        {
                                            var cartaVenta = _context.CupostT026FacturaCompraCupo.FirstOrDefault(x => x.A026CodigoCupo == especiesExportar.A005codigoCupo);

                                            if (cartaVenta != null)
                                            {
                                                n.carta = Convert.ToString(cartaVenta.A026CodigoFacturaCompra);
                                            }
                                            else
                                            {
                                                n.carta = "No Aplica";
                                            }

                                            var cuposEspecies = _context.CupostT002Cupos.FirstOrDefault(x => x.PkT002codigo == especiesExportar.A005codigoCupo && x.A002estadoRegistro == StringHelper.estadoActivo && x.A002codigoEmpresa == solicitud.A019CodigoEmpresa);

                                            if (cuposEspecies != null)
                                            {
                                                n.inicial = cod.NUMEROINTERNOINICIAL;
                                                n.final = cod.NUMEROINTERNOFINAL;
                                                n.resolucion = cuposEspecies.A002numeroResolucion;

                                                codigos.Add(n);
                                            }
                                        }
                                    }
                                }

                                for (var i = 0; i < codigos.Count; i++)
                                {
                                    table.AddCell("" + codigos[i].inicial);
                                    table.AddCell("" + codigos[i].final);
                                    table.AddCell("" + codigos[i].zoocriadero);
                                    table.AddCell("" + codigos[i].resolucion);
                                    table.AddCell("" + codigos[i].carta);
                                }

                                doc.Add(table);
                            }
                        }

                    }
                    else if (tipoSolicitud == "Marquillas con verificacion de corte certificada por ministerio" || tipoSolicitud == "Marquillas con verificación de corte certificada por ministerio")
                    {
                        // Crear un Chunk para el texto antes de "detalleSolicitud.ESPECIE"
                        Chunk textoAntesEspecie_21 = new Chunk("Representante Legal:\r\n" + detalleSolicitud.ESTABLECIMIENTO + "\r\n" + detalleSolicitud.DIRECCIONENTREGA + "\r\n" + detalleSolicitud.CIUDADENTREGA + "\r\n\r\nAsunto:\r\n" + detalleSolicitud.TIPOSOLICITUD + "\r\n\r\nSeñor(a):" + "\r\n" + detalleSolicitud.PRIMERNOMBRE + " " + detalleSolicitud.SEGUNDONOMBRE + " " + detalleSolicitud.PRIMERAPELLIDO + " " + detalleSolicitud.SEGUNDOAPELLIDO + "\r\n\r\nDe manera atenta procedemos a informarle que han sido asignadas " + amount +
                            " marquillas " + color + " para " + amount + " artículos elaborados en piel ", FontFactory.GetFont(FontFactory.HELVETICA, 10));

                        // Crear un Chunk para "detalleSolicitud.ESPECIE" en cursiva
                        Chunk especieEnCursiva_21 = new Chunk(detalleSolicitud.ESPECIE, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC));

                        // Crear un Chunk para el texto después de "detalleSolicitud.ESPECIE"
                        Chunk textoDespuesEspecie_21 = new Chunk(". Se informa que la numeración otorgada es " +
                            codigoInicial + " a " + codigoFinal +
                            ", de acuerdo con la solicitud realizada con el radicado " + detalleSolicitud.NUMERORADICACION + ", y que a su vez ha sido informada a la empresa Alphex para su elaboración.\r\n\r\n", FontFactory.GetFont(FontFactory.HELVETICA, 10));

                        // Agregar los Chunks al párrafo
                        Paragraph paragraph_21 = new Paragraph();
                        paragraph_21.Add(textoAntesEspecie_21);
                        paragraph_21.Add(especieEnCursiva_21);
                        paragraph_21.Add(textoDespuesEspecie_21);

                        // Agregar el párrafo al documento
                        doc.Add(paragraph_21);

                        doc.Add(new Paragraph(espacio));

                        PdfPTable tableMin = new PdfPTable(4);
                        // Esta es la primera fila
                        tableMin.AddCell("Procedencia");
                        tableMin.AddCell("Tipo de fracción");
                        tableMin.AddCell("Cantidad de productos");
                        tableMin.AddCell("Salvoconducto de movilización");

                        if (solicitud != null)
                        {
                            var safeGu = _context.CupostT028SalvoconductosSolicitud.Where(p => p.A028CodigoSolicitud == solicitud.Pk_T019Codigo).ToList();
                            var cutinn = _context.CupostT029CortesPielSolicitud.Where(p => p.A029CodigoSolicitud == solicitud.Pk_T019Codigo).ToList();

                            List<SafeGua> safe = new List<SafeGua>();
                            List<Cut> corte = new List<Cut>();

                            foreach (var n in safeGu)
                            {
                                var safeGuard = _context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.PK_T017Codigo == n.A028CodigoActaVisitaSalvoconducto).FirstOrDefault();

                                SafeGua safeG = new SafeGua();

                                safeG.codigoSalvo = safeGuard != null ? safeGuard.A017SalvoConductoNumero : 0;

                                safe.Add(safeG);
                            }

                            foreach (var n in cutinn)
                            {
                                var cutting = _context.CupostT008CortePiels.Where(p => p.A008codigo == n.A029CodigoCortePiel).FirstOrDefault();
                                var acta = _context.CupostT007ActaVisita.Where(p => p.PkT007codigo == (cutting != null ? cutting.A008codigoActaVisita : 0)).FirstOrDefault();
                                Cut cort = new Cut();

                                cort.fechaActa = acta != null ? acta.A007fechaActa.ToShortDateString() : "";
                                cort.cantidad = n.A029Cantidad;
                                cort.FractionType = cutting != null ? cutting.A008tipoParte : "";

                                corte.Add(cort);
                            }

                            var arr = safe.Select(p => p.codigoSalvo).ToArray();

                            var sa = string.Join(",", arr);

                            int rowCount = corte.Count;

                            for (var i = 0; i < corte.Count; i++)
                            {
                                tableMin.AddCell("Acta de visita de corte (" + corte[i].fechaActa + ")");
                                tableMin.AddCell("" + corte[i].FractionType);
                                tableMin.AddCell("" + corte[i].cantidad);

                                if (i == 0)
                                {
                                    // Agregar la última columna "Salvoconducto de movilización" en la primera iteración
                                    PdfPCell cell = new PdfPCell(new Phrase(new Phrase("" + sa)));
                                    cell.Rowspan = rowCount;
                                    tableMin.AddCell(cell);
                                }
                            }

                            doc.Add(tableMin);
                        }
                    }
                    else
                    {
                        // Crear un Chunk para el texto antes de "detalleSolicitud.ESPECIE"
                        Chunk textoAntesEspecie_22 = new Chunk("Representante Legal:\r\n" + detalleSolicitud.ESTABLECIMIENTO + "\r\n" + detalleSolicitud.DIRECCIONENTREGA + "\r\n" + detalleSolicitud.CIUDADENTREGA + "\r\n\r\nAsunto:\r\n" + detalleSolicitud.TIPOSOLICITUD + "\r\n\r\nSeñor(a):" + "\r\n" + detalleSolicitud.PRIMERNOMBRE + " " + detalleSolicitud.SEGUNDONOMBRE + " " + detalleSolicitud.PRIMERAPELLIDO + " " + detalleSolicitud.SEGUNDOAPELLIDO + "\r\n\r\nDe manera atenta procedemos a informarle que han sido asignadas " + amount +
                            " maquillas " + color + " para " + amount + " artículos elaborados en piel ", FontFactory.GetFont(FontFactory.HELVETICA, 10));

                        // Crear un Chunk para "detalleSolicitud.ESPECIE" en cursiva
                        Chunk especieEnCursiva_22 = new Chunk(detalleSolicitud.ESPECIE, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC));

                        // Crear un Chunk para el texto después de "detalleSolicitud.ESPECIE"
                        Chunk textoDespuesEspecie_22 = new Chunk(". Se informa que la numeración otorgada es " +
                            codigoInicial + " a " + codigoFinal +
                            ", de acuerdo con la solicitud realizada con el radicado " + detalleSolicitud.NUMERORADICACION + ", y que a su vez ha sido informada a la empresa Alphex para su elaboración.", FontFactory.GetFont(FontFactory.HELVETICA, 10));

                        // Agregar los Chunks al párrafo
                        Paragraph paragraph_22 = new Paragraph();
                        paragraph_22.Add(textoAntesEspecie_22);
                        paragraph_22.Add(especieEnCursiva_22);
                        paragraph_22.Add(textoDespuesEspecie_22);

                        // Agregar el párrafo al documento
                        doc.Add(paragraph_22);

                    }

                    doc.Add(new Paragraph(espacio));

                    Chunk texto_3 = new Chunk("\r\nCordialmente,\r\n\r\n");
                    texto_3.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    doc.Add(new Paragraph(texto_3));

                    if (tipoCarta == "cartaFirmada")
                    {
                        iTextSharp.text.Image imagenFirma = iTextSharp.text.Image.GetInstance(@"..\..\vital-cupos-app\CUPOS_FRONT\wwwroot\images\FirmaDirector.png");
                        imagenFirma.BorderWidth = 0;
                        imagenFirma.Alignment = Element.ALIGN_LEFT;
                        float percentageFirma = 0.0f;
                        percentageFirma = 150 / imagenFirma.Width;
                        imagenFirma.ScalePercent(percentageFirma * 150);

                        doc.Add(imagenFirma);
                    }

                    Chunk texto_4 = new Chunk("\r\n" + (director != null ? director.A012primerNombre : "") + " " + (director != null ? director.A012segundoNombre : "") + " " + (director != null ? director.A012primerApellido : "") + " " + (director != null ? director.A012segundoApellido : "") + "\r\nDirector Dirección de Bosques, Biodiversidad y Servicion Ecositémicos\r\n\r\n" + "Elaborado: " + (usuarioElabora != null ? usuarioElabora.A012primerNombre : "") + " " + (usuarioElabora != null ? usuarioElabora.A012primerApellido : "") + " - Analista\r\n" + "Reviso: " + (coordinador != null ? coordinador.A012primerNombre : "") + " " + (coordinador != null ? coordinador.A012primerApellido : ""));
                    texto_4.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    doc.Add(new Paragraph(texto_4));

                    Chunk pie = new Chunk("Los arriba firmantes declaramos que hemos revisado el presente documento y lo encontramos ajustado a las normas y disposiciones legales y/o técnicas vigentes y, por lo tanto, bajo nuestra responsabilidad lo presentamos para la firma del Remitente");
                    pie.Font = FontFactory.GetFont(FontFactory.HELVETICA, 6);
                    doc.Add(new Paragraph(pie));

                    Chunk informacion = new Chunk("\r\n\r\n\r\nCalle 37 No. 8 - 40\r\nConmutador: +57 6013323400\r\nwww.minambiente.gov.co\r\nBogotá, Colombia");
                    informacion.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    doc.Add(new Paragraph(informacion));

                    doc.Close();

                    var pdf = mem.ToArray();
                    var pdf64 = Convert.ToBase64String(pdf);

                    var documento = new Files()
                    {
                        codigo = code,
                        adjuntoBase64 = pdf64,
                        nombreAdjunto = code + ".pdf",
                        tipoAdjunto = "application/pdf"
                    };

                    SaveFileFtp(identity, documento, "generarCarta");

                    return ResponseManager.generaRespuestaGenerica("", documento, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica("", StringHelper.msgIntenteNuevamente, token, true);
                }
            }
            catch (Exception)
            {
                return ResponseManager.generaRespuestaGenerica("", StringHelper.msgIntenteNuevamente, token, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documento"></param>
        /// <returns></returns>
        public string SaveFileFtp(ClaimsIdentity identity, Files documento, string estado)
        {

            int codigoParametricaTipoDocumento = 0;
            string nombreDocumento = "";

            if (estado == "generarCarta" || estado == "cartaFirmada")
            {
                codigoParametricaTipoDocumento = 10170;
                nombreDocumento = "CARTA-SOLICITUD-" + documento.codigo + ".pdf";
            }
            else if (estado == "cartaDesistimiento")
            {
                codigoParametricaTipoDocumento = 10169;
                nombreDocumento = "SOPORTE-DESISTIMIENTO-" + documento.codigo + ".pdf";
            }

            var documentos = (from a in _context.CupostT020RlSolicitudesDocumento
                              join b in _context.AdmintT009Documentos on a.A020CodigoDocumento equals b.PkT009codigo
                              where a.A020CodigoSolicitud == documento.codigo
                              && b.A009codigoParametricaTipoDocumento == codigoParametricaTipoDocumento
                              select new { a.Pk_T020Codigo, b.PkT009codigo });

            if (documentos.Count() > 0)
            {
                List<Numeration> list = new List<Numeration>();

                foreach (var docum in documentos)
                {
                    Numeration n = new Numeration();

                    n.initial = docum.Pk_T020Codigo;
                    n.origen = docum.PkT009codigo;

                    list.Add(n);
                }

                var documentoSolicitud = _context.CupostT020RlSolicitudesDocumento.SingleOrDefault(x => x.Pk_T020Codigo == list[0].initial);
                var documentoAdministrador = _context.AdmintT009Documentos.SingleOrDefault(x => x.PkT009codigo == list[0].origen);

                _context.Remove(documentoSolicitud);
                _ = _context.SaveChanges();

                _context.Remove(documentoAdministrador);
                _ = _context.SaveChanges();
            }

            documento.nombreAdjunto = nombreDocumento;

            string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

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

            string eliminar = "data:" + documento.tipoAdjunto + ";base64,";
            string SinData = documento.adjuntoBase64?.Replace(eliminar, String.Empty) ?? "";

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

            var document = new DocumentInformation()
            {
                codigo = documento.codigo,
                nombreArchivo = documento.codigo + ".pdf",
                url = uri
            };

            List<DocumentInformation> listDocumento = new List<DocumentInformation>();
            listDocumento.Add(document);

            AdmintT009Documento doc = new AdmintT009Documento();

            doc.A009codigoUsuarioCreacion = Convert.ToInt32(codigoUsuario);
            doc.A009codigoUsuarioModificacion = null;
            doc.A009codigoParametricaTipoDocumento = codigoParametricaTipoDocumento;
            doc.A009codigoPlantilla = 1;
            doc.A009estadoRegistro = StringHelper.estadoActivo;
            doc.A009fechaCreacion = DateTime.Now;
            doc.A009fechaModificacion = null;
            doc.A009firmaDigital = "firma";
            doc.A009documento = documento.nombreAdjunto;
            doc.A009descripcion = documento.nombreAdjunto;
            doc.A009url = uri;

            _context.AdmintT009Documentos.Add(doc);
            _context.SaveChanges();

            var idDocumento = doc.PkT009codigo;

            CupostT020RlSolicitudesDocumento solDoc = new CupostT020RlSolicitudesDocumento();

            solDoc.A020CodigoSolicitud = documento.codigo;
            solDoc.A020CodigoDocumento = idDocumento;
            solDoc.A020CodigoUsuarioCreacion = Convert.ToInt32(codigoUsuario);
            solDoc.A020CodigoUsuarioModificacion = null;
            solDoc.A020FechaCreacion = DateTime.Now;
            solDoc.A020FechaModificacion = null;
            solDoc.A020EstadoRegistro = StringHelper.estadoActivo;

            _context.CupostT020RlSolicitudesDocumento.Add(solDoc);
            _context.SaveChanges();

            GetFiles(listDocumento);

            return uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses DocumentSeal(ClaimsIdentity identity, string ipAddress, int code, int type)
        {
            List<DocumentInformation> listDocumento = new List<DocumentInformation>();
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var query = from a in _context.AdmintT009Documentos
                        join b in _context.CupostT020RlSolicitudesDocumento
                        on a.PkT009codigo equals b.A020CodigoDocumento
                        where b.A020CodigoSolicitud == code &&
                        a.A009codigoParametricaTipoDocumento == type
                        select new
                        {
                            a.A009url,
                            a.A009documento
                        };

            if (query != null)
            {
                foreach (var q in query)
                {
                    var document = new DocumentInformation()
                    {
                        codigo = code,
                        nombreArchivo = q.A009documento,
                        url = q.A009url
                    };

                    listDocumento.Add(document);
                }

                GetFiles(listDocumento);

                return ResponseManager.generaRespuestaGenerica("", documentoEnviar != null ? documentoEnviar: "", token, false);
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("", "", token, false);
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses GetFiles(List<DocumentInformation> documentos)
        {
            if (documentos.Any())
            {
                List<Files> filesList = new List<Files>();
                var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
                string usuario = "";
                string clave = "";
                foreach (var c in query1)
                {
                    switch (c.A008descripcion)
                    {
                        case "USUARIO":
                            usuario = c.A008valor;
                            break;
                        case "CONTRASEÑA":
                            clave = c.A008valor;
                            break;
                    }
                }

                foreach (var doc in documentos)
                {
                    bool esPdf = doc.url?.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                    bool esDocx = doc.url?.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                    bool esJpeg = doc.url?.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                    bool esPng = doc.url?.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                    bool esJpg = doc.url?.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase) ?? false;
                    bool esXlsx = doc.url?.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase) ?? false;

                    #pragma warning disable SYSLIB0014
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(doc.url);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    request.Credentials = new NetworkCredential(usuario, clave);

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();

                    var base64 = ConvertToBase64(responseStream);

                    string tipoAdjunto = "";

                    if (esPdf)
                    {
                        tipoAdjunto = "application/pdf";
                    }
                    else if (esDocx)
                    {
                        tipoAdjunto = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }
                    else if (esJpeg)
                    {
                        tipoAdjunto = "image/jpeg";
                    }
                    else if (esPng)
                    {
                        tipoAdjunto = "image/png";
                    }
                    else if (esJpg)
                    {
                        tipoAdjunto = "image/jpg";
                    }
                    else if (esXlsx)
                    {
                        tipoAdjunto = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }

                    Files documento = new Files();
                    documento.codigo = doc.codigo;
                    documento.adjuntoBase64 = "data:" + tipoAdjunto + ";base64," + base64;
                    documento.nombreAdjunto = doc.nombreArchivo;
                    documento.tipoAdjunto = tipoAdjunto;

                    filesList.Add(documento);
                }

                documentoEnviar = filesList;

                return ResponseManager.generaRespuestaGenerica("", filesList, "", false);
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("", "", "", true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datos"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses WithdrawRequest(ClaimsIdentity identity, DesistNationalSealsManagement datos, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (datos != null)
            {
                var solicitudes = _context.CupostT019Solicitudes.SingleOrDefault(x => x.Pk_T019Codigo == datos.code && x.A019EstadoRegistro == StringHelper.estadoActivo);

                if (solicitudes != null)
                {
                    solicitudes.A019FechaModificacion = now;
                    solicitudes.A019FechaCambioEstado = now;
                    solicitudes.A019EstadoSolicitud = configuration.GetValue<int>("EstadosCupos:Desistido:IdEstado");

                    if (solicitudes.A019ObservacionesDesistimiento != datos.observations)
                    {
                        valorAnterior.Add(solicitudes.A019ObservacionesDesistimiento ?? "");
                        valorActual.Add(datos.observations ?? "");
                        campos.Add("A019ObservacionesDesistimiento");
                    }

                    solicitudes.A019ObservacionesDesistimiento = datos.observations;

                    var documento = new Files()
                    {
                        codigo = datos.file.codigo,
                        adjuntoBase64 = datos.file.adjuntoBase64,
                        nombreAdjunto = datos.file.nombreAdjunto,
                        tipoAdjunto = datos.file.tipoAdjunto

                    };

                    SaveFileFtp(identity, documento, "cartaDesistimiento");

                    _context.Update(solicitudes);
                    _ = _context.SaveChanges();

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaTrabajoValidacionSolicitud, valorAnterior, valorActual, campos, 3, null, null);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, true, token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datos"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses SignDocumentRequest(ClaimsIdentity identity, SignApplicationDocument datos, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string tipoCarta = "cartaFirmada";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (datos != null)
            {
                var solicitudes = _context.CupostT019Solicitudes.FirstOrDefault(x => x.Pk_T019Codigo == datos.code && x.A019EstadoRegistro == StringHelper.estadoActivo);

                if (solicitudes != null)
                {
                    solicitudes.A019FechaModificacion = now;
                    solicitudes.A019EstadoSolicitud = configuration.GetValue<int>("EstadosCupos:Aprobado:IdEstado");

                    var codigoColor = _context.CupostT006Precintosymarquillas.FirstOrDefault(x => x.A006codigoSolicitud == datos.code && x.A006estadoRegistro == StringHelper.estadoActivo);

                    if (codigoColor != null)
                    {
                        var nombreColor = _context.AdmintT008Parametricas.FirstOrDefault(x => x.PkT008codigo == codigoColor.A006codigoParametricaColorPrecintosymarquillas && x.A008estadoRegistro == StringHelper.estadoActivo);
                        var tipoSolicitud = _context.AdmintT008Parametricas.FirstOrDefault(p => p.PkT008codigo == solicitudes.A019TipoSolicitud);
                        var preciontoMarquilla = _context.CupostT006Precintosymarquillas.FirstOrDefault(p => p.A006codigoSolicitud == datos.code);
                        if (nombreColor != null && tipoSolicitud != null)
                        {
                            GenerateDocument(identity, ipAddress, datos.code, Convert.ToInt32(solicitudes.A019Cantidad), nombreColor.A008valor, preciontoMarquilla != null ? preciontoMarquilla.A006numeroInicialNumerico  : 0, preciontoMarquilla != null ? preciontoMarquilla.A006numeroFinalNumerico : 0, tipoCarta, tipoSolicitud.A008valor);
                        }
                    }

                    _context.Update(solicitudes);
                    _ = _context.SaveChanges();

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smBandejaTrabajoValidacionSolicitud, valorAnterior, valorActual, campos, 3, null, null);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, true, token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, true, token, true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Responses CheckCodesSeals(ClaimsIdentity identity, int code)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var query = (from a in _context.CupostT006Precintosymarquillas
                         join b in _context.CupostT019Solicitudes on a.A006codigoSolicitud equals b.Pk_T019Codigo
                         where b.Pk_T019Codigo == code
                         select new { a.A006numeroInicial, a.A006numeroFinal, b.A019Cantidad });

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Responses CheckCodesSealsMin(ClaimsIdentity identity, int code)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var tipoSolicitud = _context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == code).FirstOrDefault();

            var safeG = _context.CupostT028SalvoconductosSolicitud.Where(p => p.A028CodigoSolicitud == (tipoSolicitud != null ? tipoSolicitud.Pk_T019Codigo  : 0)).ToList();
            var fraction = _context.CupostT029CortesPielSolicitud.Where(p => p.A029CodigoSolicitud == (tipoSolicitud != null ? tipoSolicitud.Pk_T019Codigo : 0)).ToList();

            List<decimal> safe = new List<decimal>();
            List<ValCut> corte = new List<ValCut>();

            foreach (var val in safeG)
            {

                var codActSafe = _context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.PK_T017Codigo == val.A028CodigoActaVisitaSalvoconducto).FirstOrDefault();

                if (codActSafe != null)
                    safe.Add(codActSafe.A017SalvoConductoNumero);
            }

            var arr = string.Join(",", safe.ToArray());

            foreach (var val in fraction)
            {
                var codCorteP = _context.CupostT008CortePiels.Where(p => p.A008codigo == val.A029CodigoCortePiel).FirstOrDefault();

                ValCut n = new ValCut();

                n.cuttingCode = val.Pk_T029Codigo;
                n.cantCut = val.A029Cantidad;
                n.areaCut = val.A029AreaTotal;
                n.safeGuard = arr;

                if (codCorteP != null)
                    n.tipoPart = codCorteP.A008tipoParte;

                corte.Add(n);
            }

            return ResponseManager.generaRespuestaGenerica("", corte, token, false);

        }



        /// <summary>
        /// /
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public Responses CheckApplicationNumbers(ClaimsIdentity identity, int idSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var codSol = _context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == idSolicitud).FirstOrDefault();

            if (codSol != null)
            {
                if (codSol.A019TipoSolicitud == 20206 || codSol.A019TipoSolicitud == 10200)
                {
                    var numeraciones = _context.CupostT027NumeracionesSolicitud.Where(p => p.A027CodigoSolicitud == idSolicitud && p.A027EstadoRegistro == StringHelper.estadoActivo).ToList();

                    List<Numeration> listnumer = new List<Numeration>();

                    foreach (var num in numeraciones)
                    {
                        Numeration n = new Numeration();

                        n.initial = num.A027NumeroInternoInicial;
                        n.final = num.A027NumeroInternoFinal;

                        listnumer.Add(n);
                    }

                    return ResponseManager.generaRespuestaGenerica("", listnumer, token, false);

                }
                else
                {
                    var safeG = _context.CupostT028SalvoconductosSolicitud.Where(p => p.A028CodigoSolicitud == codSol.Pk_T019Codigo).ToList();
                    var fraction = _context.CupostT029CortesPielSolicitud.Where(p => p.A029CodigoSolicitud == codSol.Pk_T019Codigo).ToList();

                    CuttingSafeGuar cuttingSafeGuar = new CuttingSafeGuar();

                    List<decimal> safe = new List<decimal>();
                    List<ValCut> corte = new List<ValCut>();

                    foreach (var val in safeG)
                    {

                        var codActSafe = _context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.PK_T017Codigo == val.A028CodigoActaVisitaSalvoconducto).FirstOrDefault();

                        if (codActSafe != null)
                            safe.Add(codActSafe.A017SalvoConductoNumero);
                    }

                    foreach (var val in fraction)
                    {
                        var codCorteP = _context.CupostT008CortePiels.Where(p => p.A008codigo == val.A029CodigoCortePiel).FirstOrDefault();

                        ValCut n = new ValCut();

                        n.cantCut = val.A029Cantidad;
                        n.areaCut = val.A029AreaTotal;

                        if (codCorteP != null)
                            n.tipoPart = codCorteP.A008tipoParte;

                        corte.Add(n);
                    }

                    cuttingSafeGuar.Cut = corte;
                    cuttingSafeGuar.salvoCon = safe;

                    return ResponseManager.generaRespuestaGenerica("", cuttingSafeGuar, token, false);

                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgSolicitudNoEncontrada, "", token, true);
            }

        }
        /// <summary>
		/// /
		/// </summary>
		/// <param name="identity"></param>
		/// <param name="idSolicitud"></param>
		/// <returns></returns>
		public Responses ConsultNitCompany(ClaimsIdentity identity, int idSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }
            List<NitEmpresa> listNit = new List<NitEmpresa>();

            var solicitud = _context.CupostT019Solicitudes.FirstOrDefault(p => p.Pk_T019Codigo == idSolicitud && p.A019EstadoRegistro == StringHelper.estadoActivo);

            if (solicitud != null)
            {
                var nitEmpresa = _context.CupostT001Empresas.Where(p => p.PkT001codigo == solicitud.A019CodigoEmpresa && p.A001estadoRegistro == StringHelper.estadoActivo).ToList();

                foreach (var nit in nitEmpresa)
                {
                    NitEmpresa n = new NitEmpresa();

                    n.a001nit = nit.A001nit;

                    listNit.Add(n);
                }
            }

            return ResponseManager.generaRespuestaGenerica("", listNit, token, false);
        }
        /// <summary>
		/// /
		/// </summary>
		/// <param name="identity"></param>
		/// <param name="idSolicitud"></param>
		/// <returns></returns>
		public Responses ConsultColorSeal(ClaimsIdentity identity, int idSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            List<ColorPrecinto> listColor = new List<ColorPrecinto>();

            var codigoColor = _context.CupostT006Precintosymarquillas.FirstOrDefault(p => p.A006codigoSolicitud == idSolicitud && p.A006estadoRegistro == StringHelper.estadoActivo);

            if (codigoColor != null)
            {
                var nombreColor = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == codigoColor.A006codigoParametricaColorPrecintosymarquillas && p.A008estadoRegistro == StringHelper.estadoActivo).ToList();

                foreach (var color in nombreColor)
                {
                    ColorPrecinto n = new ColorPrecinto();

                    n.a008valor = color.A008valor;

                    listColor.Add(n);
                }
            }

            return ResponseManager.generaRespuestaGenerica("", listColor, token, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="correo"></param>
        /// <param name="body"></param>
        /// <param name="asunto"></param>
        /// <param name="codigoSolicitud"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public async Task<Responses> SendApprovedMail(ClaimsIdentity identity, List<MailApproval> datos)
        {
            List<DocumentInformation> listDocumento = new List<DocumentInformation>();

            var codigoInicial = _context.CupostT006Precintosymarquillas.FirstOrDefault(p => p.A006codigoSolicitud == datos[0].code && p.A006estadoRegistro == StringHelper.estadoActivo);
            var codigoFinal = _context.CupostT006Precintosymarquillas.OrderByDescending(p => p.PkT006codigo).FirstOrDefault(p => p.A006codigoSolicitud == datos[0].code && p.A006estadoRegistro == StringHelper.estadoActivo);

            var tipoSolicitud = _context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == datos[0].code).FirstOrDefault();
            var detalleSolicitud = _context.CuposV003SolicitudPrecintosNacionales.Where(x => x.ID == datos[0].code).FirstOrDefault();
            var body = new StringBuilder();

            if (tipoSolicitud != null)
            {
                if (tipoSolicitud.A019TipoSolicitud == 10200)
                {
                    body.Append($"<table style=\"border: black 1px solid; border-collapse:collapse; width:100%;\"><thead><tr><th style=\"border: black 1px solid; padding:5px;\">Radicado</th><th style=\"border: black 1px solid; padding:5px;\">Fecha Radicación</th><th style=\"border: black 1px solid; padding:5px;\">Establecimiento</th><th style=\"border: black 1px solid; padding:5px;\">Nit</th><th style=\"border: black 1px solid; padding:5px;\">Ciudad</th><th style=\"border: black 1px solid; padding:5px;\">Dirección</th><th style=\"border: black 1px solid; padding:5px;\">Teléfono</th><th style=\"border: black 1px solid; padding:5px;\">Cantidad Total</th><th style=\"border: black 1px solid; padding:5px;\">Color</th><th style=\"border: black 1px solid; padding:5px;\">Codificación Inicial</th><th style=\"border: black 1px solid; padding:5px;\">Codificación Final</th><th style=\"border: black 1px solid; padding:5px;\">Código de zoocriadero de origen</th><th style=\"border: black 1px solid; padding:5px;\">Numeración Interna Inicial</th><th style=\"border: black 1px solid; padding:5px;\">Numeración Interna Final</th><th style=\"border: black 1px solid; padding:5px;\">Subtotal</th><th style=\"border: black 1px solid; padding:5px;\">Valor de la consignación</th><th style=\"border: black 1px solid; padding:5px;\">Fecha de Envío</th><th style=\"border: black 1px solid; padding:5px;\">Analista</th></tr></thead><tbody>");
                }
                else if (tipoSolicitud.A019TipoSolicitud == 20207)
                {
                    body.Append($"<table style=\"border: black 1px solid; border-collapse:collapse; width:100%;\"><thead><tr><th style=\"border: black 1px solid; padding:5px;\">Radicado</th><th style=\"border: black 1px solid; padding:5px;\">Fecha Radicación</th><th style=\"border: black 1px solid; padding:5px;\">Establecimiento</th><th style=\"border: black 1px solid; padding:5px;\">Nit</th><th style=\"border: black 1px solid; padding:5px;\">Ciudad</th><th style=\"border: black 1px solid; padding:5px;\">Dirección</th><th style=\"border: black 1px solid; padding:5px;\">Teléfono</th><th style=\"border: black 1px solid; padding:5px;\">Cantidad Total</th><th style=\"border: black 1px solid; padding:5px;\">Color</th><th style=\"border: black 1px solid; padding:5px;\">Codificación Inicial</th><th style=\"border: black 1px solid; padding:5px;\">Codificación Final</th><th style=\"border: black 1px solid; padding:5px;\">Cantidad fraciones</th><th style=\"border: black 1px solid; padding:5px;\">Area total</th><th style=\"border: black 1px solid; padding:5px;\">Tipos de fracción</th><th style=\"border: black 1px solid; padding:5px;\">Salvoconductos</th><th style=\"border: black 1px solid; padding:5px;\">Subtotal</th><th style=\"border: black 1px solid; padding:5px;\">Valor de la consignación</th><th style=\"border: black 1px solid; padding:5px;\">Fecha de Envío</th><th style=\"border: black 1px solid; padding:5px;\">Analista</th></tr></thead><tbody>");
                }
                else
                {
                    body.Append($"<table style=\"border: black 1px solid; border-collapse:collapse; width:100%;\"><thead><tr><th style=\"border: black 1px solid; padding:5px;\">Radicado</th><th style=\"border: black 1px solid; padding:5px;\">Fecha Radicación</th><th style=\"border: black 1px solid; padding:5px;\">Establecimiento</th><th style=\"border: black 1px solid; padding:5px;\">Nit</th><th style=\"border: black 1px solid; padding:5px;\">Ciudad</th><th style=\"border: black 1px solid; padding:5px;\">Dirección</th><th style=\"border: black 1px solid; padding:5px;\">Teléfono</th><th style=\"border: black 1px solid; padding:5px;\">Cantidad Total</th><th style=\"border: black 1px solid; padding:5px;\">Color</th><th style=\"border: black 1px solid; padding:5px;\">Codificación Inicial</th><th style=\"border: black 1px solid; padding:5px;\">Codificación Final</th><th style=\"border: black 1px solid; padding:5px;\">Subtotal</th><th style=\"border: black 1px solid; padding:5px;\">Valor de la consignación</th><th style=\"border: black 1px solid; padding:5px;\">Fecha de Envío</th><th style=\"border: black 1px solid; padding:5px;\">Analista</th></tr></thead><tbody>");
                }
                foreach (MailApproval mailApproval in datos)
                {
                    mailApproval.city = detalleSolicitud != null ? detalleSolicitud.CIUDADENTREGA : "";

                    if (tipoSolicitud.A019TipoSolicitud == 10200)
                    {
                        var codes = _context.CupostT027NumeracionesSolicitud.Where(p => p.Pk_T027Codigo == mailApproval.codigonumeraciones).FirstOrDefault();

                        mailApproval.initialNumbering = codigoInicial != null ? codigoInicial.A006numeroInicial : "";
                        mailApproval.finalNumbering = codigoFinal != null ? codigoFinal.A006numeroFinal : "";

                        mailApproval.initialInternalCoding = codes != null ? Convert.ToString(codes.A027NumeroInternoInicial): "";
                        mailApproval.finalInternalCoding = codes != null ? Convert.ToString(codes.A027NumeroInternoFinal) : "";
                        body.Append($"<tr><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.numberradication}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.filingDate?.ToString("dd / MM / yyyy")}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.establishment}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.nit}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.city}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.address}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.phone}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.amount}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.color}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.initialNumbering}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.finalNumbering}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.zoo}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.initialInternalCoding}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.finalInternalCoding}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.subtotal}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.consignmentValue}</th><th style=\"border: black 1px solid; padding:5px;\">{DateTime.Now.ToString("dd / MM / yyyy")}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.analyst}</th></tr>");
                    }
                    else if (tipoSolicitud.A019TipoSolicitud == 20207)
                    {
                        body.Append($"<tr><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.numberradication}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.filingDate?.ToString("dd / MM / yyyy")}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.establishment}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.nit}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.city}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.address}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.phone}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.amount}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.color}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.initialNumbering}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.finalNumbering}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.cantCut}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.areaCut}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.tipoPart}</th><th style=\"border: black 1px solid; padding:5px padding:5px;\">{mailApproval.safeGuard}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.subtotal}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.consignmentValue}</th><th style=\"border: black 1px solid; padding:5px;\">{DateTime.Now.ToString("dd / MM / yyyy")}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.analyst}</th></tr>");
                    }
                    else
                    {
                        body.Append($"<tr><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.numberradication}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.filingDate?.ToString("dd / MM / yyyy")}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.establishment}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.nit}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.city}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.address}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.phone}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.amount}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.color}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.initialNumbering}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.finalNumbering}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.subtotal}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.consignmentValue}</th><th style=\"border: black 1px solid; padding:5px;\">{DateTime.Now.ToString("dd / MM / yyyy")}</th><th style=\"border: black 1px solid; padding:5px;\">{mailApproval.analyst}</th></tr>");
                    }

                }
            }

            body.Append($"</tbody></table>");
            string asunto = "Informacion para generación de " + (detalleSolicitud != null ? detalleSolicitud.TIPOSOLICITUD : "");
            string correo = "nicolaslinares2006@gmail.com";


            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from a in _context.AdmintT009Documentos
                        join b in _context.CupostT020RlSolicitudesDocumento
                        on a.PkT009codigo equals b.A020CodigoDocumento
                        where b.A020CodigoSolicitud == datos[0].code &&
                        a.A009codigoParametricaTipoDocumento == StringHelper.tipoDocumentoAdjuntoFactura &&
                        a.A009estadoRegistro == StringHelper.estadoActivo
                        select new
                        {
                            a.A009url,
                            a.A009documento
                        };

            if (query != null)
            {
                foreach (var q in query)
                {
                    var document = new DocumentInformation()
                    {
                        codigo = datos[0].code,
                        nombreArchivo = q.A009documento,
                        url = q.A009url
                    };
                    listDocumento.Add(document);
                }
                GetFiles(listDocumento);
            }

            foreach (var documento in documentoEnviar)
            {
                Files nuevoDocumento = new Files();
                nuevoDocumento.codigo = documento.codigo;
                nuevoDocumento.adjuntoBase64 = documento.adjuntoBase64;
                nuevoDocumento.nombreAdjunto = documento.nombreAdjunto;
                nuevoDocumento.tipoAdjunto = documento.tipoAdjunto;

                // Hacer algo con el objeto "nuevoDocumento" aquí
                await EmailHelper.sendApprovalMail(correo, "Coordinador", asunto, Convert.ToString(body), nuevoDocumento);
            }

            return ResponseManager.generaRespuestaGenerica("El correo ha sido enviado", "", token, false);
        }

        /// <summary>
        /// obtner ultimas numeraciones
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public Responses GetNumbersSeals(ClaimsIdentity identity, string ipAddress, string tipoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            Numbers num = new Numbers();
            CupostT006Precintosymarquilla numeraciones = new CupostT006Precintosymarquilla();
            if (tipoSolicitud == "Precintos")
            {
                numeraciones = _context.CupostT006Precintosymarquillas.OrderByDescending(x => x.A006numeroFinalNumerico).Where(p => p.A006codigoPrecintoymarquilla.Contains("MMA")).FirstOrDefault() ?? new CupostT006Precintosymarquilla();
            }
            else
            {
                numeraciones = _context.CupostT006Precintosymarquillas.OrderByDescending(x => x.A006numeroFinalNumerico).Where(p => p.A006codigoPrecintoymarquilla.Contains("CO")).FirstOrDefault() ?? new CupostT006Precintosymarquilla();
            }
            
            num.initial = 0;
            num.final = 0;

            return ResponseManager.generaRespuestaGenerica("", num, token, false);
        }


        /// <summary>
        /// obtner ultimas numeraciones
        /// </summary>
        /// <param name="requestCode"></param>       
        /// <returns></returns>
        public async Task<Responses> GetTypesFractionsRequest(ClaimsIdentity identity, decimal requestCode)
        {

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var fracciones = await genericsMethodsHelper.ConsultarTiposFraccionesSolicitud(requestCode);

            return ResponseManager.generaRespuestaGenerica("", fracciones, token, false);
        }

        /// <summary>
        /// consulta de puertos
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses GetEspecimen(ClaimsIdentity identity, decimal code)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var qry = from especimen in _context.AdmintT005Especimen
                      where especimen.A005estadoRegistro == 72 &&
                      especimen.PkT005codigo == code
                      select new
                      {
                          code = especimen.PkT005codigo,
                          name = especimen.A005nombreCientifico,
                          nameComun = especimen.A005nombreComun
                      };

            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }
    }
}
