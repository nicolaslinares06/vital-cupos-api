using API.Helpers;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;
using static Repository.Helpers.Models.ActaVisitaCortesModel;
using static Repository.Helpers.Models.PaginatioModels;

namespace Repository.Persistence.Repository
{
    public class ActaVisitasCortesRepository : IActaVisitasCortesRepository
    {
        private readonly DBContext context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly IConfiguration configuration;
        private readonly IGenericsMethodsHelper genericsMethodsHelper;
        private readonly LogManager logManager;

        public ActaVisitasCortesRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, IConfiguration configuration,
                                             IGenericsMethodsHelper genericsMethodsHelper)
        {
            this.context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.configuration = configuration;
            this.genericsMethodsHelper = genericsMethodsHelper;
            this.logManager = new LogManager(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarEmpresas(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var empresasEstablecimientos = await context.CupostT001Empresas.ToListAsync();


            if (empresasEstablecimientos == null)
                empresasEstablecimientos = new List<CupostT001Empresa>();

            return ResponseManager.generaRespuestaGenerica("", empresasEstablecimientos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarDepartamentos(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var departamentos = await genericsMethodsHelper.ObtenerDepartamentos();

            return ResponseManager.generaRespuestaGenerica("", departamentos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="departamentoId"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarCiudadesPorDepartamento(ClaimsIdentity identity, decimal departamentoId)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var ciudades = await genericsMethodsHelper.ObtenerCiudadesPorIdDepartamento(departamentoId);
            return ResponseManager.generaRespuestaGenerica("", ciudades, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="tipoEstablecimiento"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarEstablecimientosPorTipo(ClaimsIdentity identity, decimal tipoEstablecimiento)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var establecimientos = await context.CupostT001Empresas.Where(e => e.A001estadoRegistro == StringHelper.estadoActivo && e.A001codigoParametricaTipoEntidad == tipoEstablecimiento)
                .OrderBy(c => c.A001nombre)
                .Select(e => new
                {
                    TipoEstablecimiento = e.A001codigoParametricaTipoEntidad,
                    EstablecimientoID = e.PkT001codigo,
                    NombreEstablecimiento = e.A001nombre
                })
                .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", establecimientos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="nit"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarEmpresaPorNit(ClaimsIdentity identity, decimal nit)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var TiposEstablecimiento = await context.AdmintT008Parametricas.Where(p => p.A008estadoRegistro == StringHelper.estadoActivo
                                                                       && p.A008parametrica == "TIPO EMPRESA")
                                                                     .Select(t => t.PkT008codigo)
                                                                    .ToListAsync();

            var establecimiento = context.CupostT001Empresas.Where(e => e.A001nit == nit &&
                                                           e.A001estadoRegistro == 72)
                                                    .FirstOrDefault();
            if (establecimiento == null)
                return ResponseManager.generaRespuestaGenerica("No existe empresa", "", token, true);



            var IsExistsQS = (from num in TiposEstablecimiento
                              select num).Contains(establecimiento.A001codigoParametricaTipoEntidad);

            if (!IsExistsQS)
                return ResponseManager.generaRespuestaGenerica("La empresa no corresponde al tipo parametizado", "", token, true);

            var empresaEstablecimiento = (from empresa in context.CupostT001Empresas
                                          join parametrica in context.AdmintT008Parametricas
                                          on empresa.A001codigoParametricaTipoEntidad equals parametrica.PkT008codigo
                                          where empresa.A001nit == nit &&
                                                empresa.A001estadoRegistro == 72
                                          select new
                                          {
                                              TipoEstablecimiento = empresa.A001codigoParametricaTipoEntidad,
                                              EstablecimientoID = empresa.PkT001codigo,
                                              NombreEstablecimiento = empresa.A001nombre,
                                              TipoEstablecimientoNombre = parametrica.A008valor
                                          }).FirstOrDefault();

            if (empresaEstablecimiento == null)
            {
                return ResponseManager.generaRespuestaGenerica("", "", token, false);
            }

            return ResponseManager.generaRespuestaGenerica("", empresaEstablecimiento, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarEstablecimientoPorTipo(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var TiposEstablecimiento = await context.AdmintT008Parametricas.Where(p => p.A008estadoRegistro == StringHelper.estadoActivo
                                                                                   && p.A008parametrica == "TIPO EMPRESA")
                                                                                .ToListAsync();

            if (TiposEstablecimiento == null)
                TiposEstablecimiento = new List<AdmintT008Parametrica>();

            return ResponseManager.generaRespuestaGenerica("", TiposEstablecimiento, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="idEstablecimento"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarActasEstablecimientosPorId(ClaimsIdentity identity, decimal idEstablecimento)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007codigoEntidad == idEstablecimento &&
                                                     actaVisitas.A007estadoRegistro == 72
                                               select new
                                               {
                                                   ActaVisitaId = actaVisitas.PkT007codigo,
                                                   TipoActaId = actaVisitas.A007TipoActa,
                                                   NroVisita = actaVisitas.A007VisitaNumero,
                                                   Establecimiento = empresa.A001nombre,
                                                   TipoEstablecimiento = parametricas.A008valor,
                                                   Fecha = actaVisitas.A007fechaActa
                                               }).ToListAsync();


            return ResponseManager.generaRespuestaGenerica("", ActasEstablecimientos, token, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="criterios"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarActasEstablecimientosPorTipo(ClaimsIdentity identity, VisitRecordsSearch criterios)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var ActasEstablecimientos = new List<VisitReportsEstablishments>();
            if (criterios.SearchType == 1)
            {     
                ActasEstablecimientos = await context.CuposV005ActaVisitaCortes
                                                     .Where(e => e.TipoEstablecimientoId == criterios.EstablishmentTypeId)
                                                     .Select(e => new VisitReportsEstablishments()
                                                     {
                                                         VisitReportId = e.ActaVisitaId,
                                                         ReportTypeId = e.TipoEstablecimientoId,
                                                         VisitNumber = e.VisitaNumero,
                                                         Establishment = e.Establecimiento,
                                                         EstablishmentType = e.TipoEstablecimiento,
                                                         Date = e.FechaActaVisita,
                                                         VisitNumberOne = e.VisitaUno,
                                                         VisitNumberTwo = e.VisitaDos,
                                                         RegistrationStatus = e.EstadoRegistro,
                                                         CreationDateDecimal = e.FechaCreacionDecimal

                                                     }).OrderByDescending(o => o.ReportTypeId)
                                                     .ThenByDescending(t => t.VisitReportId)
                                                     .ToListAsync();
            }

            if (criterios.SearchType == 2)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where empresa.A001codigoParametricaTipoEntidad == criterios.EstablishmentTypeId
                                                     && actaVisitas.A007fechaActa == criterios.VisitDate
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }

            if (criterios.SearchType == 3)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007codigoEntidad == criterios.EstablishmentId
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }

            if (criterios.SearchType == 4)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007codigoEntidad == criterios.EstablishmentId
                                                     && actaVisitas.A007fechaActa == criterios.VisitDate
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }

            if (criterios.SearchType == 5)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007fechaActa == criterios.VisitDate
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }


            //Equivale a consultar todos los registros
            if (criterios.SearchType == -1)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                   .ThenByDescending(t => t.VisitReportId)
                                                   .ToListAsync();

            }

            return ResponseManager.generaRespuestaGenerica("", ActasEstablecimientos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="criterios"></param>
        /// <param name="parametrosPagination"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarActasPagination(ClaimsIdentity identity, VisitRecordsSearch criterios, ParamsPaginations parametrosPagination)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var ActasEstablecimientos = new List<VisitReportsEstablishments>();
            if (criterios.SearchType == 1)
            {
                ActasEstablecimientos = await context.CuposV005ActaVisitaCortes
                                                     .Where(e => e.TipoEstablecimientoId == criterios.EstablishmentTypeId)
                                                     .Select(e => new VisitReportsEstablishments()
                                                     {
                                                         VisitReportId = e.ActaVisitaId,
                                                         ReportTypeId = e.TipoEstablecimientoId,
                                                         VisitNumber = e.VisitaNumero,
                                                         Establishment = e.Establecimiento,
                                                         EstablishmentType = e.TipoEstablecimiento,
                                                         Date = e.FechaActaVisita,
                                                         VisitNumberOne = e.VisitaUno,
                                                         VisitNumberTwo = e.VisitaDos,
                                                         RegistrationStatus = e.EstadoRegistro,
                                                         CreationDateDecimal = e.FechaCreacionDecimal

                                                     }).OrderByDescending(o => o.ReportTypeId)
                                                     .ThenByDescending(t => t.VisitReportId)
                                                     .ToListAsync();
            }

            if (criterios.SearchType == 2)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where empresa.A001codigoParametricaTipoEntidad == criterios.EstablishmentTypeId
                                                     && actaVisitas.A007fechaActa == criterios.VisitDate
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }

            if (criterios.SearchType == 3)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007codigoEntidad == criterios.EstablishmentId
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }

            if (criterios.SearchType == 4)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007codigoEntidad == criterios.EstablishmentId
                                                     && actaVisitas.A007fechaActa == criterios.VisitDate
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }

            if (criterios.SearchType == 5)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               where actaVisitas.A007fechaActa == criterios.VisitDate
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                    .ThenByDescending(t => t.VisitReportId)
                                                    .ToListAsync();
            }


            //Equivale a consultar todos los registros
            if (criterios.SearchType == -1)
            {
                ActasEstablecimientos = await (from empresa in context.CupostT001Empresas
                                               join actaVisitas in context.CupostT007ActaVisita
                                               on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                               join parametricas in context.AdmintT008Parametricas
                                               on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                               select new VisitReportsEstablishments()
                                               {
                                                   VisitReportId = actaVisitas.PkT007codigo,
                                                   ReportTypeId = actaVisitas.A007TipoActa,
                                                   VisitNumber = actaVisitas.A007VisitaNumero,
                                                   Establishment = empresa.A001nombre,
                                                   EstablishmentType = parametricas.A008valor,
                                                   Date = actaVisitas.A007fechaActa,
                                                   VisitNumberOne = actaVisitas.A007VisitaNumero1,
                                                   VisitNumberTwo = actaVisitas.A007VisitaNumero2,
                                                   RegistrationStatus = actaVisitas.A007estadoRegistro,
                                                   CreationDateDecimal = actaVisitas.A007fechaCreacion

                                               }).OrderByDescending(o => o.ReportTypeId)
                                                   .ThenByDescending(t => t.VisitReportId)
                                                   .ToListAsync();

            }

            return ResponseManager.generaRespuestaGenerica("", ActasEstablecimientos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="idActaVisita"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarDocumentosOrigenPiel(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var documentos = await context.CupostT015ActaVisitaDocOrigenPiel.Where(p => p.A015CodigoActaVisita == idActaVisita)
                                                                            .Select(s => new
                                                                            {
                                                                                NumeroDocumento = s.A015DocumentoOrigenPielNumero
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", documentos, token, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="idActaVisita"></param>
        /// <returns></returns>
        public async Task<Responses> ConsultarDocumentosResolucion(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var documentos = await context.CupostT016ActaVisitaNumResolucion.Where(p => p.A016CodigoActaVisita == idActaVisita)
                                                                            .Select(s => new
                                                                            {
                                                                                NumeroDocumento = s.A016ResolucionNumero
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", documentos, token, false);
        }

        public async Task<Responses> ConsultarDocumentosSavoConductos(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var documentos = await context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.A017CodigoActaVisita == idActaVisita)
                                                                            .Select(s => new
                                                                            {
                                                                                NumeroDocumento = s.A017SalvoConductoNumero
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", documentos, token, false);
        }


        public async Task<Responses> ConsultarTipoPielidentificablelActaVisita(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var tipoPieles = await context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == idActaVisita
                                                                        && p.A008tipoPiel != "")
                                                                            .Select(s => new
                                                                            {
                                                                                TipoPiel = s.A008tipoPiel,
                                                                                Cantidad = s.A008cantidad
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", tipoPieles, token, false);
        }

        public async Task<Responses> ConsultarTipoParteIdentificable(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var tipoPartes = await context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == idActaVisita
                                                                        && p.A008tipoParte != "")
                                                                            .Select(s => new
                                                                            {
                                                                                TipoParte = s.A008tipoParte,
                                                                                Cantidad = s.A008cantidad
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", tipoPartes, token, false);
        }

        public async Task<Responses> ConsultarTipoPielIrregularActaVisita(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var tipoPielesIrregulares = await context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == idActaVisita
                                                                        && p.A008tipoPiel != "")
                                                                            .Select(s => new
                                                                            {
                                                                                TipoPielIrregular = s.A008tipoPiel,
                                                                                AreaPromedioTipoPiel = s.A008areaPromedio,
                                                                                CantidadTipoPiel = s.A008cantidad,
                                                                                AreaTotalTipoPiel = s.A008total
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", tipoPielesIrregulares, token, false);
        }


        public async Task<Responses> ConsultarTipoParteIrregular(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var tipoPartesIrregulares = await context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == idActaVisita
                                                                        && p.A008tipoParte != "")
                                                                            .Select(s => new
                                                                            {
                                                                                TipoParte = s.A008tipoParte,
                                                                                CantidadTipoParte = s.A008cantidad,
                                                                                AreaTotalTipoParte = s.A008total
                                                                            })
                                                                            .ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", tipoPartesIrregulares, token, false);
        }

        public async Task<Responses> ConsultarArchivosActaVisita(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }



            var archivosActaVisitaDB = await context.CupostT018ActaVisitaArchivosDocumentos
                                                    .Where(p => p.A018CodigoActaVisita == idActaVisita)                                                                    
                                                    .ToListAsync();


            var archivosActaVisita = new List<ActaVisitasPropArchivos>();

            

            if (archivosActaVisitaDB != null)
            {
                foreach (var item in archivosActaVisitaDB)
                {
                    var archivo = new ActaVisitasPropArchivos();
                    archivo.Base64string = item.A018RutaDocumento != null ? ConvertirArchivoToBase64(item.A018RutaDocumento) : "";
                    if (!String.IsNullOrEmpty(archivo.Base64string))
                    {
                        archivo.UrlFTP = item.A018RutaDocumento;
                        archivo.FileName = item.A018NombreArchivo;
                        archivo.FileType = item.A018RutaDocumento != null ? ObtenerTipoArchivo(item.A018RutaDocumento): null;
                        archivosActaVisita.Add(archivo);
                    }
                    else
                        break;
                
                
                }
            }

            return ResponseManager.generaRespuestaGenerica("", archivosActaVisita, token, false);
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarArchivoPrecintoActaVisita(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var archivoExcelPrecinto = await context.CupostT007ActaVisita.Where(p => p.PkT007codigo == idActaVisita).Select(s => new ActaVisitasPropArchivos()
            {
                FileName = "Documento Excel Precintos",
                UrlFTP = s.A007PrecintoAdjunto,
                FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            })
                                                                    .FirstOrDefaultAsync();


            if (archivoExcelPrecinto != null && !String.IsNullOrEmpty(archivoExcelPrecinto.UrlFTP))
            {
                archivoExcelPrecinto.Base64string = ConvertirArchivoExcelToBase64(archivoExcelPrecinto.UrlFTP);
                var arregloNombre = archivoExcelPrecinto.UrlFTP.Split('/');
                archivoExcelPrecinto.FileName = arregloNombre[arregloNombre.Length - 1];

            }

            if(archivoExcelPrecinto == null)
                return ResponseManager.generaRespuestaGenerica("", "", token, false);

            return ResponseManager.generaRespuestaGenerica("", archivoExcelPrecinto, token, false);
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ValidarDatosExcelPrecintos(ClaimsIdentity identity, string base64Excel, decimal nitEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var empresa = await context.CupostT001Empresas
                                       .Where(empresa => empresa.A001nit == nitEmpresa).FirstOrDefaultAsync();


            if (empresa is null)
                return ResponseManager.generaRespuestaGenerica("Error de Validacion de datos, el nit especificado no es valido, o en su defecto, llene el campo NIT", "", token, true);

            var listaDatosExcel = ObtenerListadoExcel(base64Excel);

            if (listaDatosExcel.Any())
            {

                if (empresa.A001nombre.ToLower() !=  listaDatosExcel[0].NombreZoocriaderOrigen.ToLower())
                    return ResponseManager.generaRespuestaGenerica("Error de datos en Excel, el nombre de empresa no es correcto", "", token, true);

                int cantidadNombresUnicos = listaDatosExcel.Select(p => p.NombreZoocriaderOrigen).Distinct().Count();
                if(cantidadNombresUnicos > 1)
                    return ResponseManager.generaRespuestaGenerica("Error de datos en Excel, hay mas de un nombre de empresa", "", token, true);


                IEnumerable<int> chequeoPrecintosDuplicados = listaDatosExcel.GroupBy(x => x.NumeroPrecinto)
                   .Where(g => g.Count() > 1)
                   .Select(x => x.Key);

                if (chequeoPrecintosDuplicados.Any())
                    return ResponseManager.generaRespuestaGenerica($"Error de datos en Excel, Se ha detectado duplicacion de los siguientes Precintos: {String.Join(", ", chequeoPrecintosDuplicados)}", "", token, true);

                IEnumerable<int> chequeoNumeracionesInternasDuplicados = listaDatosExcel.GroupBy(x => x.NumInternoCodigo)
                         .Where(g => g.Count() > 1)
                         .Select(x => x.Key);


                if (chequeoNumeracionesInternasDuplicados.Any())
                    return ResponseManager.generaRespuestaGenerica($"Error de datos en Excel, Se ha detectado duplicacion de los siguientes Numeros internos: {String.Join(", ", chequeoNumeracionesInternasDuplicados)}", "", token, true);

                var tipoEmpresa = genericsMethodsHelper.ObtenerNombreParametricaPorId(empresa.A001codigoParametricaTipoEntidad);

                var existeRegistroCartaVenta = await ValidarCuposCartaVenta(empresa, listaDatosExcel[0]);

                if (!existeRegistroCartaVenta && tipoEmpresa != "ZOOCRIADERO")
                    return ResponseManager.generaRespuestaGenerica($"Error de datos en Excel, no se ha encontrado registro de carta venta de {empresa.A001nombre} de tipo {tipoEmpresa}", "", token, true);

                if (tipoEmpresa != "ZOOCRIADERO")
                    empresa = await ObtenerEmpresaZoocriaderoOrigen(empresa, listaDatosExcel[0]);

                if(empresa is null)
                    return ResponseManager.generaRespuestaGenerica($"Error de datos en Excel, se ha generado error al validar Zoocriadero Origen", "", token, true);


                var codigoCupoPK = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == listaDatosExcel[0].NumeroResolucion &&
                                                             p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                             p.A002CodigoZoocriadero == listaDatosExcel[0].CodigoZoocriaderoOrigen &&
                                                             p.A002rangoCodigoInicial <= listaDatosExcel[0].NumeracionInterna &&
                                                             p.A002rangoCodigoFin >= listaDatosExcel[0].NumeracionInterna
                                        ).Select(a => a.PkT002codigo).FirstOrDefaultAsync();

                if (codigoCupoPK <= 0)
                    return ResponseManager.generaRespuestaGenerica("Error de datos en Excel, no se ha coincidido con el registro de cupo", "", token, true);

   



                var cuposDisponibles = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == listaDatosExcel[0].NumeroResolucion &&
                                                                           p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                                           p.A002CodigoZoocriadero == listaDatosExcel[0].CodigoZoocriaderoOrigen &&
                                                                           p.A002rangoCodigoInicial <= listaDatosExcel[0].NumeracionInterna &&
                                                                           p.A002rangoCodigoFin >= listaDatosExcel[0].NumeracionInterna &&
                                                                           p.PkT002codigo == codigoCupoPK)
                                                                      .Select(a => a.A002cuposDisponibles).FirstOrDefaultAsync();

                var especie = await context.CupostT005Especieaexportars
                                           .Where(e => e.A005codigoCupo == codigoCupoPK)
                                           .Select(e => e.A005codigoEspecie).FirstOrDefaultAsync();
                if (String.IsNullOrEmpty(especie) || especie == "0")
                    return ResponseManager.generaRespuestaGenerica($"Error de datos en Excel, no hay especie para asociar los precintos", "", token, true);

                var especieToInt = Convert.ToDecimal(especie);




                if (cuposDisponibles <= 0)
                    return ResponseManager.generaRespuestaGenerica($"No hay cupos disponibles", "", token, true);


                if (listaDatosExcel.Count > cuposDisponibles)
                    return ResponseManager.generaRespuestaGenerica($"La cantidad de cupos supera a los cupos disponibles registrados", "", token, true);





                foreach (var item in listaDatosExcel)
                {

                   

                    var numeroResolucion = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == item.NumeroResolucion &&
                                                                                p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                                                p.PkT002codigo == codigoCupoPK)
                                                                        .Select(s => s.A002numeroResolucion).FirstOrDefaultAsync();

                    if (numeroResolucion == null || numeroResolucion <= 0)
                    {
                        return ResponseManager.generaRespuestaGenerica($"Resolucion Nro {item.NumeroResolucion} no existe en el sistema", "", token, true);
                    }

                    var fechaProduccion = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == item.NumeroResolucion &&
                                                                              p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                                              p.PkT002codigo == codigoCupoPK)
                                                                        .Select(a => a.A002fechaProduccion).FirstOrDefaultAsync();

                    string anioProduccion = "";

                    if (fechaProduccion != null)
                    {
                        anioProduccion = fechaProduccion.Value.ToString("yyyy");
                    }


                    if (Convert.ToInt32(anioProduccion) != item.AnioProduccion)
                        return ResponseManager.generaRespuestaGenerica($"Año de produccion no coincide", "", token, true);

                    var fechaResolucion = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == item.NumeroResolucion &&
                                                                              p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                                              p.PkT002codigo == codigoCupoPK)
                                                                       .Select(a => a.A002fechaResolucion).FirstOrDefaultAsync();

                    string? anioResolucion = "";

                    if (fechaResolucion != null)
                        anioResolucion = fechaResolucion.Value.ToString("yyyy");

                    if (Convert.ToInt32(anioResolucion) != item.AnioResolucion)
                        return ResponseManager.generaRespuestaGenerica($"Año de resolucion no coincide", "", token, true);

                    var codigoZoocriadero = await context.CupostT002Cupos.Where(p => p.A002CodigoZoocriadero == item.CodigoZoocriaderoOrigen &&
                                                                                p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                                                p.A002numeroResolucion == item.NumeroResolucion &&
                                                                                p.PkT002codigo == codigoCupoPK)
                                                            .Select(a => a.A002CodigoZoocriadero).FirstOrDefaultAsync();

                    if (codigoZoocriadero is null)
                        return ResponseManager.generaRespuestaGenerica($"Codigo de Zoocriadero no coincide", "", token, true);

                    var codigoColor= await context.AdmintT008Parametricas
                                              .Where(p => p.A008parametrica == "COLOR" &&
                                                     p.A008valor == item.Color.ToUpper())
                                              .Select(p => p.PkT008codigo).FirstOrDefaultAsync();

                    if (codigoColor <= 0)
                        return ResponseManager.generaRespuestaGenerica($"El color No coincide al parametizado en el sistema", "", token, true);




                    var numeroPrecinto = await ValidarPrecintoNumero(item.NumeroPrecinto, especieToInt, codigoColor);

                    if (String.IsNullOrEmpty(numeroPrecinto))
                        return ResponseManager.generaRespuestaGenerica($"No se ha encontrado coincidencia del precinto numero: {item.NumeroPrecinto} en el sistema", "", token, true);


                }




            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("Error de validacion de datos en el Excel", "", token, true);

            }




            return ResponseManager.generaRespuestaGenerica("validarDatosExcelPrecintos", "", token, false);
        }

        public async Task<Responses> ConsultarActaVisitaporId(ClaimsIdentity identity, decimal idActaVisita)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var ciudad = await context.CupostT007ActaVisita.Where(c => c.PkT007codigo == idActaVisita)
                                                            .Select(c => c.A007CiudadDepartamento).FirstOrDefaultAsync();

            var departamento = 0;
            if (ciudad > 0)
                departamento = (int)await context.AdmintT004Ciudads.Where(d => d.PkT004codigo == ciudad)
                                                              .Select(d => d.A004codigoDepartamento).FirstOrDefaultAsync();

            var funcionarioCitesId = await context.CupostT007ActaVisita.Where(c => c.PkT007codigo == idActaVisita)
                                                           .Select(c => c.A007codigoUsuarioAutoridadCites).FirstOrDefaultAsync();


            var usuarioCITES = new AdmintT012Usuario();
            if (funcionarioCitesId > 0)
                usuarioCITES = await context.AdmintT012Usuarios.Where(u => u.PkT012codigo == funcionarioCitesId).FirstOrDefaultAsync();






            var ActaVisita = await (from empresa in context.CupostT001Empresas
                                    join actaVisitas in context.CupostT007ActaVisita
                                    on empresa.PkT001codigo equals actaVisitas.A007codigoEntidad
                                    join parametricas in context.AdmintT008Parametricas
                                    on empresa.A001codigoParametricaTipoEntidad equals parametricas.PkT008codigo
                                    where actaVisitas.PkT007codigo == idActaVisita
                                    select new
                                    {
                                        ActaVisitaId = actaVisitas.PkT007codigo,
                                        VisitaNumero = actaVisitas.A007VisitaNumero,
                                        TipoEstablecimiento = parametricas.PkT008codigo,
                                        CantidadPielACortar = actaVisitas.A007cantidadPielesAcortar,
                                        PrecintoIdentificacion = actaVisitas.A007codigoPrecintoymarquilla,
                                        EstadoPiel = actaVisitas.A007estadoPieles,
                                        FuncionarioAutoridadCites = actaVisitas.A007codigoUsuarioAutoridadCites,
                                        DocumentoRepresentante = actaVisitas.A007RepresentanteIdentificacion,
                                        RepresentanteEstablecimiento = actaVisitas.A007RepresentanteNombre,
                                        Fecha = actaVisitas.A007fechaActa,
                                        TipoEstablecimientoNombre = parametricas.A008valor,
                                        NombreEstablecimiento = empresa.A001nombre,
                                        EstablecimientoID = empresa.PkT001codigo,
                                        TipoActaVisita = actaVisitas.A007TipoActa,
                                        Ciudad = actaVisitas.A007CiudadDepartamento,
                                        Departamento = departamento,
                                        VisitaNumero1 = actaVisitas.A007VisitaNumero1,
                                        VisitaNumero2 = actaVisitas.A007VisitaNumero2,
                                        NombreFuncionarioCites = usuarioCITES != null ? usuarioCITES.A012login : null,
                                        NitEstablecimiento = empresa.A001nit
                                    }).ToListAsync();

            return ResponseManager.generaRespuestaGenerica("", ActaVisita, token, false);
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarTiposPartesLista(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var tiposPartesCortes = await genericsMethodsHelper.ObtenerListaparametricaPorNombre("TIPO PARTE PIEL");


            return ResponseManager.generaRespuestaGenerica("", tiposPartesCortes, token, false);
        }


        public async Task<Responses> CrearActaVisita(ClaimsIdentity identity, CupostT007ActaVisitum actaVisitaCorte, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            actaVisitaCorte.A007codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            actaVisitaCorte.A007codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            actaVisitaCorte.A007firmaUsuarioAutoridadCites = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            actaVisitaCorte.A007observaciones = "";
            actaVisitaCorte.A007codigoUsuarioAutoridadCites = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            context.CupostT007ActaVisita.Add(actaVisitaCorte);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", actaVisitaCorte, actaVisitaCorte.PkT007codigo.ToString());
            _ = context.SaveChanges();
            return ResponseManager.generaRespuestaGenerica("", actaVisitaCorte.PkT007codigo, token, false);
        }

        public async Task<Responses> CrearActaVisitaIrregular(ClaimsIdentity identity, CupostT007ActaVisitum actaVisitaCorteIrregular)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            actaVisitaCorteIrregular.A007codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            actaVisitaCorteIrregular.A007codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            actaVisitaCorteIrregular.A007firmaUsuarioAutoridadCites = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            actaVisitaCorteIrregular.A007observaciones = "";

            context.CupostT007ActaVisita.Add(actaVisitaCorteIrregular);
            await context.SaveChangesAsync();
            return ResponseManager.generaRespuestaGenerica("", actaVisitaCorteIrregular.PkT007codigo, token, false);
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ActualizarActaVisita(ClaimsIdentity identity, CupostT007ActaVisitum actaVisitaIdentificable, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var actaVisita = await context.CupostT007ActaVisita.Where(p => p.PkT007codigo == actaVisitaIdentificable.PkT007codigo).FirstOrDefaultAsync();
            if (actaVisita is null)
                return ResponseManager.generaRespuestaGenerica("Registro no encontrado", "", "", true);

            if (actaVisita.A007codigoEntidad != actaVisitaIdentificable.A007codigoEntidad)
            {
                valorAnterior.Add(actaVisita.A007codigoEntidad.ToString());
                valorActual.Add(actaVisitaIdentificable.A007codigoEntidad.ToString());
                campos.Add("A007codigoEntidad");
            }
            actaVisita.A007codigoEntidad = actaVisitaIdentificable.A007codigoEntidad;
            if (actaVisita.A007codigoPrecintoymarquilla != actaVisitaIdentificable.A007codigoPrecintoymarquilla)
            {
                valorAnterior.Add(actaVisita.A007codigoPrecintoymarquilla.ToString());
                valorActual.Add(actaVisitaIdentificable.A007codigoPrecintoymarquilla.ToString());
                campos.Add("A007codigoPrecintoymarquilla");
            }
            actaVisita.A007codigoPrecintoymarquilla = actaVisitaIdentificable.A007codigoPrecintoymarquilla;
            if (actaVisita.A007fechaActa != actaVisitaIdentificable.A007fechaActa)
            {
                valorAnterior.Add(actaVisita.A007fechaActa.ToString());
                valorActual.Add(actaVisitaIdentificable.A007fechaActa.ToString());
                campos.Add("A007fechaActa");
            }
            actaVisita.A007fechaActa = actaVisitaIdentificable.A007fechaActa;
            if (actaVisita.A007estadoPieles != actaVisitaIdentificable.A007estadoPieles)
            {
                valorAnterior.Add(actaVisita.A007estadoPieles);
                valorActual.Add(actaVisitaIdentificable.A007estadoPieles);
                campos.Add("A007estadoPieles");
            }
            actaVisita.A007estadoPieles = actaVisitaIdentificable.A007estadoPieles;
            if (actaVisita.A007cantidadPielesAcortar != actaVisitaIdentificable.A007cantidadPielesAcortar)
            {
                valorAnterior.Add(actaVisita.A007cantidadPielesAcortar.ToString());
                valorActual.Add(actaVisitaIdentificable.A007cantidadPielesAcortar.ToString());
                campos.Add("A007cantidadPielesAcortar");
            }
            actaVisita.A007cantidadPielesAcortar = actaVisitaIdentificable.A007cantidadPielesAcortar;
            if (actaVisita.A007RepresentanteIdentificacion != actaVisitaIdentificable.A007RepresentanteIdentificacion)
            {
                valorAnterior.Add(actaVisita.A007RepresentanteIdentificacion.ToString());
                valorActual.Add(actaVisitaIdentificable.A007RepresentanteIdentificacion.ToString());
                campos.Add("A007RepresentanteIdentificacion");
            }
            actaVisita.A007RepresentanteIdentificacion = actaVisitaIdentificable.A007RepresentanteIdentificacion;
            if (actaVisita.A007CiudadDepartamento != actaVisitaIdentificable.A007CiudadDepartamento)
            {
                valorAnterior.Add(actaVisita.A007CiudadDepartamento.ToString());
                valorActual.Add(actaVisitaIdentificable.A007CiudadDepartamento.ToString());
                campos.Add("A007CiudadDepartamento");
            }
            actaVisita.A007CiudadDepartamento = actaVisitaIdentificable.A007CiudadDepartamento;
            if (actaVisita.A007RepresentanteNombre != actaVisitaIdentificable.A007RepresentanteNombre)
            {
                valorAnterior.Add(actaVisita.A007RepresentanteNombre != null ? actaVisita.A007RepresentanteNombre : "");
                valorActual.Add(actaVisitaIdentificable.A007RepresentanteNombre != null ? actaVisitaIdentificable.A007RepresentanteNombre : "");
                campos.Add("A007RepresentanteNombre");
            }
            actaVisita.A007RepresentanteNombre = actaVisitaIdentificable.A007RepresentanteNombre;
            if (actaVisita.A007VisitaNumero != actaVisitaIdentificable.A007VisitaNumero)
            {
                valorAnterior.Add(actaVisita.A007VisitaNumero.ToString());
                valorActual.Add(actaVisitaIdentificable.A007VisitaNumero.ToString());
                campos.Add("A007VisitaNumero");
            }
            actaVisita.A007VisitaNumero = actaVisitaIdentificable.A007VisitaNumero;
            if (actaVisita.A007VisitaNumero1 != actaVisitaIdentificable.A007VisitaNumero1)
            {
                valorAnterior.Add(actaVisita.A007VisitaNumero1.ToString());
                valorActual.Add(actaVisitaIdentificable.A007VisitaNumero1.ToString());
                campos.Add("A007VisitaNumero1");
            }
            actaVisita.A007VisitaNumero1 = actaVisitaIdentificable.A007VisitaNumero1;
            if (actaVisita.A007VisitaNumero2 != actaVisitaIdentificable.A007VisitaNumero2)
            {
                valorAnterior.Add(actaVisita.A007VisitaNumero2.ToString());
                valorActual.Add(actaVisitaIdentificable.A007VisitaNumero2.ToString());
                campos.Add("A007VisitaNumero2");
            }
            actaVisita.A007VisitaNumero2 = actaVisitaIdentificable.A007VisitaNumero2;
            if (actaVisita.A007PrecintoAdjunto != actaVisitaIdentificable.A007PrecintoAdjunto)
            {
                valorAnterior.Add(actaVisita.A007PrecintoAdjunto ?? "");
                valorActual.Add(actaVisitaIdentificable.A007PrecintoAdjunto ?? "");
                campos.Add("A007PrecintoAdjunto");
            }
            actaVisita.A007PrecintoAdjunto = actaVisitaIdentificable.A007PrecintoAdjunto;

            actaVisita.A007codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            actaVisita.A007fechaModificacion = actaVisitaIdentificable.A007fechaModificacion;

            var result = await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smRegistroActaVisitaCortes, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", actaVisita.PkT007codigo.ToString());
            _ = context.SaveChanges();

            if (result > 0)
                return ResponseManager.generaRespuestaGenerica("Registro actualizado", "", token, false);
            else
                return ResponseManager.generaRespuestaGenerica("Error al actualizar datos", "", token, true);
        }


        public async Task<Responses> DesHabilitarActaVisita(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var actaVisita = await context.CupostT007ActaVisita.Where(p => p.PkT007codigo == idActaVisita).FirstOrDefaultAsync();
            if (actaVisita is null)
                return ResponseManager.generaRespuestaGenerica("Error, no se encontro el registro", "", "", true);

            if (actaVisita.A007estadoRegistro != 73)
            {
                valorAnterior.Add(actaVisita.A007estadoRegistro.ToString() ?? "");
                valorActual.Add("73");
                campos.Add("A007estadoRegistro");
            }
            actaVisita.A007estadoRegistro = 73;

            var result = await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smRegistroActaVisitaCortes, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", "Acta visita numero: " + actaVisita.PkT007codigo.ToString());
            _ = context.SaveChanges();

            if (result > 0)
                return ResponseManager.generaRespuestaGenerica("Registro Deshabilitado", "", token, false);

            else
                return ResponseManager.generaRespuestaGenerica("Error al procesar archivo", "", token, true);

        }


        public async Task<Responses> InsertarTipoPielIdentificable(ClaimsIdentity identity, CupostT008CortePiel tipoCortePielIdentificable, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            context.CupostT008CortePiels.Add(tipoCortePielIdentificable);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", tipoCortePielIdentificable, tipoCortePielIdentificable.A008codigo.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", tipoCortePielIdentificable.A008codigo, token, false);

        }

        public async Task<Responses> InsertTipoParteIdentificable(ClaimsIdentity identity, CupostT008CortePiel tipoParteIdentificable, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            context.CupostT008CortePiels.Add(tipoParteIdentificable);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", tipoParteIdentificable, tipoParteIdentificable.A008codigo.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", tipoParteIdentificable.A008codigo, token, false);

        }

        public async Task<Responses> InsertarTipoPielIrregular(ClaimsIdentity identity, CupostT008CortePiel tipoPielIrregular, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            context.CupostT008CortePiels.Add(tipoPielIrregular);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", tipoPielIrregular, tipoPielIrregular.A008codigo.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", tipoPielIrregular.A008codigo, token, false);
        }

        public async Task<Responses> InsertarTipoParteIrregular(ClaimsIdentity identity, CupostT008CortePiel tipoParteIrregular, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            context.CupostT008CortePiels.Add(tipoParteIrregular);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", tipoParteIrregular, tipoParteIrregular.A008codigo.ToString());
            _ = context.SaveChanges();

            if (tipoParteIrregular.A008codigo <= 0)
                return ResponseManager.generaRespuestaGenerica("Error al guardar registro", "", token, true);


            return ResponseManager.generaRespuestaGenerica("", tipoParteIrregular.A008codigo, token, false);
        }

        public async Task<Responses> InsertarActaVisitaDocumentoorigenPiel(ClaimsIdentity identity, CupostT015ActaVisitaDocumentoOrigenPiel documentoOrigenPiel, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            documentoOrigenPiel.A015CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            documentoOrigenPiel.A015CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            documentoOrigenPiel.A015FechaCreacion = DateTime.Now;
            documentoOrigenPiel.A015FechaModificacion = DateTime.Now;

            context.CupostT015ActaVisitaDocOrigenPiel.Add(documentoOrigenPiel);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", documentoOrigenPiel, documentoOrigenPiel.PK_T015Codigo.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", documentoOrigenPiel.PK_T015Codigo, token, false);

        }

        public async Task<Responses> InsertarActaVisitaResolucionNumero(ClaimsIdentity identity, CupostT016ActaVisitaResolucion documentoResolucion, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            documentoResolucion.A016CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            documentoResolucion.A016CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            documentoResolucion.A016FechaCreacion = DateTime.Now;
            documentoResolucion.A016FechaModificacion = DateTime.Now;

            context.CupostT016ActaVisitaNumResolucion.Add(documentoResolucion);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", documentoResolucion, "Numero de resolucion: " + documentoResolucion.A016ResolucionNumero.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", documentoResolucion.PK_T016Codigo, token, false);

        }

        public async Task<Responses> InsertarActaVisitaSAlvoConductoNumero(ClaimsIdentity identity, CupostT017ActaVisitaSalvoConducto documentoSalvoConducto, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            documentoSalvoConducto.A017CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            documentoSalvoConducto.A017CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            documentoSalvoConducto.A017FechaCreacion = DateTime.Now;
            documentoSalvoConducto.A017FechaModificacion = DateTime.Now;

            context.CupostT017ActaVisitaDocSalvoConducto.Add(documentoSalvoConducto);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", documentoSalvoConducto, documentoSalvoConducto.PK_T017Codigo.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", documentoSalvoConducto.PK_T017Codigo, token, false);

        }

