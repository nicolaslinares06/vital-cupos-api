using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT011RolTest
	{
		[Fact]
		public void AdmintT011Rol()
		{
			AdmintT011Rol datos = new AdmintT011Rol();
			datos.PkT011codigo = 1;
			datos.A011codigoUsuarioCreacion = 1;
			datos.A011codigoUsuarioModificacion = 1;
			datos.A011estadoRegistro = 1;
			datos.A011fechaCreacion = DateTime.Now;
			datos.A011fechaModificacion = DateTime.Now;
			datos.A011nombre = null!;
			datos.A011cargo = null!;
			datos.A011descripcion = null!;
			datos.A011modulo = null!;
			datos.A011tipoUsuario = null!;

			var type = Assert.IsType<AdmintT011Rol>(datos);
			Assert.NotNull(type);
		}
	}
}
