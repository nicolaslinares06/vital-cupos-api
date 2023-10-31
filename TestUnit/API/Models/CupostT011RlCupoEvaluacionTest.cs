using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT011RlCupoEvaluacionTest
	{
		[Fact]
		public void CupostT011RlCupoEvaluacion()
		{
			CupostT011RlCupoEvaluacion datos = new CupostT011RlCupoEvaluacion();
			datos.PkT011codigo = 1;
			datos.A011codigoUsuarioCreacion = 1;
			datos.A011codigoUsuarioModificacion = 1;
			datos.A011codigoCupo = 1;
			datos.A011codigoEvaluacion = 1;
			datos.A011estadoRegistro = 1;
			datos.A011fechaCreacion = DateTime.Now;
			datos.A011fechaModificacion = DateTime.Now;

			var type = Assert.IsType<CupostT011RlCupoEvaluacion>(datos);
			Assert.NotNull(type);
		}
	}
}
