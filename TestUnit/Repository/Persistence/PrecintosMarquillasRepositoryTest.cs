using API.Helpers;
using Repository;
using Repository.Helpers;
using Repository.Persistence.Repository;
using System.Security.Claims;
using System.Security.Cryptography;

namespace TestUnit.API
{
    public class PrecintosMarquillasRepositoryTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly PrecintosMarquillas precintosMarquillas;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
        private readonly ClaimsIdentity user;
        

        public PrecintosMarquillasRepositoryTest()
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
            precintosMarquillas = new PrecintosMarquillas(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void Consultar()
        {
            var tipoDocumento = "NIT";
            var fechaInicial = "2023-09-07 16:11:50.137";
            var numero = "SEP07";
            var numeroDocumento = "897564233";
            var fechaFinal = "2023-09-07 23:34:13";
            var color = "NEGRO";
            var nombreEmpresa = "Bisa COMERCIALIZADORA";
            var vigencia = "2023-09-07 22:59:03";
            var pagina = 1;

            var r = precintosMarquillas.Consultar(user, ipAddress, tipoDocumento, fechaInicial, numero, numeroDocumento,
                fechaFinal, color, nombreEmpresa, vigencia, pagina);
            Assert.IsType<Responses>(r);

            r = precintosMarquillas.Consultar(user, ipAddress, tipoDocumento, fechaInicial, numero, numeroDocumento,
                fechaFinal, color, nombreEmpresa, vigencia, pagina);
            Assert.IsType<Responses>(r);
        }

        [Fact]
        public void TipoDocumentoEmpresa()
        {
            var r = precintosMarquillas.TipoDocumentoEmpresa(user, ipAddress);
            Assert.IsType<Responses>(r);
        }

        [Fact]
        public void Color()
        {
            var r = precintosMarquillas.Color(user, ipAddress);
            Assert.IsType<Responses>(r);
        }
        
    }
}
