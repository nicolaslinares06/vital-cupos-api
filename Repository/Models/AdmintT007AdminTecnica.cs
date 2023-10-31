using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT007AdminTecnica
    {
        public decimal pkT007Codigo { get; set; }
        public decimal a007codigoUsuarioCreacion { get; set; }
        public decimal? a007codigoUsuarioModificacion { get; set; }
        public DateTime a007fechaCreacion { get; set; }
        public DateTime? a007fechaModificacion { get; set; }
        public decimal a007estadoRegistro { get; set; }
        public string a007valor { get; set; } = null!;
        public string a007nombre { get; set; } = null!;
        public string a007descripcion { get; set; } = null!;
        public string a007modulo { get; set; } = "";
    }
}
