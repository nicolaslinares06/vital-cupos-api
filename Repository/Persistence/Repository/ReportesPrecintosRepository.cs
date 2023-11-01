using API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ReportesPrecintosModels;

namespace Repository.Persistence.Repository
{
    public class ReportesPrecintosRepository : IReportesPrecintosRepository
    {
        private readonly DBContext context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public readonly IConfiguration configuration;

        public ReportesPrecintosRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        [ExcludeFromCodeCoverage]
        public async Task<Responses> ConsultarDatosPrecintos(ClaimsIdentity identity, SealFilters filtros)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<SealDataModel> data = new List<SealDataModel>();

            var estadoAprobado = configuration.GetValue<string>("EstadosCupos:Aprobado:IdEstado");

            var query = from solicitud in context.CupostT019Solicitudes
                        from empresa in context.CupostT001Empresas
                        where solicitud.A019CodigoEmpresa == empresa.PkT001codigo
                        select new
                        {
                            solicitud,
                            empresa.A001nombre,
                            empresa.A001nit,
                            empresa.A001telefono
                        };

            query = query.Where(x => (x.solicitud.A019TipoSolicitud == StringHelper.tipoSolictudPrecintos) && (x.solicitud.A019EstadoSolicitud == Convert.ToDecimal(estadoAprobado)));

            if (filtros.Establishment != 0)
                query = query.Where(x => x.solicitud.A019CodigoEmpresa == filtros.Establishment);

            if (filtros.NIT != 0 && filtros.NIT != null)
                query = query.Where(x => x.A001nit == filtros.NIT);

            if(filtros.ResolutionNumber != "" && filtros.ResolutionNumber != null)
                query = query.Where(x => x.solicitud.A019NumeroRadicacion == filtros.ResolutionNumber);

            if (filtros.RadicationDate != null)
                query = query.Where(x => x.solicitud.A019FechaRadicacion == filtros.RadicationDate);

            foreach (var registro in query)
            {
                SealDataModel tempData = new SealDataModel();
                tempData.RadicationNumber = registro.solicitud.A019NumeroRadicacion;
                tempData.RadicationDate = registro.solicitud.A019FechaRadicacion?.ToString("dd/MM/yyyy");
                tempData.CompanyName = registro.A001nombre;
                tempData.NIT = registro.A001nit;
                tempData.DeliveryAddress = registro.solicitud.A019DireccionEntrega;
                tempData.Telephone = registro.A001telefono.ToString();
                tempData.LesserLength = registro.solicitud.A019LongitudMenor;
                tempData.GreaterLength = registro.solicitud.A019LongitudMayor;
                tempData.Quantity = registro.solicitud.A019Cantidad;
                tempData.ProductionYear = 0;
                tempData.CompanyCode = registro.solicitud.A019CodigoEmpresa;
                tempData.DepositValue = registro.solicitud.A019ValorConsignacion == 0 ? "" : registro.solicitud.A019ValorConsignacion.ToString();

                var ciudad = from ciu in context.AdmintT004Ciudads
                             where registro.solicitud.A019CodigoCiudad == ciu.PkT004codigo
                             select ciu;

                tempData.City = ciudad.FirstOrDefault()?.A004nombre;

                var especimen = from especiman in context.AdmintT005Especimen
                                where registro.solicitud.A019CodigoEspecieExportar == especiman.PkT005codigo
                                select new
                                {
                                    especiman
                                };

                if (especimen.Any())
                {
                    tempData.Species = especimen.FirstOrDefault()?.especiman.A005nombreCientifico;
                }
                else
                {
                    tempData.Species = "";
                }

                var PyM = from marquillas in context.CupostT006Precintosymarquillas
                          from color in context.AdmintT008Parametricas
                          where marquillas.A006codigoParametricaColorPrecintosymarquillas == color.PkT008codigo
                          where registro.solicitud.Pk_T019Codigo == marquillas.A006codigoSolicitud
                          select new
                          {
                              marquillas,
                              color.A008valor
                          };

                if (PyM.Any())
                {
                    tempData.InitialNumber = PyM.FirstOrDefault()?.marquillas.A006numeroInicial is null ? "" : PyM.FirstOrDefault()?.marquillas.A006numeroInicial;
                    tempData.FinalNumber = PyM.FirstOrDefault()?.marquillas.A006numeroFinal is null ? "" : PyM.FirstOrDefault()?.marquillas.A006numeroFinal;
                    tempData.InitialInternalNumber = PyM.FirstOrDefault()?.marquillas.A006numeroInicialNumerico;
                    tempData.FinalInternalNumber = PyM.FirstOrDefault()?.marquillas.A006numeroFinalNumerico;
                    tempData.Color = PyM.FirstOrDefault()?.A008valor;
                }
                else
                {
                    tempData.InitialNumber = "";
                    tempData.FinalNumber = "";
                    tempData.InitialInternalNumber = 0;
                    tempData.FinalInternalNumber = 0;
                    tempData.Color = "";
                }

                if (registro.solicitud.A019AnalistaAsignacion != null)
                {
                    var evaluador = from usuario in context.AdmintT012Usuarios
                                    where registro.solicitud.A019AnalistaAsignacion == usuario.PkT012codigo
                                    select usuario;

                    tempData.Analyst = evaluador.FirstOrDefault()?.A012primerNombre + " " + evaluador.FirstOrDefault()?.A012primerApellido;
                }
                else
                {
                    tempData.Analyst = "";
                }

                data.Add(tempData);
            }
            
            return ResponseManager.generaRespuestaGenerica("", data, token, false);
        }

        public async Task<Responses> ConsultarEstablecimientos(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var empresasEstablecimientos = await context.CupostT001Empresas.ToListAsync();

            var listaEmpresas = new List<EstablishmentProperties>();

            if (empresasEstablecimientos is not null)
            {
                listaEmpresas = empresasEstablecimientos
                                .Select(s => new EstablishmentProperties
                                {
                                    EstablishmentId  = s.PkT001codigo,
                                    EstablishmentName = s.A001nombre
                                }).ToList();

            }

            return ResponseManager.generaRespuestaGenerica("", listaEmpresas, token, false);
        }
    }
}
