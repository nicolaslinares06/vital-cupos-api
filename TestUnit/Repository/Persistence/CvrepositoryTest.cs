using API.Helpers;
using Microsoft.Extensions.Configuration;
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
    public class CvrepositoryTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly Cvrepository repository;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
        public static SupportDocuments? documentoEnviar;

        public CvrepositoryTest()
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
            repository = new Cvrepository(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void ConsultDocument2()
        {

            var dato = _context.CupostT004FacturacompraCartaventa.FirstOrDefault(x => x.A004estadoRegistro != 0);

            decimal docuid = dato?.PkT004codigo ?? 0;

            var r = repository.ConsultDocument2(user, docuid);
            Assert.True(r != null);

            docuid = 20126;
            r = repository.ConsultDocument2(user, docuid);
            Assert.True(r != null);

            docuid = 20127;
            r = repository.ConsultDocument2(user, docuid);
            Assert.True(r != null);
        }

        [Fact]
        public void Consultpecespdf()
        {
            decimal idresolucionp = 10057;
            var r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

            idresolucionp = 10059;
            r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

            idresolucionp = 10060;
            r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

            idresolucionp = 10061;
            r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

            idresolucionp = 10062;
            r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

            idresolucionp = 10065;
            r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

            idresolucionp = 10066;
            r = repository.Consultpecespdf(user, idresolucionp);
            Assert.True(r != null);

        }

        [Fact]
        public void ConsultSituacionpdf()
        {
            decimal situacionid = 47;
            var r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);

            situacionid = 46;
            r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);

            situacionid = 45;
            r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);

            situacionid = 44;
            r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);

            situacionid = 43;
            r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);

            situacionid = 42;
            r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);

            situacionid = 41;
            r = repository.ConsultSituacionpdf(user, situacionid);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultOneQuota2()
        {

            var dato = _context.CupostT002Cupos.FirstOrDefault(x => x.A002estadoCupo != null);

            decimal quotaCode = dato?.PkT002codigo ?? 0;
            var r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);

            quotaCode = 50198;
            r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);

            quotaCode = 50191;
            r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);

            quotaCode = 50197;
            r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);

            quotaCode = 50192;
            r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);

            quotaCode = 50196;
            r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);

            quotaCode = 50193;
            r = repository.ConsultOneQuota2(user, quotaCode);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultCertificateshj2()
        {

            var dato = _context.CupostT021CertificadoFloraNoMaderable.FirstOrDefault(x => x.A021EstadoRegistro != 0);

            decimal idcertificado = dato?.Pk_T021Codigo ?? 0;
            var r = repository.ConsultCertificateshj2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10061;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10060;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10059;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10058;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10057;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10056;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10055;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);

            idcertificado = 10054;
            r = repository.ConsultDocument2(user, idcertificado);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultSituacionid()
        {

            var empresa = _context.CupostT001Empresas.FirstOrDefault(x => x.A001estadoRegistro != 0);
            var situacion = _context.CupostT003Novedads.FirstOrDefault(x => x.A003estadoRegistro != 0);

            var r = repository.ConsultSituacionid(user, empresa?.PkT001codigo ?? 0, situacion?.PkT003codigo ?? 0);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultSituacionnovedadultima()
        {

            var empresa = _context.CupostT001Empresas.FirstOrDefault(x => x.A001estadoRegistro != 0);

            var r = repository.ConsultSituacionnovedadultima(user, empresa?.PkT001codigo ?? 0);
            Assert.True(r != null);
        }

        [Fact]
        public void GetFile()
        {

            var documento = _context.AdmintT009Documentos.FirstOrDefault(x => x.A009estadoRegistro != 0);

            var r = repository.GetFile(documento ?? new AdmintT009Documento());
            Assert.True(r != null);
        }
    }
}
