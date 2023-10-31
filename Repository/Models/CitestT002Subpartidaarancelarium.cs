using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT002Subpartidaarancelarium
    {
        public decimal PkT002codigo { get; set; }
        public decimal A002codigoCertificado { get; set; }
        public decimal A002codigoUsuarioCreacion { get; set; }
        public decimal? A002codigoUsuarioModificacion { get; set; }
        public decimal A002cantidadTotal { get; set; }
        public decimal? A002estadoRegistro { get; set; }
        public DateTime A002fechaCreacion { get; set; }
        public DateTime? A002fechaModificacion { get; set; }
        public string A002subpartidaArancelaria { get; set; } = null!;
        public string A002descripcionSubpartida { get; set; } = null!;
        public string A002descripcionProducto { get; set; } = null!;
        public decimal A002valorUnitario { get; set; }
        public decimal A002valorFbo { get; set; }
        public string A002unidadComercial { get; set; } = null!;

        public virtual CitestT001Certificado A002codigoCertificadoNavigation { get; set; } = null!;
    }
}
