using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.PermitResolution;
using Web.Models;
using Repository.Helpers.Models;

namespace TestUnit.Helpers.Models
{
    public class ResolucionPecesTest
    {
        [Fact]
        public void ResolucionPermisos()
        {
            var supportDocument = new SupportDocuments
            {
                code = 1,
                base64Attachment = "Base64EncodedData",
                attachmentName = "DocumentName.pdf",
                attachmentType = "pdf",
                tempAction = "TempAction123",
            };

            // Arrange
            var resolucion = new ResolucionPermisos
            {
                resolutionCode = 1,
                companyCode = 2,
                resolutionNumber = 3,
                resolutionDate = DateTime.Now,
                startDate = DateTime.Now,
                endDate = DateTime.Now,
                attachment = supportDocument, // Debes definir SupportDocuments según tu enumeración.
                resolutionObject = "Objeto de Resolución",
            };

            var type = Assert.IsType<ResolucionPermisos>(resolucion);
            Assert.NotNull(type);
        }
    }
}
