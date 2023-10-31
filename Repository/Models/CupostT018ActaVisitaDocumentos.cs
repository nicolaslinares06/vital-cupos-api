using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CupostT018ActaVisitaDocumentos
    {
        public decimal Pk_T018Codigo { get; set; }
        public decimal A018CodigoActaVisita { get; set; }
        public string? A018RutaDocumento { get; set; }
        public decimal A018CodigoUsuarioCreacion { get; set; }
        public decimal A018CodigoUsuarioModificacion { get; set; }
        public DateTime A018FechaCreacion { get; set; }
        public DateTime A018FechaModificacion { get; set; }
        public string A018NombreArchivo { get; set; } = "";
    }
}
