using API.Controllers;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace TestUnit.API.Controllers
{
    public class WeatherForecastControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly WeatherForecastController controller;

        public WeatherForecastControllerTest()
        {
            controller = new WeatherForecastController();

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
        public void IEnumerable()
        {
            var r = controller.Get();
            Assert.True(r != null);
        }
    }
}
