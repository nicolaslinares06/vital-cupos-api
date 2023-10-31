using API.Controllers;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Helpers;
using System.Security.Claims;
using System.Security.Cryptography;
using static Repository.Helpers.Models.ReportesPrecintosModels;

namespace TestUnit.API.Controllers
{
	public class SealsLabelsControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly SealsLabelsController precintosMarquillas;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public SealsLabelsControllerTest()
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
			precintosMarquillas = new SealsLabelsController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<SealsLabelsController>());

			precintosMarquillas.ControllerContext = new ControllerContext
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
		public void Consult()
		{
            var filtros = new FiltrosPrecintosMarquillas
            {
                documentType = "Type123",
                initialDate = DateTime.Now,
                number = "Number123",
                documentNumber = "DocNumber123",
                finalDate = DateTime.Now,
                color = "Color123",
                companyName = "Company123",
                validity = "Validity123",
            };

            var r = precintosMarquillas.Consult(filtros);
			Assert.True(r != null);
		}

		[Fact]
		public void CompanyDocumentType()
		{
			var r = precintosMarquillas.CompanyDocumentType();
			Assert.True(r != null);
		}

		[Fact]
		public void Colors()
		{
			var r = precintosMarquillas.Colors();
			Assert.True(r != null);
		}

	}
}
