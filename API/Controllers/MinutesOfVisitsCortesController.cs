using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Net;
using System.Security.Claims;
using static Repository.Helpers.Models.ActaVisitaCortesModel;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinutesOfVisitsCortesController : ControllerBase
    {
        private readonly IActaVisitasCortesRepository actaVisitasCortesRepository;
        private readonly ILogger<MinutesOfVisitsCortesController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actaVisitasCortesRepository"></param>
        /// <param name="logger"></param>
        public MinutesOfVisitsCortesController(IActaVisitasCortesRepository actaVisitasCortesRepository, ILogger<MinutesOfVisitsCortesController> logger)
        {
            this.actaVisitasCortesRepository = actaVisitasCortesRepository;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataVisitReport"></param>
        /// <returns></returns>
        [HttpPost("CreateActVisitCutsIdentfiable")]
        public async Task<IActionResult> CrearActaVisitaIdentificable(VisitCutsRegistration dataVisitReport)
        {
            var rutaDocumento = await ValidarYGuardarArchivoFTP(dataVisitReport.ExcelSealFile, dataVisitReport.EstablishmentID);
            
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosActaVisita = new CupostT007ActaVisitum()
            {
                A007codigoEntidad = dataVisitReport.EstablishmentID,
                A007codigoPrecintoymarquilla = dataVisitReport.IdentificationSeal,
                A007fechaModificacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaCreacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaActa = dataVisitReport.Date,
                A007estadoRegistro = 72,
                A007estadoPieles = dataVisitReport.SkinStatus ?? "",
                A007cantidadPielesAcortar = dataVisitReport.QuantityOfSkinToCut,
                A007TipoActa = 1,
                A007CiudadDepartamento = dataVisitReport.City,
                A007RepresentanteNombre = dataVisitReport.EstablishmentRepresentative,
                A007RepresentanteIdentificacion = dataVisitReport.RepresentativeDocument,
                A007VisitaNumero = dataVisitReport.VisitNumber,
                A007VisitaNumero1 = dataVisitReport.VisitNumber1,
                A007VisitaNumero2 = dataVisitReport.VisitNumber2,
                A007PrecintoAdjunto = rutaDocumento
            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.CrearActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosActaVisita, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportCuttingData"></param>
        /// <returns></returns>
        [HttpPut("EditActVisitIdentfiable")]
        public async Task<IActionResult> EditarActaVisita(EditVisitReportAct visitReportCuttingData)
        {

            var rutaDocumento = "";
            await actaVisitasCortesRepository.IncrementarCantidadCupoPrecintos(visitReportCuttingData.VisitReportId);            

            string dateNowString = DateTime.Now.ToString("yyyyMMddHHmmss");
            visitReportCuttingData.ExcelSealFile.FileName = $"{dateNowString}_Cupos de Precintos.xlsx";

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            rutaDocumento = await ValidarYGuardarArchivoFTP(visitReportCuttingData.ExcelSealFile, visitReportCuttingData.EstablishmentID); 
            var datosActaVisita = new CupostT007ActaVisitum()
            {
                PkT007codigo = visitReportCuttingData.VisitReportId,
                A007codigoEntidad = visitReportCuttingData.EstablishmentID,
                A007codigoPrecintoymarquilla = visitReportCuttingData.SealIdentification,
                A007fechaModificacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaActa = visitReportCuttingData.Date,
                A007estadoRegistro = 72,
                A007estadoPieles = visitReportCuttingData.SkinStatus ?? "",
                A007cantidadPielesAcortar = visitReportCuttingData.AmountOfSkinToCut,
                A007CiudadDepartamento = visitReportCuttingData.City,
                A007RepresentanteNombre = visitReportCuttingData.EstablishmentRepresentative,
                A007RepresentanteIdentificacion = visitReportCuttingData.RepresentativeDocument,
                A007VisitaNumero = visitReportCuttingData.VisitNumber,
                A007VisitaNumero1 = visitReportCuttingData.VisitNumber1,
                A007VisitaNumero2 = visitReportCuttingData.VisitNumber2,
                A007PrecintoAdjunto = rutaDocumento
            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ActualizarActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosActaVisita, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportCutData"></param>
        /// <returns></returns>
        [HttpPost("CreateActVisitCutsIrregulars")]
        public async Task<IActionResult> CrearActaVisitaIrregular(VisitCutsRegistration visitReportCutData)
        {
            var rutaDocumento = await ValidarYGuardarArchivoFTP(visitReportCutData.ExcelSealFile, visitReportCutData.EstablishmentID);

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosActaVisita = new CupostT007ActaVisitum()
            {
                A007codigoEntidad = visitReportCutData.EstablishmentID,
                A007codigoPrecintoymarquilla = visitReportCutData.IdentificationSeal,
                A007fechaModificacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaCreacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaActa = visitReportCutData.Date,
                A007estadoRegistro = 72,
                A007estadoPieles = visitReportCutData.SkinStatus ?? "",
                A007cantidadPielesAcortar = visitReportCutData.QuantityOfSkinToCut,
                A007CiudadDepartamento = visitReportCutData.City,
                A007RepresentanteNombre = visitReportCutData.EstablishmentRepresentative,
                A007RepresentanteIdentificacion = visitReportCutData.RepresentativeDocument,
                A007TipoActa = 2,
                A007VisitaNumero = visitReportCutData.VisitNumber,
                A007VisitaNumero1 = visitReportCutData.VisitNumber1,
                A007VisitaNumero2 = visitReportCutData.VisitNumber2,
                A007PrecintoAdjunto = rutaDocumento
            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.CrearActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosActaVisita, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiableSkinCutType"></param>
        /// <returns></returns>
        [HttpPost("InsertTypeSkinIdentfiable")]
        public async Task<IActionResult> InsertTipoPielIdentificable(IdentifiableSkinCutsType identifiableSkinCutType)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosTipoPiel = new CupostT008CortePiel()
            {
                A008codigoActaVisita = identifiableSkinCutType.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = "",
                A008tipoPiel = identifiableSkinCutType.SkinType ?? "",
                A008cantidad = identifiableSkinCutType.Quantity,
                A008total = 0,
                A008areaPromedio = ""

            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarTipoPielIdentificable(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosTipoPiel, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiablePartType"></param>
        /// <returns></returns>
        [HttpPost("InsertPartTypeIdentfiable")]
        public async Task<IActionResult> InsertTipoParteIdentificable(IdentifiableSkinPartsType identifiablePartType)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosTipoParte = new CupostT008CortePiel()
            {
                A008codigoActaVisita = identifiablePartType.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = identifiablePartType.SkinPartType ?? "",
                A008tipoPiel = "",
                A008cantidad = identifiablePartType.Quantity,
                A008total = 0,
                A008areaPromedio = ""

            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertTipoParteIdentificable(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosTipoParte, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="irregularSkinType"></param>
        /// <returns></returns>
        [HttpPost("InsertPartSkinIrregular")]
        public async Task<IActionResult> InsertTipoPielIrregular(IrregularSkinTypes irregularSkinType)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosTipoPielIrregular = new CupostT008CortePiel()
            {
                A008codigoActaVisita = irregularSkinType.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = "",
                A008tipoPiel = irregularSkinType.IrregularSkinType ?? "",
                A008cantidad = irregularSkinType.SkinTypeQuantity,
                A008total = irregularSkinType.TotalAreaForSkinType,
                A008areaPromedio = (irregularSkinType.AverageAreaForSkinType == null)  ? "" : irregularSkinType.AverageAreaForSkinType
            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarTipoPielIrregular(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosTipoPielIrregular, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="irregularPartType"></param>
        /// <returns></returns>
        [HttpPost("InsertIrregularPartType")]
        public async Task<IActionResult> InsertTipoParteIrregular(IrregularPartTypes irregularPartType)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosTipoParteIrregular = new CupostT008CortePiel()
            {
                A008codigoActaVisita = irregularPartType.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = irregularPartType.PartType ?? "",
                A008tipoPiel = "",
                A008cantidad = irregularPartType.PartTypeQuantity,
                A008total = irregularPartType.TotalAreaForPartType,
                A008areaPromedio = ""

            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarTipoParteIrregular(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosTipoParteIrregular, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinOriginDocument"></param>
        /// <returns></returns>
        [HttpPost("InsertRecordVisitSkinOrigin")]
        public async Task<IActionResult> InsertActaVisitaOrigenPiel(VisitReportSkinOriginDocument skinOriginDocument)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosdocOrigenPiel = new CupostT015ActaVisitaDocumentoOrigenPiel()
            {
                A015CodigoActaVisita = skinOriginDocument.VisitReportCode,
                A015DocumentoOrigenPielNumero = skinOriginDocument.SkinOriginDocumentNumber

            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarActaVisitaDocumentoorigenPiel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosdocOrigenPiel, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolutionData"></param>
        /// <returns></returns>
        [HttpPost("InsertActVisitResolutionNumber")]
        public async Task<IActionResult> InsertActaVisitaResolucionNumero(VisitReportResolutionNumber resolutionData)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosResolucionDB = new CupostT016ActaVisitaResolucion()
            {
                A016CodigoActaVisita = resolutionData.VisitReportCode,
                A016ResolucionNumero = resolutionData.ResolutionNumber

            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarActaVisitaResolucionNumero(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosResolucionDB, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="safeConductData"></param>
        /// <returns></returns>
        [HttpPost("InsertActVisitExceptDuct")]
        public async Task<IActionResult> InsertActaVisitaSalvoConducto(VisitReportSafeConduct safeConductData)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosSalvoConductoContext = new CupostT017ActaVisitaSalvoConducto()
            {
                A017CodigoActaVisita = safeConductData.VisitReportCode,
                A017SalvoConductoNumero = safeConductData.SafeConductNumber

            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarActaVisitaSAlvoConductoNumero(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosSalvoConductoContext, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportDocument"></param>
        /// <returns></returns>
        [HttpPost("InsertFileActVisit")]
        public async Task<IActionResult> InsertarActaVisitaArchivos(VisitReportDocument visitReportDocument)
        {
            var obtenerRuta = await GuardarArchivoFtp(visitReportDocument);

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            var datosDocumentoActaVisita = new CupostT018ActaVisitaDocumentos()
            {
                A018CodigoActaVisita = visitReportDocument.Code,
                A018RutaDocumento = obtenerRuta,
                A018NombreArchivo = visitReportDocument.FileName ?? ""


            };

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.InsertarActaVisitaDocumento(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosDocumentoActaVisita, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ConsultTypesOfEstablishments")]
        public async Task<IActionResult> ConsultarEstablecimientoPorTIpo()
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarEstablecimientoPorTipo(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity());
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ConsultBusiness")]
        public async Task<IActionResult> ConsultarEmpresasEstablecimientos()
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarEmpresas(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity());
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("ConsultCitiesByDepartment")]
        public async Task<IActionResult> ConsultarCiudadesPorDepartamento(decimal departmentId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarCiudadesPorDepartamento(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), departmentId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        [HttpGet("ConsultBussinesByNIT")]
        public async Task<IActionResult> ConsultarEmpresaPorNit(decimal nit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarEmpresaPorNit(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), nit);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentType"></param>
        /// <returns></returns>
        [HttpGet("ConsultBussinesByType")]
        public async Task<IActionResult> ConsultarEstablecimientosPorTipo(decimal establishmentType)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarEstablecimientosPorTipo(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), establishmentType);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ConsultDepartments")]
        public async Task<IActionResult> ConsultarDepartamentos()
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDepartamentos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity());
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentType"></param>
        /// <returns></returns>
        [HttpGet("ConsultActsEstablishmentsById")]
        public async Task<IActionResult> ConsultarActasEstablecimientosPorId(decimal establishmentType)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarActasEstablecimientosPorId(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), establishmentType);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost("ConsultActsEstablishmentsByType")]
        public async Task<IActionResult> ConsultarActasEstablecimientosPorTipo(VisitRecordsSearch criteria)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarActasEstablecimientosPorTipo(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), criteria);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultActVisitById")]
        public async Task<IActionResult> ConsultarActaVisitaPorId(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarActaVisitaporId(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultNumberSkinoOrigin")]
        public async Task<IActionResult> ConsultarDocumentosOrigenPiel(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDocumentosOrigenPiel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultNumberResolutionActVisit")]
        public async Task<IActionResult> ConsultarDocumentosResolucionActaVisita(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDocumentosResolucion(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultNumberExceptDuctActVisit")]
        public async Task<IActionResult> ConsultarDocumentosSalvoConductoActaVisita(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDocumentosSavoConductos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultSkinTypeIdentfiableActVisit")]
        public async Task<IActionResult> ConsultarTipoPielIdentificableActaVisita(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoPielidentificablelActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultIdentfiablePartTypeActVisit")]
        public async Task<IActionResult> ConsultarTipoParteIdentificableActaVisita(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoParteIdentificable(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultIrregularSkinTypeActVisit")]
        public async Task<IActionResult> ConsultarTipoPielIrregularActaVisita(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoPielIrregularActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultIrregularPartTypeActVisit")]
        public async Task<IActionResult> ConsultarTipoParteIrregularActaVisita(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoParteIrregular(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ConsultTypePartSkin")]
        public async Task<IActionResult> ConsultarTiposPartesLista()
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTiposPartesLista(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity());
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultFilePDFActVisit")]
        public async Task<IActionResult> ConsultarArchivoPDF(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarArchivosActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpGet("ConsultFileExcelSeals")]
        public async Task<IActionResult> ConsultarExcelPrecinto(decimal visitReportId)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarArchivoPrecintoActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelFileBase64"></param>
        /// <returns></returns>
        [HttpPost("ValidateDataExcelSeals")]
        public async Task<IActionResult> ValidarDatosExcelPrecintos(ExcelSealsFile excelFileBase64)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ValidarDatosExcelPrecintos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), excelFileBase64.Base64Excel, excelFileBase64.NIT);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNumberSkinOriginActVisit")]
        public async Task<IActionResult> EliminarDocsOrigenPielActaVisita(decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarDocumentosOrigenPiel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteResolutionsNumberActVisit")]
        public async Task<IActionResult> EliminarDocsResolucionActaVisita(decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarDocResolucionActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteExceptDuctsNumerActVisita")]
        public async Task<IActionResult> EliminarDocsSalvoConductosActaVisita(decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarDocsSalvoConductos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpDelete("DeletePartSkinsActVisit")]
        public async Task<IActionResult> DeleteTiposPielesActaVisita(decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarTiposPielIdentificables(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpDelete("DeletePartsTypeActVisit")]
        public async Task<IActionResult> DeleteTiposPartesActaVisita(decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarTiposPartesIdentificables(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpPut("DisableActVisit")]
        public async Task<IActionResult> InhabilitarActaVisita([FromQuery] decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.DesHabilitarActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitReportId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFileActVisit")]
        public async Task<IActionResult> BorrarArchivosActaVisita(decimal visitReportId)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarArchivosActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), visitReportId, ipAddress);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private async Task<string> GuardarArchivoFtp(VisitReportDocument document)
        {
            var query1 = await actaVisitasCortesRepository.ObtenerDatosFTP();
            string urlAdjjunto = "";
            string Puerto = "";
            string usuraio = "";
            string clave = "";
            foreach (var c in query1)
            {
                switch (c.A008descripcion)
                {
                    case "URL":
                        urlAdjjunto = c.A008valor;
                        break;
                    case "PUERTO":
                        Puerto = c.A008valor;
                        break;
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }

            Stream? requestStream = null;

            string eliminar = "data:" + document.FileType + ";base64,";
            string SinData = document.Base64String != null ? document.Base64String.Replace(eliminar, String.Empty) : "";

            byte[] buffer = Convert.FromBase64String(SinData);

            string uri = "ftp://" + urlAdjjunto + ":" + Puerto + "/CUPOS/docs/" + document.FileName;

            #pragma warning disable SYSLIB0014
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.ContentLength = buffer.Length;
            request.EnableSsl = false;
            request.Credentials = new NetworkCredential(usuraio, clave);

            requestStream = request.GetRequestStream();

            requestStream.Write(buffer, 0, buffer.Length);

            requestStream.Close();

            return uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelFileBase64"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private async Task<string> ValidarYGuardarArchivoFTP(VisitReportDocument excelFileBase64, decimal companyId)
        {
            var rutaDocumento = "";
            var modificacionCantidadCupo = await actaVisitasCortesRepository.ModificarCantidadCupoPrecintos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), excelFileBase64.Base64String  ?? "",  companyId);

            var validacionPrecintos = await actaVisitasCortesRepository.ActualizarNumeroPrecinto(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), excelFileBase64.Base64String ?? "", companyId, StringHelper.estadoInactivo);

            if (modificacionCantidadCupo && !validacionPrecintos.Error)
                rutaDocumento = await GuardarArchivoFtp(excelFileBase64);

            return rutaDocumento;
        }
    }
}
