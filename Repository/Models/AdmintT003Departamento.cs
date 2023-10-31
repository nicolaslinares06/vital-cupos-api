using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT003Departamento
    {
        public AdmintT003Departamento()
        {
            AdmintT004Ciudads = new HashSet<AdmintT004Ciudad>();
        }

        public decimal PkT003codigo { get; set; }
        public decimal A003codigoUsuarioCreacion { get; set; }
        public decimal? A003codigoUsuarioModificacion { get; set; }
        public decimal A003codigoPais { get; set; }
        public decimal A003estadoRegistro { get; set; } 
        public DateTime A003fechaCreacion { get; set; }
        public DateTime? A003fechaModificacion { get; set; }
        public string A003nombre { get; set; } = null!;

        public virtual AdmintT002Pai A003codigoPaisNavigation { get; set; } = null!;
        public virtual ICollection<AdmintT004Ciudad> AdmintT004Ciudads { get; set; }
    }
}
