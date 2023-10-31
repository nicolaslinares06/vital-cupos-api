using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT005EspecimanTest
	{
		[Fact]
		public void AdmintT005Especiman()
		{
			AdmintT005Especiman datos = new AdmintT005Especiman();
			datos.PkT005codigo = 1;
			datos.A005codigoUsuarioCreacion = 1;
			datos.A005codigoUsuarioModificacion = 1;
			datos.A005codigoTipoEspecimen = 1;
			datos.A005fechaCreacion = DateTime.Now;
			datos.A005fechaModificacion = DateTime.Now;
			datos.A005estadoRegistro = 1;
			datos.A005apendice = null;
			datos.A005nombreCientifico = null!;
			datos.A005nombre = null!;
			datos.A005nombreComun = null!;
			datos.A005familia = null!;
			datos.A005reino = null;
			datos.A005clase = null;

			var type = Assert.IsType<AdmintT005Especiman>(datos);
			Assert.NotNull(type);
		}
	}
}
