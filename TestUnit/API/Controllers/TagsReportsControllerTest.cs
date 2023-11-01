using API.Controllers;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Persistence.IRepository;
using System.Security.Claims;
using System.Security.Cryptography;
using Web.Models;
using static Repository.Helpers.Models.ReportesMarquillasModels;
using static Repository.Helpers.Models.ReportesPrecintosModels;

namespace TestUnit.API.Controllers
{
    public class TagsReportsControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly TagsReportsController controller;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
        
        public TagsReportsControllerTest()
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
            controller = new TagsReportsController(_context, jwtAuthenticationManager);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Administrador")
                    }, "someAuthTypeName"))
                }
            };
        }

        [Fact]
        public void ConsultarDatosPrecintos()
        {

            var datos = new TagsFilters
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                RadicationNumber = ""
            };

            // Act
            var result = controller.ConsultarDatosMarquillas(datos);

            // Assert
            Assert.NotNull(result);
            // Add more assertions as needed
        }

    }
}
