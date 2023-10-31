using API.Helpers;
using Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Repository.Helpers;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class CompanyComtrollerTest
	{
		private readonly DBContext _context;
		private readonly CompanyController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public CompanyComtrollerTest()
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
			controller = new CompanyController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<CompanyController>());

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
		public void Actualizar()
		{
			EntityRequest datos = new EntityRequest();
			datos.CompanyCode = 1;
			datos.DocumentTypeId = 1;
			datos.EntityTypeId = 1;
			datos.CompanyName = "1";
			datos.NIT = 1;
			datos.CityId = 1;
			datos.Address = "1";
			datos.Phone = 1;
			datos.Email = "1";
			datos.BusinessRegistration = "1";

			var r = controller.Actualizar(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultNovedades()
		{
			var r = controller.ConsultNovedades(1, 1);
			Assert.True(r != null);
		}

		[Fact]
		public void Save()
		{
			NoveltiesRequest req = new NoveltiesRequest();
			var r = controller.Save(req);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCupos()
		{
			var r = controller.ConsultCupos(1);
			Assert.True(r != null);
		}
	}
}
