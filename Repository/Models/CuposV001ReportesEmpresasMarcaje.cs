using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CuposV001ReportesEmpresasMarcaje
    {
        public string? NombreEmpresa { get; set; } = "";
        public decimal? NIT { get; set; } = 0;
        public decimal? TipoEmpresa { get; set; } = 0;   
        public decimal? Estado { get; set; } = 0;
        public decimal? EstadoEmisionCITES { get; set; } = 0;
        public decimal? NumeroResolucion { get; set; } = 0;
        public DateTime? FechaResolucion { get; set; }
        public string? Especies { get; set; } = "";
        public decimal? Machos { get; set; } = 0;
        public decimal? Hembras { get; set; } = 0;
        public decimal? PoblacionTotalParental { get; set; } = 0;
        public decimal? AnioProduccion { get; set; } = 0;
        public decimal? CuposComercializacion { get; set; } = 0;
        public string? CuotaRepoblacion { get; set; } 
        public decimal? CuposAsignadosTotal { get; set; } = 0;
        public decimal? SoportesRepoblacion { get; set; } = 0;
        public decimal? CupoUtilizado { get; set; } = 0;
        public decimal? CupoDisponible { get; set; } = 0;
        public DateTime? FechaRadicacion { get; set; }
    }
}