        public async Task<Responses> EliminarDocumentosOrigenPiel(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var documentos = await context.CupostT015ActaVisitaDocOrigenPiel.Where(p => p.A015CodigoActaVisita == idActaVisita)
                                                                              .ToListAsync();

            if (documentos == null || !documentos.Any())
                return ResponseManager.generaRespuestaGenerica("No se encontraron coincidencias", "", token, false);


            context.CupostT015ActaVisitaDocOrigenPiel.RemoveRange(documentos);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smRegistroActaVisitaCortes, "", "", "", documentos, documentos.FirstOrDefault()?.A015CodigoActaVisita.ToString() ?? "");
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("Registros Procesados", "", token, false);


        }

        public async Task<Responses> EliminarDocResolucionActaVisita(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var documentos = await context.CupostT016ActaVisitaNumResolucion.Where(p => p.A016CodigoActaVisita == idActaVisita)
                                                                               .ToListAsync();

            if (documentos == null || !documentos.Any())
                return ResponseManager.generaRespuestaGenerica("No se encontraron coincidencias", "", token, false);


            context.CupostT016ActaVisitaNumResolucion.RemoveRange(documentos);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smRegistroActaVisitaCortes, "", "", "", documentos, documentos.FirstOrDefault()?.A016ResolucionNumero.ToString() ?? "");
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("Registros Procesados", "", token, false);


        }

        public async Task<Responses> EliminarDocsSalvoConductos(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var documentos = await context.CupostT017ActaVisitaDocSalvoConducto.Where(p => p.A017CodigoActaVisita == idActaVisita)
                                                                             .ToListAsync();

            if (documentos == null || !documentos.Any())
                return ResponseManager.generaRespuestaGenerica("No se encontraron coincidencias", "", token, false);


            context.CupostT017ActaVisitaDocSalvoConducto.RemoveRange(documentos);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smRegistroActaVisitaCortes, "", "", "", documentos, documentos.FirstOrDefault()?.A017CodigoActaVisita.ToString() ?? "");
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("Registros Procesados", "", token, false);

        }

        public async Task<Responses> EliminarTiposPielIdentificables(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var tipoPieles = await context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == idActaVisita
                                                                  && p.A008tipoPiel != "")
                                                                      .ToListAsync();

            if (tipoPieles == null || !tipoPieles.Any())
                return ResponseManager.generaRespuestaGenerica("No se encontraron coincidencias", "", token, false);


            context.CupostT008CortePiels.RemoveRange(tipoPieles);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smRegistroActaVisitaCortes, "", "", "", tipoPieles, "Acta visita numero: " + tipoPieles.FirstOrDefault()?.A008codigoActaVisita.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("Registros Procesados", "", token, false);

        }

        public async Task<Responses> EliminarTiposPartesIdentificables(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var tipoPartes = await context.CupostT008CortePiels.Where(p => p.A008codigoActaVisita == idActaVisita
                                                                         && p.A008tipoParte != "")
                                                                             .ToListAsync();

            if (tipoPartes == null || !tipoPartes.Any())
                return ResponseManager.generaRespuestaGenerica("No se encontraron coincidencias", "", token, false);


            context.CupostT008CortePiels.RemoveRange(tipoPartes);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smRegistroActaVisitaCortes, "", "", "", tipoPartes, "Acta Visita Numero: " + tipoPartes.FirstOrDefault()?.A008codigoActaVisita);
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("Registros Procesados", "", token, false);

        }

        public async Task<Responses> EliminarArchivosActaVisita(ClaimsIdentity identity, decimal idActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var archivosActaVisita = await context.CupostT018ActaVisitaArchivosDocumentos.Where(p => p.A018CodigoActaVisita == idActaVisita)
                                                                                                .ToListAsync();

            if (archivosActaVisita == null || !archivosActaVisita.Any())
                return ResponseManager.generaRespuestaGenerica("No se encontraron coincidencias", "", token, false);


            context.CupostT018ActaVisitaArchivosDocumentos.RemoveRange(archivosActaVisita);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smRegistroActaVisitaCortes, "", "", "", archivosActaVisita, "Acta Visita Numero: " + archivosActaVisita.FirstOrDefault()?.A018CodigoActaVisita);
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("Registros Procesados", "", token, false);

        }

        public async Task<Responses> InsertarActaVisitaDocumento(ClaimsIdentity identity, CupostT018ActaVisitaDocumentos archivoActaVisita, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            archivoActaVisita.A018CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            archivoActaVisita.A018CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            archivoActaVisita.A018FechaCreacion = DateTime.Now;
            archivoActaVisita.A018FechaModificacion = DateTime.Now;

            context.CupostT018ActaVisitaArchivosDocumentos.Add(archivoActaVisita);
            await context.SaveChangesAsync();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRegistroActaVisitaCortes, "", "", "", archivoActaVisita, archivoActaVisita.Pk_T018Codigo.ToString());
            _ = context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica("", archivoActaVisita.Pk_T018Codigo, token, false);

        }

        public async Task<List<AdmintT008Parametrica>> ObtenerDatosFTP()
        {
            var datosFTP = await context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP")
                                                                      .ToListAsync();
            return datosFTP;

        }

        [ExcludeFromCodeCoverage]
        private string ConvertirArchivoToBase64(string urlFTP)
        {

            try
            {
                var query1 = context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
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

                string dataAdjun = "";
                var tipoAchivo = new TiposArchivos();
                bool esPdf = urlFTP.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                bool esExcel = urlFTP.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);
                bool esWord = urlFTP.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                bool esPPTX = urlFTP.Contains(".pptx", System.StringComparison.CurrentCultureIgnoreCase);

                if (esPdf)
                {
                    tipoAchivo = TipoArchivo(".pdf");
                }
                if (esExcel)
                    tipoAchivo = TipoArchivo(".xlsx");

                if (esWord)
                    tipoAchivo = TipoArchivo(".docx");

                if (esPPTX)
                    tipoAchivo = TipoArchivo(".pptx");


                dataAdjun = tipoAchivo.TipoArchivo;

                #pragma warning disable SYSLIB0014
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlFTP);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(usuraio, clave);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                Metodos metodosFTP = new Metodos(context);
                var base64 = metodosFTP.ConvertToBase64(responseStream);



                return "data:" + dataAdjun + ";base64," + base64;
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ConnectFailure)
            {

                return "";
            }
            catch (WebException)
            {
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        [ExcludeFromCodeCoverage]
        private string ConvertirArchivoExcelToBase64(string urlFTP)
        {

            try
            {
                var query1 = context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
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

                Metodos metodosFTP = new Metodos(context);
                var base64 = metodosFTP.ConvertToBase64(responseStream);


                string dataAdjun = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return "data:" + dataAdjun + ";base64," + base64;

            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ConnectFailure)
            {
                
                return "";
            }
            catch (WebException)
            {                
                return "";
            }
            catch (Exception)
            {               
                return "";
            }

        }


        [ExcludeFromCodeCoverage]

        private sealed class ActaVisitasPropArchivos
        {
            public string? FileName { get; set; } = "";
            public string? Base64string { get; set; } = "";
            public string? FileType { get; set; } = "";
            public string? UrlFTP { get; set; } = "";

        }

        [ExcludeFromCodeCoverage]
        private sealed class TiposArchivos
        {
            public string TipoArchivo { get; set; } = "";
            public string Extension { get; set; } = "";


        }

        [ExcludeFromCodeCoverage]
        private sealed class ColumnasExcelPrecintos
        {
            public int AnioProduccion { get; set; } = 0;
            public int NumeroResolucion { get; set; } = 0;
            public int AnioResolucion { get; set; } = 0;
            public string CodigoZoocriaderoOrigen { get; set; } = "";
            public string NombreZoocriaderOrigen { get; set; } = "";
            public int NumInternoCodigo { get; set; } = 0;
            public int NumeracionInterna { get; set; } = 0;
            public int AnioPrecintoMovNacional { get; set; } = 0;
            public int NumeroPrecinto { get; set; } = 0;
            public DateTime FechaExpedicionSUNL { get; set; } = DateTime.Now;
            public int NumeroSUNL { get; set; } = 0;
            public string TipoSUNL { get; set; } = "";
            public string Color { get; set; } = "";


        }

        [ExcludeFromCodeCoverage]
        private string ObtenerTipoArchivo(string url)
        {
            var tipoAchivo = new TiposArchivos();
            bool esPdf = url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
            bool esExcel = url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);
            bool esWord = url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
            bool esPPTX = url.Contains(".pptx", System.StringComparison.CurrentCultureIgnoreCase);

            if (esPdf)
            {
                tipoAchivo = TipoArchivo(".pdf");
            }
            if (esExcel)
                tipoAchivo = TipoArchivo(".xlsx");

            if (esWord)
                tipoAchivo = TipoArchivo(".docx");

            if (esPPTX)
                tipoAchivo = TipoArchivo(".pptx");

            return tipoAchivo.TipoArchivo;

        }

        [ExcludeFromCodeCoverage]
        private TiposArchivos TipoArchivo(string extension)
        {


            List<TiposArchivos> listaTipos = new List<TiposArchivos>()
                                        {
                                            new TiposArchivos
                                            {
                                                TipoArchivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                                Extension = ".xlsx"

                                            },
                                             new TiposArchivos
                                            {
                                                TipoArchivo = "application/pdf",
                                                Extension = ".pdf"

                                            },
                                            new TiposArchivos
                                            {
                                                TipoArchivo = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                                                Extension = ".docx"

                                            },
                                            new TiposArchivos
                                            {
                                                TipoArchivo = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                                                Extension = ".pptx"

                                            },
                                        };

            var tipoArchivo = listaTipos.Find(a => a.Extension == extension);

            if(tipoArchivo != null)
            {
                return tipoArchivo;
            }
            else
            {
                return new TiposArchivos();
            }
        }

        private int obtenerColumna(string referencia)
        {
            int columna1 = 0;
            int columna2 = 0;
            if (referencia[0] >= 'A' && referencia[0] <= 'Z')
                columna1 = (referencia[0] - 'A') + 1;
            if (referencia[1] >= 'A' && referencia[1] <= 'Z')
                columna2 = (referencia[1] - 'A') + 1;
            return (columna2 > 0) ? columna1 * 26 + columna2 : columna1;
        }

        private static string GetCellValue(SpreadsheetDocument document, DocumentFormat.OpenXml.Spreadsheet.Cell cell)
        {
            if (document.WorkbookPart != null && document.WorkbookPart.SharedStringTablePart != null)
            {
                SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;

                if (cell.CellValue == null)
                {
                    return "";
                }
                string value = cell.CellValue.InnerXml;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
                else
                {
                    return value;
                }
            }

            return "";
        }

        [ExcludeFromCodeCoverage]
        private bool ValidarCeldaColumnas(int indiceFila, string[,] infoExcel)
        {

            bool datosValido = true;
            int[] tiposValores = { 1, 1, 1, 2, 2, 1, 1, 1, 1, 2, 3, 1, 2 };

            for (int i = 0; i < tiposValores.Length; i++)
            {
                var item = infoExcel[indiceFila, i];
                if (tiposValores[i] == (int)TiposValores.Numero)
                {
                    bool datoValido = ValidarTipoDatoEntero(item);
                    if (!datoValido)
                        return false;

                }
                if (tiposValores[i] == (int)TiposValores.Texto)
                {
                    bool datoValido = ValidarDatoVacioNulo(item);
                    if (!datoValido)
                        return false;

                }
                if (tiposValores[i] == (int)TiposValores.Fecha)
                {
                    bool datoValido = ValidarDatoFecha(item);
                    if (!datoValido)
                        return false;

                }

            }

            return datosValido;



        }

        [ExcludeFromCodeCoverage]
        private List<ColumnasExcelPrecintos> ObtenerListadoExcel(string base64Excel)
        {

            string[] columnasExcelPordefecto = {    "Año producción",
                                                    "No Resolución",
                                                    "Año Resolución",
                                                    "Codigo zoocriadero origen",
                                                    "Nombre zoocriadero origen",
                                                    "codigo num interna",
                                                    "numeracion interna",
                                                    "Año precinto mov nacional",
                                                    "No precinto",
                                                    "Color",
                                                    "Fecha expedición SUNL",
                                                    "No SUNL",
                                                    "Tipo SUNL"};


            List<ColumnasExcelPrecintos> listaDatosExcel = new List<ColumnasExcelPrecintos>();




            string[] base64 = base64Excel.Split(',');
            byte[] buffer = Convert.FromBase64String(base64[1]);
            var responseStream = new MemoryStream(buffer);

            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(responseStream, false);
            WorkbookPart wbPart = spreadsheetDocument.WorkbookPart;
            int numHojas = wbPart.Workbook.Sheets.Count();
            WorksheetPart worksheetPart = wbPart.WorksheetParts.First();
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            if (numHojas == 1)
            {
                int filamax = 1;
                foreach (Row r in sheetData.Elements<Row>())
                {
                    foreach (DocumentFormat.OpenXml.Spreadsheet.Cell celda in r.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                    {

                        string referencia = celda.CellReference;
                        int fila = 0;
                        for (int h = 0; h < referencia.Length; h++)
                        {
                            if (referencia[h] >= '0' && referencia[h] <= '9')
                                fila = fila * 10 + (referencia[h] - 48);
                        }
                        if (fila > filamax)
                            filamax = fila;
                    }
                }

                int totalFilas = filamax + 1;
                int totalColumnas = 22;

                string[,] info = new string[totalFilas, totalColumnas];
                for (int i = 0; i < totalFilas; i++)
                {
                    for (int j = 0; j < totalColumnas; j++)
                    {
                        info[i, j] = "";
                    }
                }

                string text;
                foreach (Row r in sheetData.Elements<Row>())
                {
                    foreach (DocumentFormat.OpenXml.Spreadsheet.Cell celda in r.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                    {

                        string referencia = celda.CellReference;
                        int fila = 0;
                        int columna = obtenerColumna(referencia) - 1;
                        for (int h = 0; h < referencia.Length; h++)
                        {
                            if (referencia[h] >= '0' && referencia[h] <= '9')
                                fila = fila * 10 + (referencia[h] - 48);
                        }
                        fila--;
                        if (columna < totalColumnas)
                        {
                            text = GetCellValue(spreadsheetDocument, celda);
                            info[fila, columna] = text;
                        }
                    }
                }
                spreadsheetDocument.Close();

                List<string> listaColumnasExcel = new List<string>();

                for (int i = 0; i < totalColumnas; i++)
                {
                    var item = info[0, i];
                    if (!String.IsNullOrEmpty(item))
                    {
                        listaColumnasExcel.Add(item.Trim());
                    }
                }

                string[] arrayColumnasArchivo = listaColumnasExcel.ToArray();

                var columnasValidas = Enumerable.SequenceEqual(columnasExcelPordefecto, arrayColumnasArchivo);

                if (!columnasValidas)
                    return listaDatosExcel;

                for (int i = 1; i < totalFilas; i++)
                {
                    var celdaFila = info[i, 0];
                    if (!String.IsNullOrEmpty(celdaFila))
                    {
                        var celdasValidadas = ValidarCeldaColumnas(i, info);
                        if (!celdasValidadas)
                            return listaDatosExcel;

                        listaDatosExcel.Add(
                            new ColumnasExcelPrecintos
                            {
                                AnioProduccion = Convert.ToInt32(info[i, 0]),
                                NumeroResolucion = Convert.ToInt32(info[i, 1]),
                                AnioResolucion = Convert.ToInt32(info[i, 2]),
                                CodigoZoocriaderoOrigen = info[i, 3],
                                NombreZoocriaderOrigen = info[i, 4],
                                NumInternoCodigo = Convert.ToInt32(info[i, 5]),
                                NumeracionInterna = Convert.ToInt32(info[i, 6]),
                                AnioPrecintoMovNacional = Convert.ToInt32(info[i, 7]),
                                NumeroPrecinto = Convert.ToInt32(info[i, 8]),
                                Color = info[i, 9].ToUpper(),
                                NumeroSUNL = Convert.ToInt32(info[i, 11]),
                                TipoSUNL = info[i, 12]

                            }
                         );
                    }

                }


            }

            return listaDatosExcel;

        }

        private enum TiposValores
        {
            Numero = 1,
            Texto = 2,
            Fecha = 3
        }

        [ExcludeFromCodeCoverage]
        private bool ValidarTipoDatoEntero(string valor)
        {
            bool datoValido = false;
            int numero = 0;
            bool canConvert = int.TryParse(valor, out numero);
            if (canConvert)
                datoValido = true;

            return datoValido;
        }

        private bool ValidarDatoVacioNulo(string valor)
        {
            bool datoValido = false;
            if (!String.IsNullOrEmpty(valor.Trim()))
                datoValido = true;
            return datoValido;
        }

        private bool ValidarDatoFecha(string valor)
        {
            bool datoValido = false;

            if (!String.IsNullOrEmpty(valor.Trim()))
            {
                datoValido = true;

            }

            return datoValido;

        }

        public async Task<bool> ModificarCantidadCupoPrecintos(ClaimsIdentity identity, string excelPrecintosBase64, decimal idEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            bool cantidadValido = false;


            if (token == null)
            {
                return false;
            }

            var listadoDatosExcel = ObtenerListadoExcel(excelPrecintosBase64);

            if (listadoDatosExcel.Any())
            {

                var cupoDisponible = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == listadoDatosExcel[0].NumeroResolucion &&
                                                                         p.A002CodigoZoocriadero == listadoDatosExcel[0].CodigoZoocriaderoOrigen && p.A002rangoCodigoInicial <= listadoDatosExcel[0].NumeracionInterna &&
                                                                         p.A002rangoCodigoFin >= listadoDatosExcel[0].NumeracionInterna &&
                                                                         p.A002codigoEmpresa == idEmpresa)
                                                                   .FirstOrDefaultAsync();

                if (cupoDisponible is null)
                    return false;

                var cantidadCupoDisponible = cupoDisponible.A002cuposDisponibles - listadoDatosExcel.Count;

                if (cantidadCupoDisponible <= 0)
                    return false;

                cupoDisponible.A002cuposDisponibles = cantidadCupoDisponible;


                var result = await context.SaveChangesAsync();

                if (result > 0)
                    cantidadValido = true;


            }

            return cantidadValido;


        }


        public async Task<bool> IncrementarCantidadCupoPrecintos(decimal idActavisita)
        {
            bool cantidadValido = false;

            string base64Excel = "";
            List<ColumnasExcelPrecintos> listaDatosExcel = new List<ColumnasExcelPrecintos>();

            var actaVisita = await context.CupostT007ActaVisita.Where(p => p.PkT007codigo == idActavisita).FirstOrDefaultAsync();
            if (actaVisita is null)
                return false;

            if (!String.IsNullOrEmpty(actaVisita.A007PrecintoAdjunto))
                base64Excel = ConvertirArchivoExcelToBase64(actaVisita.A007PrecintoAdjunto);

            if (String.IsNullOrEmpty(base64Excel))
                return false;

            listaDatosExcel = ObtenerListadoExcel(base64Excel);

            if (listaDatosExcel.Any())
            {
                var cupoDisponible = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == listaDatosExcel[0].NumeroResolucion &&
                                                                         p.A002CodigoZoocriadero == listaDatosExcel[0].CodigoZoocriaderoOrigen &&
                                                                         p.A002rangoCodigoInicial <= listaDatosExcel[0].NumeracionInterna &&
                                                                         p.A002rangoCodigoFin >= listaDatosExcel[0].NumeracionInterna &&
                                                                         p.A002codigoEmpresa == actaVisita.A007codigoEntidad)
                                                                   .FirstOrDefaultAsync();

                if (cupoDisponible is null)
                    return false;

                var especie = await context.CupostT005Especieaexportars
                                .Where(e => e.A005codigoCupo == cupoDisponible.PkT002codigo)
                                .Select(e => e.A005codigoEspecie).FirstOrDefaultAsync();

                if (!String.IsNullOrEmpty(especie))
                {
                    var especieToInt = Convert.ToDecimal(especie);

                    foreach (var item in listaDatosExcel)
                    {

                        var codigoColor = await context.AdmintT008Parametricas
                                         .Where(p => p.A008parametrica == "COLOR" &&
                                                p.A008valor == item.Color.ToUpper())
                                         .Select(p => p.PkT008codigo).FirstOrDefaultAsync();

                        var numeroPrecinto = await ValidarPrecintoNumero(item.NumeroPrecinto, especieToInt, codigoColor);
                        if (!String.IsNullOrEmpty(numeroPrecinto))
                        {
                            var codigoPrecintoExiste = await context.CupostT006Precintosymarquillas
                                                    .Where(p => p.A006codigoEspecieExportar == especieToInt &&
                                                           p.A006codigoPrecintoymarquilla == numeroPrecinto).FirstOrDefaultAsync();

                            if (codigoPrecintoExiste != null)
                            {
                                codigoPrecintoExiste.A006estadoRegistro = StringHelper.estadoActivo;
                                await context.SaveChangesAsync();
                            }

                        }

                    }
                }

                var cantidadCupoDisponible = cupoDisponible.A002cuposDisponibles + listaDatosExcel.Count;

                cupoDisponible.A002cuposDisponibles = cantidadCupoDisponible;

                var result = await context.SaveChangesAsync();

                if (result > 0)
                    cantidadValido = true;

            }

            return cantidadValido;

        }

        [ExcludeFromCodeCoverage]
        public async Task<string> ValidarPrecintoNumero(int numeroPrecinto, decimal especie, decimal CodigoColor)
        {

        

            var codigoPrecinto = await context.CupostT006Precintosymarquillas
                                                    .Where(p => p.A006codigoEspecieExportar == especie &&
                                                           p.A006estadoRegistro == StringHelper.estadoActivo &&
                                                           p.A006codigoParametricaColorPrecintosymarquillas == CodigoColor)
                                                    .Select(p => p.A006codigoPrecintoymarquilla).FirstOrDefaultAsync();

        


            if (String.IsNullOrEmpty(codigoPrecinto))
                return "";

            var codigoPrecintoConLetras = $"{codigoPrecinto.Substring(0, 10)}{numeroPrecinto}";

            var codigoPrecintoExiste = await context.CupostT006Precintosymarquillas
                                                    .Where(p => p.A006codigoEspecieExportar == especie &&
                                                           p.A006estadoRegistro == StringHelper.estadoActivo &&
                                                           p.A006codigoPrecintoymarquilla == codigoPrecintoConLetras &&
                                                           p.A006codigoParametricaColorPrecintosymarquillas == CodigoColor)
                                                    .Select(p => p.A006codigoPrecintoymarquilla).FirstOrDefaultAsync();

            if (String.IsNullOrEmpty(codigoPrecintoExiste))
                return "";

            else
                return codigoPrecintoExiste;



        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ActualizarNumeroPrecinto(ClaimsIdentity identity, string excelPrecintosBase64, decimal idEmpresa, decimal estado) 
        {

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var empresa = await context.CupostT001Empresas
                                        .Where(empresa => empresa.PkT001codigo == idEmpresa).FirstOrDefaultAsync();






            if (empresa is null)
                return ResponseManager.generaRespuestaGenerica("Error de Validacion de datos, el codigo de empresa no es valido", "", token, true);


            var listaDatosExcel = ObtenerListadoExcel(excelPrecintosBase64);


            if (listaDatosExcel.Any())
            {
                var codigoCupoPK = await context.CupostT002Cupos.Where(p => p.A002numeroResolucion == listaDatosExcel[0].NumeroResolucion &&
                                                         p.A002codigoEmpresa == empresa.PkT001codigo &&
                                                         p.A002CodigoZoocriadero == listaDatosExcel[0].CodigoZoocriaderoOrigen &&
                                                         p.A002rangoCodigoInicial <= listaDatosExcel[0].NumeracionInterna &&
                                                         p.A002rangoCodigoFin >= listaDatosExcel[0].NumeracionInterna
                                    ).Select(a => a.PkT002codigo).FirstOrDefaultAsync();

                if (codigoCupoPK <= 0)
                    return ResponseManager.generaRespuestaGenerica("Error de datos en Excel, no se ha coincidido con el registro de cupo", "", token, true);

                var especie = await context.CupostT005Especieaexportars
                                     .Where(e => e.A005codigoCupo == codigoCupoPK)
                                     .Select(e => e.A005codigoEspecie).FirstOrDefaultAsync();

                if (String.IsNullOrEmpty(especie) || especie == "0")
                    return ResponseManager.generaRespuestaGenerica($"Error de datos en Excel, no hay especie para asociar los precintos", "", token, true);

                var especieToInt = Convert.ToDecimal(especie);

                foreach (var item in listaDatosExcel)
                {

                    var codigoColor = await context.AdmintT008Parametricas
                                        .Where(p => p.A008parametrica == "COLOR" &&
                                               p.A008valor == item.Color.ToUpper())
                                        .Select(p => p.PkT008codigo).FirstOrDefaultAsync();

                    var numeroPrecinto = await ValidarPrecintoNumero(item.NumeroPrecinto, especieToInt, codigoColor);

                   

                    if (!String.IsNullOrEmpty(numeroPrecinto))
                    {
                        var codigoPrecintoExiste = await context.CupostT006Precintosymarquillas
                                                .Where(p => p.A006codigoEspecieExportar == especieToInt &&
                                                       p.A006codigoPrecintoymarquilla == numeroPrecinto &&
                                                       p.A006codigoParametricaColorPrecintosymarquillas == codigoColor).FirstOrDefaultAsync();

                        if(codigoPrecintoExiste != null)
                        {
                            codigoPrecintoExiste.A006estadoRegistro = estado;
                            await context.SaveChangesAsync();
                        }                       

                    }

                }

                return ResponseManager.generaRespuestaGenerica($"Se han actualizado los datos de precintos", "", token, false);


            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("Error de Validacion de datos, no se enuentran coincidencias en el documento", "", token, true);

            }

        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> IngresarSalvoConductosExcel(ClaimsIdentity identity, string excelPrecintosBase64, decimal idActaVisita)
        {

            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var actaVisita = await context.CupostT007ActaVisita
                                        .Where(empresa => empresa.PkT007codigo == idActaVisita).FirstOrDefaultAsync();



            if (actaVisita is null)
                return ResponseManager.generaRespuestaGenerica("Error de Validacion de datos, el codigo de Acta Visita para SalvoConducto no es valido", "", token, true);

            


            var listaDatosExcel = ObtenerListadoExcel(excelPrecintosBase64);


            if (listaDatosExcel.Any())
            {  
                foreach (var item in listaDatosExcel)
                {
                    var actaVisitumDB = new CupostT017ActaVisitaSalvoConducto();

                    if(item.NumeroSUNL > 0)
                    {
                        actaVisitumDB.A017CodigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        actaVisitumDB.A017CodigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        actaVisitumDB.A017FechaCreacion = DateTime.Now;
                        actaVisitumDB.A017FechaModificacion = DateTime.Now;
                        actaVisitumDB.A017SalvoConductoNumero = item.NumeroSUNL;
                        actaVisitumDB.A017CodigoActaVisita = idActaVisita;

                        context.CupostT017ActaVisitaDocSalvoConducto.Add(actaVisitumDB);
                        await context.SaveChangesAsync();
                    }                   

                }

                return ResponseManager.generaRespuestaGenerica($"Se ha registrado el numero Salvoconducto", "", token, false);


            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("Error de Validacion de datos, no se encuentran coincidencias en el documento salvoconducto", "", token, true);

            }

        }

        [ExcludeFromCodeCoverage]
        private async Task<bool> ValidarCuposCartaVenta(CupostT001Empresa empresa, ColumnasExcelPrecintos columnaExcel)
        {
            var resultado = await (from facturaCompraCupo in context.CupostT026FacturaCompraCupo
                             join facturaCompraVenta in context.CupostT004FacturacompraCartaventa on facturaCompraCupo.A026CodigoFacturaCompra equals facturaCompraVenta.PkT004codigo
                             join cupo in context.CupostT002Cupos on facturaCompraCupo.A026CodigoCupo equals cupo.PkT002codigo
                             where cupo.A002numeroResolucion == columnaExcel.NumeroResolucion
                                   && facturaCompraVenta.A004codigoEntidadCompra == empresa.PkT001codigo
                                   && cupo.A002CodigoZoocriadero == columnaExcel.CodigoZoocriaderoOrigen
                                   && cupo.A002rangoCodigoInicial <= columnaExcel.NumeracionInterna
                                   && cupo.A002rangoCodigoFin >= columnaExcel.NumeracionInterna
                             select new
                             {

                                 CodigoFactura = facturaCompraCupo.A026CodigoFacturaCompra,
                                 CodigoCupo = cupo.PkT002codigo

                             }).AnyAsync();

            return resultado;

        }

        [ExcludeFromCodeCoverage]

        private async Task<CupostT001Empresa> ObtenerEmpresaZoocriaderoOrigen(CupostT001Empresa empresa, ColumnasExcelPrecintos columnaExcel)
        {
            var resultado = await (from facturaCompraCupo in context.CupostT026FacturaCompraCupo
                                   join facturaCompraVenta in context.CupostT004FacturacompraCartaventa on facturaCompraCupo.A026CodigoFacturaCompra equals facturaCompraVenta.PkT004codigo
                                   join cupo in context.CupostT002Cupos on facturaCompraCupo.A026CodigoCupo equals cupo.PkT002codigo
                                   where cupo.A002numeroResolucion == columnaExcel.NumeroResolucion
                                         && facturaCompraVenta.A004codigoEntidadCompra == empresa.PkT001codigo
                                         && cupo.A002CodigoZoocriadero == columnaExcel.CodigoZoocriaderoOrigen
                                         && cupo.A002rangoCodigoInicial <= columnaExcel.NumeracionInterna
                                         && cupo.A002rangoCodigoFin >= columnaExcel.NumeracionInterna
                                   select new
                                   {
                                       CodigoEmpresaZoocriadero = cupo.A002codigoEmpresa
                                   }).FirstOrDefaultAsync();

            var empresaZoocriaadero = await context.CupostT001Empresas
                                       .Where(empresa => empresa.PkT001codigo == (resultado != null ? resultado.CodigoEmpresaZoocriadero : 0)) 
                                       .FirstOrDefaultAsync() ?? new CupostT001Empresa();



            return empresaZoocriaadero;
        }
        
    }
}
