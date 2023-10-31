using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Repository.Helpers.Models;
using Repository.Helpers;

namespace TestUnit.API
{
	public class EmpresaRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Empresa repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		

		public EmpresaRepositoryTest()
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
			repository = new Empresa(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Actualizar()
		{
			EntityRequest datos = new EntityRequest();
			datos.CompanyCode = 1;
			datos.DocumentTypeId = 1;
			datos.EntityTypeId = 1;
			datos.CompanyName = "1";
			datos.NIT = 1;
			datos.CityId = 1;
			datos.Address = "1";
			datos.Phone = 1;
			datos.Email = "1";
			datos.BusinessRegistration = "1";

			var r = repository.Actualizar(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultaNovedades()
		{
            var empresa = _context.CupostT001Empresas.FirstOrDefault(x => x.A001estadoRegistro != 0);
            var noveda = _context.CupostT003Novedads.FirstOrDefault(x => x.A003estadoRegistro != 0);

            decimal codigoEmpresa = empresa?.PkT001codigo ?? 0;
			decimal? idNovedad = noveda?.PkT003codigo ?? 0;
			var r = repository.ConsultaNovedades(user, codigoEmpresa, idNovedad);
			Assert.True(r != null);
		}

		[Fact]
		public void RegistroNovedad()
		{
            var dato = _context.CupostT003Novedads.FirstOrDefault(x => x.A003estadoRegistro != 0);

            NoveltiesRequest datos = new NoveltiesRequest();
			datos.code = dato?.PkT003codigo ?? 0;
			datos.companyCode = 1;
			datos.typeOfNoveltyId = 1;
			datos.companyStatusId = 1;
			datos.CITESPermitIssuanceStatusId = 1;
			datos.noveltyRegistrationDate = DateTime.Now;
			datos.observations = "1";
			datos.availableProductionBalance = 1;
			datos.availableQuotas = 1;
			datos.availableInventory = 1;
			datos.pendingQuotasToProcess = 1;
			datos.specimenDispositionId = 1;
			datos.zooCompanyId = 1;
			datos.otherDescription = "1";
			datos.detailedObservations = "1";
			datos.documents = null;
			datos.documentsToDelete = new List<Archivo>();

            var r = repository.RegistroNovedad(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ElimiarDocumentoNovedad()
		{
            var dato = _context.CupostT012RlNovedadDocumentos.FirstOrDefault(x => x.A012estadoRegistro != 0);

            decimal idNovedad = dato?.A012codigoNovedad ?? 0; 
			decimal idArchivo = dato?.A012codigoDocumento ?? 0;

			var r = repository.ElimiarDocumentoNovedad(user, idNovedad, idArchivo);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarCupos()
		{
			decimal idEmpresa = 1;
			var r = repository.ConsultarCupos(user, idEmpresa);
			Assert.True(r != null);
		}
	}
}
