using API.Helpers;
using Microsoft.AspNetCore.Http;
using NHibernate.Driver;
using NHibernate.Linq;
using Repository.Configurations;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Persistence.Repository
{
    public class QuotasReportsRepository : IQuotasReports
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;


        public QuotasReportsRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// consultar resoluciones
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resolutionNumber"></param>
        /// <param name="BussinesName"></param>
        /// <param name="BussinesNit"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Responses ConsultResolutions(ClaimsIdentity identity, string? resolutionNumber, string? BussinesName, string? BussinesNit, string? fromDate, string? toDate)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                DateTime? dateEmpty = new DateTime();
                List<Cupos> quotas = new List<Cupos>();
                if (resolutionNumber==null && BussinesName==null && BussinesNit == null && fromDate == null && toDate == null)
                {
                    var cupos = _context.CuposV001ResolucionCupos.ToList();
                    foreach(var cupo in cupos)
                    {
                        quotas.Add(llenarObjetoCupos(cupo));
                    }
                }
                else
                {
                    DateTime initialDate = new DateTime();
                    DateTime finalDate = new DateTime();
                    if (fromDate != null)
                    {
                        var fDate=fromDate.Replace(" 12:00:00 a. m.", String.Empty);
                        string[] initial = fDate.Split("/");
                        initialDate = new DateTime(Int32.Parse(initial[2]), Int32.Parse(initial[1]), Int32.Parse(initial[0]));
                    }
                    if (toDate != null)
                    {
                        var tDate = toDate.Replace(" 12:00:00 a. m.", String.Empty);
                        string[] final = tDate.Split("/");
                        finalDate = new DateTime( Int32.Parse(final[2]), Int32.Parse(final[1]), Int32.Parse(final[0]));
                    }

                    var cupos = _context.CuposV001ResolucionCupos.Where(p => (resolutionNumber!=null ? p.numeroResolucion == Convert.ToDecimal(resolutionNumber) : true) && (BussinesName !=null ? p.NombreEmpresa == BussinesName : true) && (BussinesNit!=null ? p.NitEmpresa == Convert.ToDecimal(BussinesNit) : true) && (fromDate!=null ? initialDate.Date <= p.fechaResolucion.Date : true) && (toDate!=null ? finalDate.Date >= p.fechaResolucion : true)).ToList();
                    foreach (var cupo in cupos)
                    {
                        quotas.Add(llenarObjetoCupos(cupo));
                    }
                }

                return ResponseManager.generaRespuestaGenerica("", quotas, token, false);
            }
            catch (Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }

       public Cupos llenarObjetoCupos(CuposV001ResolucionCupos cupo)
        {
            DateTime dateEmpty = new DateTime();
            
            Cupos quota = new Cupos();
            quota.nombreEmnpresa = cupo.NombreEmpresa;
            quota.nitEmpresa = cupo.NitEmpresa.ToString();
            quota.tipoEmpresa = cupo.TipoEntidadEmpresa;
            quota.numeroResolucion = cupo.numeroResolucion.ToString();
            quota.fechaRegistroResolucion = cupo.fechaRegistroResolucion;
            quota.fechaResolucion = cupo.fechaResolucion;
            quota.autoridadEmiteResolucion = cupo.autoridadEmiteResolucion ?? "";
            quota.codigoEspecie = cupo.codigoEspecie;
            quota.tipoEspecimen =cupo.NombreEspecieExportar;
            quota.anioProduccion = cupo.fechaProduccion.Year;
            quota.cuposAprovechamientoComercializacion = Convert.ToDecimal(cupo.cuposAprovechamientoComercializacion);
            quota.cuotaRepoblacion = cupo.cuotaRepoblacion;
            quota.cuposTotal = cupo.cuposTotal;
            quota.codigoParametricaPagoCuotaDerepoblacion = Convert.ToDecimal(cupo.PagoCuotaRepoblacion);
            quota.fechaRadicado = cupo.fechaRadicado;
            quota.soporteRepoblacion = cupo.fechaRadicado == null ? false : true;
            quota.cuposUtilizados = cupo.cuposTotal - cupo.cuposDisponibles;
            quota.cuposDisponibles = cupo.cuposDisponibles;
            
            return quota;
        }

    }
}
