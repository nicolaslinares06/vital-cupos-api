using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Security.Claims;
using Xunit;
using static Repository.Helpers.Models.ReportesEmpresasMarcajeModels;

namespace TestUnit.API.Controllers
{
    public class CompanyCheckInsReportsControllerTests
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly CompanyCheckInsReportsController controller;
        readonly IReportesEmpresasMarcaje companyCheckInsReportsController;

        public CompanyCheckInsReportsControllerTests()
        {
            controller = new CompanyCheckInsReportsController(companyCheckInsReportsController, new LoggerFactory().CreateLogger<CompanyCheckInsReportsController>());

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
        public async Task ConsultarDatosCupoEmpresas_ShouldReturnOk()
        {
            // Arrange
            BusinessFilters datos = new BusinessFilters();
            datos.BusinessType = 1;
            datos.CompanyName = "1";
            datos.NIT = 1;
            datos.Status = 1;
            datos.CITESIssuanceStatus = 1;
            datos.ResolutionNumber = 1;
            datos.ResolutionIssuanceStartDate = DateTime.Now;
            datos.ResolutionIssuanceEndDate = DateTime.Now;
            datos.SpecificSearch = 1;

            // Act
            var result = await controller.ConsultarDatosCupoEmpresas(datos);

            // Assert
            Assert.True(result != null);
        }

        [Fact]
        public async Task ConsultarDatosCupoEmpresasShouldThrowException()
        {
            // Arrange
            BusinessFilters datos = new BusinessFilters();
            datos.BusinessType = 1;
            datos.CompanyName = "1";
            datos.NIT = 1;
            datos.Status = 1;
            datos.CITESIssuanceStatus = 1;
            datos.ResolutionNumber = 1;
            datos.ResolutionIssuanceStartDate = DateTime.Now;
            datos.ResolutionIssuanceEndDate = DateTime.Now;
            datos.SpecificSearch = 1;

            // Act
            var result = await controller.ConsultarDatosCupoEmpresas(datos);

            // Assert
            Assert.True(result != null);
        }


        [Fact]
        public void ConsultarDatosReporteEmpresa()
        {
            var r = controller.ConsultarDatosReporteEmpresa();
            Assert.True(r != null);
        }
    }
}