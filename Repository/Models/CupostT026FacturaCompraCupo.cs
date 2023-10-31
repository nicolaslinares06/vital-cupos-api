using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT026FacturaCompraCupo
    {
        public decimal Pk_T026Codigo { get; set; }
        public decimal A026CodigoFacturaCompra { get; set; }
        public decimal A026CodigoCupo { get; set; }
        public decimal? A026NumeracionInicial { get; set; }
        public decimal? A026NumeracionFinal { get; set; }
        public decimal? A026NumeracionInicialRepoblacion { get; set; }
        public decimal? A026NumeracionFinalRepoblacion { get; set; }
        public decimal? A026NumeracionInicialPrecintos{ get; set; }
        public decimal? A026NumeracionFinalPrecintos { get; set; }
        public decimal? A026CuposDisponibles  { get; set; }
        public decimal A026CantidadEspecimenesComprados { get; set; }
    }
}
