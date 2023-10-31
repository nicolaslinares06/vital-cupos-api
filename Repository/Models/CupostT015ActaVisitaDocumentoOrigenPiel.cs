using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT015ActaVisitaDocumentoOrigenPiel
    {
        public decimal PK_T015Codigo { get; set; }
        public decimal A015CodigoActaVisita { get; set; }
        public string A015DocumentoOrigenPielNumero { get; set; } = "";
        public decimal A015CodigoUsuarioCreacion { get; set; }
        public decimal A015CodigoUsuarioModificacion { get; set; }
        public DateTime A015FechaCreacion { get; set; }
        public DateTime A015FechaModificacion { get; set; }

    }
}
