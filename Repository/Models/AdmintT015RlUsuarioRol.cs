using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT015RlUsuarioRol
    {
        public decimal PkT0015codigo { get; set; }
        public decimal A015codigoUsuario { get; set; }
        public decimal A015codigoUsuarioCreacion { get; set; }
        public decimal? A015codigoUsuarioModificacion { get; set; }
        public string A015codigoRol { get; set; } = "";
        public decimal A015estadoRegistro { get; set; }
        public DateTime A015fechaCreacion { get; set; }
        public DateTime? A015fechaModificacion { get; set; }
        public string A015estadoSolicitud { get; set; } = "";

        public virtual AdmintT012Usuario A015codigoUsuarioNavigation { get; set; } = null!;
    }
}
