using API.Helpers;
using Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Web.Models;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebServices.Controllers;

namespace TestUnit.API.Controllers
{
	public class UserControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly UserController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public UserControllerTest()
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
			controller = new UserController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<UserController>());

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
		public void Authenticate()
		{
			ReqLogin datos = new ReqLogin();
			datos.user = "1";
			datos.password = "1";
			var r = controller.Authenticate(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ChangePassword()
		{
			ReqChangePassword datos = new ReqChangePassword();
			datos.user = "1";
			datos.password = "1";
			datos.newPassword = "1";
			datos.acceptsTerms = true;
			datos.acceptsProcessingPersonalData = true;
			var r = controller.ChangePassword(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void SendEmailChangePassword()
		{
			ReqSimpleUser datos = new ReqSimpleUser();
			datos.user = "1";
			var r = controller.SendEmailChangePassword(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Consult()
		{
			string cadenaBusqueda = "1";
			var r = controller.Consult(cadenaBusqueda);
			Assert.True(r != null);
		}

		[Fact]
		public void Create()
		{
			ReqUser datos = new ReqUser();
			datos.code = 1;
			datos.cityAddress = 1;
			datos.codeParametricDocumentType = 1;
			datos.codeParametricUserType = 1;
			datos.dependence = "1";
			datos.acceptsTerms = true;
			datos.acceptsProcessingPersonalData = true;
			datos.identification = 1;
			datos.firstName = "1";
			datos.secondName = "1";
			datos.firstLastName = "1";
			datos.SecondLastName = "1";
			datos.login = "1";
			datos.address = "1";
			datos.phone = 1;
			datos.email = "1";
			datos.celular = "1";
			datos.password = "1";
			datos.digitalSignature = "1";
			datos.contractStartDate = DateTime.Now;
			datos.contractFinishDate = DateTime.Now;
			datos.registrationStatus = true;
			datos.rol = "1";
			var r = controller.Create(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Actualizar()
		{
			ReqUser datos = new ReqUser();
			datos.code = 1;
			datos.cityAddress = 1;
			datos.codeParametricDocumentType = 1;
			datos.codeParametricUserType = 1;
			datos.dependence = "1";
			datos.acceptsTerms = true;
			datos.acceptsProcessingPersonalData = true;
			datos.identification = 1;
			datos.firstName = "1";
			datos.secondName = "1";
			datos.firstLastName = "1";
			datos.SecondLastName = "1";
			datos.login = "1";
			datos.address = "1";
			datos.phone = 1;
			datos.email = "1";
			datos.celular = "1";
			datos.password = "1";
			datos.digitalSignature = "1";
			datos.contractStartDate = DateTime.Now;
			datos.contractFinishDate = DateTime.Now;
			datos.registrationStatus = true;
			datos.rol = "1";
			var r = controller.Actualizar(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultEdit()
		{
			decimal id = 1;
			var r = controller.ConsultEdit(id);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultTerminos()
		{
			string? login = "1";
			var r = controller.ConsultTerminos(login);
			Assert.True(r != null);
		}
	}
}
