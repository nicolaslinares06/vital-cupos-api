using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT017DiaNoHabilTest
	{
		[Fact]
		public void AdmintT017DiaNoHabil()
		{
			AdmintT017DiaNoHabil datos = new AdmintT017DiaNoHabil();
			datos.PkT017Codigo = 1;
			datos.A017anio = 1;
			datos.A017fechaNoHabil = DateTime.Now;
			datos.A017codigoUsuarioCreacion = 1;
			datos.A017codigoUsuarioModificacion = 1;
			datos.A017fechaCreacion = DateTime.Now;
			datos.A017fechaModificacion = DateTime.Now;

			var type = Assert.IsType<AdmintT017DiaNoHabil>(datos);
			Assert.NotNull(type);
		}
	}
}
