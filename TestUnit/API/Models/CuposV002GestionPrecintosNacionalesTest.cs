using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CuposV002GestionPrecintosNacionalesTest
	{
		[Fact]
		public void CuposV002GestionPrecintosNacionales()
		{
			CuposV002GestionPrecintosNacionales datos = new CuposV002GestionPrecintosNacionales();
			datos.ID = 1;
			datos.NUMERORADICADO = "1";
			datos.FECHARADICADO = DateTime.Now;
			datos.PRECINTOSNACIONALES = "1";
			datos.ENTIDAD = "1";
			datos.FECHASOLICITUD = DateTime.Now;
			datos.ESTADO = "1";
			datos.ANALISTA = 1;
			datos.NUMERORADICADOSALIDA = "1";
			datos.TIPOSOLICITUD = "1";
			datos.FECHARADICADOSALIDA = DateTime.Now;

			var type = Assert.IsType<CuposV002GestionPrecintosNacionales>(datos);
			Assert.NotNull(type);
		}
	}
}
