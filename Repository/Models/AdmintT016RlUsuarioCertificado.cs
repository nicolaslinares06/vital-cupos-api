using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT016RlUsuarioCertificado
    {
        public decimal PkT016codigo { get; set; }
        public decimal A016codigoUsuario { get; set; }
        public decimal A016codigoUsuarioCreacion { get; set; }
        public decimal? A016codigoUsuarioModificacion { get; set; }
        public decimal A016codigoCertificado { get; set; }
        public decimal A016estadoRegistro { get; set; }
        public DateTime A016fechaCreacion { get; set; }
        public DateTime? A016fechaModificacion { get; set; }

        public virtual CitestT001Certificado A016codigoCertificadoNavigation { get; set; } = null!;
        public virtual AdmintT012Usuario A016codigoUsuarioNavigation { get; set; } = null!;
    }
}
