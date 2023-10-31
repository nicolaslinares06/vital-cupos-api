using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Helpers.Models
{
    
    public partial class CuposAuditoria
    {
        public decimal PkT002codigo { get; set; }
        public string? A002codigoEmpresa { get; set; }
        public string? A002codigoUsuarioCreacion { get; set; }
        public decimal? A002codigoUsuarioModificacion { get; set; }
        public decimal? A002codigoDocumentoCarta { get; set; }
        public decimal? A002codigoDocumentoResolucion { get; set; }
        public decimal? A002codigoDocumentoConsignacion { get; set; }
        public decimal A002estadoRegistro { get; set; }
        public DateTime? A002fechaVigencia { get; set; }
        public DateTime? A002fechaProduccion { get; set; }
        public DateTime A002fechaCreacion { get; set; }
        public DateTime? A002fechaModificacion { get; set; }
        public DateTime? A002fechaResolucion { get; set; }
        public DateTime? A002fechaRegistroResolucion { get; set; }
        public DateTime? A002fechaRadicadoSolicitud { get; set; }
        public DateTime? A002fechaRadicadoRespuesta { get; set; }
        public string? A002observaciones { get; set; } = null!;
        public decimal? A002cuposAsignados { get; set; }
        public decimal? A002cuposDisponibles { get; set; }
        public decimal? A002cuposTotal { get; set; }
        public decimal? A002precintosymarquillasValorpago { get; set; }
        public decimal? A002rangoCodigoInicial { get; set; } = null!;
        public decimal? A002rangoCodigoFin { get; set; } = null!;
        public decimal? A002pielLongitudMenor { get; set; }
        public decimal? A002pielLongitudMayor { get; set; }
        public byte[]? A002estadoCupo { get; set; } = null!;
        public decimal? A002numeroResolucion { get; set; }
        public string? A002cuotaRepoblacion { get; set; } = null!;
        public string? A002AutoridadEmiteResolucion { get; set; }
        public decimal? A002NumeracionInicialPrecintos { get; set; } = null!;
        public decimal? A002NumeracionFinalPrecintos { get; set; } = null!;
        public string? A002CodigoZoocriadero { get; set; }

        public virtual CupostT001Empresa? A002codigoEmpresaNavigation { get; set; } = null!;
        public virtual ICollection<CupostT005Especieaexportar>? CupostT005Especieaexportars { get; set; }
    }
}
