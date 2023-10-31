using API.Helpers;
using Repository;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Security.Claims;
using System.Security.Cryptography;

namespace TestUnit.API
{
    public class PrecintosMarquillasControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly PrecintosMarquillas precintosMarquillas;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
        private readonly ClaimsIdentity user;
        

        public PrecintosMarquillasControllerTest()
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
            var fechaInicial = "2022-11-20";
            var numero = "0";
            var numeroDocumento = "0";
            var fechaFinal = "2022-11-24";
            
            var r = precintosMarquillas.Consultar(user, ipAddress, null, fechaInicial, numero, numeroDocumento,
                fechaFinal, null, null, null, null);
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
