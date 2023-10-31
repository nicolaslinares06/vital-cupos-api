using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class WSV001CheckQuotasSealsLabelsTest
	{
		[Fact]
		public void WSV001CheckQuotasSealsLabels()
		{
			Wsv001CheckQuotasSealsLabels datos = new Wsv001CheckQuotasSealsLabels();
			datos.NIT = 1;
			datos.NOMBRECIENTIFICO = "1";
			datos.ID = 1;
			datos.NOMBRECOMUN = "1";
			datos.CUPOS = 1;
			datos.SALDO = 1;
			datos.VIGENCIA = DateTime.Now;
			datos.NUMERACIONINICIALPRECINTOS = 1;
			datos.NUMERACIONFINALPRECINTOS = 1;
			datos.NUMERACIONINICIALMARQUILLA = 1;
			datos.NUMERACIONFINALMARQUILLA = 1;

			var type = Assert.IsType<Wsv001CheckQuotasSealsLabels>(datos);
			Assert.NotNull(type);
		}
	}
}
