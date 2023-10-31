using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT011RlCertificadoEvaluacion
    {
        public decimal PkT011codigo { get; set; }
        public decimal A011codigoUsuarioCreacion { get; set; }
        public decimal? A011codigoUsuarioModificacion { get; set; }
        public decimal A011codigoCertificado { get; set; }
        public decimal A011codigoEvaluacion { get; set; }
        public decimal A011estadoRegistro { get; set; } 
        public DateTime A011fechaCreacion { get; set; }
        public DateTime? A011fechaModificacion { get; set; }
        public string A011tipoDocumento { get; set; } = null!;

        public virtual CitestT001Certificado A011codigoCertificadoNavigation { get; set; } = null!;
        public virtual CitestT004Evaluacion A011codigoEvaluacionNavigation { get; set; } = null!;
    }
}
