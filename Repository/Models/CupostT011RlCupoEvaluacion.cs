using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT011RlCupoEvaluacion
    {
        public decimal PkT011codigo { get; set; }
        public decimal A011codigoUsuarioCreacion { get; set; }
        public decimal? A011codigoUsuarioModificacion { get; set; }
        public decimal A011codigoCupo { get; set; }
        public decimal? A011codigoEvaluacion { get; set; }
        public decimal A011estadoRegistro { get; set; } 
        public DateTime A011fechaCreacion { get; set; }
        public DateTime? A011fechaModificacion { get; set; }
    }
}
