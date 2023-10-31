using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Mailjet.Client.Resources;

namespace TestUnit.API
{
	public class ModulosRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Modulos repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public static SupportDocuments? documentoEnviar;

		public ModulosRepositoryTest()
		{
			var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
			var authenticationType = "AuthenticationTypes.Federation";

			user = new ClaimsIdentity(authenticationType);
			user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "20099"));
			user.AddClaim(new Claim("aud", "CUPOS"));
			user.AddClaim(new Claim("exp", "1668005030"));
			user.AddClaim(new Claim("iat", "1668004130"));
			user.AddClaim(new Claim("nbf", "1668004130"));

			

			_context = new DBContext();

			jwtAuthenticationManager = new JwtAuthenticationManager(key);
			repository = new Modulos(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void ConsultarTodos()
		{
			var r = repository.ConsultarTodos(user, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarRol()
		{
            var r = repository.ConsultarRol(user, ipAddress);
			Assert.True(r != null);
		}
	}
}
