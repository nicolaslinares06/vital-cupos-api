using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT014RlRolModuloPermisoTest
	{
		[Fact]
		public void AdmintT014RlRolModuloPermiso()
		{
			AdmintT014RlRolModuloPermiso datos = new AdmintT014RlRolModuloPermiso();
			datos.PkT014codigo = 1;
			datos.A014codigoRol = 1;
			datos.A014codigoModulo = 1;
			datos.A014codigoUsuarioCreacion = 1;
			datos.A014codigoUsuarioModificacion = 1;
			datos.A014estadoRegistro = 1;
			datos.A014fechaCreacion = DateTime.Now;
			datos.A014fechaModificacion = DateTime.Now;
			datos.A014eliminar = true;
			datos.A014crear = true;
			datos.A014consultar = true;
			datos.A014actualizar = true;
			datos.A014verDetalle = true;

			var type = Assert.IsType<AdmintT014RlRolModuloPermiso>(datos);
			Assert.NotNull(type);
		}
	}
}
