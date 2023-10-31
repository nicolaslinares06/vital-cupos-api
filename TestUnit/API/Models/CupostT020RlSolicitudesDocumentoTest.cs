using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT020RlSolicitudesDocumentoTest
	{
		[Fact]
		public void CupostT020RlSolicitudesDocumento()
		{
			CupostT020RlSolicitudesDocumento datos = new CupostT020RlSolicitudesDocumento();
			datos.Pk_T020Codigo = 1;
			datos.A020CodigoSolicitud = 1;
			datos.A020CodigoDocumento = 1;
			datos.A020CodigoUsuarioCreacion = 1;
			datos.A020CodigoUsuarioModificacion = 1;
			datos.A020FechaCreacion = DateTime.Now;
			datos.A020FechaModificacion = DateTime.Now;
			datos.A020EstadoRegistro = 1;

			var type = Assert.IsType<CupostT020RlSolicitudesDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
