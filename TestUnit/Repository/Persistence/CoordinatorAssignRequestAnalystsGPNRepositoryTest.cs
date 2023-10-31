using API.Helpers;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Repository.Helpers;
using static Repository.Helpers.Models.ModelsAppSettings;
using Microsoft.Extensions.Options;
using static Repository.Helpers.Models.PaginatioModels;

namespace Repository.Persistence.Repository
{
	public class CoordinatorAssignRequestAnalystsGPNRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly CoordinatorAssignRequestAnalystsGpnRepository repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		public static SupportDocuments? documentoEnviar;
        public readonly IGenericsMethodsHelper genericsMethodsHelper;

        public class FakeEstadosCuposSettings : IOptions<EstadosCuposSettings>
        {
            public EstadosCuposSettings Value { get; }

            public FakeEstadosCuposSettings()
            {
                Value = new EstadosCuposSettings
                {
                    Enviada = new EstadoCupo { IdEstado = 60357, ValorEstado = "ENVIADA" },
                    Evaluacion = new EstadoCupo { IdEstado = 60358, ValorEstado = "EVALUACIÓN" },
                    EnEstudio = new EstadoCupo { IdEstado = 60359, ValorEstado = "EN ESTUDIO" },
                    PreAprobado = new EstadoCupo { IdEstado = 60360, ValorEstado = "PRE-APROBADO" },
                    PreNegado = new EstadoCupo { IdEstado = 60361, ValorEstado = "PRE-NEGADO" },
                    Aprobado = new EstadoCupo { IdEstado = 60362, ValorEstado = "APROBADO" },
                    Negado = new EstadoCupo { IdEstado = 60363, ValorEstado = "NEGADO" },
                    Desistido = new EstadoCupo { IdEstado = 60364, ValorEstado = "DESISTIDO" },
                    EnRequerimiento = new EstadoCupo { IdEstado = 60365, ValorEstado = "EN REQUERIMIENTO" },
                    Radicada = new EstadoCupo { IdEstado = 60366, ValorEstado = "RADICADA" },
                    AprobadoParaFirma = new EstadoCupo { IdEstado = 60367, ValorEstado = "APROBADO PARA FIRMA" }
                };
            }
        }

