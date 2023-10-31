using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT004Evaluacion
    {
        public CitestT004Evaluacion()
        {
            CitestT011RlCertificadoEvaluacions = new HashSet<CitestT011RlCertificadoEvaluacion>();
        }

        public decimal PkT004codigo { get; set; }
        public decimal A004codigoUsuarioCreacion { get; set; }
        public decimal? A004codigoUsuarioModificacion { get; set; }
        public decimal A004codigoUsuarioAsigna { get; set; }
        public decimal A004codigoUsuarioEvaluadopor { get; set; }
        public decimal A004codigoDocumento { get; set; }
        public string A004codigoCertificado { get; set; } = null!;
        public decimal A004estadoRegistro { get; set; }
        public string A004estadoCertificado { get; set; } = null!;
        public DateTime A004fechaCreacion { get; set; }
        public DateTime? A004fechaModificacion { get; set; }
        public DateTime A004fechaVencimiento { get; set; }
        public string A004observacion { get; set; } = null!;
        public string A004notas { get; set; } = null!;
        public DateTime A004fechaCambioEstado { get; set; }

        public virtual AdmintT012Usuario A004codigoUsuarioAsignaNavigation { get; set; } = null!;
        public virtual AdmintT012Usuario A004codigoUsuarioEvaluadoporNavigation { get; set; } = null!;
        public virtual ICollection<CitestT011RlCertificadoEvaluacion> CitestT011RlCertificadoEvaluacions { get; set; }
    }
}
