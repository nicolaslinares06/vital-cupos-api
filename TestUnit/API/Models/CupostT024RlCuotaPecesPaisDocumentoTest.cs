using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT024RlCuotaPecesPaisDocumentoTest
	{
		[Fact]
		public void CupostT024RlCuotaPecesPaisDocumento()
		{
			CupostT024RlCuotaPecesPaisDocumento datos = new CupostT024RlCuotaPecesPaisDocumento();
			datos.Pk_T024Codigo = 1;
			datos.A024CodigoCuotaPecesPais = 1;
			datos.A024CodigoDocumento = 1;
			datos.A024FechaCreacion = DateTime.Now;
			datos.A024FechaModificacion = DateTime.Now;
			datos.A024UsuarioCreacion = 1;
			datos.A024UsuarioModificacion = 1;
			datos.A024EstadoRegistro = 1;

			var type = Assert.IsType<CupostT024RlCuotaPecesPaisDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
