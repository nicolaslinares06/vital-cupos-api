using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT004FacturacompraCartaventum
    {
        public decimal PkT004codigo { get; set; }
        public decimal A004codigoUsuarioCreacion { get; set; }
        public decimal? A004codigoUsuarioModificacion { get; set; }
        public decimal A004codigoParametricaTipoCartaventa { get; set; }
        public decimal A004codigoEntidadCompra { get; set; }
        public decimal A004codigoDocumentoSoporte { get; set; }
        public decimal A004codigoDocumentoFactura { get; set; }
        public decimal A004codigoEntidadVende { get; set; }
        public DateTime A004fechaCreacion { get; set; }
        public DateTime? A004fechaModificacion { get; set; }
        public decimal A004estadoRegistro { get; set; }
        public DateTime A004fechaVenta { get; set; } 
        public decimal A004totalCuposObtenidos { get; set; }
        public decimal A004saldoEntidadVendeInicial { get; set; }
        public decimal A004saldoEntidadVendeFinal { get; set; }
        public string A004observacionesCompra { get; set; } = null!;
        public decimal A004totalCuposVendidos { get; set; }
        public decimal A004saldoEntidadCompraInicial { get; set; }
        public decimal A004saldoEntidadCompraFinal { get; set; }
        public string A004observacionesVenta { get; set; } = null!;
        public decimal? A004codigoCupo { get; set; }
        public DateTime A004fechaRegistroCartaVenta { get; set; }
        public string? A004numeroCartaVenta { get; set; }
        public decimal A004disponibilidadInventario { get; set; }
        public string? A004tipoEspecimenEntidadVende { get; set; }
        public string? A004tipoEspecimenEntidadCompra { get; set; }
        public virtual CupostT001Empresa A004codigoEntidadVendeNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A004codigoParametricaTipoCartaventaNavigation { get; set; } = null!;
    }
}
