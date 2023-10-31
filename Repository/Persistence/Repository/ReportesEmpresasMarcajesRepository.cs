using API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Helpers;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ReportesEmpresasMarcajeModels;

namespace Repository.Persistence.Repository
{
    public class ReportesEmpresasMarcajesRepository : IReportesEmpresasMarcaje
    {
        private readonly DBContext context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly IConfiguration configuration;

        public ReportesEmpresasMarcajesRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, IConfiguration configuration)
        {
            this.context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.configuration = configuration;
        }


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

        public Responses ConsultarDatosReportes(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            
            List<DatosEmpresasModel> datosEmpresas = new List<DatosEmpresasModel>();
            
            var query = from datos in context.CuposV001ReportesEmpresas
                        from parEnt in context.AdmintT008Parametricas
                        from estEstablecimiento in context.AdmintT008Parametricas
                        from estCITES in context.AdmintT008Parametricas
                        from especie in context.AdmintT005Especimen
                        where datos.Especies == especie.PkT005codigo.ToString()
                        where datos.EstadoEmisionCITES == estCITES.PkT008codigo
                        where datos.Estado == estEstablecimiento.PkT008codigo
                        where datos.TipoEmpresa == parEnt.PkT008codigo
                        select new
                        {
                            datos,
                            tipoEmpresa = parEnt.A008valor,
                            estEstablecimiento = estEstablecimiento.A008valor,
                            estCITES = estCITES.A008valor,
                            especie = especie.A005nombreCientifico
                        };

            foreach (var Entidad in query)
            {
                DatosEmpresasModel datos = new DatosEmpresasModel();

                datos.TipoEmpresa = Entidad.tipoEmpresa;
                datos.NombreEmpresa = Entidad.datos.NombreEmpresa;
                datos.NIT = Entidad.datos.NIT;
                datos.Estado = Entidad.estEstablecimiento;
                datos.EstadoEmisionCITES = Entidad.estCITES;
                datos.NumeroResolucion = Entidad.datos.NumeroResolucion.ToString();
                datos.FechaEmisionResolucion = Entidad.datos.FechaResolucion.ToString();
                datos.Especies = Entidad.especie;
                datos.Machos = Entidad.datos.Machos;
                datos.Hembras = Entidad.datos.Hembras;
                datos.PoblacionTotalParental = Entidad.datos.PoblacionTotalParental;
                datos.AnioProduccion = Entidad.datos.AnioProduccion;
                datos.CuposComercializacion = Entidad.datos.CuposComercializacion;
                datos.CuotaRepoblacion = Entidad.datos.CuotaRepoblacion;
                datos.CuposAsignadosTotal = Entidad.datos.CuposAsignadosTotal;
                datos.SoportesRepoblacion = true;
                datos.CupoUtilizado = Entidad.datos.CupoUtilizado;
                datos.CupoDisponible = Entidad.datos.CupoDisponible;

                datosEmpresas.Add(datos);
            }

            return ResponseManager.generaRespuestaGenerica("", datosEmpresas, token, false);
        }

        public async Task<Responses> ConsultarDatosEmpresas(ClaimsIdentity identity, BusinessFilters filtros)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var consultaDatos = new List<DatosEmpresasModel>();
            var query = await listaDatos(filtros);

            if (query.Any())
            {
                consultaDatos = query.Select(itemQuery => new DatosEmpresasModel
                {
                    TipoEmpresa = context.AdmintT008Parametricas
                                          .Where(e => e.PkT008codigo == itemQuery.TipoEmpresa)
                                          .Select(s => s.A008valor)
                                          .FirstOrDefault(),
                    NombreEmpresa = itemQuery.NombreEmpresa,
                    NIT = itemQuery.NIT,
                    Estado = Convert.ToInt32(itemQuery.Estado) == StringHelper.estadoActivo ? "ACTIVO" : "INACTIVO",
                    EstadoEmisionCITES = context.AdmintT008Parametricas
                                          .Where(e => e.PkT008codigo == Convert.ToDecimal(itemQuery.EstadoEmisionCITES))
                                          .Select(s => s.A008valor)
                                          .FirstOrDefault(),
                    NumeroResolucion = itemQuery.NumeroResolucion.ToString(),
                    FechaEmisionResolucion = itemQuery.FechaResolucion == null ? "" : itemQuery.FechaResolucion?.ToString("dd/MM/yyyy"),
                    Especies = context.AdmintT005Especimen
                                .Where(e => e.PkT005codigo == Convert.ToDecimal(itemQuery.Especies))
                                .Select(s => s.A005nombreCientifico).FirstOrDefault(),
                    Machos = itemQuery.Machos,
                    Hembras = itemQuery.Hembras,
                    PoblacionTotalParental = itemQuery.PoblacionTotalParental,
                    AnioProduccion = itemQuery.AnioProduccion,
                    CuposComercializacion = itemQuery.CuposComercializacion,
                    CuotaRepoblacion = itemQuery.CuotaRepoblacion == null ? "" : itemQuery.CuotaRepoblacion,
                    CuposAsignadosTotal = itemQuery.CuposAsignadosTotal,
                    SoportesRepoblacion = itemQuery.FechaRadicacion == null ? false : true,
                    CupoUtilizado = itemQuery.CupoUtilizado,
                    CupoDisponible = itemQuery.CupoDisponible
                }).ToList();






            }



