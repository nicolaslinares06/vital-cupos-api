using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using WebServices.Controllers;
using Repository.Helpers.Models;
using static Repository.Helpers.Models.ActaVisitaCortesModel;
using System;

namespace TestUnit.WebService.Controllers
{
    public class VitalControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly VitalController controller;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;

        public VitalControllerTest()
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
            controller = new VitalController(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void Authenticate()
        {
            ReqLogin datos = new ReqLogin();
            datos.user = "Administrador";
            datos.password = "123456";

            var r = controller.Authenticate(datos);
            Assert.True(r != null);

            datos.user = "123";
            datos.password = "123";

            r = controller.Authenticate(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void AuthUsers()
        {
            ReqLogin datos = new ReqLogin();
            datos.user = "26032011";
            datos.password = "contraseña_esperada";

            var r = controller.AuthUsers(datos);
            Assert.True(r != null);

            datos.password = "1";

            r = controller.AuthUsers(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void AuthenticationResult()
        {
            AuthenticationResult datos = new AuthenticationResult();
            datos.Code = "";
            datos.Status = "";
            datos.Permissions = "";
            datos.ID = 1;
            datos.User = "";
            datos.Name = "";
            datos.Document = "";
            datos.EMail = "";
            datos.LastLogin = DateTime.Now;
            datos.Active = "";
            datos.Enabled = "";
            datos.Module = "";
            datos.Url = "";
            datos.Token =  Guid.NewGuid(); 
            datos.UrlError = "";
            

            var type = Assert.IsType<AuthenticationResult>(datos);
            Assert.NotNull(type);
        }
    }
}
