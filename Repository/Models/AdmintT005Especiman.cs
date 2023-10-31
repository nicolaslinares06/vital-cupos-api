using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT005Especiman
    {
        public AdmintT005Especiman()
        {
        }

        public decimal PkT005codigo { get; set; }
        public decimal A005codigoUsuarioCreacion { get; set; }
        public decimal? A005codigoUsuarioModificacion { get; set; }
        public decimal A005codigoTipoEspecimen { get; set; }
        public DateTime A005fechaCreacion { get; set; }
        public DateTime? A005fechaModificacion { get; set; }
        public decimal A005estadoRegistro { get; set; }
        public string? A005apendice { get; set; }
        public string A005nombreCientifico { get; set; } = null!;
        public string A005nombre { get; set; } = null!;
        public string A005nombreComun { get; set; } = null!;
        public string A005familia { get; set; } = null!;
        public string? A005reino { get; set; }
        public string? A005clase { get; set; }

    }
}
