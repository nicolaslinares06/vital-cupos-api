using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT029CortesPielSolicitud
    {
        public int Pk_T029Codigo { get; set; }
        public decimal A029CodigoCortePiel { get; set; }
        public decimal A029CodigoSolicitud { get; set; }
        public decimal A029CodigoUsuarioCreacion { get; set; }
        public decimal? A029Cantidad { get; set; }
        public decimal A029AreaTotal { get; set; }
        public decimal? A029CodigoUsuarioModificacion { get; set; }
        public DateTime A029FechaCreacion { get; set; }
        public DateTime? A029FechaModificacion { get; set; }
        public decimal A029EstadoRegistro { get; set; }
    }
}
