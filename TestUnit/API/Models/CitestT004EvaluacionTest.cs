using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT004EvaluacionTest
	{
		[Fact]
		public void CitestT004Evaluacion()
		{
			CitestT004Evaluacion datos = new CitestT004Evaluacion();
			datos.PkT004codigo = 1;
			datos.A004codigoUsuarioCreacion = 1;
			datos.A004codigoUsuarioModificacion = 1;
			datos.A004codigoUsuarioAsigna = 1;
			datos.A004codigoUsuarioEvaluadopor = 1;
			datos.A004codigoDocumento = 1;
			datos.A004codigoCertificado = null!;
			datos.A004estadoRegistro = 1;
			datos.A004estadoCertificado = null!;
			datos.A004fechaCreacion = DateTime.Now;
			datos.A004fechaModificacion = DateTime.Now;
			datos.A004fechaVencimiento = DateTime.Now;
			datos.A004observacion = null!;
			datos.A004notas = null!;
			datos.A004fechaCambioEstado = DateTime.Now;
			datos.A004codigoUsuarioAsignaNavigation = null!;
			datos.A004codigoUsuarioEvaluadoporNavigation = null!;
			datos.CitestT011RlCertificadoEvaluacions = null!;

			var type = Assert.IsType<CitestT004Evaluacion>(datos);
			Assert.NotNull(type);
		}
	}
}
