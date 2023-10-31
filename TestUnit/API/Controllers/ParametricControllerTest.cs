using API.Helpers;
using Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Repository.Helpers;
using Web.Models;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Persistence.Repository;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class ParametricControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly ParametricController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public ParametricControllerTest()
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
			controller = new ParametricController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<ParametricController>());

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
			var r = controller.Consult("", 0);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarParametrica()
		{
			ReqParametric datos = new ReqParametric();
			datos.code = 1;
			datos.name = "COLOR";
			datos.value = "CAFE";
			datos.description = "CAFE";
			datos.estate = StringHelper.estadoActivo;
			var r = controller.Update(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Create()
		{
			ReqParametric datos = new ReqParametric();
			datos.code = 1;
			datos.name = "COLOR";
			datos.value = "CAFE";
			datos.description = "CAFE";
			datos.estate = StringHelper.estadoActivo;

			var r = controller.Create(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Update()
		{
			ReqParametric datos = new ReqParametric();
			datos.code = 10165;
			datos.name = "COLOR";
			datos.value = "CAFE";
			datos.description = "CAFE";
			datos.estate = StringHelper.estadoActivo;

			var r = controller.Update(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ActivateInactivate()
		{
			ReqParametric datos = new ReqParametric();
			datos.code = 10165;
			datos.name = "COLOR";
			datos.value = "CAFE";
			datos.description = "CAFE";
			datos.estate = StringHelper.estadoActivo;

			var r = controller.ActivateInactivate(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDocumentType()
		{
			var r = controller.ConsultDocumentType();
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultParametric()
		{
			string parametrica = "1";
			var r = controller.ConsultParametric(parametrica);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCountries()
		{
			var r = controller.ConsultCountries();
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDepartments()
		{
			int idpais = 1;
			var r = controller.ConsultDepartments(idpais);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCities()
		{
			int iddepartamento = 1;
			var r = controller.ConsultCities(iddepartamento);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultEstateCertificate()
		{
			var r = controller.ConsultEstateCertificate();
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDependence()
		{
			var r = controller.ConsultDependence();
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultParameters()
		{
			string parametro = "1";
			var r = controller.ConsultParameters(parametro);
			Assert.True(r != null);

            r = controller.ConsultParameters("");
            Assert.True(r != null);
        }
	}
}
