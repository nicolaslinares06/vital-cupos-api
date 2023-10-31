using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class AdmintV004Auditoria
    {
        public decimal? pkT013codigo { get; set; }
        public string? usuarioAuditado { get; set; }
        public string? rol { get; set; }
        public string? modulo { get; set; }
        public DateTime? fecha { get; set; }
        public string? accion { get; set; }
        public string? ip { get; set; }
    }
}
