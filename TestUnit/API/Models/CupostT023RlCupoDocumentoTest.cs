using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT023RlCupoDocumentoTest
	{
		[Fact]
		public void CupostT023RlCupoDocumento()
		{
			CupostT023RlCupoDocumento datos = new CupostT023RlCupoDocumento();
			datos.Pk_T023Codigo = 1;
			datos.A023CodigoCupo = 1;
			datos.A023CodigoDocuemento = 1;
			datos.A023FechaCreacion = DateTime.Now;
			datos.A023FechaModificacion = DateTime.Now;
			datos.A023UsuarioCreacion = 1;
			datos.A023UsuarioModificacion = 1;
			datos.A023EstadoRegistro = 1;

			var type = Assert.IsType<CupostT023RlCupoDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
