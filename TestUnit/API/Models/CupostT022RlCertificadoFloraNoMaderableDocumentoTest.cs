using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT022RlCertificadoFloraNoMaderableDocumentoTest
	{
		[Fact]
		public void CupostT022RlCertificadoFloraNoMaderableDocumento()
		{
			CupostT022RlCertificadoFloraNoMaderableDocumento datos = new CupostT022RlCertificadoFloraNoMaderableDocumento();
			datos.Pk_T022Codigo = 1;
			datos.A022CodigoCertificadoFloraNoMaderable = 1;
			datos.A022CodigoDocuemento = 1;
			datos.A022FechaCreacion = DateTime.Now;
			datos.A022FechaModificacion = DateTime.Now;
			datos.A022UsuarioCreacion = 1;
			datos.A022UsuarioModificacion = 1;
			datos.A022EstadoRegistro = 1;

			var type = Assert.IsType<CupostT022RlCertificadoFloraNoMaderableDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
