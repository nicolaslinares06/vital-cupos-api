using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{
    public class ReportesEmpresasMarcajeModels
    {
        /// <summary>
        /// 
        /// </summary>
        public class BusinessFilters
        {
            public decimal BusinessType { get; set; } = 0;
            public string CompanyName { get; set; } = "";
            public decimal NIT { get; set; } = 0;
            public decimal Status { get; set; } = 0;
            public decimal CITESIssuanceStatus { get; set; } = 0;
            public decimal ResolutionNumber { get; set; } = 0;
            public DateTime ResolutionIssuanceStartDate { get; set; }
            public DateTime ResolutionIssuanceEndDate { get; set; }
            public int SpecificSearch { get; set; }
            public int CombinationType { get; set; }
        }

        public class DatosEmpresasModel
        {
            public string? TipoEmpresa { get; set; } = "";
            public string? NombreEmpresa { get; set; } = "";
            public decimal? NIT { get; set; } = 0;
            public string? Estado { get; set; } = "";
            public string? EstadoEmisionCITES { get; set; } = "";
            public string? NumeroResolucion { get; set; } = "";
            public string? FechaEmisionResolucion { get; set; }
            public string? Especies { get; set; } = "";
            public decimal? Machos { get; set; } = 0;
            public decimal? Hembras { get; set; } = 0;
            public decimal? PoblacionTotalParental { get; set; } = 0;
            public decimal? AnioProduccion { get; set; } = 0;
            public decimal? CuposComercializacion { get; set; } = 0;
            public string? CuotaRepoblacion { get; set; } = "";
            public decimal? CuposAsignadosTotal { get; set; } = 0;
            public bool? SoportesRepoblacion { get; set; }
            public decimal? CupoUtilizado { get; set; } = 0;
            public decimal? CupoDisponible { get; set; } = 0;

        }
    }
}
