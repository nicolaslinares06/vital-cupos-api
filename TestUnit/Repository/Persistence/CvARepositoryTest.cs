using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace TestUnit.API
{
	public class CvARepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly Cvrepository repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;

		public CvARepositoryTest()
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
			repository = new Cvrepository(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void Buscar()
		{
			decimal documentypecv = 1;
			string documentid = "1";
			var r = repository.Buscar(user, documentypecv, documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void Situacion()
		{
			decimal documentypecv = 1;
			string documentid = "1";
			var r = repository.Situacion(user, documentypecv, documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void Resolucioncupos()
		{
			string documentid = "1";
			var r = repository.Resolucioncupos(user, documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCertificateshj()
		{
			var r = repository.ConsultCertificateshj(user);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultPeces()
		{
			decimal documentid = 1;
			var r = repository.ConsultPeces(user, documentid);
			Assert.True(r != null);
		}

		[Fact]
		public void DocumentoVenta()
		{
			var r = repository.DocumentoVenta(user, 0);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDocument2()
		{
			decimal docuid = 1;
			var r = repository.ConsultDocument2(user, docuid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultDocumentid()
		{
			decimal docuid = 1;
			var r = repository.ConsultDocumentid(user, docuid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultOneQuota2()
		{
			decimal quotaCode = 1;
			var r = repository.ConsultOneQuota2(user, quotaCode);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultCertificateshj2()
		{
			decimal idcertificado = 1;
			var r = repository.ConsultCertificateshj2(user, idcertificado);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionpdf()
		{
			decimal situacionid = 1;
			var r = repository.ConsultSituacionpdf(user, situacionid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionid()
		{
			decimal codigoEmpresa = 1;
			decimal situacionid = 1;
			var r = repository.ConsultSituacionid(user, codigoEmpresa, situacionid);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionnovedad()
		{
			decimal codigoEmpresa = 1;
			var r = repository.ConsultSituacionnovedad(user, codigoEmpresa);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultSituacionnovedadultima()
		{
			decimal codigoEmpresa = 1;
			var r = repository.ConsultSituacionnovedadultima(user, codigoEmpresa);
			Assert.True(r != null);
		}

		[Fact]
		public void Consultpecespdf()
		{
			decimal idresolucionp = 1;
			var r = repository.Consultpecespdf(user, idresolucionp);
			Assert.True(r != null);
		}

		[Fact]
		public void GetFile()
		{
            var dato = _context.AdmintT009Documentos.FirstOrDefault(x => x.A009estadoRegistro != 0);

            AdmintT009Documento datos = new AdmintT009Documento();
			datos.PkT009codigo = dato?.PkT009codigo ?? 0;
			datos.A009codigoUsuarioCreacion = 1;
			datos.A009codigoUsuarioModificacion = 1;
			datos.A009codigoParametricaTipoDocumento = 1;
			datos.A009codigoPlantilla = 1;
			datos.A009estadoRegistro = 1;
			datos.A009fechaCreacion = DateTime.Now;
			datos.A009fechaModificacion = DateTime.Now;
			datos.A009firmaDigital = "abc";
			datos.A009documento = "abc";
			datos.A009descripcion = "abc";
			datos.A009url = dato?.A009url ?? "";

			var r = repository.GetFile(datos);
			Assert.NotNull(r);
		}
		
	}
}
