using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT012UsuarioTest
	{
		[Fact]
		public void AdmintT012Usuario()
		{
			AdmintT012Usuario datos = new AdmintT012Usuario();
			datos.PkT012codigo = 1;
			datos.A012codigoUsuarioCreacion = 1;
			datos.A012codigoUsuarioModificacion = 1;
			datos.A012codigoCiudadDireccion = 1;
			datos.A012codigoParametricaTipoDocumento = 1;
			datos.A012codigoParametricaTipousuario = 1;
			datos.A012dependencia = "1";
			datos.A012aceptaTerminos = true;
			datos.A012aceptaTratamientoDatosPersonales = true;
			datos.A012identificacion = 1;
			datos.A012primerNombre = null!;
			datos.A012segundoNombre = null!;
			datos.A012primerApellido = null!;
			datos.A012segundoApellido = null!;
			datos.A012direccion = null!;
			datos.A012telefono = 1;
			datos.A012correoElectronico = null!;
			datos.A012celular = "1";
			datos.A012login = null!;
			datos.A012contrasena = null!;
			datos.A012firmaDigital = "1";
			datos.A012estadoRegistro = 1;
			datos.A012estadoSolicitud = 1;
			datos.A012fechaCreacion = DateTime.Now;
			datos.A012fechaModificacion = DateTime.Now;
			datos.A012fechaExpiracontraseña = DateTime.Now;
			datos.A012fechaModificacionContrasena = DateTime.Now;
			datos.A012cantidadIntentosIngresoincorrecto = 1;
			datos.A012fechaInicioContrato = DateTime.Now;
			datos.A012fechaFinContrato = DateTime.Now;
			datos.A012tokenTemporal = "1";
			datos.A012CodigoEmpresa = 1;
			datos.A012codigoCiudadDireccionNavigation = null;
			datos.A012codigoParametricaTipoDocumentoNavigation = null;
			datos.AdmintT015RlUsuarioRols = new List<AdmintT015RlUsuarioRol>();
			datos.AdmintT016RlUsuarioCertificados = new List<AdmintT016RlUsuarioCertificado>();
			datos.CitestT004EvaluacionA004codigoUsuarioAsignaNavigations = new List<CitestT004Evaluacion>();
			datos.CitestT004EvaluacionA004codigoUsuarioEvaluadoporNavigations = new List<CitestT004Evaluacion>();
            datos.A012Modulo = "";

			var type = Assert.IsType<AdmintT012Usuario>(datos);
			Assert.NotNull(type);
		}
	}
}
