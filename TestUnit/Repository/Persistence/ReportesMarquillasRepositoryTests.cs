using API.Helpers;
using System.Security.Claims;
using static Repository.Helpers.Models.ReportesMarquillasModels;
using Repository.Persistence.Repository;
using System.Security.Cryptography;

namespace Repository.Persistence.Test
{
    public class ReportesMarquillasRepositoryTests
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly ReportesMarquillasRepository repository;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
        

        public ReportesMarquillasRepositoryTests()
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
            repository = new ReportesMarquillasRepository(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void ConsultarMarquillas_DevuelveUnaListaDeTagsData_CuandoLaConsultaEsCorrecta()
        {

            var dato = _context.CupostT019Solicitudes.FirstOrDefault(x => x.A019EstadoRegistro != 0);

            var filtros = new TagsFilters();
            filtros.DateFrom = DateTime.Now;
            filtros.DateTo = DateTime.Now;
            filtros.RadicationNumber = dato?.A019NumeroRadicacion ?? "";

            // Act
            var respuesta = repository.ConsultarMarquillas(user, filtros);
            Assert.True(respuesta != null);

            filtros.DateFrom = null;
            filtros.DateTo = null;
            filtros.RadicationNumber = dato?.A019NumeroRadicacion ?? "";

            // Act
            respuesta = repository.ConsultarMarquillas(user, filtros);
            Assert.True(respuesta != null);
        }

        [Fact]
        public void ConsultarMarquillas_DevuelveUnaListaVacia_CuandoNoHayResultados()
        {
            var filtros = new TagsFilters();
            filtros.DateFrom = DateTime.Now;
            filtros.DateTo = DateTime.Now;

            // Act
            var respuesta = repository.ConsultarMarquillas(user, filtros);
            Assert.True(respuesta != null);
        }
    }
}
