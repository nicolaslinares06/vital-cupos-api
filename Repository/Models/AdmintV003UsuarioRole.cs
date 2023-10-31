using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class AdmintV003UsuarioRole
    {
        public string? nombre { get; set; }
        public decimal a012CodigoParametricaTipoUsuario { get; set; }
        public string? a012segundoNombre { get; set; }
        public string? a012segundoApellido { get; set; }
        public decimal? a012identificacion { get; set; }
        public string? a012correoElectronico { get; set; }
        public decimal codigoUsuario { get; set; }
        public decimal codigoRol { get; set; }
        public string nombreRol { get; set; } = "";
        public decimal pkT0015codigo { get; set; }
        public string? a015estadoSolicitud { get; set; }
    }
}
