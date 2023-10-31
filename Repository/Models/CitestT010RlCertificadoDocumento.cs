using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT010RlCertificadoDocumento
    {
        public decimal PkT010codigo { get; set; }
        public decimal A010codigoUsuarioCreacion { get; set; }
        public decimal? A010codigoUsuarioModificacion { get; set; }
        public decimal? A010codigoDocumento { get; set; }
        public decimal? A010codigoCertificado { get; set; }
        public string A010codigoParametricaTipoDocumento { get; set; } = null!;
        public decimal A010estadoRegistro { get; set; }
        public DateTime A010fechaCreacion { get; set; }
        public DateTime? A010fechaModificacion { get; set; }

        public virtual CitestT001Certificado A010codigoCertificadoNavigation { get; set; } = null!;
        public virtual AdmintT009Documento A010codigoDocumentoNavigation { get; set; } = null!;
    }
}
