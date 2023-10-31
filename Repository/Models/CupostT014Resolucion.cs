using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT014Resolucion
    {
        public decimal PkT014codigo { get; set; }
        public decimal A014codigoUsuarioCreacion { get; set; }
        public decimal? A014codigoUsuarioModificacion { get; set; }
        public decimal? A014codigoDocumentoSoporte { get; set; }
        public decimal A014estadoRegistro { get; set; } 
        public DateTime A014fechaCreacion { get; set; }
        public DateTime? A014fechaModificacion { get; set; }
        public DateTime A014fechaResolucion { get; set; }
        public DateTime A014fechaInicio { get; set; }
        public DateTime A014fechaFin { get; set; }
        public decimal? A014numeroResolucion { get; set; }
        public string? A014objetoResolucion { get; set; }
        public decimal? A014codigoEmpresa { get; set; }
    }
}
