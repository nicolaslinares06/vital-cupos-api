using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT010RlCertificadoDocumentoTest
	{
		[Fact]
		public void AdmintT011Rol()
		{
			CitestT010RlCertificadoDocumento datos = new CitestT010RlCertificadoDocumento();
			datos.PkT010codigo = 1;
			datos.A010codigoUsuarioCreacion = 1;
			datos.A010codigoUsuarioModificacion = 1;
			datos.A010codigoDocumento = 1;
			datos.A010codigoCertificado = 1;
			datos.A010codigoParametricaTipoDocumento = null!;
			datos.A010estadoRegistro = 1;
			datos.A010fechaCreacion = DateTime.Now;
			datos.A010fechaModificacion = DateTime.Now;
			datos.A010codigoCertificadoNavigation = null!;
			datos.A010codigoDocumentoNavigation = null!;

			var type = Assert.IsType<CitestT010RlCertificadoDocumento>(datos);
			Assert.NotNull(type);
		}
	}
}
