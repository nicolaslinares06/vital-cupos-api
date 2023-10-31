using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT006Precintosymarquilla
    {
        public decimal PkT006codigo { get; set; }
        public string A006codigoPrecintoymarquilla { get; set; } = "";
        public decimal? A006codigoEspecieExportar { get; set; }
        public decimal A006codigoUsuarioCreacion { get; set; }
        public decimal? A006codigoUsuarioModificacion { get; set; }
        public decimal? A006codigoParametricaTipoPrecintomarquilla { get; set; }
        public decimal? A006codigoParametricaColorPrecintosymarquillas { get; set; }
        public decimal A006estadoRegistro { get; set; } 
        public DateTime A006fechaCreacion { get; set; }
        public DateTime? A006fechaModificacion { get; set; }
        public string A006observacion { get; set; } = null!;
        public string? A006numeroInicial { get; set; } = null!;
        public string? A006numeroFinal { get; set; } = null!;
        public decimal? A006numeroInicialNumerico { get; set; } = null!;
        public decimal? A006numeroFinalNumerico { get; set; } = null!;
        public decimal? A006codigoSolicitud { get; set; }
        public virtual CupostT005Especieaexportar? A006codigoEspecieExportarNavigation { get; set; }
    }
}
