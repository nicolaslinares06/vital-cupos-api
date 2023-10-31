using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT003DepartamentoTest
	{
		[Fact]
		public void AdmintT003Departamento()
		{
			AdmintT003Departamento datos = new AdmintT003Departamento();
			datos.PkT003codigo = 1;
			datos.A003codigoUsuarioCreacion = 1;
			datos.A003codigoUsuarioModificacion = 1;
			datos.A003codigoPais = 1;
			datos.A003estadoRegistro = 1;
			datos.A003fechaCreacion = DateTime.Now;
			datos.A003fechaModificacion = DateTime.Now;
			datos.A003nombre = null!;
			datos.A003codigoPaisNavigation = null!;
			datos.AdmintT004Ciudads = new List<AdmintT004Ciudad>();

			var type = Assert.IsType<AdmintT003Departamento>(datos);
			Assert.NotNull(type);
		}
	}
}
