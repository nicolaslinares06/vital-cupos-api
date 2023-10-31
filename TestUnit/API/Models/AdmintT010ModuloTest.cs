using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT010ModuloTest
	{
		[Fact]
		public void AdmintT010Modulo()
		{
			AdmintT010Modulo datos = new AdmintT010Modulo();
			datos.PkT010codigo = 1;
			datos.A010codigoUsuarioCreacion = 1;
			datos.A010codigoUsuarioModificacion = 1;
			datos.A010estadoRegistro = 1;
			datos.A010fechaCreacion = DateTime.Now;
			datos.A010fechaModificacion = DateTime.Now;
			datos.A010modulo = null!;
			datos.A010descripcion = null!;
			datos.A010informacionAyuda = null!;
			datos.A010aplicativo = null!;

			var type = Assert.IsType<AdmintT010Modulo>(datos);
			Assert.NotNull(type);
		}
	}
}
