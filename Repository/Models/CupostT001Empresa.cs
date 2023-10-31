using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT001Empresa
    {
        public CupostT001Empresa()
        {
            CitestT001CertificadoA001codigoEmpresaNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoEntidadExportadorNavigations = new HashSet<CitestT001Certificado>();
            CupostT002Cupos = new HashSet<CupostT002Cupo>();
            CupostT004FacturacompraCartaventa = new HashSet<CupostT004FacturacompraCartaventum>();
        }

        public decimal PkT001codigo { get; set; }
        public decimal A001codigoUsuarioCreacion { get; set; }
        public decimal? A001codigoUsuarioModificacion { get; set; }
        public decimal A001codigoParametricaTipoEntidad { get; set; }
        public decimal A001codigoPersonaRepresentantelegal { get; set; }
        public decimal A001codigoCiudad { get; set; }
        public decimal A001estadoRegistro { get; set; }
        public DateTime A001fechaCreacion { get; set; }
        public DateTime? A001fechaModificacion { get; set; }
        public decimal A001nit { get; set; }
        public decimal A001telefono { get; set; }
        public string A001nombre { get; set; } = null!;
        public string A001correo { get; set; } = null!;
        public string A001direccion { get; set; } = null!;
        public string A001matriculaMercantil { get; set; } = "";
        public decimal? A001numeroInternoInicial { get; set; }
        public decimal? A001numeroInternoFinal { get; set; }

        public virtual AdmintT004Ciudad A001codigoCiudadNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A001codigoParametricaTipoEntidadNavigation { get; set; } = null!;
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoEmpresaNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoEntidadExportadorNavigations { get; set; }
        public virtual ICollection<CupostT002Cupo> CupostT002Cupos { get; set; }
        public virtual ICollection<CupostT004FacturacompraCartaventum> CupostT004FacturacompraCartaventa { get; set; }
    }
}
