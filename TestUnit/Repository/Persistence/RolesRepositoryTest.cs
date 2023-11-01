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
	public class RolesRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Roles repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public RolesRepositoryTest()
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
			repository = new Roles(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void ConsultarTodos()
		{
			string? cadenaBusqueda = "1";
			string? estado = "1";
			var r = repository.ConsultarTodos(user, ipAddress, cadenaBusqueda, estado);
			Assert.True(r != null);

            r = repository.ConsultarTodos(user, ipAddress, null, estado);
            Assert.True(r != null);

            r = repository.ConsultarTodos(user, ipAddress, cadenaBusqueda, null);
            Assert.True(r != null);

            r = repository.ConsultarTodos(user, ipAddress, null, null);
            Assert.True(r != null);
        }

		[Fact]
		public void ConsultarRoles()
		{
			var r = repository.ConsultarRoles(user, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Consultar()
		{
			int rol = 1;
			string cargo = "1";
			bool estado = true;
			var r = repository.Consultar(user, rol, cargo, estado, ipAddress);
			Assert.True(r != null);

            r = repository.Consultar(user, 0, cargo, estado, ipAddress);
            Assert.True(r != null);

            var dato = _context.AdmintT011Rols.FirstOrDefault(x => x.A011estadoRegistro != 0);

            r = repository.Consultar(user, Convert.ToInt32(dato?.PkT011codigo ?? 0), cargo, estado, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void ActualizarFuncionalidades()
		{
            var dato = _context.AdmintT011Rols.FirstOrDefault(x => x.A011estadoRegistro != 0);

            RolModPermition datos = new RolModPermition();
			datos.rolId = Convert.ToInt32(dato?.PkT011codigo ?? 0);
			datos.moduleId = 6;
			datos.consult = true;
			datos.create = true;
			datos.update = true;
			datos.delete = true;
			datos.see = true;
			datos.name = "1";
			var r = repository.ActualizarFuncionalidades(user, datos, ipAddress);
			Assert.True(r != null);

            datos.rolId = Convert.ToInt32(dato?.PkT011codigo ?? 0);
            datos.moduleId = 5;
            datos.consult = false;
            datos.create = false;
            datos.update = false;
            datos.delete = false;
            datos.see = false;
            datos.name = "2";
            r = repository.ActualizarFuncionalidades(user, datos, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void Crear()
		{
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);

            ReqRol datos = new ReqRol();
			datos.rolId = 1;
			datos.position = "1";
			datos.description = "1";
			datos.estate = true;
			datos.name = "1" + numeroAleatorio;
			var r = repository.Crear(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Actualizar()
		{

            var dato = _context.AdmintT011Rols.FirstOrDefault(x => x.A011estadoRegistro != 0);
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);

            ReqRol datos = new ReqRol();
			datos.rolId = Convert.ToInt32(dato?.PkT011codigo ?? 0);
			datos.position = "1" + numeroAleatorio;
			datos.description = "1" + numeroAleatorio;
			datos.estate = true;
			datos.name = "1" + numeroAleatorio;
			var r = repository.Actualizar(user, datos, ipAddress);
			Assert.True(r != null);
		}
	}
}
