using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintV003UsuarioRoleTest
	{
		[Fact]
		public void AdmintV003UsuarioRole()
		{
			AdmintV003UsuarioRole datos = new AdmintV003UsuarioRole();
			datos.nombre = "1";
			datos.a012CodigoParametricaTipoUsuario = 1;
			datos.a012segundoNombre = "1";
			datos.a012segundoApellido = "1";
			datos.a012identificacion = 1;
			datos.a012correoElectronico = "1";
			datos.codigoUsuario = 1;
			datos.codigoRol = 1;
			datos.nombreRol = "1";
			datos.pkT0015codigo = 1;
			datos.a015estadoSolicitud = "1";

			var type = Assert.IsType<AdmintV003UsuarioRole>(datos);
			Assert.NotNull(type);
		}
	}
}
