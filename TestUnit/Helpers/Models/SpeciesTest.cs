using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace TestUnit.Helpers.Models
{
	public class SpeciesTest
	{
		[Fact]
		public void Species()
		{
			Species datos = new Species();
			datos.Code = 1;
			datos.CommonName = "1";
			datos.Name = "1";
			datos.NameFamily = "1";

			var type = Assert.IsType<Species>(datos);
			Assert.NotNull(type);
		}


	}
}
