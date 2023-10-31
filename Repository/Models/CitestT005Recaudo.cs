using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT005Recaudo
    {
        public decimal PkT005codigo { get; set; }
        public decimal A005codigoDocumentoSoportetransferencia { get; set; }
        public string A005codigoParametricaTipodocumento { get; set; } = null!;
        public decimal A005codigoParametricaTipoPago { get; set; }
        public decimal A005codigoUsuarioCreacion { get; set; }
        public decimal? A005codigoUsuarioModificacion { get; set; }
        public decimal A005codigoCertificado { get; set; }
        public string A005banco { get; set; } = null!;
        public decimal A005estadoRegistro { get; set; }
        public DateTime A005fechaCreacion { get; set; }
        public DateTime? A005fechaModificacion { get; set; }
        public DateTime A005fechaConsignacion { get; set; }
        public decimal A005numeroCuenta { get; set; }
        public decimal A005valor { get; set; }
        public string A005observaciones { get; set; } = null!;

        public virtual CitestT001Certificado A005codigoCertificadoNavigation { get; set; } = null!;
        public virtual AdmintT009Documento A005codigoDocumentoSoportetransferenciaNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A005codigoParametricaTipoPagoNavigation { get; set; } = null!;
    }
}
