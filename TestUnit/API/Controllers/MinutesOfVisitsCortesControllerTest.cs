using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Controllers
{
    public class MinutesOfVisitsCortesControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly MinutesOfVisitsCortesController controller;
        readonly IActaVisitasCortesRepository actaVisitasCortesRepository;

        public MinutesOfVisitsCortesControllerTest()
        {
            controller = new MinutesOfVisitsCortesController(actaVisitasCortesRepository, new LoggerFactory().CreateLogger<MinutesOfVisitsCortesController>());

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
        public void CrearActaVisitaIdentificable()
        {
            var documento = new VisitReportDocument
            {
                Code = 123.45m,
                FileName = "DocumentoVisita.pdf",
                Base64String = "Base64StringEjemplo",
                FileType = "pdf"
            };

            var datos = new VisitCutsRegistration
            {
                VisitNumber = 1.0m,
                VisitNumber1 = true,
                VisitNumber2 = false,
                EstablishmentType = 2,
                EstablishmentID = 345.67m,
                QuantityOfSkinToCut = 10,
                IdentificationSeal = 6789,
                SkinStatus = "Estado de piel de ejemplo",
                CitesAuthorityOfficer = 0.0m,
                RepresentativeDocument = 456.78m,
                EstablishmentRepresentative = "Representante de Establecimiento Ejemplo",
                City = 789.01m,
                Date = DateTime.Now,
                ExcelSealFile = documento
            };


            var r = controller.CrearActaVisitaIdentificable(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void EditarActaVisita()
        {
            var documento = new VisitReportDocument
            {
                Code = 123.45m,
                FileName = "DocumentoVisita.pdf",
                Base64String = "Base64StringEjemplo",
                FileType = "pdf"
            };

            var datos = new EditVisitReportAct
            {
                VisitReportId = 1.0m,
                VisitNumber = 2.0m,
                EstablishmentType = 3.0m,
                EstablishmentTypeName = "Tipo de Establecimiento Ejemplo",
                EstablishmentID = 4.0m,
                EstablishmentName = "Nombre de Establecimiento Ejemplo",
                AmountOfSkinToCut = 5.0m,
                SealIdentification = 6.0m,
                SkinStatus = "Estado de piel de ejemplo",
                CitesAuthorityOfficial = 7.0m,
                RepresentativeDocument = 8.0m,
                EstablishmentRepresentative = "Representante de Establecimiento Ejemplo",
                City = 9.0m,
                Date = DateTime.Now,
                DateFormat = "Formato de fecha",
                VisitReportType = 10.0m,
                SkinStatusInt = 11,
                VisitNumber1 = true,
                VisitNumber2 = false,
                CitesAuthorityOfficialName = "Nombre de la Autoridad CITES",
                ExcelSealFile = documento
            };

            var r = controller.EditarActaVisita(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void CrearActaVisitaIrregular()
        {
            var documento = new VisitReportDocument
            {
                Code = 123.45m,
                FileName = "DocumentoVisita.pdf",
                Base64String = "Base64StringEjemplo",
                FileType = "pdf"
            };

            var datos = new VisitCutsRegistration
            {
                VisitNumber = 1.0m,
                VisitNumber1 = true,
                VisitNumber2 = false,
                EstablishmentType = 2,
                EstablishmentID = 345.67m,
                QuantityOfSkinToCut = 10,
                IdentificationSeal = 6789,
                SkinStatus = "Estado de piel de ejemplo",
                CitesAuthorityOfficer = 0.0m,
                RepresentativeDocument = 456.78m,
                EstablishmentRepresentative = "Representante de Establecimiento Ejemplo",
                City = 789.01m,
                Date = DateTime.Now,
                ExcelSealFile = documento
            };

            var r = controller.CrearActaVisitaIrregular(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarActaVisitaArchivos()
        {
            var documento = new VisitReportDocument
            {
                Code = 123.45m,
                FileName = "DocumentoVisita.pdf",
                Base64String = "Base64StringEjemplo",
                FileType = "pdf"
            };

            var r = controller.InsertarActaVisitaArchivos(documento);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarTiposPartesLista()
        {
            var r = controller.ConsultarTiposPartesLista();
            Assert.True(r != null);
        }
    }
}
