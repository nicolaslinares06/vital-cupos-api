using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT013Auditorium
    {
        public AdmintT013Auditorium()
        {
            CupostT005EspecieaexportarA005codigoUsuarioCreacionNavigations = new HashSet<CupostT005Especieaexportar>();
            CupostT005EspecieaexportarA005codigoUsuarioModificacionNavigations = new HashSet<CupostT005Especieaexportar>();
        }

        public decimal PkT013codigo { get; set; }
        public decimal A013codigoUsuarioCreacion { get; set; }
        public decimal? A013codigoUsuarioModificacion { get; set; }
        public DateTime A013fechaCreacion { get; set; }
        public DateTime? A013fechaModificacion { get; set; }
        public decimal A013estadoRegistro { get; set; }
        public decimal A013codigoUsuarioAuditado { get; set; }
        public string? A013codigoRol { get; set; }
        public string A013codigoModulo { get; set; } = null!;
        public DateTime A013fechaHora { get; set; }
        public string A013accion { get; set; } = null!;
        public string A013ip { get; set; } = null!;
        public string? A013estadoAnterior { get; set; }
        public string? A013estadoActual { get; set; }
        public string? A013camposModificados { get; set; }
        public string? A013registroModificado { get; set; }

        public virtual ICollection<CupostT005Especieaexportar> CupostT005EspecieaexportarA005codigoUsuarioCreacionNavigations { get; set; }
        public virtual ICollection<CupostT005Especieaexportar> CupostT005EspecieaexportarA005codigoUsuarioModificacionNavigations { get; set; }
    }
}
