using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Repository.Helpers.Models;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class TechnicalAdminControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly TechnicalAdminController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
		
		public static SupportDocuments? documentoEnviar;

        public TechnicalAdminControllerTest()
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
			controller = new TechnicalAdminController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<TechnicalAdminController>());

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
		public void Consult()
		{
			string? valor = "1";
			var r = controller.Consult(valor);
			Assert.True(r != null);
		}

		[Fact]
		public void List()
		{
			var r = controller.List();
			Assert.True(r != null);
		}

		[Fact]
		public void Update()
		{
			TechnicalAdminReq datos = new TechnicalAdminReq();
			datos.code = 1;
			datos.name = "1";
			datos.value = "1";
			datos.description = "1";
			datos.registrationStatus = true;
			var r = controller.Update(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Delete()
		{
			ReqId datos = new ReqId();
			datos.id = 1;
			var r = controller.Delete(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Create()
		{
			TechnicalAdminReq datos = new TechnicalAdminReq();
			datos.code = 1;
			datos.name = "1";
			datos.value = "1";
			datos.description = "1";
			datos.registrationStatus = true;
			var r = controller.Create(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultTechnicalValues()
		{
			string parametro = "1";
			var r = controller.ConsultTechnicalValues(parametro);
			Assert.True(r != null);
		}
	}
}
