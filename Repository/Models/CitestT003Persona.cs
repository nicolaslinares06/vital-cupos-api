using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT003Persona
    {
        public CitestT003Persona()
        {
            CitestT001CertificadoA001codigoPersonaApoderadoNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoPersonaDestinatarioNavigations = new HashSet<CitestT001Certificado>();
            CitestT001CertificadoA001codigoPersonaTitularNavigations = new HashSet<CitestT001Certificado>();
            CitestT007SalvoconductomovilizacionA007codigoPersonaSalvoconductoNavigations = new HashSet<CitestT007Salvoconductomovilizacion>();
            CitestT007SalvoconductomovilizacionA007codigoPersonaTransportadorNavigations = new HashSet<CitestT007Salvoconductomovilizacion>();
        }

        public decimal PkT003codigo { get; set; }
        public decimal A003codigoCiudad { get; set; }
        public decimal A003codigoUsuarioCreacion { get; set; }
        public decimal? A003codigoUsuarioModificacion { get; set; }
        public decimal A003codigoPuertoEntrada { get; set; }
        public decimal A003codigoPuertoSalida { get; set; }
        public decimal A003codigoParametricaTipoIdentificacion { get; set; }
        public bool A003aceptaTerminosycondiciones { get; set; }
        public bool A003aceptaTratamientoDatosPersonales { get; set; }
        public decimal A003estadoRegistro { get; set; }
        public DateTime A003fechaCreacion { get; set; }
        public DateTime? A003fechaModificacion { get; set; }
        public string A003nombres { get; set; } = null!;
        public string? A003apellidos { get; set; }
        public string A003identificacion { get; set; } = null!;
        public string A003direccion { get; set; } = null!;
        public string A003telefono { get; set; } = null!;
        public string A003correoElectronico { get; set; } = null!;
        public string? A003fax { get; set; }
        public string? A003segundoNombre { get; set; } = null!;
        public string? A003segundoApellido { get; set; } = null!;

        public virtual AdmintT004Ciudad A003codigoCiudadNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A003codigoParametricaTipoIdentificacionNavigation { get; set; } = null!;
        public virtual AdmintT001Puerto A003codigoPuertoEntradaNavigation { get; set; } = null!;
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoPersonaApoderadoNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoPersonaDestinatarioNavigations { get; set; }
        public virtual ICollection<CitestT001Certificado> CitestT001CertificadoA001codigoPersonaTitularNavigations { get; set; }
        public virtual ICollection<CitestT007Salvoconductomovilizacion> CitestT007SalvoconductomovilizacionA007codigoPersonaSalvoconductoNavigations { get; set; }
        public virtual ICollection<CitestT007Salvoconductomovilizacion> CitestT007SalvoconductomovilizacionA007codigoPersonaTransportadorNavigations { get; set; }
    }
}
