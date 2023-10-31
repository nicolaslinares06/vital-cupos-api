using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintV002Role
    {
        public decimal? PkT010codigo { get; set; }
        public decimal? A014codigoRol { get; set; }
        public string? A010descripcion { get; set; }
        public bool A014eliminar { get; set; } = false;
        public bool A014crear { get; set; } = false;
        public bool A014consultar { get; set; } = false;
        public bool A014actualizar { get; set; } = false;
        public bool A014verDetalle { get; set; } = false;
        public string A011cargo { get; set; } = "";
        public decimal? A011estadoRegistro { get; set; }
    }
}
