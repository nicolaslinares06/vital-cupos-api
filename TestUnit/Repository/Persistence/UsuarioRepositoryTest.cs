using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Web.Models;

namespace TestUnit.API
{
	public class UsuarioRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Usuario repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public static SupportDocuments? documentoEnviar;

		public UsuarioRepositoryTest()
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
			repository = new Usuario(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Authenticate()
		{
			ReqLogin datos = new ReqLogin();
			datos.user = "1";
			datos.password = "1";
			var r = repository.Authenticate(datos, ipAddress);
			Assert.True(r != null);

            datos.user = "Administrador";
            datos.password = "123456";

            r = repository.Authenticate(datos, ipAddress);
            Assert.True(r != null);

            datos.user = "Adminisrador";
            datos.password = "12356";

            r = repository.Authenticate(datos, ipAddress);
            Assert.True(r != null);

            datos.user = "Adminisrador";
            datos.password = "12356";

            r = repository.Authenticate(datos, ipAddress);
            Assert.True(r != null);

            datos.user = "Adminisrador";
            datos.password = "12356";

            r = repository.Authenticate(datos, ipAddress);
            Assert.True(r != null);

            datos.user = "Adminisrador";
            datos.password = "12356";

            r = repository.Authenticate(datos, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void CambiaContrasena()
		{
			ReqChangePassword datos = new ReqChangePassword();
			datos.user = "Administrador";
			datos.password = "123456";
			datos.newPassword = "123456";
			datos.acceptsTerms = true;
			datos.acceptsProcessingPersonalData = true;
			var r = repository.CambiaContrasena(datos, ipAddress);
			Assert.True(r != null);

            datos.user = "fgfd";
            datos.password = "123456";
            r = repository.CambiaContrasena(datos, ipAddress);
            Assert.True(r != null);
			
			datos.user = "AnalistaFlora";
            datos.password = "123456";
            datos.acceptsTerms = false;
            datos.acceptsProcessingPersonalData = false;
            r = repository.CambiaContrasena(datos, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void enviaEmailRecuperarContrasena()
		{
			ReqSimpleUser datos = new ReqSimpleUser();
			datos.user = "Administrador";
			var r = repository.enviaEmailRecuperarContrasena(datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Consultar()
		{
			string CorreoElectronico = "carolinabisa830@gmail.com";
			var r = repository.Consultar(user, CorreoElectronico, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarTodos()
		{
			string cadenaBusqueda = "1";
			var r = repository.ConsultarTodos(user, ipAddress, cadenaBusqueda);
			Assert.True(r != null);
		}

		[Fact]
		public void Create()
		{
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);

            ReqUser datos = new ReqUser();
			datos.code = 20081;
			datos.cityAddress = numeroAleatorio;
			datos.codeParametricDocumentType = numeroAleatorio;
			datos.codeParametricUserType = numeroAleatorio;
			datos.dependence = "1" + numeroAleatorio;
			datos.acceptsTerms = true;
			datos.acceptsProcessingPersonalData = true;
			datos.identification = numeroAleatorio;
			datos.firstName = "1" + numeroAleatorio;
			datos.secondName = "1" + numeroAleatorio;
			datos.firstLastName = "1" + numeroAleatorio;
			datos.SecondLastName = "1" + numeroAleatorio;
			datos.login = "1" + numeroAleatorio;
			datos.address = "1" + numeroAleatorio;
			datos.phone = numeroAleatorio;
			datos.email = "correo@ejemplo.com" + numeroAleatorio;
			datos.celular = "1" + numeroAleatorio;
			datos.password = "1" + numeroAleatorio;
			datos.digitalSignature = "1" + numeroAleatorio;
			datos.contractStartDate = DateTime.Now;
			datos.contractFinishDate = DateTime.Now;
			datos.registrationStatus = true;
			datos.rol = "1"+ numeroAleatorio;
			var r = repository.Create(user, datos, ipAddress);
			Assert.True(r != null);

            datos.acceptsTerms = false;
            r = repository.Create(user, datos, ipAddress);
            Assert.True(r != null);

            datos.email = "correo";
            r = repository.Create(user, datos, ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void Actualizar()
		{
            var usuario = _context.AdmintT015RlUsuarioRols.FirstOrDefault(x => x.A015estadoRegistro != 0);
            var ciudad = _context.AdmintT004Ciudads.FirstOrDefault(x => x.A004estadoRegistro != 0);
            var documento = _context.AdmintT008Parametricas.FirstOrDefault(x => x.A008parametrica == "TIPO DE DOCUMENTO");
            var tipo = _context.AdmintT008Parametricas.FirstOrDefault(x => x.A008parametrica == "TIPO DE USUARIO");

            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);

            ReqUser datos = new ReqUser();
			datos.code = usuario?.A015codigoUsuario ?? 0;
			datos.cityAddress = ciudad?.PkT004codigo ?? 0;
			datos.codeParametricDocumentType = documento?.PkT008codigo ?? 0;
			datos.codeParametricUserType = tipo?.PkT008codigo ?? 0;
			datos.dependence = "1" + numeroAleatorio;
			datos.acceptsTerms = true;
			datos.acceptsProcessingPersonalData = true;
			datos.identification = numeroAleatorio;
			datos.firstName = "1" + numeroAleatorio;
			datos.secondName = "1" + numeroAleatorio;
			datos.firstLastName = "1" + numeroAleatorio;
			datos.SecondLastName = "1" + numeroAleatorio;
			datos.login = "1" + numeroAleatorio;
			datos.address = "1" + numeroAleatorio;
			datos.phone = numeroAleatorio;
			datos.email = "correo@ejemplo.com" + numeroAleatorio;
			datos.celular = "11231" + numeroAleatorio;
			datos.password = "1" + numeroAleatorio;
			datos.digitalSignature = "1" + numeroAleatorio;
			datos.contractStartDate = DateTime.Now;
			datos.contractFinishDate = DateTime.Now;
			datos.registrationStatus = true;
			datos.rol = "1"+ numeroAleatorio;
			var r = repository.Actualizar(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarEdit()
		{
			decimal id = 1;
			var r = repository.ConsultarEdit(user, ipAddress, id);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarTerminos()
		{
			string? login = "1";
			var r = repository.ConsultarTerminos(login, ipAddress);
			Assert.True(r != null);
		}
	}
}
