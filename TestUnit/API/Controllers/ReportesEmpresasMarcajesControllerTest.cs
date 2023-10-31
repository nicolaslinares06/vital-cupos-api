using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using static Repository.Helpers.Models.ReportesEmpresasMarcajeModels;
using API.Controllers;
using Repository.Persistence.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class ReportesEmpresasMarcajesControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly CompanyAttendanceReportsController controller;
		private readonly ClaimsIdentity user;
		public static SupportDocuments? documentoEnviar;
        public readonly IReportesEmpresasMarcaje reportesEmpresasMarcaje;

        public ReportesEmpresasMarcajesControllerTest()
		{
			var authenticationType = "AuthenticationTypes.Federation";

			user = new ClaimsIdentity(authenticationType);
			user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
			user.AddClaim(new Claim("aud", "CUPOS"));
			user.AddClaim(new Claim("exp", "1668005030"));
			user.AddClaim(new Claim("iat", "1668004130"));
			user.AddClaim(new Claim("nbf", "1668004130"));

			controller = new CompanyAttendanceReportsController(reportesEmpresasMarcaje, new LoggerFactory().CreateLogger<CompanyAttendanceReportsController>());

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, "Administrador")
					}, "someAuthTypeName"))
				}
			};
		}

		[Fact]
		public void ConsultarDatosCupoEmpresas()
		{
			BusinessFilters datos = new BusinessFilters();
			datos.BusinessType= 1;
			datos.CompanyName = "1";
			datos.NIT = 1;
			datos.Status = 1;
			datos.CITESIssuanceStatus = 1;
			datos.ResolutionNumber = 1;
			datos.ResolutionIssuanceStartDate = DateTime.Now;
			datos.ResolutionIssuanceEndDate = DateTime.Now;
			datos.SpecificSearch = 1;
			var r = controller.ConsultDataQuotaBusiness(datos);
			Assert.True(r != null);
		}
	}
}
