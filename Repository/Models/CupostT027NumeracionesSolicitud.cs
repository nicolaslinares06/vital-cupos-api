using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT027NumeracionesSolicitud
    {
        public decimal Pk_T027Codigo { get; set; }
        public decimal A027CodigoSolicitud { get; set; }
        public decimal A027CodigoUsuarioCreacion { get; set; }
        public decimal? A027CodigoUsuarioModificacion { get; set; }
        public DateTime A027FechaCreacion { get; set; }
        public DateTime? A027FechaModificacion { get; set; }
        public decimal A027EstadoRegistro { get; set; }
        public decimal A027NumeroInternoInicial { get; set; }
        public decimal A027NumeroInternoFinal { get; set; }
        public decimal? A027OrigenSolicitud { get; set; }
        public decimal A027NumeroInicialPrecintos { get; set; }
        public decimal A027NumeroFinalPrecintos { get; set; }
        public decimal? A027NumeroInicialMarquillas { get; set; }
        public decimal? A027NumeroFinalMarquillas { get; set; }
        public decimal? A027CodigoEmpresaOrigenNumeraciones { get; set; }
    }
}
