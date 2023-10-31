using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT023RlCupoDocumento
    {
        public decimal Pk_T023Codigo { get; set; }
        public decimal? A023CodigoCupo { get; set; }
        public decimal? A023CodigoDocuemento { get; set; }
        public DateTime A023FechaCreacion { get; set; }
        public DateTime? A023FechaModificacion { get; set; }
        public decimal A023UsuarioCreacion { get; set; }
        public decimal? A023UsuarioModificacion{ get; set; }
        public decimal A023EstadoRegistro { get; set; }
    }
}
