﻿using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace TestUnit.Helpers.Models
{
	public class QuotaTest
	{
		[Fact]
		public void Quota()
		{
			Quota datos = new Quota();
			datos.Code = 1;
			datos.NumberResolution = 1;
			datos.QuotasGrant = 1;
			datos.QuotasAdvantageCommercialization = 1;
			datos.QuotasRePoblation = "1";
			datos.QuotasAvailable = 1;
			datos.ProductionDate = DateTime.Now;
			datos.YearProduction = 1;
			datos.SpeciesCode = 1;
			datos.SpeciesName = "1";
			datos.QuotasSold = 1;
			datos.InitialNumeration = 1;
			datos.FinalNumeration = 1;
			datos.CompanyCode = 1;
			datos.InitialNumerationRePoblation = 1;
			datos.FinalNumerationRePoblation = 1;
			datos.InitialNumerationSeal = 1;
			datos.FinalNumerationSeal = 1;

			var type = Assert.IsType<Quota>(datos);
			Assert.NotNull(type);
		}
	}
}
