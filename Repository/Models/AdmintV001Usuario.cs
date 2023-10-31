using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintV001Usuario
    {
        public decimal PkT012codigo { get; set; }
        public string? Roles { get; set; }
        public decimal A012identificacion { get; set; }
        public string A012direccion { get; set; } = null!;
        public decimal A012telefono { get; set; }
        public string A012correoElectronico { get; set; } = null!;
        public string? A012Celular { get; set; }
        public decimal A012estadoRegistro { get; set; }
        public DateTime A012fechaCreacion { get; set; }
        public DateTime? A012fechaModificacion { get; set; }
        public DateTime? A012fechaExpiracontraseña { get; set; }
        public DateTime? A012fechaFinContrato { get; set; }
        public DateTime? A012fechaInicioContrato { get; set; }
        public string A012primerNombre { get; set; } = null!;
        public string A012segundoNombre { get; set; } = null!;
        public string A012segundoApellido { get; set; } = null!;
        public string A012primerApellido { get; set; } = null!;
        public string A012login { get; set; } = null!;
    }
}
