using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT028SalvoconductosSolicitud
    {
        public decimal Pk_T028Codigo { get; set; }
        public decimal A028CodigoActaVisitaSalvoconducto { get; set; }
        public decimal A028CodigoSolicitud { get; set; }
        public decimal A028CodigoUsuarioCreacion { get; set; }
        public decimal? A028CodigoUsuarioModificacion { get; set; }
        public DateTime A028FechaCreacion { get; set; }
        public DateTime? A028FechaModificacion { get; set; }
        public decimal A028EstadoRegistro { get; set; }
    }
}
