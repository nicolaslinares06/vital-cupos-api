using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;

namespace TestUnit.API
{
	public class WSCheckQuotasSealsLabelsRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly WSCheckQuotasSealsLabels repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public WSCheckQuotasSealsLabelsRepositoryTest()
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
			repository = new WSCheckQuotasSealsLabels(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void ConsultCheckQuotasSealsLabels()
		{
			int nit = 897564231;
			var r = repository.ConsultCheckQuotasSealsLabels(user, ipAddress, nit);
			Assert.True(r != null);

            r = repository.ConsultCheckQuotasSealsLabels(new ClaimsIdentity(), "", nit);
            Assert.True(r != null);
        }
	}
}
