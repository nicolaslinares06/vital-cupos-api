using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Repository.Helpers;
using Web.Models;
using Repository.Persistence.IRepository;
using File = Repository.Helpers.Models.Files;
using API.Controllers;
using iTextSharp.text;

namespace TestUnit.API
{
    public class ParametricasControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly Parametricas parametricas;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
        private readonly ClaimsIdentity user;
        
        public ParametricasControllerTest()
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
            parametricas = new Parametricas(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void Consultar()
        {
            var r = parametricas.Consultar(user, "", ipAddress);
            Assert.IsType<Responses>(r);

            r = parametricas.Consultar(user, "", ipAddress);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarParametrica()
        {
            var r = parametricas.Consultar(user, ipAddress, "COLOR", 1);
            Assert.IsType<Responses>(r);

            r = parametricas.Consultar(user, ipAddress, null, 1);
            Assert.True(r != null);
        }

        [Fact]
        public void Crear()
        {
            ReqParametric datos = new ReqParametric();
            datos.code = 0;
            datos.name = "COLOR";
            datos.value = "MORADO";
            datos.description = "MORADO";
            datos.estate = StringHelper.estadoActivo;

            var r = parametricas.Crear(user, datos, ipAddress);
            Assert.True(r != null);

            Random rand = new Random();
            int numero = rand.Next(26);
            char letra = (char)(((int)'A') + numero);

            datos.code = 1;
            datos.name = "COLOR" + letra + "" + letra;
            datos.value = "COLOR" + letra + "" + letra;
            datos.description = "COLOR" + letra + "" + letra;
            datos.estate = StringHelper.estadoActivo;

            r = parametricas.Crear(user, datos, ipAddress);
            Assert.True(r.Message == StringHelper.msgGuardadoExitoso);
        }

        [Fact]
        public void Actualizar()
        {
            var dato = _context.AdmintT008Parametricas.FirstOrDefault(x => x.A008estadoRegistro != 0);

            Random rand = new Random();
            int numero = rand.Next(26);
            char letra = (char)(((int)'A') + numero);
            ReqParametric datos = new ReqParametric();
            datos.code = Convert.ToInt32(dato?.PkT008codigo ?? 0);
            datos.name = "COLOR" + letra + "" + letra;
            datos.value = "COLOR" + letra + "" + letra;
            datos.description = "COLOR" + letra + "" + letra;
            datos.estate = StringHelper.estadoActivo;

            var r = parametricas.Actualizar(user, datos, ipAddress);
            Assert.True(r.Message == StringHelper.msgGuardadoExitoso);
        }

        [Fact]
        public void ActivarInactivar()
        {
            var dato = _context.AdmintT008Parametricas.FirstOrDefault(x => x.A008estadoRegistro == 73);

            ReqParametric datos = new ReqParametric();
            datos.code = Convert.ToInt32(dato?.PkT008codigo ?? 0);
            datos.name = "COLOR";
            datos.value = "CAFE";
            datos.description = "CAFE";
            datos.estate = StringHelper.estadoActivo;

            var r = parametricas.ActivarInactivar(user, datos, ipAddress);
            Assert.True(r.Message == StringHelper.msgGuardadoExitoso);
        }

        [Fact]
        public void ConsultarParametros()
        {
            var r = parametricas.ConsultarParametros(user, ipAddress, "ROJO");
            Assert.IsType<Responses>(r);
        }
    }
}
