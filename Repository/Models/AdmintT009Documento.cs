using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT009Documento
    {
        public AdmintT009Documento()
        {
            CitestT001Certificados = new HashSet<CitestT001Certificado>();
            CitestT005Recaudos = new HashSet<CitestT005Recaudo>();
            CitestT006InformacionespecimanA006codigoDocumentoPermisoPaisOrigenNavigations = new HashSet<CitestT006Informacionespeciman>();
            CitestT006InformacionespecimanA006codigoDocumentoProcedenciaNavigations = new HashSet<CitestT006Informacionespeciman>();
            CitestT010RlCertificadoDocumentos = new HashSet<CitestT010RlCertificadoDocumento>();
        }

        public decimal? PkT009codigo { get; set; }
        public decimal A009codigoUsuarioCreacion { get; set; }
        public decimal? A009codigoUsuarioModificacion { get; set; }
        public decimal A009codigoParametricaTipoDocumento { get; set; }
        public decimal A009codigoPlantilla { get; set; }
        public decimal A009estadoRegistro { get; set; } 
        public DateTime A009fechaCreacion { get; set; }
        public DateTime? A009fechaModificacion { get; set; }
        public string A009firmaDigital { get; set; } = null!;
        public string A009documento { get; set; } = null!;
        public string A009descripcion { get; set; } = null!;
        public string A009url { get; set; } = null!;

        public virtual AdmintT008Parametrica? A009codigoParametricaTipoDocumentoNavigation { get; set; } = null!;
        public virtual AdmintT006PlantillaDocumento? A009codigoPlantillaNavigation { get; set; } = null!;
        public virtual ICollection<CitestT001Certificado>? CitestT001Certificados { get; set; }
        public virtual ICollection<CitestT005Recaudo>? CitestT005Recaudos { get; set; }
        public virtual ICollection<CitestT006Informacionespeciman>? CitestT006InformacionespecimanA006codigoDocumentoPermisoPaisOrigenNavigations { get; set; }
        public virtual ICollection<CitestT006Informacionespeciman>? CitestT006InformacionespecimanA006codigoDocumentoProcedenciaNavigations { get; set; }
        public virtual ICollection<CitestT010RlCertificadoDocumento>? CitestT010RlCertificadoDocumentos { get; set; }
    }
}
