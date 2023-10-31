using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT017ActaVisitaSalvoConducto
    {
        public decimal PK_T017Codigo { get; set; }
        public decimal A017CodigoActaVisita { get; set; }
        public decimal A017SalvoConductoNumero { get; set; }
        public decimal A017CodigoUsuarioCreacion { get; set; }
        public decimal A017CodigoUsuarioModificacion { get; set; }
        public DateTime A017FechaCreacion { get; set; }
        public DateTime A017FechaModificacion { get; set; }
    }
}
