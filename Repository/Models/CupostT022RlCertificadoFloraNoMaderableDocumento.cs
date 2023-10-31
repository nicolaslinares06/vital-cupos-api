using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT022RlCertificadoFloraNoMaderableDocumento
    {
        public decimal Pk_T022Codigo { get; set; }
        public decimal? A022CodigoCertificadoFloraNoMaderable { get; set; }
        public decimal? A022CodigoDocuemento { get; set; }
        public DateTime A022FechaCreacion { get; set; }
        public DateTime? A022FechaModificacion { get; set; }
        public decimal A022UsuarioCreacion { get; set; }
        public decimal? A022UsuarioModificacion{ get; set; }
        public decimal A022EstadoRegistro { get; set; }
    }
}
