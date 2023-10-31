using API.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.PaginatioModels;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace Repository.Helpers
{
    public interface IGenericsMethodsHelper
    {
        ParamsPaginations CalcularPaginas(ParamsPaginations parametrosPaginacion);
        Task<List<CuttingSaveModel>> ConsultarTiposFraccionesSolicitud(decimal codigoSolicitud);
        Task<List<AdmintT004Ciudad>> ObtenerCiudadesPorIdDepartamento(decimal departamentoId);
        Task<List<AdmintT003Departamento>> ObtenerDepartamentos();
        Task<List<AdmintT008Parametrica>> ObtenerListaparametricaPorNombre(string nombreParametrica);
        string ObtenerNombreParametricaPorId(decimal idParametrica);
    }
    [ExcludeFromCodeCoverage]
    public class GenericsMethodsHelper: IGenericsMethodsHelper
    {
        private readonly DBContext context;


        public GenericsMethodsHelper(DBContext context)
        {
            this.context = context;           
        }


        public async Task<List<AdmintT003Departamento>> ObtenerDepartamentos()
        {
            var departamentos = await context.AdmintT003Departamentos.Where(p => p.A003estadoRegistro == StringHelper.estadoActivo).ToListAsync();


            if (departamentos == null)
                departamentos = new List<AdmintT003Departamento>();

            return departamentos;
        }

        public async Task<List<AdmintT004Ciudad>> ObtenerCiudadesPorIdDepartamento(decimal departamentoId)
        {
            var ciudades = await context.AdmintT004Ciudads.Where(p => p.A004estadoRegistro == StringHelper.estadoActivo && p.A004codigoDepartamento == departamentoId)
              .OrderBy(c => c.A004nombre)
              .ToListAsync();


            if (ciudades == null)
                ciudades = new List<AdmintT004Ciudad>();

            return ciudades;
        }

        public async Task<List<AdmintT008Parametrica>> ObtenerListaparametricaPorNombre(string nombreParametrica)
        {
            var listaParametricas = await context.AdmintT008Parametricas.Where(p => p.A008parametrica == nombreParametrica && p.A008estadoRegistro == StringHelper.estadoActivo).ToListAsync();


            if (listaParametricas == null)
                listaParametricas = new List<AdmintT008Parametrica>();

            return listaParametricas;
        }

        public async Task<List<CuttingSaveModel>> ConsultarTiposFraccionesSolicitud(decimal codigoSolicitud)
        {
           

            var fracciones = new List<CuttingSaveModel>();

            fracciones = await context.CupostT029CortesPielSolicitud
                                          .Where(c => c.A029CodigoSolicitud == codigoSolicitud && c.A029EstadoRegistro == StringHelper.estadoActivo)
                                          .Select(c => new CuttingSaveModel
                                          {
                                              fractionType = context.CupostT008CortePiels
                                                                     .Where(f => f.A008codigo == c.A029CodigoCortePiel)
                                                                     .Select(f => f.A008tipoParte).FirstOrDefault() ?? "",
                                              totalAreaSelected = Convert.ToInt32(c.A029AreaTotal),
                                              amountSelected = Convert.ToInt32(c.A029Cantidad)
                                          }).ToListAsync() ?? new List<CuttingSaveModel>();

           return fracciones;
        }

        public  string ObtenerNombreParametricaPorId(decimal idParametrica)
        {
            var nombreParametrica =  context.AdmintT008Parametricas
                                                 .Where(p => p.PkT008codigo == idParametrica && p.A008estadoRegistro == StringHelper.estadoActivo)
                                                 .Select(p => p.A008valor)
                                                 .FirstOrDefault() ?? "";


            

            return nombreParametrica;
        }

        public ParamsPaginations CalcularPaginas(ParamsPaginations parametrosPaginacion)
        {
            var parametrosPrincipales = new ParamsPaginations();

            parametrosPaginacion.QuantityPages = parametrosPaginacion.TotalQuantity / parametrosPaginacion.QuantityRecordsForpage;
            var residuo = parametrosPaginacion.TotalQuantity % parametrosPaginacion.QuantityRecordsForpage;
            if (residuo > 0) parametrosPaginacion.QuantityPages++;

            parametrosPrincipales.QuantityPages = parametrosPaginacion.QuantityPages;
            parametrosPrincipales.QuantityRecords = parametrosPaginacion.QuantityRecords;


            return parametrosPrincipales;

        }


    }
}
