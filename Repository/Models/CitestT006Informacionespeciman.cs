using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT006Informacionespeciman
    {
        public decimal? PkT006codigo { get; set; }
        public decimal A006codigoUsuarioCreacion { get; set; }
        public decimal? A006codigoUsuarioModificacion { get; set; }
        public decimal A006codigoCertificado { get; set; }
        public string A006codigoEspecimen { get; set; } = "";
        public decimal? A006codigoDocumentoProcedencia { get; set; }
        public decimal? A006codigoDocumentoPermisoPaisOrigen { get; set; }
        public decimal? A006codigoParametricaUnidadMedida { get; set; }
        public DateTime A006fechaCreacion { get; set; }
        public DateTime? A006fechaModificacion { get; set; }
        public decimal A006estadoRegistro { get; set; }
        public DateTime? A006fechaProcedencia { get; set; }
        public string? A006sexo { get; set; } = null!;
        public string? A007talla { get; set; } = null!;
        public string? A006observaciones { get; set; } = null!;
        public decimal? A006cantidad { get; set; }
        public string? A006cantidadRealExportada { get; set; } = null!;

        public virtual CitestT001Certificado? A006codigoCertificadoNavigation { get; set; } = null!;
        public virtual AdmintT009Documento? A006codigoDocumentoPermisoPaisOrigenNavigation { get; set; } = null!;
        public virtual AdmintT009Documento? A006codigoDocumentoProcedenciaNavigation { get; set; } = null!;
    }
}
