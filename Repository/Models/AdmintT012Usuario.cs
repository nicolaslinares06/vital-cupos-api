using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT012Usuario
    {
        public AdmintT012Usuario()
        {
            AdmintT015RlUsuarioRols = new HashSet<AdmintT015RlUsuarioRol>();
            AdmintT016RlUsuarioCertificados = new HashSet<AdmintT016RlUsuarioCertificado>();
            CitestT004EvaluacionA004codigoUsuarioAsignaNavigations = new HashSet<CitestT004Evaluacion>();
            CitestT004EvaluacionA004codigoUsuarioEvaluadoporNavigations = new HashSet<CitestT004Evaluacion>();
        }

        public decimal PkT012codigo { get; set; }
        public decimal A012codigoUsuarioCreacion { get; set; }
        public decimal? A012codigoUsuarioModificacion { get; set; }
        public decimal? A012codigoCiudadDireccion { get; set; }
        public decimal? A012codigoParametricaTipoDocumento { get; set; }
        public decimal? A012codigoParametricaTipousuario { get; set; }
        public string? A012dependencia { get; set; }
        public bool A012aceptaTerminos { get; set; }
        public bool A012aceptaTratamientoDatosPersonales { get; set; }
        public decimal A012identificacion { get; set; }
        public string A012primerNombre { get; set; } = null!;
        public string A012segundoNombre { get; set; } = null!;
        public string A012primerApellido { get; set; } = null!;
        public string A012segundoApellido { get; set; } = null!;
        public string A012direccion { get; set; } = null!;
        public decimal A012telefono { get; set; }
        public string A012correoElectronico { get; set; } = null!;
        public string? A012celular { get; set; }
        public string A012login { get; set; } = null!;
        public string A012contrasena { get; set; } = null!;
        public string? A012firmaDigital { get; set; }
        public decimal A012estadoRegistro { get; set; } 
        public decimal? A012estadoSolicitud { get; set; }
        public DateTime A012fechaCreacion { get; set; }
        public DateTime? A012fechaModificacion { get; set; }
        public DateTime? A012fechaExpiracontraseña { get; set; }
        public DateTime? A012fechaModificacionContrasena { get; set; }
        public decimal? A012cantidadIntentosIngresoincorrecto { get; set; }
        public DateTime? A012fechaInicioContrato { get; set; }
        public DateTime? A012fechaFinContrato { get; set; }
        public string? A012tokenTemporal { get; set; }
        public decimal? A012CodigoEmpresa { get; set; }
        public string A012Modulo { get; set; } = "";
        public DateTime? A012fechaDesbloqueo { get; set; }

        public virtual AdmintT004Ciudad? A012codigoCiudadDireccionNavigation { get; set; }
        public virtual AdmintT008Parametrica? A012codigoParametricaTipoDocumentoNavigation { get; set; }
        public virtual ICollection<AdmintT015RlUsuarioRol> AdmintT015RlUsuarioRols { get; set; }
        public virtual ICollection<AdmintT016RlUsuarioCertificado> AdmintT016RlUsuarioCertificados { get; set; }
        public virtual ICollection<CitestT004Evaluacion> CitestT004EvaluacionA004codigoUsuarioAsignaNavigations { get; set; }
        public virtual ICollection<CitestT004Evaluacion> CitestT004EvaluacionA004codigoUsuarioEvaluadoporNavigations { get; set; }
    }
}
