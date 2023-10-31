using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT009CuotaPecesPai
    {
        public CupostT009CuotaPecesPai()
        {
            CupostT010CantidadCuotaPecesPai = new HashSet<CupostT010CantidadCuotaPecesPai>();
        }

        public decimal PkT009codigo { get; set; }
        public decimal A009codigoUsuarioCreacion { get; set; }
        public decimal? A009codigoUsuarioModificacion { get; set; }
        public decimal A009codigoParametricaTipoMaritimo { get; set; }
        public decimal? A009codigoDocumentoSoporte { get; set; }
        public decimal A009estadoRegistro { get; set; }
        public DateTime A009fechaCreacion { get; set; }
        public DateTime? A009fechaModificacion { get; set; }
        public DateTime A009fechaResolucion { get; set; }
        public DateTime? A009fechaVigencia { get; set; }
        public decimal A009numeroResolucion { get; set; }
        public string? A009objetoResolucion { get; set; }
        public string A009tipo { get; set; } = "";
        public DateTime A009fechaInicialVigencia { get; set; }
        public DateTime? A009fechaFinalVigencia { get; set; }
        public virtual ICollection<CupostT010CantidadCuotaPecesPai> CupostT010CantidadCuotaPecesPai { get; set; }
    }
}
