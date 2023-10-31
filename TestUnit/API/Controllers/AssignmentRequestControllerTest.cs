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
	public class AssignmentRequestControllerTest
	{
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly AssignmentRequestController controller;
		private readonly DBContext _context;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		public static SupportDocuments? documentoEnviar;

        public AssignmentRequestControllerTest()
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
			controller = new AssignmentRequestController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<AssignmentRequestController>());

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
			string nombreUsuario = "Administrador";
			var r = controller.Consult(nombreUsuario);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultStatus()
		{
			var r = controller.ConsultStatus();
			Assert.True(r != null);
		}

		[Fact]
		public void Update()
		{
			ReqAssignment datos = new ReqAssignment();
			datos.id = 1;
			datos.identification = 1;
			datos.firstName = "1";
			datos.secondName = "1";
			datos.firstLastname = "1";
			datos.secondLastname = "1";
			datos.rolId = "1";
			datos.estate = 1;

			var r = controller.Update(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void UpdateStatus()
		{
			ReqAssignmentUpdate datos = new ReqAssignmentUpdate();
			datos.id = 1;
			datos.statusRequest = "1";

			var r = controller.UpdateStatus(datos);
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
        public void Assign()
        {
            var dato = _context.AdmintT011Rols.FirstOrDefault(x => x.A011estadoRegistro != 0);

            var datos = new VitalReq
            {
                code = 1.23m,
                status = "Activo",
                permissions = "PermisosEjemplo",
                message = "MensajeEjemplo",
                ID = 123.45m,
                User = "UsuarioEjemplo",
                Name = "Nombre Ejemplo",
                Document = 67890.12m,
                EMail = "correo@ejemplo.com",
                LastLogin = DateTime.Now,
                Active = "Sí",
                Enabled = "Sí",
                Module = "MóduloEjemplo",
                Url = "URLDeEjemplo",
                Token = "TokenEjemplo",
                UrlError = "URLDeErrorEjemplo",
                rol = dato != null ? dato.PkT011codigo : 0
            };


            var r = controller.Assign(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void VerifyDocument()
        {
            var dato = _context.AdmintV003UsuarioRole.FirstOrDefault(x => x.a015estadoSolicitud != null);
            var r = controller.VerifyDocument(Convert.ToString(dato?.a012identificacion ?? 0));
            Assert.True(r != null);
        }
    }
}
