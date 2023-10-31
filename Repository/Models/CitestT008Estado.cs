using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT008Estado
    {
        public decimal PkT008codigo { get; set; }
        public decimal A008codigoUsuarioCreacion { get; set; }
        public decimal? A008codigoUsuarioModificacion { get; set; }
        public decimal A008posicion { get; set; }
        public decimal A008codigoParametricaEstado { get; set; }
        public decimal A008estadoRegistro { get; set; }
        public string A008descripcion { get; set; } = null!;
        public DateTime A008fechaCreacion { get; set; }
        public DateTime? A008fechaModificacion { get; set; }
        public string A008etapa { get; set; } = "";
        public string A008modulo { get; set; } = "";
    }
}
