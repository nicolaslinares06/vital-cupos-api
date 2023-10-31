using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT010CantidadCuotaPecesPai
    {
        public decimal PkT010codigo { get; set; }
        public decimal A010codigoUsuarioCreacion { get; set; }
        public decimal? A010codigoUsuarioModificacion { get; set; }
        public decimal A010codigoCuotaPecesPais { get; set; }
        public decimal A0010codigoParametricaRegion { get; set; }
        public decimal A010codigoEspecimen { get; set; }
        public decimal A010estadoRegistro { get; set; } 
        public DateTime A010fechaCreacion { get; set; }
        public DateTime? A010fechaModificacion { get; set; }
        public decimal A010cuota { get; set; }
        public decimal A010total { get; set; }
        public virtual CupostT009CuotaPecesPai A010codigoCuotaPecesPaisNavigation { get; set; } = null!; 
    }
}
