using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT002PaiTest
	{
		[Fact]
		public void AdmintT002Pai()
		{
			AdmintT002Pai datos = new AdmintT002Pai();
			datos.PkT002codigo = 1;
			datos.A002codigoUsuarioCreacion = 1;
			datos.A002codigoUsuarioModificacion = 1;
			datos.A002estadoRegistro = 1;
			datos.A002fechaCreacion = DateTime.Now;
			datos.A002fechaModificacion = DateTime.Now;
			datos.A002nombre = null!;
			datos.AdmintT003Departamentos = new List<AdmintT003Departamento>();
			datos.CitestT001Certificados = new List<CitestT001Certificado>();

			var type = Assert.IsType<AdmintT002Pai>(datos);
			Assert.NotNull(type);
		}
	}
}
