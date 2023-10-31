using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT026FacturaCompraCupoTest
	{
		[Fact]
		public void CupostT026FacturaCompraCupo()
		{
			CupostT026FacturaCompraCupo datos = new CupostT026FacturaCompraCupo();
			datos.Pk_T026Codigo = 1;
			datos.A026CodigoFacturaCompra = 1;
			datos.A026CodigoCupo = 1;
			datos.A026NumeracionInicial = 1;
			datos.A026NumeracionFinal = 1;
			datos.A026NumeracionInicialRepoblacion = 1;
			datos.A026NumeracionFinalRepoblacion = 1;
			datos.A026NumeracionInicialPrecintos = 1;
			datos.A026NumeracionFinalPrecintos = 1;
			datos.A026CuposDisponibles = 1;
			datos.A026CantidadEspecimenesComprados = 1;

			var type = Assert.IsType<CupostT026FacturaCompraCupo>(datos);
			Assert.NotNull(type);
		}
	}
}
