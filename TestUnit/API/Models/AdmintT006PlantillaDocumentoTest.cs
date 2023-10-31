using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT006PlantillaDocumentoTest
	{
		[Fact]
		public void AdmintT006PlantillaDocumento()
		{
			AdmintT006PlantillaDocumento datos = new AdmintT006PlantillaDocumento();
			datos.PkT006codigo = 1;
			datos.A006codigoUsuarioCreacion = 1;
			datos.A006codigoUsuarioModificacion = 1;
			datos.A006estadoRegistro = 1;
			datos.A006fechaCreacion = DateTime.Now;
			datos.A006fechaModificacion = DateTime.Now;
			datos.A006nombre = "1";
			datos.A006descripcion = "1";
			datos.A006plantillaUrl = "1";

			var type = Assert.IsType<AdmintT006PlantillaDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
