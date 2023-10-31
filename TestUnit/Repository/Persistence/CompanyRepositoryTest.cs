﻿using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using Repository.Helpers.Models;
using System.Security.Claims;
using Repository.Helpers;
using Web.Models;

namespace TestUnit.API
{
    public class CompanyRepositoryTest
    {
        private readonly DBContext _context;
        private readonly Empresa empresa;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
        private readonly ClaimsIdentity user;
        

        public CompanyRepositoryTest()
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
            empresa = new Empresa(_context, jwtAuthenticationManager);
        }

        [Fact]
        public void ActualizarEmpresa_getResponses()
        {
            EntityRequest req = new EntityRequest();
            var r = empresa.Actualizar(user, req, ipAddress);
            Assert.IsType<Responses>(r);
        }

        [Fact]
        public void ActualizarEmpresa_Unauthorized()
        {
            EntityRequest req = new EntityRequest();
            var r = empresa.Actualizar(new ClaimsIdentity(), req, ipAddress);
            Assert.True(r.Message == StringHelper.msgNoAutorizado);
        }

        [Fact]
        public void ConsultarNovedades_getResponses()
        {
            var r = empresa.ConsultaNovedades(user, 1, 1);
            Assert.IsType<Responses>(r);
        }

        [Fact]
        public void ActualizarEmpresa_NotFound()
        {
            EntityRequest req = new EntityRequest();
            req.CompanyCode = 1000;
            var r = empresa.Actualizar(user, req, ipAddress);
            Assert.True(r.Message == StringHelper.msgNoEncontradoEditar);
        }

        [Fact]
        public void ActualizarEmpresa_Ok()
        {
            EntityRequest req = new EntityRequest();
            req.CompanyCode = 1;
            var result = _context.CupostT001Empresas.SingleOrDefault(x => x.PkT001codigo == req.CompanyCode);

            req.EntityTypeId = result?.A001codigoParametricaTipoEntidad ?? 0;
            req.CompanyName = result?.A001nombre ?? "";
            req.Phone = result?.A001telefono ?? 0;
            req.Address = result?.A001direccion ?? "";
            req.Email = result?.A001correo ?? "";
            req.CityId = result?.A001codigoCiudad ?? 0;

            var r = empresa.Actualizar(user, req, ipAddress);
            Assert.True(r.Message == StringHelper.msgGuardadoExitoso);
        }

        [Fact]
        public void ConsultaNovedades_Unauthorized()
        {
            var r = empresa.ConsultaNovedades(new ClaimsIdentity(), 1, 1);
            Assert.True(r.Message == StringHelper.msgNoAutorizado);
        }

        [Fact]
        public void ConsultaNovedades_Ok()
        {
            var r = empresa.ConsultaNovedades(user, 1, 1);
            Assert.IsType<List<Novedad>>(r.Response);
        }

        [Fact]
        public void GuardarNovedad_getResponses()
        {
            NoveltiesRequest datos = new NoveltiesRequest();
			datos.code = 1;
			datos.companyCode = 1;
			datos.typeOfNoveltyId = 1;
			datos.companyStatusId = 1;
			datos.CITESPermitIssuanceStatusId = 1;
			datos.noveltyRegistrationDate = DateTime.Now;
			datos.observations = "1";
			datos.availableProductionBalance = 1;
			datos.availableQuotas = 1;
			datos.availableInventory = 1;
			datos.pendingQuotasToProcess = 1;
			datos.specimenDispositionId = 1;
			datos.zooCompanyId = 1;
			datos.otherDescription = "1";
			datos.detailedObservations = "1";
			datos.documents = null;
			datos.documentsToDelete = new List<Archivo>();
            var r = empresa.RegistroNovedad(user, datos, ipAddress);
			Assert.True(r != null);
		}

        [Fact]
        public void GuardarNovedad_Unauthorized()
        {
            NoveltiesRequest req = new NoveltiesRequest();
            var r = empresa.RegistroNovedad(new ClaimsIdentity(), req, ipAddress);
            Assert.True(r.Message == StringHelper.msgNoAutorizado);
        }

        [Fact]
        public void GuardarNovedad_Ok()
        {
            NoveltiesRequest novedad = new NoveltiesRequest();
            novedad.code = 1;
            var result = _context.CupostT003Novedads.Where(x => x.PkT003codigo == novedad.code).FirstOrDefault();

            if (result != null)
            {
                novedad.companyCode = result.A003codigoEmpresa;
                novedad.companyStatusId = result.A003estadoRegistro;
                novedad.CITESPermitIssuanceStatusId = result.A003estadoEmisionPermisosCITES;
                novedad.typeOfNoveltyId = result.A003codigoParametricaTiponovedad;
                novedad.noveltyRegistrationDate = result.A003fechaRegistroNovedad;
                novedad.observations = result.A003observaciones;
                novedad.availableProductionBalance = result.A003saldoProduccionDisponible;
                novedad.availableQuotas = result.A003cuposDisponibles;
                novedad.availableInventory = result.A003inventarioDisponible;
                novedad.pendingQuotasToProcess = result.A003numeroCupospendientesportramitar;
                novedad.specimenDispositionId = result.A003codigoParametricaDisposicionEspecimen;
                novedad.zooCompanyId = result.A003codigoEmpresaTraslado;
                novedad.otherDescription = result.A003otroCual;
                novedad.detailedObservations = result.A003observacionesDetalle;

                var r = empresa.RegistroNovedad(new ClaimsIdentity(), novedad, ipAddress);

                Assert.True(r.Message == StringHelper.msgGuardadoExitoso);
            }
        }

        [Fact]
        public void GuardarNovedad_Error()
        {
            NoveltiesRequest req = new NoveltiesRequest();
            var r = empresa.RegistroNovedad(user, req, ipAddress);
            Assert.True(r.Message == StringHelper.msgIntenteNuevamente);
        }

        [Fact]
        public void EliminarDocumentoNovedad_getResponses()
        {
            var r = empresa.ElimiarDocumentoNovedad(user, 1, 1);
            Assert.IsType<Responses>(r);
        }

        [Fact]
        public void EliminarDocumentoNovedad_Unauthorized()
        {
            var r = empresa.ElimiarDocumentoNovedad(new ClaimsIdentity(), 1, 1);
            Assert.True(r.Message == StringHelper.msgNoAutorizado);
        }

        [Fact]
        public void EliminarDocumentoNovedad_Ok()
        {
            var r = empresa.ElimiarDocumentoNovedad(user, 1, 1);
            Assert.True(r.Message == "Documento Eliminado");
        }

        [Fact]
        public void ConsultarCupos_getResponses()
        {
            var r = empresa.ConsultarCupos(user, 1);
            Assert.IsType<Responses>(r);
        }

        [Fact]
        public void ConsultarCupos_Unauthorized()
        {
            var r = empresa.ConsultarCupos(new ClaimsIdentity(), 1);
            Assert.True(r.Message == StringHelper.msgNoAutorizado);
        }

        [Fact]
        public void ConsultarCupos_Ok()
        {
            var r = empresa.ConsultarCupos(user, 1);
            Assert.IsType<TotalQuotas>(r.Response);
        }
    }
}
