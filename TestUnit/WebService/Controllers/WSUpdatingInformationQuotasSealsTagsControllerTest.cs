using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using WebServices.Controllers;
using Repository.Helpers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Controllers;

namespace TestUnit.WebService.Controllers
{
	public class WSUpdatingInformationQuotasSealsTagsControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private WSUpdatingInformationQuotasSealsTagsController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		
        public WSUpdatingInformationQuotasSealsTagsControllerTest()
		{
			var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
			var authenticationType = "AuthenticationTypes.Federation";

			user = new ClaimsIdentity(authenticationType);
			user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
			user.AddClaim(new Claim("aud", "CUPOS"));
			user.AddClaim(new Claim("exp", "1668005030"));
			user.AddClaim(new Claim("iat", "1668004130"));
			user.AddClaim(new Claim("nbf", "1668004130"));

			

			_context = new DBContext();

			jwtAuthenticationManager = new JwtAuthenticationManager(key);
			controller = new WSUpdatingInformationQuotasSealsTagsController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<WSUpdatingInformationQuotasSealsTagsController>());

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
		public void UpdatingInformationQuotasSealsTags()
		{
			int nit = 897564231;
			int cupos = 1;
			string nombreCientifico = "1";
			int idnombreCientifico = 1;
			string nombreComun = "1";
			decimal numeroInicialPrecinto = 1;
			decimal numeroFinalPrecinto = 1;
			decimal numeroInicialMarquilla = 1;
			decimal numeroFinalMarquill = 1;
			var r = controller.UpdatingInformationQuotasSealsTags(nit, cupos, nombreCientifico,
				idnombreCientifico, nombreComun, numeroInicialPrecinto, numeroFinalPrecinto, numeroInicialMarquilla,
				numeroFinalMarquill);
			Assert.True(r != null);
		}
	}
}