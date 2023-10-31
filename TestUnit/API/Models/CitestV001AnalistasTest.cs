using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestV001AnalistasTest
	{
		[Fact]
		public void CitestV001Analistas()
		{
			CitestV001Analistas datos = new CitestV001Analistas();
			datos.PkV001Codigo = 1;
			datos.A001PrimerNombre = "1";
			datos.A001PrimerApellido = "1";
			datos.A001Descripcion = "1";
			datos.A001Asignados = 1;

			var type = Assert.IsType<CitestV001Analistas>(datos);
			Assert.NotNull(type);
		}
	}
}
