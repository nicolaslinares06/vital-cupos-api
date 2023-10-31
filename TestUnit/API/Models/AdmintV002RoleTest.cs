using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintV002RoleTest
	{
		[Fact]
		public void AdmintV002Role()
		{
			AdmintV002Role datos = new AdmintV002Role();
			datos.PkT010codigo = 1;
			datos.A014codigoRol = 1;
			datos.A010descripcion = "1";
			datos.A014eliminar = true;
			datos.A014crear = true;
			datos.A014consultar = true;
			datos.A014actualizar = true;
			datos.A014verDetalle = true;
			datos.A011cargo = "1";
			datos.A011estadoRegistro = 1;

			var type = Assert.IsType<AdmintV002Role>(datos);
			Assert.NotNull(type);
		}
	}
}
