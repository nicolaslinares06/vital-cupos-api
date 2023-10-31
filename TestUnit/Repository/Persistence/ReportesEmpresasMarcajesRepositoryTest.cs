using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Repository.Helpers;
using Web.Models;
using static Repository.Helpers.Models.ReportesEmpresasMarcajeModels;
using Castle.Core.Configuration;

namespace TestUnit.Repository.Persistence
{
	public class ReportesEmpresasMarcajesRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly ReportesEmpresasMarcajesRepository repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
        public readonly IConfiguration configuration;


        public ReportesEmpresasMarcajesRepositoryTest()
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
			repository = new ReportesEmpresasMarcajesRepository(_context, jwtAuthenticationManager, null);
		}

		[Fact]
		public void Consultar()
		{
			decimal tipoEstablecimiento = 1;
			var r = repository.ConsultarEstablecimientosPorTipo(user, tipoEstablecimiento);
			Assert.True(r != null);

            r = repository.ConsultarEstablecimientosPorTipo(user, tipoEstablecimiento);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarDatosReportes()
        {
            var r = repository.ConsultarDatosReportes(user);
            Assert.True(r != null);

            r = repository.ConsultarDatosReportes(user);
            Assert.True(r != null);
        }

        [Fact]
		public void ConsultarDatosEmpresas()
		{
			BusinessFilters datos = new BusinessFilters();
			datos.BusinessType= 1;
			datos.CompanyName = "BISA";
			datos.NIT = 897564231;
			datos.Status = 72;
			datos.CITESIssuanceStatus = 72;
			datos.ResolutionNumber = 1;
			datos.ResolutionIssuanceStartDate = DateTime.Now;
			datos.ResolutionIssuanceEndDate = DateTime.Now;
			datos.SpecificSearch = 1;
			datos.CombinationType = 1;

            var r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 3;

            r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
			datos.SpecificSearch = 2;

            r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 3;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 3;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 4;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 5;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 6;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 7;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 1;
            datos.SpecificSearch = 8;

			r = repository.ConsultarDatosEmpresas(user, datos);
			Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 1;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 2;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 3;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 3;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 4;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

			datos.CombinationType = 2;
            datos.SpecificSearch = 5;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 6;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 7;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 8;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 9;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 10;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 11;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 12;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 13;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 14;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 15;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 16;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 17;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 18;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 19;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 20;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 21;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 22;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 23;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 24;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 25;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 26;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 27;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 28;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            datos.CombinationType = 2;
            datos.SpecificSearch = 30;

            r = repository.ConsultarDatosEmpresas(user, datos);
            Assert.True(r != null);

            r = repository.ConsultarDatosEmpresas(new ClaimsIdentity(), datos);
            Assert.True(r != null);

        }
	}
}
