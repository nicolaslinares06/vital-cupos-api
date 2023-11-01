using API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using static Repository.Helpers.Models.CoordinadorGpnSolicitudes;
using static Repository.Helpers.Models.ModelsAppSettings;
using static Repository.Helpers.Models.PaginatioModels;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace Repository.Persistence.Repository
{
    public class CoordinatorAssignRequestAnalystsGpnRepository : ICoordinatorAssignRequestAnalystsGpnRepository
    {
        private readonly DBContext context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly IGenericsMethodsHelper genericsMethodsHelper;
        private readonly EstadosCuposSettings _estadosCuposSettings;
        public readonly IConfiguration configuration;

        public CoordinatorAssignRequestAnalystsGpnRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager,
            IGenericsMethodsHelper genericsMethodsHelper, IOptions<EstadosCuposSettings> estadosCuposSettings)
        {
            this.context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.genericsMethodsHelper = genericsMethodsHelper;
            _estadosCuposSettings = estadosCuposSettings.Value;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarSolicitudes(ClaimsIdentity identity, string tipoEvaluacion)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            decimal[] tiposEstadosSolicitud;
            string[] estadosSolicitud = { _estadosCuposSettings.Enviada.ValorEstado,
                                          _estadosCuposSettings.Radicada.ValorEstado,
                                          _estadosCuposSettings.EnRequerimiento.ValorEstado};

            tiposEstadosSolicitud = await context.AdmintT008Parametricas
                                          .Where(t => t.A008parametrica == "ESTADO SOLICITUD CUPOS"
                                                    && estadosSolicitud.Contains(t.A008valor)
                                                    && t.A008modulo == "CUPOS")
                                          .Select(t => t.PkT008codigo).ToArrayAsync();

            var solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();


            if (tipoEvaluacion == _estadosCuposSettings.Enviada.ValorEstado)
            {
                solicitudesPrecintos = await (from solicitud in context.CupostT019Solicitudes
                                              join parametricaValor in context.AdmintT008Parametricas
                                              on solicitud.A019EstadoSolicitud equals parametricaValor.PkT008codigo
                                              join empresa in context.CupostT001Empresas
                                              on solicitud.A019CodigoEmpresa equals empresa.PkT001codigo
                                              join parametricaValorTipoSolicitud in context.AdmintT008Parametricas
                                              on solicitud.A019TipoSolicitud equals parametricaValorTipoSolicitud.PkT008codigo
                                              where solicitud.A019EstadoRegistro == 72 &&
                                                    tiposEstadosSolicitud.Contains(solicitud.A019EstadoSolicitud)
                                              select new ListaSolicitudesCoordinador
                                              {
                                                  CodigoSolicitud = solicitud.Pk_T019Codigo,
                                                  NumeroRadicado = solicitud.A019NumeroRadicacion,
                                                  FechaSolicitud = solicitud.A019FechaSolicitud,
                                                  FechaRadicacion = solicitud.A019FechaRadicacion,
                                                  Estado = parametricaValor.A008valor,
                                                  SolicitudPrecintoNacional = "Solicitud " + parametricaValorTipoSolicitud.A008valor,
                                                  NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                                                     .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                                                     .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                                                  NumeroRadicacionSalida = solicitud.A019NumeroRadicacionSalida == null ? "" : solicitud.A019NumeroRadicacionSalida,
                                                  FechaRadicacionSalida = solicitud.A019FechaRadicacionSalida
                                              }).ToListAsync();


            }
            else
            {
                decimal codigoPKSolicitud = await context.AdmintT008Parametricas
                                          .Where(t => t.A008parametrica == "ESTADO SOLICITUD CUPOS"
                                                    && t.A008valor == tipoEvaluacion 
                                                    && t.A008modulo == "CUPOS")
                                          .Select(t => t.PkT008codigo).FirstOrDefaultAsync();

                solicitudesPrecintos = await (from solicitud in context.CupostT019Solicitudes
                                              join parametricaValor in context.AdmintT008Parametricas
                                              on solicitud.A019EstadoSolicitud equals parametricaValor.PkT008codigo
                                              join empresa in context.CupostT001Empresas
                                              on solicitud.A019CodigoEmpresa equals empresa.PkT001codigo
                                              join parametricaValorTipoSolicitud in context.AdmintT008Parametricas
                                              on solicitud.A019TipoSolicitud equals parametricaValorTipoSolicitud.PkT008codigo
                                              where solicitud.A019EstadoRegistro == 72 &&
                                                        solicitud.A019EstadoSolicitud == codigoPKSolicitud
                                              select new ListaSolicitudesCoordinador
                                              {
                                                  CodigoSolicitud = solicitud.Pk_T019Codigo,
                                                  NumeroRadicado = solicitud.A019NumeroRadicacion,
                                                  FechaSolicitud = solicitud.A019FechaSolicitud,
                                                  FechaRadicacion = solicitud.A019FechaRadicacion,
                                                  Estado = parametricaValor.A008valor,
                                                  SolicitudPrecintoNacional = "Solicitud " + parametricaValorTipoSolicitud.A008valor,
                                                  NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                                                     .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                                                     .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                                                  NumeroRadicacionSalida = solicitud.A019NumeroRadicacionSalida == null ? "" : solicitud.A019NumeroRadicacionSalida,
                                                  FechaRadicacionSalida = solicitud.A019FechaRadicacionSalida
                                              }).ToListAsync();
            }

            if (solicitudesPrecintos is null)
                solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();





            return ResponseManager.generaRespuestaGenerica("", solicitudesPrecintos, token, false);
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarSolicitudesPagination(ClaimsIdentity identity, decimal tipoEvaluacion, ParamsPaginations parametrosPaginacion)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

    


            decimal[] tiposEstadosSolicitud;
            string[] estadosSolicitud = { "ENVIADA", "RADICADA", "EN REQUERIMIENTO" };


            tiposEstadosSolicitud = await context.AdmintT008Parametricas
                                          .Where(t => t.A008parametrica == "ESTADO SOLICITUD CUPOS" && estadosSolicitud.Contains(t.A008valor) && t.A008modulo == "CUPOS")
                                          .Select(t => t.PkT008codigo).ToArrayAsync();

            var solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();


            if (tipoEvaluacion == 10166)
            {
                solicitudesPrecintos = await context.CupostT019Solicitudes                      
                      .Where(combo => combo.A019EstadoRegistro == 72 && tiposEstadosSolicitud.Contains(combo.A019EstadoSolicitud))
                      .Skip((parametrosPaginacion.PageNumber - 1) * parametrosPaginacion.QuantityRecordsForpage)
                      .Take(parametrosPaginacion.QuantityRecordsForpage)
                      .Select(combo => new ListaSolicitudesCoordinador
                      {
                          CodigoSolicitud = combo.Pk_T019Codigo,
                          NumeroRadicado = combo.A019NumeroRadicacion,
                          FechaSolicitud = combo.A019FechaSolicitud,
                          FechaRadicacion = combo.A019FechaRadicacion,
                          Estado = combo.A019EstadoRegistro.ToString(),
                          SolicitudPrecintoNacional = "Solicitud " + tiposEstadosSolicitud,
                          NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                           .Where(u => u.PkT012codigo == combo.A019CodigoUsuarioCreacion)
                                                           .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                          NumeroRadicacionSalida = combo.A019NumeroRadicacionSalida == null ? "" : combo.A019NumeroRadicacionSalida,
                          FechaRadicacionSalida = combo.A019FechaRadicacionSalida
                      }).ToListAsync();


            }
            else
            {

                solicitudesPrecintos = await context.CupostT019Solicitudes
                      .Join(context.AdmintT008Parametricas,
                          solicitud => solicitud.A019EstadoSolicitud,
                          parametricaValor => parametricaValor.PkT008codigo,
                          (solicitud, parametricaValor) => new { solicitud, parametricaValor })
                      .Join(context.CupostT001Empresas,
                          combo => combo.solicitud.A019CodigoEmpresa,
                          empresa => empresa.PkT001codigo,
                          (combo, empresa) => new { combo.solicitud, combo.parametricaValor, empresa })
                      .Join(context.AdmintT008Parametricas,
                          combo => combo.solicitud.A019TipoSolicitud,
                          parametricaValorTipoSolicitud => parametricaValorTipoSolicitud.PkT008codigo,
                          (combo, parametricaValorTipoSolicitud) => new { combo.solicitud, combo.parametricaValor, combo.empresa, parametricaValorTipoSolicitud })
                      .Where(combo => combo.solicitud.A019EstadoRegistro == 72 && combo.solicitud.A019EstadoSolicitud == tipoEvaluacion)
                      .Skip((parametrosPaginacion.PageNumber - 1) * parametrosPaginacion.QuantityRecordsForpage)
                      .Take(parametrosPaginacion.QuantityRecordsForpage)
                      .Select(combo => new ListaSolicitudesCoordinador
                      {
                          CodigoSolicitud = combo.solicitud.Pk_T019Codigo,
                          NumeroRadicado = combo.solicitud.A019NumeroRadicacion,
                          FechaSolicitud = combo.solicitud.A019FechaSolicitud,
                          FechaRadicacion = combo.solicitud.A019FechaRadicacion,
                          Estado = combo.parametricaValor.A008valor,
                          SolicitudPrecintoNacional = "Solicitud " + combo.parametricaValorTipoSolicitud.A008valor,
                          NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                           .Where(u => u.PkT012codigo == combo.solicitud.A019CodigoUsuarioCreacion)
                                                           .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                          NumeroRadicacionSalida = combo.solicitud.A019NumeroRadicacionSalida == null ? "" : combo.solicitud.A019NumeroRadicacionSalida,
                          FechaRadicacionSalida = combo.solicitud.A019FechaRadicacionSalida
                      }).ToListAsync();
            
            }

            if (solicitudesPrecintos is null)
                solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();





            return ResponseManager.generaRespuestaGenerica("", solicitudesPrecintos, token, false);
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarSolicitudesPorFiltro(ClaimsIdentity identity, decimal tipoEvaluacion, ParamsPaginations parametrosPaginacion)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }




            decimal[] tiposEstadosSolicitud;
            string[] estadosSolicitud = { "ENVIADA", "RADICADA", "EN REQUERIMIENTO" };


            tiposEstadosSolicitud = await context.AdmintT008Parametricas
                                          .Where(t => t.A008parametrica == "ESTADO SOLICITUD CUPOS" && estadosSolicitud.Contains(t.A008valor) && t.A008modulo == "CUPOS")
                                          .Select(t => t.PkT008codigo).ToArrayAsync();

            var solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();


            if (tipoEvaluacion == 10166)
            {
                solicitudesPrecintos = await context.CupostT019Solicitudes
                      .Where(combo => combo.A019EstadoRegistro == 72 && combo.A019EstadoSolicitud == tipoEvaluacion && (
                             (combo.A019NumeroRadicacion != null &&  combo.A019NumeroRadicacion.Contains(parametrosPaginacion.FilterCriterium)) ||
                             (combo.A019NumeroRadicacionSalida != null && combo.A019NumeroRadicacionSalida.Contains(parametrosPaginacion.FilterCriterium))))
                      .Skip((parametrosPaginacion.PageNumber - 1) * parametrosPaginacion.QuantityRecordsForpage)
                      .Take(parametrosPaginacion.QuantityRecordsForpage)
                      .Select(combo => new ListaSolicitudesCoordinador
                      {
                          CodigoSolicitud = combo.Pk_T019Codigo,
                          NumeroRadicado = combo.A019NumeroRadicacion,
                          FechaSolicitud = combo.A019FechaSolicitud,
                          FechaRadicacion = combo.A019FechaRadicacion,
                          Estado = combo.A019EstadoRegistro.ToString(),
                          SolicitudPrecintoNacional = "Solicitud " + tiposEstadosSolicitud,
                          NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                           .Where(u => u.PkT012codigo == combo.A019CodigoUsuarioCreacion)
                                                           .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                          NumeroRadicacionSalida = combo.A019NumeroRadicacionSalida == null ? "" : combo.A019NumeroRadicacionSalida,
                          FechaRadicacionSalida = combo.A019FechaRadicacionSalida
                      }).ToListAsync();


            }
            else
            {

                solicitudesPrecintos = await context.CupostT019Solicitudes
                      .Join(context.AdmintT008Parametricas,
                          solicitud => solicitud.A019EstadoSolicitud,
                          parametricaValor => parametricaValor.PkT008codigo,
                          (solicitud, parametricaValor) => new { solicitud, parametricaValor })
                      .Join(context.CupostT001Empresas,
                          combo => combo.solicitud.A019CodigoEmpresa,
                          empresa => empresa.PkT001codigo,
                          (combo, empresa) => new { combo.solicitud, combo.parametricaValor, empresa })
                      .Join(context.AdmintT008Parametricas,
                          combo => combo.solicitud.A019TipoSolicitud,
                          parametricaValorTipoSolicitud => parametricaValorTipoSolicitud.PkT008codigo,
                          (combo, parametricaValorTipoSolicitud) => new { combo.solicitud, combo.parametricaValor, combo.empresa, parametricaValorTipoSolicitud })
                      .Where(combo => combo.solicitud.A019EstadoRegistro == 72 && tiposEstadosSolicitud.Contains(combo.solicitud.A019EstadoSolicitud)  && (
                             (combo.solicitud.A019NumeroRadicacion != null && combo.solicitud.A019NumeroRadicacion.Contains(parametrosPaginacion.FilterCriterium)) ||
                             (combo.solicitud.A019NumeroRadicacionSalida != null && combo.solicitud.A019NumeroRadicacionSalida.Contains(parametrosPaginacion.FilterCriterium))))
                      .Skip((parametrosPaginacion.PageNumber - 1) * parametrosPaginacion.QuantityRecordsForpage)
                      .Take(parametrosPaginacion.QuantityRecordsForpage)
                      .Select(combo => new ListaSolicitudesCoordinador
                      {
                          CodigoSolicitud = combo.solicitud.Pk_T019Codigo,
                          NumeroRadicado = combo.solicitud.A019NumeroRadicacion,
                          FechaSolicitud = combo.solicitud.A019FechaSolicitud,
                          FechaRadicacion = combo.solicitud.A019FechaRadicacion,
                          Estado = combo.parametricaValor.A008valor,
                          SolicitudPrecintoNacional = "Solicitud " + combo.parametricaValorTipoSolicitud.A008valor,
                          NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                           .Where(u => u.PkT012codigo == combo.solicitud.A019CodigoUsuarioCreacion)
                                                           .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                          NumeroRadicacionSalida = combo.solicitud.A019NumeroRadicacionSalida == null ? "" : combo.solicitud.A019NumeroRadicacionSalida,
                          FechaRadicacionSalida = combo.solicitud.A019FechaRadicacionSalida
                      }).ToListAsync();

            }

            if (solicitudesPrecintos is null)
                solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();





            return ResponseManager.generaRespuestaGenerica("", solicitudesPrecintos, token, false);
        }


        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarSolicitudesByFilter(ClaimsIdentity identity, decimal tipoEvaluacion, ParamsPaginations parametrosPaginacion)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }




            decimal[] tiposEstadosSolicitud;
            string[] estadosSolicitud = { "ENVIADA", "RADICADA", "EN REQUERIMIENTO" };


            tiposEstadosSolicitud = await context.AdmintT008Parametricas
                                          .Where(t => t.A008parametrica == "ESTADO SOLICITUD CUPOS" && estadosSolicitud.Contains(t.A008valor) && t.A008modulo == "CUPOS")
                                          .Select(t => t.PkT008codigo).ToArrayAsync();

            var solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();


            if (tipoEvaluacion == 10166)
            {
                solicitudesPrecintos = await context.CupostT019Solicitudes
                      .Where(combo => combo.A019EstadoRegistro == 72 && tiposEstadosSolicitud.Contains(combo.A019EstadoSolicitud))
                      .Skip((parametrosPaginacion.PageNumber - 1) * parametrosPaginacion.QuantityRecordsForpage)
                      .Take(parametrosPaginacion.QuantityRecordsForpage)
                      .Select(combo => new ListaSolicitudesCoordinador
                      {
                          CodigoSolicitud = combo.Pk_T019Codigo,
                          NumeroRadicado = combo.A019NumeroRadicacion,
                          FechaSolicitud = combo.A019FechaSolicitud,
                          FechaRadicacion = combo.A019FechaRadicacion,
                          Estado = combo.A019EstadoRegistro.ToString(),
                          SolicitudPrecintoNacional = "Solicitud " + tiposEstadosSolicitud,
                          NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                           .Where(u => u.PkT012codigo == combo.A019CodigoUsuarioCreacion)
                                                           .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                          NumeroRadicacionSalida = combo.A019NumeroRadicacionSalida == null ? "" : combo.A019NumeroRadicacionSalida,
                          FechaRadicacionSalida = combo.A019FechaRadicacionSalida
                      }).ToListAsync();


            }
            else
            {

                solicitudesPrecintos = await context.CupostT019Solicitudes
                      .Join(context.AdmintT008Parametricas,
                          solicitud => solicitud.A019EstadoSolicitud,
                          parametricaValor => parametricaValor.PkT008codigo,
                          (solicitud, parametricaValor) => new { solicitud, parametricaValor })
                      .Join(context.CupostT001Empresas,
                          combo => combo.solicitud.A019CodigoEmpresa,
                          empresa => empresa.PkT001codigo,
                          (combo, empresa) => new { combo.solicitud, combo.parametricaValor, empresa })
                      .Join(context.AdmintT008Parametricas,
                          combo => combo.solicitud.A019TipoSolicitud,
                          parametricaValorTipoSolicitud => parametricaValorTipoSolicitud.PkT008codigo,
                          (combo, parametricaValorTipoSolicitud) => new { combo.solicitud, combo.parametricaValor, combo.empresa, parametricaValorTipoSolicitud })
                      .Where(combo => combo.solicitud.A019EstadoRegistro == 72 && combo.solicitud.A019EstadoSolicitud == tipoEvaluacion)
                      .Skip((parametrosPaginacion.PageNumber - 1) * parametrosPaginacion.QuantityRecordsForpage)
                      .Take(parametrosPaginacion.QuantityRecordsForpage)
                      .Select(combo => new ListaSolicitudesCoordinador
                      {
                          CodigoSolicitud = combo.solicitud.Pk_T019Codigo,
                          NumeroRadicado = combo.solicitud.A019NumeroRadicacion,
                          FechaSolicitud = combo.solicitud.A019FechaSolicitud,
                          FechaRadicacion = combo.solicitud.A019FechaRadicacion,
                          Estado = combo.parametricaValor.A008valor,
                          SolicitudPrecintoNacional = "Solicitud " + combo.parametricaValorTipoSolicitud.A008valor,
                          NombreEntidadSolicitante = context.AdmintT012Usuarios
                                                           .Where(u => u.PkT012codigo == combo.solicitud.A019CodigoUsuarioCreacion)
                                                           .Select(u => u.A012primerNombre + " " + u.A012primerApellido).FirstOrDefault() ?? "",
                          NumeroRadicacionSalida = combo.solicitud.A019NumeroRadicacionSalida == null ? "" : combo.solicitud.A019NumeroRadicacionSalida,
                          FechaRadicacionSalida = combo.solicitud.A019FechaRadicacionSalida
                      }).ToListAsync();

            }

            if (solicitudesPrecintos is null)
                solicitudesPrecintos = new List<ListaSolicitudesCoordinador>();





            return ResponseManager.generaRespuestaGenerica("", solicitudesPrecintos, token, false);
        }

        public async Task<Responses> ConsultarAnalistas(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            List<PropsAnalista> analistas = new List<PropsAnalista>();
            var analistaId = await context.CupostT019Solicitudes
                                    .Where(a => a.Pk_T019Codigo == codigoSolicitud)
                                    .Select(a => a.A019AnalistaAsignacion).FirstOrDefaultAsync();

            var idRolAnalistaPrecinto = context.AdmintT011Rols.Where(p => p.A011nombre == "Analista Precintos").ToList();
           
            var relacionRolUsuarios = context.AdmintT015RlUsuarioRols.ToList();
            foreach (var rl in relacionRolUsuarios)
            {
                foreach (var rolAnalista in idRolAnalistaPrecinto)
                {
                    var roles = rl.A015codigoRol.Split("|");
                    if (roles.Contains(rolAnalista.PkT011codigo.ToString()))
                    {
                        List<string> role = new List<string>();
                        foreach (var rol in roles)
                        {
                            var r = context.AdmintT011Rols.Where(p => p.PkT011codigo == Convert.ToDecimal(rol)).FirstOrDefault();
                            if(r != null)
                                role.Add(r.A011nombre);
                        }
                        var rols = role.ToArray();
                        var anlst = context.AdmintT012Usuarios.Where(p => p.PkT012codigo == rl.A015codigoUsuario && p.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                        if (anlst != null && anlst.PkT012codigo != analistaId)
                        {
                            PropsAnalista PropsAnalista = new PropsAnalista();
                            PropsAnalista.CodigoAnalista = anlst.PkT012codigo;
                            PropsAnalista.NombresApellidos = $"{anlst.A012primerNombre} {anlst.A012primerApellido}";
                            PropsAnalista.RolAnalista = string.Join(",", rols);
                            PropsAnalista.CantidadSolicitudes = context.CupostT019Solicitudes.Where(s => s.A019AnalistaAsignacion == anlst.PkT012codigo && (s.A019EstadoSolicitud == configuration.GetValue<int>("EstadosCupos:Radicada:IdEstado") || s.A019EstadoSolicitud == configuration.GetValue<int>("EstadosCupos:Enviada:IdEstado") || s.A019EstadoSolicitud == configuration.GetValue<int>("EstadosCupos:EnRequerimiento:IdEstado"))).Count();
                            analistas.Add(PropsAnalista);
                        }
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("", analistas, token, false);
        }

        public async Task<Responses> ConsultarDatosSolicitudCupo(ClaimsIdentity identity, decimal codigoCupo)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var datosSolicitud = await (from empresa in context.CupostT001Empresas
                                        join solicitud in context.CupostT002Cupos
                                        on empresa.PkT001codigo equals solicitud.A002codigoEmpresa
                                        join persona in context.CitestT003Personas
                                        on empresa.A001codigoPersonaRepresentantelegal equals persona.PkT003codigo
                                        where solicitud.PkT002codigo == 30083
                                        select new
                                        {

                                            Ciudad = 0,
                                            Establecimiento = empresa.A001nombre,
                                            Fecha = solicitud.A002fechaCreacion,
                                            PrimerNombre = persona.A003nombres,
                                            SegundoNombre = "",
                                            PrimerApellido = persona.A003apellidos,
                                            SegundoApellido = "",
                                            TipoIdentificacion = "",
                                            NumeroIdentificacion = persona.A003identificacion,
                                            DireccionEntrega = empresa.A001direccion,
                                            Telefonos = empresa.A001telefono,
                                            Fax = 0

                                        }).ToListAsync();


            return ResponseManager.generaRespuestaGenerica("", datosSolicitud, token, false);
        }

        public async Task<Responses> ConsultarDatosDesistimientoSolicitud(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var datosSolicitud = await context.CupostT019Solicitudes
                                    .Where(s => s.Pk_T019Codigo == codigoSolicitud)
                                    .Select(s => new
                                    {
                                        ObservacionesDesistimiento = s.A019ObservacionesDesistimiento,
                                        FechaRadicacionDesistimiento = s.A019FechaRadicacion
                                    }).FirstOrDefaultAsync();

            if(datosSolicitud != null)
                return ResponseManager.generaRespuestaGenerica("", datosSolicitud, token, false);
            else
                return ResponseManager.generaRespuestaGenerica("", "", token, false);
        }

        public async Task<Responses> ConsultarDatosSolicitud(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var datosSolicitud = await (from solicitud in context.CupostT019Solicitudes
                                        join ciudad in context.AdmintT004Ciudads
                                        on solicitud.A019CodigoCiudad equals ciudad.PkT004codigo
                                        join empresa in context.CupostT001Empresas
                                        on solicitud.A019CodigoEmpresa equals empresa.PkT001codigo
                                        join parametrica in context.AdmintT008Parametricas
                                        on solicitud.A019EstadoSolicitud equals parametrica.PkT008codigo
                                        where solicitud.Pk_T019Codigo == codigoSolicitud
                                        select new InformacionSolicitud
                                        {
                                            Ciudad = context.AdmintT004Ciudads
                                                       .Where(c => c.PkT004codigo == empresa.A001codigoCiudad)
                                                       .Select(p => p.A004nombre)
                                                       .FirstOrDefault(),
                                            Establecimiento = empresa.A001nombre,
                                            Fecha = solicitud.A019FechaSolicitud,
                                            PrimerNombre = context.AdmintT012Usuarios
                                                            .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                            .Select(p => p.A012primerNombre)
                                                            .FirstOrDefault(),

                                            SegundoNombre = context.AdmintT012Usuarios
                                                            .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                            .Select(p => p.A012segundoNombre)
                                                            .FirstOrDefault(),


                                            PrimerApellido = context.AdmintT012Usuarios
                                                            .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                            .Select(p => p.A012primerApellido)
                                                            .FirstOrDefault(),

                                            SegundoApellido = context.AdmintT012Usuarios
                                                            .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                            .Select(p => p.A012segundoApellido)
                                                            .FirstOrDefault(),
                                            TipoIdentificacion = (from persona in context.AdmintT012Usuarios
                                                                  join parametrica in context.AdmintT008Parametricas
                                                                  on persona.A012codigoParametricaTipoDocumento equals
                                                                     parametrica.PkT008codigo
                                                                  where persona.PkT012codigo == solicitud.A019CodigoUsuarioCreacion
                                                                  select parametrica.A008valor).FirstOrDefault(),
                                            NumeroIdentificacion = context.AdmintT012Usuarios
                                                             .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                             .Select(p => p.A012identificacion.ToString())
                                                             .FirstOrDefault(),
                                            DireccionEntrega = solicitud.A019DireccionEntrega,

                                            CiudadEntrega = context.AdmintT004Ciudads
                                                             .Where(c => c.PkT004codigo == solicitud.A019CodigoCiudad)
                                                             .Select(p => p.A004nombre)
                                                             .FirstOrDefault(),
                                            Telefonos = context.AdmintT012Usuarios
                                                            .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                            .Select(p => p.A012telefono.ToString())
                                                            .FirstOrDefault(),
                                            Fax = context.AdmintT012Usuarios
                                                            .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                            .Select(p => p.A012celular)
                                                            .FirstOrDefault(),
                                            Cantidad = solicitud.A019Cantidad,
                                            EspeciesSubEspecies = context.AdmintT005Especimen
                                                             .Where(e => e.PkT005codigo == solicitud.A019CodigoEspecieExportar)
                                                             .Select(e => e.A005nombreCientifico)
                                                             .FirstOrDefault() ?? "",
                                            LongitudMenor = solicitud.A019LongitudMenor,
                                            LongitudMayor = solicitud.A019LongitudMayor,
                                            FechaAsignacion = solicitud.A019FechaConsignacion,
                                            Observaciones = solicitud.A019Observaciones,
                                            Analista = solicitud.A019AnalistaAsignacion == null ? 0 : solicitud.A019AnalistaAsignacion,
                                            NumeroRadicado = solicitud.A019NumeroRadicacion,
                                            Nit = empresa.A001nit,
                                            ValorConsignacion = solicitud.A019ValorConsignacion == null ? 0 : solicitud.A019ValorConsignacion,
                                            CorreoElectronico = context.AdmintT012Usuarios
                                                                .Where(u => u.PkT012codigo == solicitud.A019CodigoUsuarioCreacion)
                                                                .Select(p => p.A012correoElectronico.ToString())
                                                                .FirstOrDefault(),
                                            Respuesta = solicitud.A019Respuesta,
                                            TipoSolicitud = context.AdmintT008Parametricas
                                                                  .Where(p => p.PkT008codigo == solicitud.A019TipoSolicitud)
                                                                  .Select(p => p.A008valor).FirstOrDefault(),
                                            TypeRequestCode = solicitud.A019TipoSolicitud ?? 0

                                        }).FirstOrDefaultAsync();

            if (datosSolicitud != null)
            {
                var solicitud = await context.CupostT019Solicitudes.Where(q => q.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();
                datosSolicitud.CodigoInicial = await ObtenerCodigoInicialEspecie(codigoSolicitud);
                datosSolicitud.CodigoFinal = await ObtenerCodigoFinalEspecie(codigoSolicitud);
                datosSolicitud.Color = await ObtenerColor(codigoSolicitud);
                if(solicitud != null)
                    datosSolicitud.DepartamentoEntrega = await ObtenerDepartamentoPorCiudadId(solicitud.A019CodigoCiudad);
            }

            if(datosSolicitud != null)
                return ResponseManager.generaRespuestaGenerica("", datosSolicitud, token, false);
            else
                return ResponseManager.generaRespuestaGenerica("", "", token, false);
        }

        public async Task<Responses> ConsultarDocumentoSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, decimal tipoDocumento)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }



            var archivo = await (from a in context.AdmintT009Documentos
                                 join b in context.CupostT020RlSolicitudesDocumento
                                 on a.PkT009codigo equals b.A020CodigoDocumento
                                 where b.A020CodigoSolicitud == codigoSolicitud &&
                                 a.A009codigoParametricaTipoDocumento == tipoDocumento
                                 select new Archivo()
                                 {
                                     urlFTP = a.A009url,
                                     nombreAdjunto = a.A009documento
                                 }).FirstOrDefaultAsync();


            if (archivo != null && archivo.urlFTP != null)
            {

                DocumentManager documentManager = new DocumentManager(context);

                archivo.adjuntoBase64 = documentManager.ConvertirArchivoToBase64(archivo.urlFTP);
                archivo.tipoAdjunto = documentManager.getTipoData(archivo.urlFTP);

            }

            if (archivo is null)
                archivo = new Archivo();

            return ResponseManager.generaRespuestaGenerica("", archivo, token, false);
        }

        public async Task<Responses> ConsultarDocumentosSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, decimal tipoDocumento)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }



            var archivos = await (from a in context.AdmintT009Documentos
                                  join b in context.CupostT020RlSolicitudesDocumento
                                  on a.PkT009codigo equals b.A020CodigoDocumento
                                  where b.A020CodigoSolicitud == codigoSolicitud &&
                                  a.A009codigoParametricaTipoDocumento == tipoDocumento
                                  select new Archivo()
                                  {
                                      urlFTP = a.A009url,
                                      nombreAdjunto = a.A009documento
                                  }).ToListAsync();


            if (archivos != null)
            {
                foreach (var archivo in archivos)
                {

                    DocumentManager documentManager = new DocumentManager(context);

                    if (archivo.urlFTP != null)
                    {
                        archivo.adjuntoBase64 = documentManager.ConvertirArchivoToBase64(archivo.urlFTP);
                        archivo.tipoAdjunto = documentManager.getTipoData(archivo.urlFTP);
                    }
                }
            }

            if (archivos is null)
                archivos = new List<Archivo>();

            return ResponseManager.generaRespuestaGenerica("", archivos, token, false);
        }




        public async Task<Responses> ConsultarAnalistaSolicitudAsignado(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var analistaSolicitud = await (from analista in context.AdmintT012Usuarios
                                           join solicitud in context.CupostT019Solicitudes
                                           on analista.PkT012codigo equals solicitud.A019AnalistaAsignacion
                                           where solicitud.Pk_T019Codigo == codigoSolicitud
                                           select new
                                           {
                                               NombresApellidos = $"{analista.A012primerNombre} {analista.A012primerApellido}",
                                               Fecha = solicitud.A019FechaSolicitud
                                           }).FirstOrDefaultAsync();

            if (analistaSolicitud is null)
                analistaSolicitud = new
                {
                    NombresApellidos = "",
                    Fecha = DateTime.Now
                };

            return ResponseManager.generaRespuestaGenerica("", analistaSolicitud, token, false);
        }


        public async Task<Responses> ActualizarIdAnalistaSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, decimal idAnalista)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var solicitud = await context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();
            if (solicitud is null)
                return ResponseManager.generaRespuestaGenerica("Error, no se encontro el registro", "", token, true);


            solicitud.A019AnalistaAsignacion = idAnalista;


            var result = await context.SaveChangesAsync();
            if (result > 0)
                return ResponseManager.generaRespuestaGenerica("El analista ha sido actualizado", "", token, false);

            else
                return ResponseManager.generaRespuestaGenerica("Error al procesar el registro", "", token, true);

        }

        public async Task<Responses> ActualizarEstadoSolicitud(ClaimsIdentity identity, decimal codigoSolicitud, string tipoEstado)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var solicitud = await context.CupostT019Solicitudes.Where(p => p.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();
            if (solicitud is null)
                return ResponseManager.generaRespuestaGenerica("Error, no se encontro el registro", "", token, true);


            var codigoTipoEstado = await context.AdmintT008Parametricas
                                          .Where(t => t.A008parametrica == "ESTADO SOLICITUD CUPOS" && t.A008valor == tipoEstado && t.A008modulo == "CUPOS")
                                          .Select(t => t.PkT008codigo).FirstOrDefaultAsync();

            solicitud.A019EstadoSolicitud = codigoTipoEstado;
            var result = await context.SaveChangesAsync();



            if (tipoEstado == _estadosCuposSettings.Radicada.ValorEstado)
            {
                await EliminarNumeracionesSolicitud(codigoSolicitud);
                await EliminarCartaSolicitud(codigoSolicitud);
            }

            if (result > 0)
                return ResponseManager.generaRespuestaGenerica("El estado de solicitud ha sido actualizado", "", token, false);

            else
                return ResponseManager.generaRespuestaGenerica("Error al procesar el registro", "", token, true);










        }

        public async Task<Responses> ConsultarNumeracionesSolicitud(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var numeraciones = new List<NumeracionesSolicitud>();

            var solicitud = await context.CupostT019Solicitudes
                                   .Where(s => s.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();
            if (solicitud is null)
                return ResponseManager.generaRespuestaGenerica("Solicitud no encontrada", numeraciones, "", true);


            var especie = await context.AdmintT005Especimen
                                .Where(e => e.PkT005codigo == solicitud.A019CodigoEspecieExportar)
                                .Select(e => e.A005nombreCientifico).FirstOrDefaultAsync();


            numeraciones = await context.CupostT027NumeracionesSolicitud
                                .Where(p => p.A027CodigoSolicitud == codigoSolicitud && p.A027EstadoRegistro == StringHelper.estadoActivo)
                                .Select(n => new NumeracionesSolicitud
                                {
                                    Especie = especie == null ? "" : especie,
                                    NumeracionInicial = n.A027NumeroInternoInicial,
                                    NumeracionFinal = n.A027NumeroInternoFinal
                                })
                                .ToListAsync();

            if (numeraciones is null)
                numeraciones = new List<NumeracionesSolicitud>();



            return ResponseManager.generaRespuestaGenerica("", numeraciones, token, false);
        }

        public async Task<Responses> ConsultarTiposFraccionesSolicitud(ClaimsIdentity identity, decimal codigoSolicitud)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var fracciones = new List<CuttingSaveModel>();

            var solicitud = await context.CupostT019Solicitudes
                         .Where(s => s.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();

            if (solicitud is null)
                return ResponseManager.generaRespuestaGenerica("Solicitud no encontrada", fracciones, token, true);

            fracciones = await genericsMethodsHelper.ConsultarTiposFraccionesSolicitud(codigoSolicitud);

            return ResponseManager.generaRespuestaGenerica("", fracciones, token, false);
        }



        private async Task<string> ObtenerCodigoInicialEspecie(decimal codigoSolicitud)
        {
            var solicitud = await context.CupostT019Solicitudes.Where(q => q.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();
            if (solicitud is null)
                return "";



            var numeracionInicial = await context.CupostT006Precintosymarquillas
                                          .Where(n => n.A006codigoSolicitud == codigoSolicitud)
                                          .Select(n => n.A006numeroInicial).FirstOrDefaultAsync();

            if (String.IsNullOrEmpty(numeracionInicial))
                numeracionInicial = "";

            return numeracionInicial;
        }

        private async Task<string> ObtenerCodigoFinalEspecie(decimal codigoSolicitud)
        {

            var solicitud = await context.CupostT019Solicitudes.Where(q => q.Pk_T019Codigo == codigoSolicitud).FirstOrDefaultAsync();
            if (solicitud is null)
                return "";



            var numeracionFinal = await context.CupostT006Precintosymarquillas
                                          .Where(n => n.A006codigoSolicitud == codigoSolicitud)
                                          .Select(n => n.A006numeroFinal).FirstOrDefaultAsync();

            if (String.IsNullOrEmpty(numeracionFinal))
                numeracionFinal = "";

            return numeracionFinal;
        }

        private async Task<string> ObtenerColor(decimal codigoSolicitud)
        {
            var color = "";
            var codigoColor = await context.CupostT006Precintosymarquillas
                                                 .Where(p => p.A006codigoSolicitud == codigoSolicitud)
                                                 .Select(p => p.A006codigoParametricaColorPrecintosymarquillas).FirstOrDefaultAsync();

            if (codigoColor > 0)
                color = await context.AdmintT008Parametricas
                                .Where(c => c.PkT008codigo == codigoColor)
                                .Select(c => c.A008valor).FirstOrDefaultAsync();

            if (color is null)
                color = "";

            return color;
        }

        private async Task EliminarNumeracionesSolicitud(decimal codigoSolicitud)
        {
            var numeraciones = await context.CupostT006Precintosymarquillas.Where(p => p.A006codigoSolicitud == codigoSolicitud)
                                                               .ToListAsync();

            if (numeraciones.Any())
            {
                context.CupostT006Precintosymarquillas.RemoveRange(numeraciones);
                await context.SaveChangesAsync();
            }
        }

        private async Task EliminarCartaSolicitud(decimal codigoSolicitud)
        {
            var documentos = await context.CupostT020RlSolicitudesDocumento
                                    .Where(d => d.A020CodigoSolicitud == codigoSolicitud).ToListAsync();

            var cartaDocumento = new AdmintT009Documento();

            if (!documentos.Any() || documentos is null)
            {
                return;
            }

            decimal? idDocumento = 0;

            foreach (var item in documentos)
            {
                cartaDocumento = await context.AdmintT009Documentos
                                       .Where(d => d.A009codigoParametricaTipoDocumento == 10170
                                                   && d.PkT009codigo == item.A020CodigoDocumento).FirstOrDefaultAsync();

                if (cartaDocumento != null)
                {
                    break;

                }


            }
            idDocumento = cartaDocumento.PkT009codigo;

            if (idDocumento <= 0)
                return;

            var rlDOcumento = await context.CupostT020RlSolicitudesDocumento
                                    .Where(d => d.A020CodigoDocumento == idDocumento
                                            && d.A020CodigoSolicitud == codigoSolicitud).FirstOrDefaultAsync();

            if(rlDOcumento != null)
            {
                context.CupostT020RlSolicitudesDocumento.Remove(rlDOcumento);
                await context.SaveChangesAsync();
                context.AdmintT009Documentos.Remove(cartaDocumento);
                await context.SaveChangesAsync();
            }
            
        }

        private async Task<string> ObtenerDepartamentoPorCiudadId(decimal idCiudad)
        {
            string? departamento = null;
            var departamentoId = await context.AdmintT004Ciudads
                                              .Where(c => c.PkT004codigo == idCiudad)
                                              .Select(dep => dep.A004codigoDepartamento).FirstOrDefaultAsync();

            if (departamentoId > 0)
            {
                departamento = await context.AdmintT003Departamentos
                                             .Where(d => d.PkT003codigo == departamentoId)
                                             .Select(nombreDep => nombreDep.A003nombre).FirstOrDefaultAsync();
            }

            return departamento == null ? "" : departamento;
        }

        private sealed class PropsAnalista
        {
            public int CantidadSolicitudes { get; set; }
            public string? RolAnalista { get; set; }
            public string? NombresApellidos { get; set; }
            public decimal? CodigoAnalista { get; set; }
        }


    }
}
