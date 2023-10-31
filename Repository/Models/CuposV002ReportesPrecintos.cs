using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CuposV002ReportesPrecintos
    {
        public string? NUmeroRadicacion { get; set; } = "";
        public DateTime? FechaRadicacion { get; set; }
        public decimal? CodigoCiudad { get; set; } = 0;
        public string? DireccionEntrega { get; set; } = "";
        public decimal? LongMenor { get; set; } = 0;
        public decimal? LongMayor { get; set; } = 0;
        public decimal? Cantidad { get; set; } = 0;
        public decimal? CodigoEmpresa { get; set; } = 0;
        public decimal? ValorConsignacion { get; set; } = 0;
        public decimal? Analista { get; set; } = 0;
        public string? PrimerNombreAnalista { get; set; } = "";
        public string? PrimerApellidoAnalista { get; set; } = "";
        public decimal? Especie { get; set; } = 0;
        public decimal? NIT { get; set; } = 0;
        public string? NombreEmpresa { get; set; } = "";
        public decimal? Codigo_Solicitud { get; set; } = 0;

    }
}
