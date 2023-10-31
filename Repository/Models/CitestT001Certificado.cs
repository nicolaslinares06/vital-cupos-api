using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT001Certificado
    {
        public CitestT001Certificado()
        {
            AdmintT016RlUsuarioCertificados = new HashSet<AdmintT016RlUsuarioCertificado>();
            CitestT002Subpartidaarancelaria = new HashSet<CitestT002Subpartidaarancelarium>();
            CitestT005Recaudos = new HashSet<CitestT005Recaudo>();
            CitestT006Informacionespecimen = new HashSet<CitestT006Informacionespeciman>();
            CitestT007Salvoconductomovilizacions = new HashSet<CitestT007Salvoconductomovilizacion>();
            CitestT009ActaSeguimientos = new HashSet<CitestT009ActaSeguimiento>();
            CitestT010RlCertificadoDocumentos = new HashSet<CitestT010RlCertificadoDocumento>();
            CitestT011RlCertificadoEvaluacions = new HashSet<CitestT011RlCertificadoEvaluacion>();
        }

        public decimal PkT001codigo { get; set; }
        public decimal? A001codigoDocumentoActoadministrativoProcedencia { get; set; }
        public decimal A001codigoEmpresa { get; set; }
        public decimal A001codigoUsuarioCreacion { get; set; }
        public decimal? A001codigoUsuarioModificacion { get; set; }
        public decimal? A001codigoCiudadDestino { get; set; }
        public decimal? A001codigoCiudadEmbarque { get; set; }
        public decimal? A001codigoUsuarioEvaluadorpuerto { get; set; }
        public decimal? A001codigoEntidadExportador { get; set; }
        public decimal? A001codigoParametricaTipoEmbarque { get; set; }
        public decimal? A001codigoParametricaTipoPermiso { get; set; }
        public decimal? A001codigoParametricaTipoSolictud { get; set; }
        public decimal? A001codigoPaisDestino { get; set; }
        public decimal? A001codigoPersonaDestinatario { get; set; }
        public decimal? A001codigoPersonaApoderado { get; set; }
        public decimal? A001codigoPersonaTitular { get; set; }
        public decimal? A001codigoPuertoentrada { get; set; }
        public decimal? A001codigoPuertosalida { get; set; }
        public string? A001codigoPuertosalidaOtro { get; set; } = null!;
        public decimal? A001codigoDocumentoPermiso { get; set; }
        public decimal A001estadoRegistro { get; set; }
        public DateTime A001fechaCreacion { get; set; }
        public DateTime? A001fechaModificacion { get; set; }
        public DateTime A001fechaVencimiento { get; set; }
        public DateTime A001fechaSolicitud { get; set; }
        public DateTime? A001fechaRadicacionRespuesta { get; set; }
        public DateTime? A001fechaRadicacionSolicitud { get; set; }
        public DateTime? A001fechaRespuesta { get; set; }
        public DateTime? A001fechaEmbarque { get; set; }
        public DateTime? A001fechaArribo { get; set; }
        public string? A001firmaAprobado { get; set; } = null!;
        public string? A001finalidad { get; set; } = null!;
        public string? A001modotransporte { get; set; } = null!;
        public decimal? A001numeroRadicacion { get; set; }
        public string? A001direccionDestino { get; set; } = null!;
        public string? A001observaciones { get; set; } = null!;
        public decimal? A001NumeroCertificado { get; set; }
        public decimal? A001codigoParametricaTipoCertificado { get; set; }
        public string? A001AutoridadEmiteCertificado { get; set; }

        public virtual AdmintT004Ciudad? A001codigoCiudadDestinoNavigation { get; set; } = null!;
        public virtual AdmintT004Ciudad? A001codigoCiudadEmbarqueNavigation { get; set; } = null!;
        public virtual AdmintT009Documento? A001codigoDocumentoPermisoNavigation { get; set; } = null!;
        public virtual CupostT001Empresa A001codigoEmpresaNavigation { get; set; } = null!;
        public virtual CupostT001Empresa? A001codigoEntidadExportadorNavigation { get; set; } = null!;
        public virtual AdmintT002Pai? A001codigoPaisDestinoNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica? A001codigoParametricaTipoEmbarqueNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A001codigoParametricaTipoPermisoNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica? A001codigoParametricaTipoSolictudNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica? A001codigoParametricaTipoCertificadoNavigation { get; set; } = null!;
        public virtual CitestT003Persona? A001codigoPersonaApoderadoNavigation { get; set; } = null!;
        public virtual CitestT003Persona? A001codigoPersonaDestinatarioNavigation { get; set; } = null!;
        public virtual CitestT003Persona? A001codigoPersonaTitularNavigation { get; set; } = null!;
        public virtual AdmintT001Puerto? A001codigoPuertoentradaNavigation { get; set; } = null!;
        public virtual AdmintT001Puerto? A001codigoPuertosalidaNavigation { get; set; } = null!;
        public virtual ICollection<AdmintT016RlUsuarioCertificado> AdmintT016RlUsuarioCertificados { get; set; }
        public virtual ICollection<CitestT002Subpartidaarancelarium> CitestT002Subpartidaarancelaria { get; set; }
        public virtual ICollection<CitestT005Recaudo> CitestT005Recaudos { get; set; }
        public virtual ICollection<CitestT006Informacionespeciman> CitestT006Informacionespecimen { get; set; }
        public virtual ICollection<CitestT007Salvoconductomovilizacion> CitestT007Salvoconductomovilizacions { get; set; }
        public virtual ICollection<CitestT009ActaSeguimiento> CitestT009ActaSeguimientos { get; set; }
        public virtual ICollection<CitestT010RlCertificadoDocumento> CitestT010RlCertificadoDocumentos { get; set; }
        public virtual ICollection<CitestT011RlCertificadoEvaluacion> CitestT011RlCertificadoEvaluacions { get; set; }
    }
}
