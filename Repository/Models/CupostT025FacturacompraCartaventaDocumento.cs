using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT025FacturaCompraCartaVentaDocumento
    {
        public decimal Pk_T025Codigo { get; set; }
        public decimal? A025CodigoFacturaCompraCartaVenta { get; set; }
        public decimal? A025CodigoDocumento { get; set; }
        public DateTime A025FechaCreacion { get; set; }
        public DateTime? A025FechaModificacion { get; set; }
        public decimal A025UsuarioCreacion { get; set; }
        public decimal? A025UsuarioModificacion { get; set; }
        public decimal A025EstadoRegistro { get; set; }
    }
}
