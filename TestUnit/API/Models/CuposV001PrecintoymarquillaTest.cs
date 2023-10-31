using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CuposV001PrecintoymarquillaTest
	{
		
		[Fact]
		public void CuposV001Precintoymarquilla()
		{
			CuposV001Precintoymarquilla datos = new CuposV001Precintoymarquilla();
			datos.PKV001CODIGO = 1;
			datos.TIPODOCUMENTO = "1";
			datos.NUMERO = 1;
			datos.NOMBRE = "1";
			datos.NUMERORADICADO = "1";
			datos.NUMEROPERMISOCITES = "1";
			datos.FECHAINICIAL = DateTime.Now;
			datos.FECHAFINAL = DateTime.Now;
			datos.NUMEROINICIAL = "1";
			datos.NUMEROFINAL = "1";
			datos.NUMEROINTERNOINICIAL = 1;
			datos.NUMEROINTERNOFINAL = 1;
			datos.VIGENCIA = DateTime.Now;
			datos.CANTIDAD = 1;
			datos.COLOR = "1";
			datos.ESPECIE = "1";
			datos.CUPOSDISPONIBLES = 1;
			datos.CUPOSTOTAL = 1;

			var type = Assert.IsType<CuposV001Precintoymarquilla>(datos);
			Assert.NotNull(type);
		}
	}
}