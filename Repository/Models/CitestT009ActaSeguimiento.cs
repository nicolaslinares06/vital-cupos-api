using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT009ActaSeguimiento
    {
        public decimal A009codigo { get; set; }
        public decimal A009codigoCertificado { get; set; }
        public decimal A009codigoUsuarioCreacion { get; set; }
        public decimal? A009codigoUsuarioModificacion { get; set; }
        public decimal A009codigoUsuarioEvaluador { get; set; }
        public DateTime? A009fechaModificacion { get; set; }
        public DateTime A009fechaCreacion { get; set; }
        public decimal A009estadoRegistro { get; set; }
        public string A009observaciones { get; set; } = null!;
        public string A009disposicionesFinales { get; set; } = null!;

        public virtual CitestT001Certificado A009codigoCertificadoNavigation { get; set; } = null!;
    }
}
