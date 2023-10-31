using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT025FacturaCompraCartaVentaDocumentoTest
	{
		[Fact]
		public void CupostT025FacturaCompraCartaVentaDocumento()
		{
			CupostT025FacturaCompraCartaVentaDocumento datos = new CupostT025FacturaCompraCartaVentaDocumento();
			datos.Pk_T025Codigo = 1;
			datos.A025CodigoFacturaCompraCartaVenta = 1;
			datos.A025CodigoDocumento = 1;
			datos.A025FechaCreacion = DateTime.Now;
			datos.A025FechaModificacion = DateTime.Now;
			datos.A025UsuarioCreacion = 1;
			datos.A025UsuarioModificacion = 1;
			datos.A025EstadoRegistro = 1;

			var type = Assert.IsType<CupostT025FacturaCompraCartaVentaDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
