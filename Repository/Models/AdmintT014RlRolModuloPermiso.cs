using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT014RlRolModuloPermiso
    {
        public decimal PkT014codigo { get; set; }
        public decimal A014codigoRol { get; set; }
        public decimal A014codigoModulo { get; set; }
        public decimal A014codigoUsuarioCreacion { get; set; }
        public decimal? A014codigoUsuarioModificacion { get; set; }
        public decimal A014estadoRegistro { get; set; }
        public DateTime A014fechaCreacion { get; set; }
        public DateTime? A014fechaModificacion { get; set; }
        public bool A014eliminar { get; set; }
        public bool A014crear { get; set; }
        public bool A014consultar { get; set; }
        public bool A014actualizar { get; set; }
        public bool A014verDetalle { get; set; }
    }
}
