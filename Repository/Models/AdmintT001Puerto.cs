using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT001Puerto
    {
        public AdmintT001Puerto()
        {
            CitestT001CertificadoA001codigoPuertoentradaNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoPuertosalidaNavigations = new HashSet<CitestT001Certificado>();
            CitestT003Personas = new HashSet<CitestT003Persona>();
        }

        public decimal PkTcodigo { get; set; }
        public decimal A001codigoUsuarioCreacion { get; set; }
        public decimal? A001codigoUsuarioModificacion { get; set; }
        public decimal A001codigoCiudad { get; set; }
        public string A001direccion { get; set; } = null!;
        public string A001estadoEstrategia { get; set; } = null!;
        public DateTime A001fechaCreacion { get; set; }
        public DateTime? A001fechaModificacion { get; set; }
        public string A001modoTransporte { get; set; } = null!;
        public string A001nombre { get; set; } = null!;

        public virtual AdmintT004Ciudad A001codigoCiudadNavigation { get; set; } = null!;
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoPuertoentradaNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoPuertosalidaNavigations { get; set; }
        public virtual ICollection<CitestT003Persona> CitestT003Personas { get; set; }
    }
}
