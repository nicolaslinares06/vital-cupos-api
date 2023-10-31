using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using static Repository.Helpers.Models.ActaVisitaCortesModel;
using Repository.Models;
using Repository.Helpers.Models;

namespace TestUnit.Helpers
{
    public class JwtAuthenticationManagerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        public JwtAuthenticationManager repository;
        private readonly ClaimsIdentity user;

        public JwtAuthenticationManagerTest()
        {
            var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var authenticationType = "AuthenticationTypes.Federation";

            user = new ClaimsIdentity(authenticationType);
            user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            user.AddClaim(new Claim("aud", "CUPOS"));
            user.AddClaim(new Claim("exp", "1668005030"));
            user.AddClaim(new Claim("iat", "1668004130"));
            user.AddClaim(new Claim("nbf", "1668004130"));

            repository = new JwtAuthenticationManager(key);
        }

        [Fact]
        public void generarJWT()
        {
            var r = repository.generarJWT(user);
            Assert.True(r != null);
        }

        [Fact]
        public void generarJWTCode()
        {
            string codigoUsuario = "0";
            var r = repository.generarJWT(codigoUsuario);
            Assert.True(r != null);
        }
    }
}
