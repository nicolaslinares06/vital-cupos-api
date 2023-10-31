using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT024RlCuotaPecesPaisDocumento
    {
        public decimal Pk_T024Codigo { get; set; }
        public decimal? A024CodigoCuotaPecesPais { get; set; }
        public decimal? A024CodigoDocumento { get; set; }
        public DateTime A024FechaCreacion { get; set; }
        public DateTime? A024FechaModificacion { get; set; }
        public decimal A024UsuarioCreacion { get; set; }
        public decimal? A024UsuarioModificacion { get; set; }
        public decimal A024EstadoRegistro { get; set; }
    }
}
