using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CuposV002ReportesPrecintosTest
	{
		[Fact]
		public void CuposV002ReportesPrecintos()
		{
			CuposV002ReportesPrecintos datos = new CuposV002ReportesPrecintos();
			datos.NUmeroRadicacion = "1";
			datos.FechaRadicacion = DateTime.Now;
			datos.CodigoCiudad = 1;
			datos.DireccionEntrega = "1";
			datos.LongMenor = 1;
			datos.LongMayor = 1;
			datos.Cantidad = 1;
			datos.CodigoEmpresa = 1;
			datos.ValorConsignacion = 1;
			datos.Analista = 1;
			datos.PrimerNombreAnalista = "1";
			datos.PrimerApellidoAnalista = "1";
			datos.Especie = 1;
			datos.NIT = 1;
			datos.NombreEmpresa = "1";

			var type = Assert.IsType<CuposV002ReportesPrecintos>(datos);
			Assert.NotNull(type);
		}
	}
}
