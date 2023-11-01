using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using static Repository.Helpers.Models.ReportesPrecintosModels;

namespace TestUnit.API
{
	public class ReportesPrecintosRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly ReportesPrecintosRepository repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		
		public ReportesPrecintosRepositoryTest()
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
			repository = new ReportesPrecintosRepository(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void ConsultarDatosPrecintos()
		{
			SealFilters datos = new SealFilters();
			datos.ResolutionNumber = "1";
			datos.Establishment = 1;
			datos.NIT = 1;
			datos.SpecificSearch = 1;

			var r = repository.ConsultarDatosPrecintos(user, datos);
			Assert.True(r != null);

            datos.SpecificSearch = 2;
            r = repository.ConsultarDatosPrecintos(user, datos);
            Assert.True(r != null);

            datos.SpecificSearch = 3;
            r = repository.ConsultarDatosPrecintos(user, datos);
            Assert.True(r != null);

            datos.SpecificSearch = 4;
            r = repository.ConsultarDatosPrecintos(user, datos);
            Assert.True(r != null);

            datos.SpecificSearch = 5;
            r = repository.ConsultarDatosPrecintos(user, datos);
            Assert.True(r != null);

            datos.SpecificSearch = 6;
            r = repository.ConsultarDatosPrecintos(user, datos);
            Assert.True(r != null);

            datos.SpecificSearch = 7;
            r = repository.ConsultarDatosPrecintos(user, datos);
            Assert.True(r != null);
        }

		[Fact]
		public void ConsultarEstablecimientos()
		{
			var r = repository.ConsultarEstablecimientos(user);
			Assert.True(r != null);
		}
	}
}
