using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT013RlFacturaDocumentoTest
	{
		[Fact]
		public void CupostT013RlFacturaDocumento()
		{
			CupostT013RlFacturaDocumento datos = new CupostT013RlFacturaDocumento();
			datos.PkT013codigo = 1;
			datos.A013codigoUsuarioCreacion = 1;
			datos.A013codigoUsuarioModificacion = 1;
			datos.A013codigoFacturacompraCartaventa = 1;
			datos.A013codigoDocumento = 1;
			datos.A013estadoRegistro = 1;
			datos.A013fechaCreacion = DateTime.Now;
			datos.A013fechaModificacion = DateTime.Now;

			var type = Assert.IsType<CupostT013RlFacturaDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
