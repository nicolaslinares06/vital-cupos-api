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
	public class DocumentManagementControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly DocumentManagementController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public DocumentManagementControllerTest()
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
			controller = new DocumentManagementController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<DocumentManagementController>());

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
			var r = controller.Consult(cadenaBusqueda);
			Assert.True(r != null);
		}

		[Fact]
		public void GetDocument()
		{
			decimal id = 1;
			var r = controller.GetDocument(id);
			Assert.True(r != null);
		}

		[Fact]
		public void EditResolution()
		{
			SaveDocumentRequest datos = new SaveDocumentRequest();
			datos.id = 1;
			datos.document = new SupportDocuments();
            var r = controller.EditResolution(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ReadDocument()
		{
			decimal id = 5;
			var r = controller.ReadDocument(id);
			Assert.True(r != null);
		}

        [Fact]
        public void UpdateDocument()
        {
            var datos = new UpdateDocument
            {
                id = 1.0m,
                documentChanges = "Cambios en el documento de ejemplo"
            };

            var r = controller.UpdateDocument(datos);
            Assert.True(r != null);
        }
    }
}
