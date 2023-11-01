using API.Helpers;
using Repository;
using Repository.Models;
using Repository.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace TestUnit.Repository.Persistence
{
    public class QuotasReportsRepositoryTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly QuotasReportsRepository parametricas;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
        
        public QuotasReportsRepositoryTest()
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
            parametricas = new QuotasReportsRepository(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void ConsultResolutions()
        {
            string resolutionNumber = "1";
            string BussinesName = "1";
            string BussinesNit = "1";
            string fromDate = "05/10/2023 12:00:00 a. m.";
            string toDate = "05/10/2023 12:00:00 a. m.";

            var r = parametricas.ConsultResolutions(user, resolutionNumber, BussinesName, BussinesNit, fromDate, toDate);
            Assert.True(r != null);

            r = parametricas.ConsultResolutions(user, null, null, null, null, null);
            Assert.True(r != null);
        }

        [Fact]
        public void llenarObjetoCupos()
        {
            CuposV001ResolucionCupos datos = new CuposV001ResolucionCupos();
            datos.codigoCupo = 1;
            datos.autoridadEmiteResolucion = "1";
            datos.codigoZoocriadero = "1";
            datos.numeroResolucion = 1;
            datos.fechaResolucion = DateTime.Now;
            datos.fechaRegistroResolucion = DateTime.Now;
            datos.fechaRadicado = DateTime.Now;
            datos.cuposOtorgados = 1;
            datos.cuposPorAnio = 1;
            datos.fechaProduccion = DateTime.Now;
            datos.cuposAprovechamientoComercializacion = "1";
            datos.cuposTotal = 1;
            datos.cuotaRepoblacion = "1";
            datos.cuposDisponibles = 1;
            datos.observaciones = "1";
            datos.codigoEmpresa = 1;
            datos.codigoEspecie = "1";
            datos.numeroInternoFinalCuotaRepoblacion = 1;
            datos.numeroInternoFinal = 1;
            datos.NombreEspecieExportar = "1";
            datos.NumeroInternoInicial = 1;
            datos.numeroInternoInicialCuotaRepoblacion = 1;
            datos.PagoCuotaRepoblacion = "1";
            datos.NombreEmpresa = "1";
            datos.NitEmpresa = 1;
            datos.TipoEntidadEmpresa = 1;

            var r = parametricas.llenarObjetoCupos(datos);
            Assert.True(r != null);
        }
    }
}
