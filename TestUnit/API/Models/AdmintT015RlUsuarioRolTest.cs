using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT015RlUsuarioRolTest
	{
		[Fact]
		public void AdmintT015RlUsuarioRol()
		{
			AdmintT015RlUsuarioRol datos = new AdmintT015RlUsuarioRol();
			datos.PkT0015codigo = 1;
			datos.A015codigoUsuario = 1;
			datos.A015codigoUsuarioCreacion = 1;
			datos.A015codigoUsuarioModificacion = 1;
			datos.A015codigoRol = "1";
			datos.A015estadoRegistro = 1;
			datos.A015fechaCreacion = DateTime.Now;
			datos.A015fechaModificacion = DateTime.Now;
			datos.A015estadoSolicitud = "1";
			datos.A015codigoUsuarioNavigation = null!;

			var type = Assert.IsType<AdmintT015RlUsuarioRol>(datos);
			Assert.NotNull(type);
		}
	}
}
