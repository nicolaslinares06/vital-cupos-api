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
	public class EstateControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly EstateController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		
		
        public EstateControllerTest()
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
			controller = new EstateController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<EstateController>());

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
			string? nombre = "1";
			string? estadoReg = "1";
			int? codigoEstado = 1;
			var r = controller.Consult(nombre, estadoReg, codigoEstado);
			Assert.True(r != null);
		}

		[Fact]
		public void Update()
		{
			ReqEstado datos = new ReqEstado();
			datos.id = 1;
			datos.position = 1;
			datos.idEstate = "1";
			datos.description = "1";
			datos.stage = "1";
			datos.estate = true;

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
			AdminStatesReq datos = new AdminStatesReq();
			datos.stage = "1";
			datos.description = "1";
			datos.state = 1;

			var r = controller.Create(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultEstates()
		{
			string? parametro = "1";
			var r = controller.ConsultEstates(parametro);
			Assert.True(r != null);
		}
	}
}
