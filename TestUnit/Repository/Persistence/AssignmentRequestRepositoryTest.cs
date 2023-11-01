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
	public class AssignmentRequestRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly AssignmentRequest repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public AssignmentRequestRepositoryTest()
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
			repository = new AssignmentRequest(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Consultar()
		{
			string nombreUsuario = "1";
			var r = repository.Consultar(user, nombreUsuario, ipAddress);
			Assert.True(r != null);

            r = repository.Consultar(user, "", ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void ConsultarEstados()
		{
			var r = repository.ConsultarEstados(user, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Actualizar()
		{
            var dato = _context.AdmintT012Usuarios.FirstOrDefault(x => x.A012estadoRegistro != 0);
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);

            ReqAssignment datos = new ReqAssignment();
			datos.id = dato?.PkT012codigo ?? 0;
			datos.identification = numeroAleatorio;
			datos.firstName = "Carolina" + numeroAleatorio;
			datos.secondName = "Carolina" + numeroAleatorio;
			datos.firstLastname = "Delgado" + numeroAleatorio;
			datos.secondLastname = "Delgado" + numeroAleatorio;
			datos.rolId = "1";
			datos.estate = 72;

			var r = repository.Actualizar(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ActualizarEstado()
		{
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);
            var dato = _context.AdmintT015RlUsuarioRols.FirstOrDefault(x => x.A015estadoSolicitud != null);

            ReqAssignmentUpdate datos = new ReqAssignmentUpdate();
			datos.id = dato?.PkT0015codigo ?? 0;
			datos.statusRequest = "" + numeroAleatorio;

			var r = repository.ActualizarEstado(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void Eliminar()
		{
            var dato = _context.AdmintT015RlUsuarioRols.FirstOrDefault(x => x.A015codigoUsuario != 0);

            ReqId datos = new ReqId();
			datos.id = dato?.A015codigoUsuario ?? 0;

			var r = repository.Eliminar(user, datos, ipAddress);
			Assert.True(r != null);
		}

        [Fact]
        public void Assign()
        {
            var dato = _context.AdmintT011Rols.FirstOrDefault(x => x.A011estadoRegistro != 0);

            var datos = new VitalReq
            {
                code = 1.23m,
                status = "Activo",
                permissions = "PermisosEjemplo",
                message = "MensajeEjemplo",
                ID = 123.45m,
                User = "UsuarioEjemplo",
                Name = "Nombre Ejemplo",
                Document = 67890.12m,
                EMail = "correo@ejemplo.com",
                LastLogin = DateTime.Now,
                Active = "Sí",
                Enabled = "Sí",
                Module = "MóduloEjemplo",
                Url = "URLDeEjemplo",
                Token = "TokenEjemplo",
                UrlError = "URLDeErrorEjemplo",
                rol = dato?.PkT011codigo ?? 0
            };

            var r = repository.Assign(user, datos, ipAddress);
            Assert.True(r != null);
        }
    }
}
