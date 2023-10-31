using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT020RlSolicitudesDocumento
    {
        public decimal Pk_T020Codigo { get; set; }
        public decimal? A020CodigoSolicitud { get; set; }
        public decimal? A020CodigoDocumento { get; set; }
        public decimal A020CodigoUsuarioCreacion { get; set; }
        public decimal? A020CodigoUsuarioModificacion { get; set; }
        public DateTime A020FechaCreacion { get; set; }
        public DateTime? A020FechaModificacion { get; set; }
        public decimal A020EstadoRegistro { get; set; }
    }
}
