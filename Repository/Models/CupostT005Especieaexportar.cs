using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT005Especieaexportar
    {
        public CupostT005Especieaexportar()
        {
            CupostT006Precintosymarquillas = new HashSet<CupostT006Precintosymarquilla>();
        }

        public decimal? PkT005codigo { get; set; }
        public decimal? A005codigoCupo { get; set; }
        public decimal? A005codigoUsuarioCreacion { get; set; }
        public decimal? A005codigoUsuarioModificacion { get; set; }
        public string? A005codigoParametricaTipoMarcaje { get; set; }
        public string A005codigoEspecie { get; set; } = "";
        public string? A005codigoParametricaPagoCuotaDerepoblacion { get; set; } = null!;
        public DateTime? A005fechaCreacion { get; set; }
        public DateTime? A005fechaModificacion { get; set; }
        public DateTime? A005fechaRadicado { get; set; }
        public decimal? A005estadoRegistro { get; set; } = null!;
        public string? A005nombreEspecie { get; set; } = null!;
        public decimal? A005añoProduccion { get; set; }
        public string? A005marcaLote { get; set; } = null!;
        public string? A005condicionesMarcaje { get; set; } = null!;
        public decimal? A005poblacionParentalMacho { get; set; }
        public decimal? A005poblacionParentalHembra { get; set; }
        public decimal? A005poblacionParentalTotal { get; set; }
        public decimal? A005poblacionSalioDeIncubadora { get; set; }
        public decimal? A005poblacionDisponibleParaCupos { get; set; }
        public string? A005individuosDestinadosARepoblacion { get; set; } = null!;
        public string? A005cupoAprovechamientoOtorgados { get; set; } = null!;
        public string? A005tasaReposicion { get; set; } = null!;
        public decimal? A005numeroMortalidad { get; set; }
        public bool? A005cuotaRepoblacionParaAprovechamiento { get; set; }
        public string? A005cupoAprovechamientoOtorgadosPagados { get; set; } = null!;
        public string? A005observaciones { get; set; } = null!;
        public decimal? A005NumeroInternoInicialCuotaRepoblacion { get; set; }
        public decimal? A005NumeroInternoFinalCuotaRepoblacion { get; set; }
        public decimal? A005codigoDocumentoSoporte { get; set; }



        public virtual CupostT002Cupo? A005codigoCupoNavigation { get; set; } = null!;
        public virtual AdmintT013Auditorium? A005codigoUsuarioCreacionNavigation { get; set; } = null!;
        public virtual AdmintT013Auditorium? A005codigoUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<CupostT006Precintosymarquilla>? CupostT006Precintosymarquillas { get; set; }
    }
}
