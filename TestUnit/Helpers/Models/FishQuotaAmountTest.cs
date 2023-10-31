using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.Helpers.Models
{
	public class FishQuotaAmountTest
	{
		[Fact]
		public void FishQuotaAmount()
		{
			FishQuotaAmount datos = new FishQuotaAmount();
			datos.CodeFishQuotaAmount = 1;
			datos.Group = 1;
			datos.SpeciesCode = 1;
			datos.SpeciesName = "1";
			datos.speciesNameComun = "1";
			datos.Quota = 1;
			datos.Total = 1;
			datos.NameRegion = "1";
			datos.Region = 1;
			datos.ActionTemp = 1;

			var type = Assert.IsType<FishQuotaAmount>(datos);
			Assert.NotNull(type);
		}
	}
}
