using API.Helpers;
using Microsoft.Extensions.Configuration;
using Repository.Helpers;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ReportesMarquillasModels;

namespace Repository.Persistence.Repository
{
    public class ReportesMarquillasRepository : IReportesMarquillasRepository
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public readonly SettingsHelper setting;
        public readonly IConfiguration configuration;

        public ReportesMarquillasRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            setting = new SettingsHelper();

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        [ExcludeFromCodeCoverage]
        public Responses ConsultarMarquillas(ClaimsIdentity identity, TagsFilters filtros)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            List<TagsData> data = new List<TagsData>();

            var estadoAprobado = configuration.GetValue<string>("EstadosCupos:Aprobado:IdEstado");

            var query = from solicitud in _context.CupostT019Solicitudes
                        from empresa in _context.CupostT001Empresas
                        where solicitud.A019CodigoEmpresa == empresa.PkT001codigo
                        select new
                        {
                            solicitud,
                            empresa.A001nombre,
                            empresa.A001nit,
                            empresa.A001telefono
                        };

            query = query.Where(x => (x.solicitud.A019TipoSolicitud == StringHelper.tipoSolictudMarquillas || x.solicitud.A019TipoSolicitud == StringHelper.tipoSolictudMarquillasAutoridadAmbiental) && (x.solicitud.A019EstadoSolicitud == Convert.ToDecimal(estadoAprobado)));
            
            if (filtros.DateFrom != null)
            {
                query = query.Where(x => x.solicitud.A019FechaRadicacion >= filtros.DateFrom);
            }

            if (filtros.DateTo != null)
            {
                query = query.Where(x => x.solicitud.A019FechaRadicacion <= filtros.DateTo);
            }

            if (filtros.RadicationNumber != null)
            {
                query = query.Where(x => x.solicitud.A019NumeroRadicacion == filtros.RadicationNumber);
            }

            foreach (var registro in query)
            {
                TagsData DataTemp = new TagsData();

                DataTemp.RadicationNumber = registro.solicitud.A019NumeroRadicacion;
                DataTemp.RadicationDate = registro.solicitud.A019FechaRadicacion;
                DataTemp.CompanyName = registro.A001nombre;
                DataTemp.NIT = registro.A001nit;
                DataTemp.Address = registro.solicitud.A019DireccionEntrega;
                DataTemp.Phone = registro.A001telefono;
                DataTemp.Amount = registro.solicitud.A019Cantidad;
                DataTemp.ConsignmentValue = registro.solicitud.A019ValorConsignacion;
                DataTemp.AnswerDate = registro.solicitud.A019FechaRadicacionSalida;

                var especimen = (from especiman in _context.AdmintT005Especimen
                                where registro.solicitud.A019CodigoEspecieExportar == especiman.PkT005codigo
                                select new
                                {
                                    especiman
                                }).FirstOrDefault();

                if (especimen != null)
                {
                    var tipo = (from parametricaTipo in _context.AdmintT008Parametricas
                               where especimen.especiman.A005codigoTipoEspecimen == parametricaTipo.PkT008codigo
                               select parametricaTipo).FirstOrDefault();

                    if (tipo != null)
                    {
                        DataTemp.Type = tipo.A008valor;
                    }
                    else
                    {
                        DataTemp.Type = "";
                    }

                    DataTemp.Species = especimen.especiman.A005nombreCientifico;
                    DataTemp.SpeciesTags = especimen.especiman.A005nombre;
                }
                else
                {
                    DataTemp.Species = "";
                    DataTemp.SpeciesTags = "";
                }
                

                var ciudad = (from ciu in _context.AdmintT004Ciudads
                             where registro.solicitud.A019CodigoCiudad == ciu.PkT004codigo
                             select ciu).FirstOrDefault();

                var PyM = (from marquillas in _context.CupostT006Precintosymarquillas
                          from color in _context.AdmintT008Parametricas
                          where marquillas.A006codigoParametricaColorPrecintosymarquillas == color.PkT008codigo
                          where registro.solicitud.Pk_T019Codigo == marquillas.A006codigoSolicitud
                          select new
                          {
                              marquillas,
                              color.A008valor
                          }).FirstOrDefault();

                if(registro.solicitud.A019AnalistaAsignacion != null)
                {
                    var evaluador = (from usuario in _context.AdmintT012Usuarios
                                    where registro.solicitud.A019AnalistaAsignacion == usuario.PkT012codigo
                                    select usuario).FirstOrDefault();

                    if(evaluador != null)
                        DataTemp.Evaluator = evaluador.A012primerNombre + " " + evaluador.A012primerApellido;
                }
                else
                {
                    DataTemp.Evaluator = "";
                }
                
                if (ciudad != null)
                    DataTemp.City = ciudad.A004nombre;

                if (PyM != null)
                {
                    DataTemp.InitialNumber = PyM.marquillas.A006numeroInicial;
                    DataTemp.FinalNumber = PyM.marquillas.A006numeroFinal;
                    DataTemp.InitialNumberTags = PyM.marquillas.A006numeroInicialNumerico.ToString();
                    DataTemp.FinalNumberTags = PyM.marquillas.A006numeroFinalNumerico.ToString();
                    DataTemp.Color = PyM.A008valor;
                }
                else
                {
                    DataTemp.InitialNumber = "";
                    DataTemp.FinalNumber = "";
                    DataTemp.InitialNumberTags = "";
                    DataTemp.FinalNumberTags = "";
                    DataTemp.Color = "";
                }

                data.Add(DataTemp);
            }

            return ResponseManager.generaRespuestaGenerica("", data, token, false);
        }
    }
}
