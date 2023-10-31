using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class CvAControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly CvAController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public CvAControllerTest()
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
			controller = new CvAController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<CvAController>());

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
		public void Buscar()
		{
			decimal documentypecv = 1;
			string documentid = "1";
			var r = controller.Buscar(documentypecv, documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void SituacionTest()
		{
			decimal documentypecv = 1;
			string documentid = "1";
			var r = controller.Situacion(documentypecv, documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void ResolucioncuposTest()
		{
			string documentid = "1";
			var r = controller.Resolucioncupos(documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCertificateshjTest()
		{
			var r = controller.ConsultCertificateshj();
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultPecesTest()
		{
			decimal documentid = 1;
			var r = controller.ConsultPeces(documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void DocumentoVentaTest()
		{
			var r = controller.DocumentoVenta(1321321);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDocument2Test()
		{
			decimal docuid = 1;
			var r = controller.ConsultDocument2(docuid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDocumentidTest()
		{
			decimal docuid = 1;
			var r = controller.ConsultDocumentid(docuid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCertificateshj2Test()
		{
			decimal idcertificado = 1;
			var r = controller.ConsultCertificateshj2(idcertificado);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionpdfTest()
		{
			decimal situacionid = 1;
			var r = controller.ConsultSituacionpdf(situacionid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionidTest()
		{
			decimal codigoEmpresa = 1;
			decimal situacionid = 1;
			var r = controller.ConsultSituacionid(codigoEmpresa, situacionid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionnovedadTest()
		{
			decimal codigoEmpresa = 1;
			var r = controller.ConsultSituacionnovedad(codigoEmpresa);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionnovedadultimaTest()
		{
			decimal codigoEmpresa = 1;
			var r = controller.ConsultSituacionnovedadultima(codigoEmpresa);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultpecespdfTest()
		{
			decimal idresolucionp = 1;
			var r = controller.Consultpecespdf(idresolucionp);
			Assert.True(r != null);
		}

        [Fact]
        public void ConsultOneQuota()
        {
            decimal idresolucionp = 1;
            var r = controller.ConsultOneQuota(idresolucionp);
            Assert.True(r != null);
        }
    }
}