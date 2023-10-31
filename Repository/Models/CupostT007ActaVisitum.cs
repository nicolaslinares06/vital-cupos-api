using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT007ActaVisitum
    {
        public decimal PkT007codigo { get; set; }
        public decimal A007codigoEntidad { get; set; }
        public decimal A007codigoUsuarioCreacion { get; set; }
        public decimal? A007codigoUsuarioModificacion { get; set; }
        public decimal A007codigoDocumentoOrigenPieles { get; set; }
        public decimal A007codigoPrecintoymarquilla { get; set; }
        public decimal A007codigoUsuarioAutoridadCites { get; set; }
        public byte[] A007firmaUsuarioAutoridadCites { get; set; } = null!;
        public decimal? A007fechaModificacion { get; set; }
        public decimal A007fechaCreacion { get; set; }
        public DateTime A007fechaActa { get; set; }
        public decimal? A007estadoRegistro { get; set; } = 0;
        public string A007estadoPieles { get; set; } = null!;
        public decimal A007cantidadPielesAcortar { get; set; }
        public string A007observaciones { get; set; } = null!;
        public decimal A007TipoActa { get; set; }
        public decimal  A007RepresentanteIdentificacion { get; set; }
        public string?  A007RepresentanteNombre { get; set; }
        public decimal A007CiudadDepartamento { get; set; }
        public decimal A007VisitaNumero { get; set; }
        public bool A007VisitaNumero1 { get; set; }
        public bool A007VisitaNumero2 { get; set; }
        public string? A007PrecintoAdjunto { get; set; }

    }
}
