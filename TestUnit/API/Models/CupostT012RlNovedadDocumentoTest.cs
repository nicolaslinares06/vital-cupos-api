using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT012RlNovedadDocumentoTest
	{
		[Fact]
		public void CupostT012RlNovedadDocumento()
		{
			CupostT012RlNovedadDocumento datos = new CupostT012RlNovedadDocumento();
			datos.PkT012codigo = 1;
			datos.A012codigoUsuarioCreacion = 1;
			datos.A012codigoUsuarioModificacion = 1;
			datos.A012codigoNovedad = 1;
			datos.A012codigoDocumento = 1;
			datos.A012estadoRegistro = 1;
			datos.A012fechaCreacion = DateTime.Now;
			datos.A012fechaModificacion = DateTime.Now;

			var type = Assert.IsType<CupostT012RlNovedadDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