            return ResponseManager.generaRespuestaGenerica("", consultaDatos, token, false);

        }

        private async Task<List<Models.CuposV001ReportesEmpresasMarcaje>> listaDatos(BusinessFilters filtros)
        {
            var query = new List<Models.CuposV001ReportesEmpresasMarcaje>();

            switch (filtros.CombinationType)
            {
                case 1:
                    query = await ListaCombinacionUnFiltro(filtros);
                    break;

                case 2:
                    query = await ListaCombinacionDosFiltros(filtros);
                    break;

                default:
                    query = new List<Models.CuposV001ReportesEmpresasMarcaje>();
                    break;

            }

            if (query is null)
                query = new List<Models.CuposV001ReportesEmpresasMarcaje>();

            return query;


        }

        private async Task<List<Models.CuposV001ReportesEmpresasMarcaje>> ListaCombinacionUnFiltro(BusinessFilters filtros)
        {
            var query = new List<Models.CuposV001ReportesEmpresasMarcaje>();
            switch (filtros.SpecificSearch)
            {

                case 1:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType).ToListAsync();
                    break;

                case 2:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName).ToListAsync();
                    break;
                case 3:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NIT == filtros.NIT).ToListAsync();
                    break;

                case 4:
                    query = await context.CuposV001ReportesEmpresas
                            .Where(q => q.Estado == filtros.Status).ToListAsync();
                    break;

                case 5:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.EstadoEmisionCITES == filtros.CITESIssuanceStatus).ToListAsync();
                    break;

                case 6:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NumeroResolucion == filtros.ResolutionNumber).ToListAsync();
                    break;

                case 7:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;

                case 8:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                default:
                    query = new List<Models.CuposV001ReportesEmpresasMarcaje>();
                    break;

            }

            if (query is null)
                query = new List<Models.CuposV001ReportesEmpresasMarcaje>();

            return query;


        }

        private async Task<List<Models.CuposV001ReportesEmpresasMarcaje>> ListaCombinacionDosFiltros(BusinessFilters filtros)
        {
            var query = new List<Models.CuposV001ReportesEmpresasMarcaje>();
            switch (filtros.SpecificSearch)
            {

                case 1:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.NombreEmpresa == filtros.CompanyName).ToListAsync();
                    break;

                case 2:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.NIT == filtros.NIT).ToListAsync();
                    break;


                case 3:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.Estado == filtros.Status).ToListAsync();
                    break;


                case 4:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.EstadoEmisionCITES == filtros.CITESIssuanceStatus).ToListAsync();
                    break;

                case 5:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.NumeroResolucion == filtros.ResolutionNumber).ToListAsync();
                    break;

                case 6:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;

                case 7:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.TipoEmpresa == filtros.BusinessType
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                case 8:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName
                                && q.NIT == filtros.NIT).ToListAsync();
                    break;

                case 9:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName
                                && q.Estado == filtros.Status).ToListAsync();
                    break;

                case 10:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName
                                && q.EstadoEmisionCITES == filtros.CITESIssuanceStatus).ToListAsync();
                    break;

                case 11:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName
                                && q.NumeroResolucion == filtros.ResolutionNumber).ToListAsync();
                    break;

                case 12:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName
                                && q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;
                case 13:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NombreEmpresa == filtros.CompanyName
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                case 14:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NIT == filtros.NIT
                                && q.Estado == filtros.Status).ToListAsync();
                    break;
                case 15:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NIT == filtros.NIT
                                && q.EstadoEmisionCITES == filtros.CITESIssuanceStatus).ToListAsync();
                    break;

                case 16:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NIT == filtros.NIT
                                && q.NumeroResolucion == filtros.ResolutionNumber).ToListAsync();
                    break;
                case 17:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NIT == filtros.NIT
                                && q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;
                case 18:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NIT == filtros.NIT
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                case 19:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.Estado == filtros.Status
                                && q.EstadoEmisionCITES == filtros.CITESIssuanceStatus).ToListAsync();
                    break;

                case 20:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.Estado == filtros.Status
                                && q.NumeroResolucion == filtros.ResolutionNumber).ToListAsync();
                    break;

                case 21:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.Estado == filtros.Status
                                && q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;

                case 22:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.Estado == filtros.Status
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;


                case 23:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.EstadoEmisionCITES == filtros.CITESIssuanceStatus
                                && q.NumeroResolucion == filtros.ResolutionNumber).ToListAsync();
                    break;

                case 24:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.EstadoEmisionCITES == filtros.CITESIssuanceStatus
                                && q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;

                case 25:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.EstadoEmisionCITES == filtros.CITESIssuanceStatus
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                case 26:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NumeroResolucion == filtros.ResolutionNumber
                                && q.FechaResolucion >= filtros.ResolutionIssuanceStartDate ).ToListAsync();
                    break;


                case 27:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.NumeroResolucion == filtros.ResolutionNumber
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                case 28:
                    query = await context.CuposV001ReportesEmpresas
                                .Where(q => q.FechaResolucion >= filtros.ResolutionIssuanceStartDate 
                                && q.FechaResolucion <= filtros.ResolutionIssuanceEndDate).ToListAsync();
                    break;

                default:
                    query = new List<Models.CuposV001ReportesEmpresasMarcaje>();
                    break;

            }

            if (query is null)
                query = new List<Models.CuposV001ReportesEmpresasMarcaje>();

            return query;


        }

    }

}