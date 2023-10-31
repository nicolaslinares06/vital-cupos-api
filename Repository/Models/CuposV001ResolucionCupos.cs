using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CuposV001ResolucionCupos
    {
        public decimal codigoCupo { get; set; }
        public string? autoridadEmiteResolucion { get; set; }
        public string? codigoZoocriadero { get; set; }
        public decimal numeroResolucion { get; set; }
        public DateTime fechaResolucion { get; set;}
        public DateTime? fechaRegistroResolucion { get; set; }
        public DateTime? fechaRadicado { get; set; }
        public decimal cuposOtorgados { get; set; }
        public decimal cuposPorAnio { get; set; }
        public DateTime fechaProduccion { get; set; }
        public string? cuposAprovechamientoComercializacion { get; set; }
        public decimal? cuposTotal { get; set; }
        public string? cuotaRepoblacion { get; set; }
        public decimal cuposDisponibles { get; set; }
        public string? observaciones { get; set; }
        public decimal? codigoEmpresa { get; set; }
        public string? codigoEspecie { get; set; }
        public decimal? numeroInternoFinalCuotaRepoblacion { get; set; }
        public decimal? numeroInternoFinal { get; set; }
        public string? NombreEspecieExportar { get; set; }
        public decimal? NumeroInternoInicial { get; set; }
        public decimal? numeroInternoInicialCuotaRepoblacion { get; set; }
        public string? PagoCuotaRepoblacion { get; set; }
        public string? NombreEmpresa { get; set; }
        public decimal? NitEmpresa { get; set; }
        public decimal? TipoEntidadEmpresa { get; set; }
    }
}
