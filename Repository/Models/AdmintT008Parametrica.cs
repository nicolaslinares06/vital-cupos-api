using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT008Parametrica
    {
        public AdmintT008Parametrica()
        {
            AdmintT009Documentos = new HashSet<AdmintT009Documento>();
            AdmintT012Usuarios = new HashSet<AdmintT012Usuario>();
            CitestT001CertificadoA001codigoParametricaTipoEmbarqueNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoParametricaTipoPermisoNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoParametricaTipoSolictudNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoParametricaTipoCertificadoNavigations = new HashSet<CitestT001Certificado>();
            CitestT003Personas = new HashSet<CitestT003Persona>();
            CitestT005Recaudos = new HashSet<CitestT005Recaudo>();
            CitestT007SalvoconductomovilizacionA007codigoParametricaTipoRutaNavigations = new HashSet<CitestT007Salvoconductomovilizacion>();
            CitestT007SalvoconductomovilizacionA007codigoParametricaTipoVehiculoNavigations = new HashSet<CitestT007Salvoconductomovilizacion>();
            CupostT001Empresas = new HashSet<CupostT001Empresa>();
            CupostT003Novedads = new HashSet<CupostT003Novedad>();
            CupostT004FacturacompraCartaventa = new HashSet<CupostT004FacturacompraCartaventum>();
        }

        public decimal PkT008codigo { get; set; }
        public decimal A008codigoUsuarioCreacion { get; set; }
        public decimal? A008codigoUsuarioModificacion { get; set; }
        public decimal A008estadoRegistro { get; set; }
        public DateTime A008fechaCreacion { get; set; }
        public DateTime? A008fechaModificacion { get; set; }
        public string A008modulo { get; set; } = null!;
        public string A008parametrica { get; set; } = null!;
        public string A008valor { get; set; } = null!;
        public string? A008descripcion { get; set; }

        public virtual ICollection<AdmintT009Documento> AdmintT009Documentos { get; set; }
        public virtual ICollection<AdmintT012Usuario> AdmintT012Usuarios { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoParametricaTipoEmbarqueNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoParametricaTipoPermisoNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoParametricaTipoSolictudNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoParametricaTipoCertificadoNavigations { get; set; }
        public virtual ICollection<CitestT003Persona> CitestT003Personas { get; set; }
        public virtual ICollection<CitestT005Recaudo> CitestT005Recaudos { get; set; }
        public virtual ICollection<CitestT007Salvoconductomovilizacion> CitestT007SalvoconductomovilizacionA007codigoParametricaTipoRutaNavigations { get; set; }
        public virtual ICollection<CitestT007Salvoconductomovilizacion> CitestT007SalvoconductomovilizacionA007codigoParametricaTipoVehiculoNavigations { get; set; }
        public virtual ICollection<CupostT001Empresa> CupostT001Empresas { get; set; }
        public virtual ICollection<CupostT003Novedad> CupostT003Novedads { get; set; }
        public virtual ICollection<CupostT004FacturacompraCartaventum> CupostT004FacturacompraCartaventa { get; set; }
    }
}
