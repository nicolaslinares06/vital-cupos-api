using API.Controllers;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Security.Claims;
using System.Security.Cryptography;
using Web.Models;
using static Repository.Helpers.Models.ActaVisitaCortesModel;

namespace TestUnit.API.Controllers
{
    public class VisitRecordsControllerTest
    {
        //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
        private readonly DBContext _context;
        private readonly VisitRecordsController controller;
        readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ClaimsIdentity user;
        public readonly IActaVisitasCortesRepository repository;


        public VisitRecordsControllerTest()
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
            controller = new VisitRecordsController(repository, new LoggerFactory().CreateLogger<VisitRecordsController>());

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
        public void ConsultarEmpresas()
        {
            var r = controller.ConsultarEmpresasEstablecimientos();
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarDepartamentos()
        {
            var r = controller.ConsultarDepartamentos();
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarCiudadesPorDepartamento()
        {
            decimal departamentoId = 1;
            var r = controller.ConsultarCiudadesPorDepartamento(departamentoId);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarEstablecimientosPorTipo()
        {
            decimal tipoEstablecimiento = 1;
            var r = controller.ConsultarEstablecimientosPorTipo(tipoEstablecimiento);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarEmpresaPorNit()
        {
            decimal nit = 897564231;
            var r = controller.ConsultarEmpresaPorNit(nit);
            Assert.True(r != null);

            nit = 999737334;
            r = controller.ConsultarEmpresaPorNit(nit);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarEstablecimientoPorTipo()
        {
            var r = controller.ConsultarEstablecimientoPorTIpo();
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarActasEstablecimientosPorId()
        {
            decimal idEstablecimento = 1;
            var r = controller.ConsultarActasEstablecimientosPorId(idEstablecimento);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarActasEstablecimientosPorTipo()
        {
            VisitRecordsSearch datos = new VisitRecordsSearch();
            datos.EstablishmentId = 1;
            datos.EstablishmentTypeId = 1;
            datos.VisitDate = DateTime.Now;
            datos.SearchType = 1;

            var r = controller.ConsultarActasEstablecimientosPorTipo(datos);
            Assert.True(r != null);

            datos.SearchType = 2;

            r = controller.ConsultarActasEstablecimientosPorTipo(datos);
            Assert.True(r != null);

            datos.SearchType = 3;

            r = controller.ConsultarActasEstablecimientosPorTipo(datos);
            Assert.True(r != null);

            datos.SearchType = 4;

            r = controller.ConsultarActasEstablecimientosPorTipo(datos);
            Assert.True(r != null);

            datos.SearchType = 5;

            r = controller.ConsultarActasEstablecimientosPorTipo(datos);
            Assert.True(r != null);

            datos.SearchType = -1;

            r = controller.ConsultarActasEstablecimientosPorTipo(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarDocumentosOrigenPiel()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarDocumentosOrigenPiel(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarDocumentosResolucion()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarDocumentosResolucionActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarDocumentosSavoConductos()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarDocumentosSalvoConductoActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarTipoPielidentificablelActaVisita()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarTipoPielIdentificableActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarTipoParteIdentificable()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarTipoParteIdentificableActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarTipoPielIrregularActaVisita()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarTipoPielIrregularActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarTipoParteIrregular()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarTipoParteIrregularActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarArchivosActaVisita()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarArchivoPDF(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarArchivoPrecintoActaVisita()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarExcelPrecinto(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void ValidarDatosExcelPrecintos()
        {
            var datos = new ExcelSealsFile
            {
                Base64Excel = "Base64ExcelValue",
                NIT = 123456789 // Valor numérico de ejemplo
            };

            var r = controller.ValidarDatosExcelPrecintos(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarActaVisitaporId()
        {
            decimal idActaVisita = 1;
            var r = controller.ConsultarActaVisitaPorId(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void CrearActaVisita()
        {
            var visitCutsRegistration = new VisitCutsRegistration
            {
                VisitNumber = 12345, // Valor numérico de ejemplo
                VisitNumber1 = true,
                VisitNumber2 = false,
                EstablishmentType = 1, // Valor numérico de ejemplo
                EstablishmentID = 6789, // Valor numérico de ejemplo
                QuantityOfSkinToCut = 5, // Valor numérico de ejemplo
                IdentificationSeal = 54321, // Valor numérico de ejemplo
                SkinStatus = "Sample Status", // Valor de cadena de ejemplo
                CitesAuthorityOfficer = 0, // Valor numérico de ejemplo
                RepresentativeDocument = 9876, // Valor numérico de ejemplo
                EstablishmentRepresentative = "Sample Representative", // Valor de cadena de ejemplo
                City = 123, // Valor numérico de ejemplo
                Date = DateTime.Today,
                ExcelSealFile = new VisitReportDocument
                {
                    Code = 456, // Valor numérico de ejemplo
                    FileName = "SampleFile.txt", // Nombre de archivo de ejemplo
                    Base64String = "Base64StringSample", // Cadena en Base64 de ejemplo
                    FileType = "txt" // Tipo de archivo de ejemplo
                }
            };


            var r = controller.CrearActaVisitaIdentificable(visitCutsRegistration);
            Assert.True(r != null);
        }

        [Fact]
        public void CrearActaVisitaIrregular()
        {
            var visitCutsRegistration = new VisitCutsRegistration
            {
                VisitNumber = 12345, // Valor numérico de ejemplo
                VisitNumber1 = true,
                VisitNumber2 = false,
                EstablishmentType = 1, // Valor numérico de ejemplo
                EstablishmentID = 6789, // Valor numérico de ejemplo
                QuantityOfSkinToCut = 5, // Valor numérico de ejemplo
                IdentificationSeal = 54321, // Valor numérico de ejemplo
                SkinStatus = "Sample Status", // Valor de cadena de ejemplo
                CitesAuthorityOfficer = 0, // Valor numérico de ejemplo
                RepresentativeDocument = 9876, // Valor numérico de ejemplo
                EstablishmentRepresentative = "Sample Representative", // Valor de cadena de ejemplo
                City = 123, // Valor numérico de ejemplo
                Date = DateTime.Today,
                ExcelSealFile = new VisitReportDocument
                {
                    Code = 456, // Valor numérico de ejemplo
                    FileName = "SampleFile.txt", // Nombre de archivo de ejemplo
                    Base64String = "Base64StringSample", // Cadena en Base64 de ejemplo
                    FileType = "txt" // Tipo de archivo de ejemplo
                }
            };

            var r = controller.CrearActaVisitaIrregular(visitCutsRegistration);
            Assert.True(r != null);
        }

        [Fact]
        public void ActualizarActaVisita()
        {
            var editVisitReportAct = new EditVisitReportAct
            {
                VisitReportId = 12345, // Valor numérico de ejemplo
                VisitNumber = 54321, // Valor numérico de ejemplo
                EstablishmentType = 1, // Valor numérico de ejemplo
                EstablishmentTypeName = "Sample Type Name", // Valor de cadena de ejemplo
                EstablishmentID = 6789, // Valor numérico de ejemplo
                EstablishmentName = "Sample Establishment Name", // Valor de cadena de ejemplo
                AmountOfSkinToCut = 10, // Valor numérico de ejemplo
                SealIdentification = 98765, // Valor numérico de ejemplo
                SkinStatus = "Sample Skin Status", // Valor de cadena de ejemplo
                CitesAuthorityOfficial = 0, // Valor numérico de ejemplo
                RepresentativeDocument = 4321, // Valor numérico de ejemplo
                EstablishmentRepresentative = "Sample Representative", // Valor de cadena de ejemplo
                City = 456, // Valor numérico de ejemplo
                Date = DateTime.Today,
                DateFormat = "yyyy-MM-dd", // Valor de cadena de ejemplo
                VisitReportType = 789, // Valor numérico de ejemplo
                SkinStatusInt = 2, // Valor numérico de ejemplo
                VisitNumber1 = true,
                VisitNumber2 = false,
                CitesAuthorityOfficialName = "Sample Official Name", // Valor de cadena de ejemplo
                ExcelSealFile = new VisitReportDocument
                {
                    Code = 321, // Valor numérico de ejemplo
                    FileName = "SampleFile.txt", // Nombre de archivo de ejemplo
                    Base64String = "Base64StringSample", // Cadena en Base64 de ejemplo
                    FileType = "txt" // Tipo de archivo de ejemplo
                }
            };


            var r = controller.EditarActaVisita(editVisitReportAct);
            Assert.True(r != null);
        }

        [Fact]
        public void DesHabilitarActaVisita()
        {
            decimal idActaVisita = 1;

            var r = controller.InhabilitarActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarTipoPielIdentificable()
        {
            var identifiableSkinCutsType = new IdentifiableSkinCutsType
            {
                SkinType = "Sample Skin Type", // Valor de cadena de ejemplo
                Quantity = 5, // Valor numérico de ejemplo
                VisitReportCode = 123 // Valor numérico de ejemplo
            };

            var r = controller.InsertTipoPielIdentificable(identifiableSkinCutsType);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertTipoParteIdentificable()
        {
            var identifiableSkinPartsType = new IdentifiableSkinPartsType
            {
                SkinPartType = "Sample Skin Part Type", // Valor de cadena de ejemplo
                Quantity = 10, // Valor numérico de ejemplo
                VisitReportCode = 456 // Valor numérico de ejemplo
            };

            var r = controller.InsertTipoParteIdentificable(identifiableSkinPartsType);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarTipoPielIrregular()
        {
            var irregularSkinTypes = new IrregularSkinTypes
            {
                IrregularSkinType = "Sample Irregular Skin Type", // Valor de cadena de ejemplo
                AverageAreaForSkinType = "Sample Average Area", // Valor de cadena de ejemplo
                SkinTypeQuantity = 15, // Valor numérico de ejemplo
                TotalAreaForSkinType = 250.5m, // Valor numérico de ejemplo
                VisitReportCode = 123 // Valor numérico de ejemplo
            };

            var r = controller.InsertTipoPielIrregular(irregularSkinTypes);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarTipoParteIrregular()
        {
            var irregularPartTypes = new IrregularPartTypes
            {
                PartType = "Sample Part Type", // Valor de cadena de ejemplo
                PartTypeQuantity = 20, // Valor numérico de ejemplo
                TotalAreaForPartType = 350.75m, // Valor numérico de ejemplo
                VisitReportCode = 456 // Valor numérico de ejemplo
            };


            var r = controller.InsertTipoParteIrregular(irregularPartTypes);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarActaVisitaDocumentoorigenPiel()
        {

            var visitReportSkinOriginDocument = new VisitReportSkinOriginDocument
            {
                VisitReportCode = 123, // Valor numérico de ejemplo
                SkinOriginDocumentNumber = "Sample Document Number" // Valor de cadena
            };

            var r = controller.InsertActaVisitaOrigenPiel(visitReportSkinOriginDocument);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarActaVisitaResolucionNumero()
        {
            var visitReportSkinOriginDocument = new VisitReportResolutionNumber
            {
                VisitReportCode = 123, // Valor numérico de ejemplo
                ResolutionNumber = 1 // Valor de cadena de ejemplo
            };


            var r = controller.InsertActaVisitaResolucionNumero(visitReportSkinOriginDocument);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarActaVisitaSAlvoConductoNumero()
        {
            var visitReportSafeConduct = new VisitReportSafeConduct
            {
                VisitReportCode = 123, // Valor numérico de ejemplo
                SafeConductNumber = 456 // Valor numérico de ejemplo
            };

            var r = controller.InsertActaVisitaSalvoConducto(visitReportSafeConduct);
            Assert.True(r != null);
        }

        [Fact]
        public void EliminarDocumentosOrigenPiel()
        {
            decimal idActaVisita = 1;

            var r = controller.EliminarDocsOrigenPielActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void EliminarDocResolucionActaVisita()
        {
            decimal idActaVisita = 1;

            var r = controller.EliminarDocsResolucionActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void EliminarDocsSalvoConductos()
        {
            decimal idActaVisita = 1;

            var r = controller.EliminarDocsSalvoConductosActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void EliminarTiposPielIdentificables()
        {
            decimal idActaVisita = 1;

            var r = controller.DeleteTiposPielesActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void EliminarTiposPartesIdentificables()
        {
            decimal idActaVisita = 1;

            var r = controller.DeleteTiposPartesActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void EliminarArchivosActaVisita()
        {
            decimal idActaVisita = 1;

            var r = controller.BorrarArchivosActaVisita(idActaVisita);
            Assert.True(r != null);
        }

        [Fact]
        public void InsertarActaVisitaDocumento()
        {
            var visitReportSkinOriginDocument = new VisitReportSkinOriginDocument
            {
                VisitReportCode = 123, // Valor numérico de ejemplo
                SkinOriginDocumentNumber = "ABC123" // Valor de cadena de ejemplo
            };

            var r = controller.InsertActaVisitaOrigenPiel(visitReportSkinOriginDocument);
            Assert.True(r != null);
        }
    }
}
