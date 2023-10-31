using Microsoft.AspNetCore.Mvc;
using WebServices.Controllers;
using Xunit;
using static WebServices.Controllers.ConsultCitesPermitController;

namespace TestUnit.API.Controllers
{
    public class ConsultCitesPermitControllerTests
    {
        private readonly ConsultCitesPermitController controller;

        public ConsultCitesPermitControllerTests()
        {
            controller = new ConsultCitesPermitController();
        }

        [Fact]
        public void ConsultCitesPermit_ShouldReturnPermitData()
        {
            // Arrange
            var salvoconductoNumber = "ABC123";
            var identificationNumber = 123456789;

            // Act
            var result = controller.ConsultCitesPermit(salvoconductoNumber, identificationNumber);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            var permitData = ((JsonResult)result).Value as PermitData;
            Assert.NotNull(permitData);
            Assert.Equal(salvoconductoNumber, permitData.SalvoconductoNumber);
            Assert.Equal(identificationNumber, permitData.IdentificationNumber);
        }

        [Fact]
        public void ConsultCitesPermit_ShouldReturnNotFound()
        {
            // Arrange
            var salvoconductoNumber = "XYZ789";
            var identificationNumber = 987654321;

            // Act
            var result = controller.ConsultCitesPermit(salvoconductoNumber, identificationNumber);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}