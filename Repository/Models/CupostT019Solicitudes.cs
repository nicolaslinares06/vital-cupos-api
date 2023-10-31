using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT019Solicitudes
    {
        public decimal Pk_T019Codigo { get; set; }
        public decimal A019CodigoCiudad { get; set; }
        public string A019DireccionEntrega { get; set; } = "";
        public DateTime A019FechaSolicitud { get; set; }
        public DateTime A019FechaConsignacion { get; set; }
        public decimal? A019CodigoEspecieExportar { get; set; }
        public decimal A019Cantidad { get; set; }
        public decimal A019LongitudMenor { get; set; }
        public decimal A019LongitudMayor { get; set; }
        public decimal A019CodigoUsuarioCreacion { get; set; }
        public decimal? A019CodigoUsuarioModificacion { get; set; }
        public DateTime A019FechaCreacion { get; set; }
        public DateTime? A019FechaModificacion { get; set; }
        public decimal A019EstadoRegistro { get; set; }
        public decimal A019EstadoSolicitud { get; set; }
        public string? A019NumeroRadicacion { get; set; }
        public DateTime? A019FechaRadicacion { get; set; }
        public decimal? A019TipoSolicitud { get; set; }
        public DateTime? A019FechaCambioEstado { get; set; }
        public string? A019ObservacionesDesistimiento { get; set; }
        public string? A019Observaciones { get; set; }
        public string? A019Respuesta { get; set; }
        public decimal? A019CodigoEmpresa { get; set; }
        public decimal? A019AnalistaAsignacion {get; set; }
        public decimal? A019ValorConsignacion {get; set; }
        public string? A019NumeroRadicacionSalida { get; set; }
        public DateTime? A019FechaRadicacionSalida { get; set; }
    }
}
