using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT011Rol
    {
        public decimal PkT011codigo { get; set; }
        public decimal A011codigoUsuarioCreacion { get; set; }
        public decimal? A011codigoUsuarioModificacion { get; set; }
        public decimal A011estadoRegistro { get; set; } 
        public DateTime A011fechaCreacion { get; set; }
        public DateTime? A011fechaModificacion { get; set; }
        public string A011nombre { get; set; } = null!;
        public string A011cargo { get; set; } = null!;
        public string A011descripcion { get; set; } = null!;
        public string A011modulo { get; set; } = "";
        public string A011tipoUsuario { get; set; } = "";

    }
}
