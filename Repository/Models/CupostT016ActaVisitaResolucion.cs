using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT016ActaVisitaResolucion
    {
        public decimal PK_T016Codigo { get; set; }
        public decimal A016CodigoActaVisita { get; set; }
        public decimal A016ResolucionNumero { get; set; }
        public decimal A016CodigoUsuarioCreacion { get; set; }
        public decimal A016CodigoUsuarioModificacion { get; set; }
        public DateTime A016FechaCreacion { get; set; }
        public DateTime A016FechaModificacion { get; set; }
    }
}
