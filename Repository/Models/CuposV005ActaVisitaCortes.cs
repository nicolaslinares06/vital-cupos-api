using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class CuposV005ActaVisitaCortes
    {
        public decimal ActaVisitaId { get; set; }
        public decimal TipoActaId { get; set; }
        public decimal VisitaNumero { get; set; }
        public string Establecimiento { get; set; } = "";
        public string TipoEstablecimiento { get; set; } = ""; 
        public DateTime FechaActaVisita { get; set; }
        public bool VisitaUno { get; set; }
        public bool VisitaDos { get; set; }
        public decimal EstadoRegistro { get; set; }
        public decimal FechaCreacionDecimal { get; set; }
        public decimal EntidadId { get; set; }
        public decimal TipoEstablecimientoId { get; set; }
    }
}
