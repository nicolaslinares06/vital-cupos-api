using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT018Notificacion
    {
        public decimal PkT018Codigo { get; set; }
        public DateTime A018fechaCreacion { get; set; }
        public DateTime? A018fechaModificacion { get; set; }
        public decimal A018codigoUsuarioCreacion { get; set; }
        public decimal? A018codigoUsuarioModificacion { get; set; }
        public decimal A018estadoRegistro { get; set; }
        public string A018correoEnvioNotificacion { get; set; } = null!;
        public DateTime A018fechaEnvioNotificacion { get; set; }
        public string A018notificacionAsunto { get; set; } = null!;
        public string A018notificacionMensaje { get; set; } = null!;
    }
}
