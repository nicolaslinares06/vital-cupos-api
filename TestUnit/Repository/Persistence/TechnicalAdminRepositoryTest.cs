using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Web.Models;

namespace TestUnit.API
{
	public class TechnicalAdminRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly TechnicalAdmin repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public TechnicalAdminRepositoryTest()
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
			repository = new TechnicalAdmin(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Consultar()
		{
			string? valor = "1";
			var r = repository.Consultar(user, valor, ipAddress);
			Assert.True(r != null);

            r = repository.Consultar(user, null, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void Listar()
		{
			var r = repository.Listar(user, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Actualizar()
		{
            var dato = _context.AdmintT007AdminTecnicas.FirstOrDefault(x => x.a007estadoRegistro != 0);
            Random rand = new Random();
            int numero = rand.Next(26);
            char letra = (char)(((int)'A') + numero);

            TechnicalAdminReq datos = new TechnicalAdminReq();
			datos.code = dato?.pkT007Codigo ?? 0;
			datos.name = "1" + letra;
			datos.value = "1" + letra;
			datos.description = "1" + letra;
			datos.registrationStatus = true;
			var r = repository.Actualizar(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Eliminar()
		{
            var dato = _context.AdmintT007AdminTecnicas.FirstOrDefault(x => x.a007estadoRegistro != 0);

            ReqId datos = new ReqId();
			datos.id = dato?.pkT007Codigo ?? 0;
			var r = repository.Eliminar(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Crear()
		{
			TechnicalAdminReq datos = new TechnicalAdminReq();
			datos.code = 1;
			datos.name = "1";
			datos.value = "1";
			datos.description = "1";
			datos.registrationStatus = true;
			var r = repository.Crear(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarValoresTecnicos()
		{
			string parametro = "1";
			var r = repository.ConsultarValoresTecnicos(user, ipAddress, parametro);
			Assert.True(r != null);
		}
	}
}
