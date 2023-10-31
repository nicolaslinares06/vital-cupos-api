using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT011RlCertificadoEvaluacionTest
	{
		[Fact]
		public void CitestT011RlCertificadoEvaluacion()
		{
			CitestT011RlCertificadoEvaluacion datos = new CitestT011RlCertificadoEvaluacion();
			datos.PkT011codigo = 1;
			datos.A011codigoUsuarioCreacion = 1;
			datos.A011codigoUsuarioModificacion = 1;
			datos.A011codigoCertificado = 1;
			datos.A011codigoEvaluacion = 1;
			datos.A011estadoRegistro = 1;
			datos.A011fechaCreacion = DateTime.Now;
			datos.A011fechaModificacion = DateTime.Now;
			datos.A011tipoDocumento = null!;
			datos.A011codigoCertificadoNavigation = null!;
			datos.A011codigoEvaluacionNavigation = null!;

			var type = Assert.IsType<CitestT011RlCertificadoEvaluacion>(datos);
			Assert.NotNull(type);
		}
	}
}
