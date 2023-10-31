using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT003Novedad
    {
        public CupostT003Novedad()
        {
        }

        public decimal PkT003codigo { get; set; }
        public decimal A003codigoEmpresa { get; set; }
        public decimal A003codigoUsuarioCreacion { get; set; }
        public decimal? A003codigoUsuarioModificacion { get; set; }
        public decimal A003codigoParametricaTiponovedad { get; set; }
        public decimal? A003codigoParametricaDisposicionEspecimen { get; set; }
        public decimal A003estadoRegistro { get; set; }
        public decimal A003estadoEmpresa { get; set; }
        public decimal A003estadoEmisionPermisosCITES { get; set; }
        public DateTime A003fechaRegistroNovedad { get; set; }
        public DateTime A003fechaCreacion { get; set; }
        public DateTime? A003fechaModificacion { get; set; }
        public decimal? A003saldoProduccionDisponible { get; set; } = null!;
        public decimal? A003cuposDisponibles { get; set; }
        public decimal? A003inventarioDisponible { get; set; }
        public decimal? A003numeroCupospendientesportramitar { get; set; }
        public decimal? A003codigoEmpresaTraslado { get; set; }
        public string? A003observaciones { get; set; } = null!;
        public string? A003otroCual { get; set; } = null!;
        public string? A003observacionesDetalle { get; set; } = null!;

        public virtual AdmintT008Parametrica A003codigoParametricaTiponovedadNavigation { get; set; } = null!;
    }
}
