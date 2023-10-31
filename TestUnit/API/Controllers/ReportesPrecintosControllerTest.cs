using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Repository.Persistence.IRepository;
using static Repository.Helpers.Models.ReportesPrecintosModels;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class ReportesPrecintosControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly SealRecordsController controller;
		private readonly ClaimsIdentity user;
		public readonly IReportesPrecintosRepository reportesPrecintos;

        public ReportesPrecintosControllerTest()
		{
			var authenticationType = "AuthenticationTypes.Federation";

			user = new ClaimsIdentity(authenticationType);
			user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
			user.AddClaim(new Claim("aud", "CUPOS"));
			user.AddClaim(new Claim("exp", "1668005030"));
			user.AddClaim(new Claim("iat", "1668004130"));
			user.AddClaim(new Claim("nbf", "1668004130"));

			
			controller = new SealRecordsController(reportesPrecintos, new LoggerFactory().CreateLogger<SealRecordsController>());

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
		public void ConsultarDatosPrecintos()
		{
			SealFilters datos = new SealFilters();
			datos.ResolutionNumber = "1";
			datos.Establishment = 1;
			datos.NIT = 1;
			datos.SpecificSearch = 1;
			var r = controller.ConsultarDatosPrecintos(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarEstablecimientos()
		{
			var r = controller.ConsultarEstablecimientos();
			Assert.True(r != null);
		}
	}
}
