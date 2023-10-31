using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT010Modulo
    {
        public decimal PkT010codigo { get; set; }
        public decimal A010codigoUsuarioCreacion { get; set; }
        public decimal? A010codigoUsuarioModificacion { get; set; }
        public decimal A010estadoRegistro { get; set; } 
        public DateTime A010fechaCreacion { get; set; }
        public DateTime? A010fechaModificacion { get; set; }
        public string A010modulo { get; set; } = null!;
        public string A010descripcion { get; set; } = null!;
        public string A010informacionAyuda { get; set; } = null!;
        public string? A010aplicativo { get; set; }
    }
}
