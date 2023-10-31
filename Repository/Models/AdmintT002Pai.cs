using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT002Pai
    {
        public AdmintT002Pai()
        {
            AdmintT003Departamentos = new HashSet<AdmintT003Departamento>();
            CitestT001Certificados = new HashSet<CitestT001Certificado>();
        }

        public decimal PkT002codigo { get; set; }
        public decimal A002codigoUsuarioCreacion { get; set; }
        public decimal? A002codigoUsuarioModificacion { get; set; }
        public decimal A002estadoRegistro { get; set; } 
        public DateTime A002fechaCreacion { get; set; }
        public DateTime? A002fechaModificacion { get; set; }
        public string A002nombre { get; set; } = null!;

        public virtual ICollection<AdmintT003Departamento> AdmintT003Departamentos { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001Certificados { get; set; }
    }
}
