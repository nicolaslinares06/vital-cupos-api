using API.Controllers;
using API.Helpers;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Controllers
{
    public class QuotasReportsControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly QuotasReportsController controller;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
        
        public QuotasReportsControllerTest()
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
            controller = new QuotasReportsController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<QuotasReportsController>());

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
        public void ConsultResolutions()
        {
            string resolutionNumber = "1";
            string BussinesName = "1";
            string BussinesNit = "1";
            string fromDate = "1";
            string toDate = "1";

            var r = controller.ConsultResolutions(resolutionNumber, BussinesName, BussinesNit, fromDate, toDate);
            Assert.True(r != null);

            r = controller.ConsultResolutions(null, null, null, null, null);
            Assert.True(r != null);
        }
    }
}
