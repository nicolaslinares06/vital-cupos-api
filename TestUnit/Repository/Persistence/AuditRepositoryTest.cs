using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;

namespace TestUnit.API
{
	public class AuditRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Audit repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public static SupportDocuments? documentoEnviar;

		public AuditRepositoryTest()
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
			repository = new Audit(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Consultar()
		{
			DateTime fechaInicio = DateTime.Now;
			DateTime fechaFinal = DateTime.Now; 
			int? pagina = 1;
			var r = repository.Consultar(user, fechaInicio, fechaFinal, ipAddress, pagina);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarDetalle()
		{
			DateTime fecha = DateTime.Now;
			var r = repository.ConsultarDetalle(user, fecha, ipAddress);
			Assert.True(r != null);
		}
	}
}
