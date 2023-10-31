using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT008CortePiel
    {
        public decimal A008codigo { get; set; }
        public decimal A008codigoActaVisita { get; set; }
        public string A008tipoCorte { get; set; } = null!;
        public string A008tipoParte { get; set; } = null!;
        public string A008tipoPiel { get; set; } = null!;
        public decimal A008cantidad { get; set; }
        public decimal A008total { get; set; }
        public string A008areaPromedio { get; set; } = null!;
        public decimal A008TipoCorteParteCode { get; set; }
    }
}
