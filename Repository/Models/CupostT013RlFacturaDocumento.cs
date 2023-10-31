using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT013RlFacturaDocumento
    {
        public decimal PkT013codigo { get; set; }
        public decimal A013codigoUsuarioCreacion { get; set; }
        public decimal? A013codigoUsuarioModificacion { get; set; }
        public decimal A013codigoFacturacompraCartaventa { get; set; }
        public decimal A013codigoDocumento { get; set; }
        public decimal A013estadoRegistro { get; set; }
        public DateTime A013fechaCreacion { get; set; }
        public DateTime? A013fechaModificacion { get; set; }
    }
}
