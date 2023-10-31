using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ActaVisitaCortesModel;

namespace TestUnit.Helpers.Models
{
	public class ActaVisitaCortesModelTest
	{
		[Fact]
		public void ActasVisitasEstablecimientos()
		{
			VisitReportsEstablishments datos = new VisitReportsEstablishments();
			datos.VisitReportId = 1;
			datos.ReportTypeId = 1;
			datos.VisitNumber = 1;
			datos.Establishment = "1";
			datos.EstablishmentType = "1";
			datos.Date = DateTime.Now;
			datos.VisitNumberOne = true;
			datos.VisitNumberTwo = true;
			datos.RegistrationStatus = 1;
			datos.CreationDateDecimal = 1;

			var type = Assert.IsType<VisitReportsEstablishments>(datos);
			Assert.NotNull(type);
		}
	}
}
