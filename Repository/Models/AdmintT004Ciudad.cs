using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT004Ciudad
    {
        public AdmintT004Ciudad()
        {
            AdmintT001Puertos = new HashSet<AdmintT001Puerto>();
            AdmintT012Usuarios = new HashSet<AdmintT012Usuario>();
            CitestT001CertificadoA001codigoCiudadDestinoNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoCiudadEmbarqueNavigations = new HashSet<CitestT001Certificado>();
            CitestT003Personas = new HashSet<CitestT003Persona>();
            CitestT007Salvoconductomovilizacions = new HashSet<CitestT007Salvoconductomovilizacion>();
            CupostT001Empresas = new HashSet<CupostT001Empresa>();
        }

        public decimal PkT004codigo { get; set; }
        public decimal A004codigoUsuarioCreacion { get; set; }
        public decimal? A004codigoUsuarioModificacion { get; set; }
        public decimal A004codigoDepartamento { get; set; }
        public decimal A004estadoRegistro { get; set; }
        public DateTime A004fechaCreacion { get; set; }
        public DateTime? A004fechaModificacion { get; set; }
        public string A004nombre { get; set; } = null!;

        public virtual AdmintT003Departamento A004codigoDepartamentoNavigation { get; set; } = null!;
        public virtual ICollection<AdmintT001Puerto> AdmintT001Puertos { get; set; }
        public virtual ICollection<AdmintT012Usuario> AdmintT012Usuarios { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoCiudadDestinoNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoCiudadEmbarqueNavigations { get; set; }
        public virtual ICollection<CitestT003Persona> CitestT003Personas { get; set; }
        public virtual ICollection<CitestT007Salvoconductomovilizacion> CitestT007Salvoconductomovilizacions { get; set; }
        public virtual ICollection<CupostT001Empresa> CupostT001Empresas { get; set; }
    }
}
