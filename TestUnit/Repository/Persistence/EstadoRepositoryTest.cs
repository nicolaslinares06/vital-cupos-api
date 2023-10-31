using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Repository.Helpers.Models;

namespace TestUnit.API
{
	public class EstadoRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Estado repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		public static SupportDocuments? documentoEnviar;

		public EstadoRepositoryTest()
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
			repository = new Estado(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Consultar()
		{
			string nombre = "1"; 
			string estadoReg = "1"; 
			int codigoEstado = 1;
			var r = repository.Consultar(user, nombre, estadoReg, codigoEstado, ipAddress);
			Assert.True(r != null);

            nombre = "";
            estadoReg = "1";
            codigoEstado = 1;
            r = repository.Consultar(user, nombre, estadoReg, codigoEstado, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void Actualizar()
		{

            ReqEstado datos = new ReqEstado();
			datos.id = 61;
			datos.position = 1;
			datos.idEstate = "ACTIVO";
			datos.description = "1";
			datos.stage = "ACTIVO";
			datos.estate = true;

			var r = repository.Actualizar(user, datos, ipAddress);
			Assert.True(r != null);

            r = repository.Actualizar(user, new ReqEstado(), ipAddress);
            Assert.True(r != null);

            datos.id = 42;
            r = repository.Actualizar(user, datos, ipAddress);
            Assert.True(r != null);

            datos.id = 42;
            datos.position = 1;
            r = repository.Actualizar(user, datos, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void Eliminar()
		{
            var dato = _context.CitestT008Estados.FirstOrDefault(x => x.A008estadoRegistro != 0);

            ReqId datos = new ReqId();
			datos.id = dato?.PkT008codigo ?? 0;

			var r = repository.Eliminar(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Crear()
		{
			AdminStatesReq datos = new AdminStatesReq();
			datos.stage = "1";
			datos.description = "1";
			datos.state = 1;

			var r = repository.Crear(user, datos, ipAddress);
			Assert.True(r != null);
		}
	}
}
