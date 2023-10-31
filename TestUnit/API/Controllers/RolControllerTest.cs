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
	public class RolControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly RolController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public RolControllerTest()
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
			controller = new RolController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<RolController>());

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
			string? cadenaBusqueda = "1";
			string? estado = "1";
			var r = controller.Consult(cadenaBusqueda, estado);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultRols()
		{
			var r = controller.ConsultRols();
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultByRol()
		{
			int rol = 1;
			string cargo = "1";
			bool estado = true;
			var r = controller.ConsultByRol(rol, cargo, estado);
			Assert.True(r != null);
		}

		[Fact]
		public void UpdateFunctionality()
		{
			RolModPermition datos = new RolModPermition();
			datos.rolId = 1;
			datos.moduleId = 1;
			datos.consult = true;
			datos.create = true;
			datos.update = true;
			datos.delete = true;
			datos.see = true;
			datos.name = "1";
			var r = controller.UpdateFunctionality(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Create()
		{
			ReqRol datos = new ReqRol();
			datos.rolId = 1;
			datos.position = "1";
			datos.description = "1";
			datos.estate = true;
			datos.name = "1";
			var r = controller.Create(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void Update()
		{
			ReqRol datos = new ReqRol();
			datos.rolId = 1;
			datos.position = "1";
			datos.description = "1";
			datos.estate = true;
			datos.name = "1";
			var r = controller.Update(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultAllRols()
		{
			string parametro = "1";
			var r = controller.ConsultAllRols(parametro);
			Assert.True(r != null);
		}

        [Fact]
        public void ConsultRolsAssign()
        {
            var r = controller.ConsultRolsAssign();
            Assert.True(r != null);
        }
    }
}