        public CoordinatorAssignRequestAnalystsGPNRepositoryTest()
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
            var fakeEstadosCuposSettings = new FakeEstadosCuposSettings();
            repository = new CoordinatorAssignRequestAnalystsGpnRepository(_context, jwtAuthenticationManager, genericsMethodsHelper ?? new GenericsMethodsHelper(_context), fakeEstadosCuposSettings);
        }

        [Fact]
		public void ConsultarSolicitudes()
		{
			string tipoEvaluacion = "APROBADA";
			var r = repository.ConsultarSolicitudes(user, tipoEvaluacion);
			Assert.True(r != null);

            tipoEvaluacion = "ENVIADA";
            r = repository.ConsultarSolicitudes(user, tipoEvaluacion);
            Assert.True(r != null);

            r = repository.ConsultarSolicitudes(new ClaimsIdentity(), tipoEvaluacion);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarSolicitudesPagination()
        {
            // Arrange
            var paramsPaginations = new ParamsPaginations();

            // Act
            paramsPaginations.QuantityRecords = 10;
            paramsPaginations.QuantityPages = 2;
            paramsPaginations.TotalQuantity = 20;
            paramsPaginations.PageNumber = 1;
            paramsPaginations.QuantityRecordsForpage = 15;
            paramsPaginations.FilterCriterium = "SampleFilter";

            decimal tipoEvaluacion = 10166;
            var r = repository.ConsultarSolicitudesPagination(new ClaimsIdentity(), tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);

            r = repository.ConsultarSolicitudesPagination(user, tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);

            tipoEvaluacion = 1;
            r = repository.ConsultarSolicitudesPagination(user, tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarSolicitudesPorFiltro()
        {
            // Arrange
            var paramsPaginations = new ParamsPaginations();

            // Act
            paramsPaginations.QuantityRecords = 10;
            paramsPaginations.QuantityPages = 2;
            paramsPaginations.TotalQuantity = 20;
            paramsPaginations.PageNumber = 1;
            paramsPaginations.QuantityRecordsForpage = 15;
            paramsPaginations.FilterCriterium = "SampleFilter";

            decimal tipoEvaluacion = 10166;
            var r = repository.ConsultarSolicitudesPorFiltro(new ClaimsIdentity(), tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);

            r = repository.ConsultarSolicitudesPorFiltro(user, tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);

            tipoEvaluacion = 1;
            r = repository.ConsultarSolicitudesPorFiltro(user, tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarSolicitudesByFilter()
        {
            // Arrange
            var paramsPaginations = new ParamsPaginations();

            // Act
            paramsPaginations.QuantityRecords = 10;
            paramsPaginations.QuantityPages = 2;
            paramsPaginations.TotalQuantity = 20;
            paramsPaginations.PageNumber = 1;
            paramsPaginations.QuantityRecordsForpage = 15;
            paramsPaginations.FilterCriterium = "SampleFilter";

            decimal tipoEvaluacion = 10166;
            var r = repository.ConsultarSolicitudesByFilter(new ClaimsIdentity(), tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);

            r = repository.ConsultarSolicitudesByFilter(user, tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);

            tipoEvaluacion = 1;
            r = repository.ConsultarSolicitudesByFilter(user, tipoEvaluacion, paramsPaginations);
            Assert.True(r != null);
        }

        [Fact]
		public void ConsultarAnalistas()
		{
			decimal codigoSolicitud = 1;
			var r = repository.ConsultarAnalistas(user, codigoSolicitud);
			Assert.True(r != null);

            r = repository.ConsultarAnalistas(new ClaimsIdentity(), codigoSolicitud);
            Assert.True(r != null);
        }
		
		[Fact]
		public void ConsultarDatosSolicitudCupo()
		{
			decimal codigoSolicitud = 1;
			var r = repository.ConsultarDatosSolicitudCupo(user, codigoSolicitud);
			Assert.True(r != null);

            r = repository.ConsultarDatosSolicitudCupo(new ClaimsIdentity(), codigoSolicitud);
            Assert.True(r != null);
        }

		[Fact]
		public void ConsultarDatosDesistimientoSolicitud()
		{
			decimal codigoSolicitud = 1;
			var r = repository.ConsultarDatosDesistimientoSolicitud(user, codigoSolicitud);
			Assert.True(r != null);

            r = repository.ConsultarDatosDesistimientoSolicitud(new ClaimsIdentity(), codigoSolicitud);
            Assert.True(r != null);
        }

		[Fact]
		public void ConsultarDatosSolicitud()
		{
			decimal codigoSolicitud = 1;
			var r = repository.ConsultarDatosSolicitud(user, codigoSolicitud);
			Assert.True(r != null);

            r = repository.ConsultarDatosSolicitud(new ClaimsIdentity(), codigoSolicitud);
            Assert.True(r != null);
        }

		[Fact]
		public void ConsultarDocumentoSolicitud()
		{
			decimal codigoSolicitud = 1;
			decimal tipoDocumento = 1;
			var r = repository.ConsultarDocumentoSolicitud(user, codigoSolicitud, tipoDocumento);
			Assert.True(r != null);

            r = repository.ConsultarDocumentoSolicitud(new ClaimsIdentity(), codigoSolicitud, tipoDocumento);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarDocumentosSolicitud()
        {
            var solicitud = _context.CupostT020RlSolicitudesDocumento.FirstOrDefault(x => x.A020EstadoRegistro != 0);
            var documento = _context.AdmintT009Documentos.FirstOrDefault(x => x.A009estadoRegistro != 0);

            decimal codigoSolicitud = solicitud?.Pk_T020Codigo ?? 0;
            decimal tipoDocumento = documento?.A009codigoParametricaTipoDocumento ?? 0;

            var r = repository.ConsultarDocumentosSolicitud(user, codigoSolicitud, tipoDocumento);
            Assert.True(r != null);

            r = repository.ConsultarDocumentosSolicitud(new ClaimsIdentity(), codigoSolicitud, tipoDocumento);
            Assert.True(r != null);
        }

        [Fact]
		public void ConsultarAnalistaSolicitudAsignado()
		{
			decimal codigoSolicitud = 1;
			var r = repository.ConsultarAnalistaSolicitudAsignado(user, codigoSolicitud);
			Assert.True(r != null);

            r = repository.ConsultarAnalistaSolicitudAsignado(new ClaimsIdentity(), codigoSolicitud);
            Assert.True(r != null);
        }

		[Fact]
		public void ActualizarIdAnalistaSolicitud()
		{
            var solicitud = _context.CupostT019Solicitudes.FirstOrDefault(x => x.A019EstadoRegistro != 0);

            decimal codigoSolicitud = solicitud?.Pk_T019Codigo ?? 0;
			decimal idAnalista = 1;

			var r = repository.ActualizarIdAnalistaSolicitud(user, codigoSolicitud, idAnalista);
			Assert.True(r != null);

            r = repository.ActualizarIdAnalistaSolicitud(new ClaimsIdentity(), codigoSolicitud, idAnalista);
            Assert.True(r != null);
        }

		[Fact]
		public void ActualizarEstadoSolicitud()
		{
            var solicitud = _context.CupostT019Solicitudes.FirstOrDefault(x => x.A019EstadoRegistro != 0);

            decimal codigoSolicitud = solicitud?.Pk_T019Codigo ?? 0;
            string tipoEstado = "1";

			var r = repository.ActualizarEstadoSolicitud(user, codigoSolicitud, tipoEstado);
			Assert.True(r != null);

            r = repository.ActualizarEstadoSolicitud(new ClaimsIdentity(), codigoSolicitud, tipoEstado);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarNumeracionesSolicitud()
        {
            var solicitud = _context.CupostT019Solicitudes.FirstOrDefault(x => x.A019EstadoRegistro != 0);

            decimal codigoSolicitud = solicitud?.Pk_T019Codigo ?? 0;
            var r = repository.ConsultarNumeracionesSolicitud(user, codigoSolicitud);
            Assert.True(r != null);

            codigoSolicitud = 1654556;
            r = repository.ConsultarNumeracionesSolicitud(user, codigoSolicitud);
            Assert.True(r != null);

            r = repository.ConsultarNumeracionesSolicitud(new ClaimsIdentity(), codigoSolicitud);
            Assert.True(r != null);
        }
    }

}
