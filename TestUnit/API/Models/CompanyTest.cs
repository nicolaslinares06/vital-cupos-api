using DocumentFormat.OpenXml.ExtendedProperties;
using Repository.Helpers;
using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company = Repository.Helpers.Models.Company;

namespace TestUnit.API.Models
{
    public class CompanyTest
    {
        [Fact]
        public void FiltrosPrecintosMarquillasCreation()
        {
            // Arrange
            var company = new Company
            {
                Code = 1,
                ReasonSocial = "Company123",
                InitialBalanceBusiness = 1000.0m,
                FinalBalanceBusiness = 1500.0m,
            };

            // Act
            var type = Assert.IsType<Company>(company);
            Assert.NotNull(type);
        }

        [Fact]
        public void EntidadCreation()
        {
            // Arrange
            var entidad = new Entidad
            {
                codigoEmpresa = 1,
                idtipoDocumento = 2,
                tipoDocumento = "TipoDocumento123",
                idtipoEntidad = 3,
                nombreEntidad = "NombreEntidad123",
                nombreEmpresa = "NombreEmpresa123",
                nit = 1234567890,
                telefono = 987654321,
                correo = "correo@empresa.com",
                idciudad = 4,
                ciudad = "Ciudad123",
                iddepartamento = 5,
                departamento = "Departamento123",
                idpais = 6,
                pais = "Pais123",
                direccion = "Dirección123",
                matriculaMercantil = "Matricula123",
                idestado = 7,
                estado = "Estado123",
            };

            // Act
            var type = Assert.IsType<Entidad>(entidad);
            Assert.NotNull(type);
        }

        [Fact]
        public void NovedadCreation()
        {
            // Arrange
            var novedad = new Novedad
            {
                codigo = 1,
                codigoEmpresa = 2,
                idTipoNovedad = 3,
                nombreTipoNovedad = "TipoNovedad123",
                fechaRegistroNovedad = DateTime.Now,
                idEstado = 4,
                estado = "Estado123",
                idEstadoEmpresa = 5,
                estadoEmpresa = "EstadoEmpresa123",
                idEstadoEmisionCITES = 6,
                estadoEmisionCITES = "EstadoCITES123",
                observaciones = "Observaciones123",
                saldoProduccionDisponible = 100.0m,
                cuposDisponibles = 50.0m,
                inventarioDisponible = 200.0m,
                numeroCupospendientesportramitar = 10.0m,
                idDisposicionEspecimen = 7,
                idEmpresaZoo = 8,
                NitEmpresaZoo = 1234567890,
                otroCual = "Otro123",
                observacionesDetalle = "ObservacionesDetalle123",
                documentos = null,
            };

            // Act
            var type = Assert.IsType<Novedad>(novedad);
            Assert.NotNull(type);
        }
    }
}
