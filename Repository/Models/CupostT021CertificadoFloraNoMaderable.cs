using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT021CertificadoFloraNoMaderable
    {
        public decimal Pk_T021Codigo { get; set; }
        public DateTime A021FechaCertificacion { get; set; }
        public DateTime A021VigenciaCertificacion { get; set; }
        public DateTime A021FechaRegistroCertificado { get; set; }
        public decimal A021TipoCertificado { get; set; }
        public string A021AutoridadEmiteCertificado { get; set; } = "";
        public string? A021NumeroCertificado { get; set; }
        public string A021TipoPermiso { get; set; } = "";
        public decimal A021CodigoEmpresa { get; set; }
        public string A021TipoEspecimenProductoImpExp { get; set; } = "";
        public string? A021Observaciones { get; set; }
        public DateTime A021FechaCreacion { get; set; }
        public DateTime? A021FechaModificacion { get; set; }
        public decimal A021UsuarioCreacion { get; set; }
        public decimal? A021UsuarioModificacion{ get; set; }
        public decimal A021EstadoRegistro { get; set; }
    }
}
