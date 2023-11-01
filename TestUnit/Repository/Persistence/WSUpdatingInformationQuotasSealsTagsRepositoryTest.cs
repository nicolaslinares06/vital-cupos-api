using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;

namespace TestUnit.API
{
	public class WSUpdatingInformationQuotasSealsTagsRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly WSUpdatingInformationQuotasSealsTags repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public WSUpdatingInformationQuotasSealsTagsRepositoryTest()
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
			repository = new WSUpdatingInformationQuotasSealsTags(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void UpdatingInformationQuotasSealsTags()
		{
			var query = _context.WSV001CheckQuotasSealsLabels.FirstOrDefault(x => x.NIT != null);


            int nit = Convert.ToInt32(query?.NIT ?? 0);
			int cupos = Convert.ToInt32(query?.CUPOS ?? 0);
			string nombreCientifico = query?.NOMBRECIENTIFICO ?? "";
			int idnombreCientifico = Convert.ToInt32(query?.ID ?? 0);
			string nombreComun = query?.NOMBRECOMUN ?? "";
			decimal numeroInicialMarquilla = Convert.ToInt32(query?.NUMERACIONINICIALMARQUILLA ?? 0);
			decimal numeroFinalMarquill = Convert.ToInt32(query?.NUMERACIONFINALMARQUILLA ?? 0);
			var r = repository.UpdatingInformationQuotasSealsTags(user, ipAddress, nit, cupos, nombreCientifico,
				idnombreCientifico, nombreComun, null, null, numeroInicialMarquilla,
				numeroFinalMarquill);
			Assert.True(r != null);


            nit = 84232;
            r = repository.UpdatingInformationQuotasSealsTags(user, ipAddress, nit, cupos, nombreCientifico,
                idnombreCientifico, nombreComun, null, null, numeroInicialMarquilla,
                numeroFinalMarquill);
            Assert.True(r != null);
        }
	}
}
