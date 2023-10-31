using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT016RlUsuarioCertificadoTest
	{
		[Fact]
		public void AdmintT016RlUsuarioCertificado()
		{
			AdmintT016RlUsuarioCertificado datos = new AdmintT016RlUsuarioCertificado();
			datos.PkT016codigo = 1;
			datos.A016codigoUsuario = 1;
			datos.A016codigoUsuarioCreacion = 1;
			datos.A016codigoUsuarioModificacion = 1;
			datos.A016codigoCertificado = 1;
			datos.A016estadoRegistro = 1;
			datos.A016fechaCreacion = DateTime.Now;
			datos.A016fechaModificacion = DateTime.Now;
			datos.A016codigoCertificadoNavigation = null!;
			datos.A016codigoUsuarioNavigation = null!;

			var type = Assert.IsType<AdmintT016RlUsuarioCertificado>(datos);
			Assert.NotNull(type);
		}
	}
}
