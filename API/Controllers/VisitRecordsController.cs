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
    public class  VisitRecordsController : ControllerBase
    {
        private readonly ILogger<VisitRecordsController> _logger;
        private readonly IActaVisitasCortesRepository actaVisitasCortesRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actaVisitasCortesRepository"></param>
        /// <param name="logger"></param>
        public VisitRecordsController(IActaVisitasCortesRepository actaVisitasCortesRepository, ILogger<VisitRecordsController> logger)
        {
            this.actaVisitasCortesRepository = actaVisitasCortesRepository;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataActVisitCuts"></param>
        /// <returns>type="View"</returns>
        [HttpPost("CreateActVisitCutsIdentfiable")]
        public async Task<IActionResult> CrearActaVisitaIdentificable(VisitCutsRegistration dataActVisitCuts)
        {
            var rutaDocumento = await ValidarYGuardarArchivoFTP(dataActVisitCuts.ExcelSealFile, dataActVisitCuts.EstablishmentID);   

            var datosActaVisita = new CupostT007ActaVisitum()
            {
                A007codigoEntidad = dataActVisitCuts.EstablishmentID,
                A007codigoPrecintoymarquilla = dataActVisitCuts.IdentificationSeal,
                A007fechaModificacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaCreacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaActa = dataActVisitCuts.Date,
                A007estadoRegistro = 72,
                A007estadoPieles = dataActVisitCuts.SkinStatus ?? "",
                A007cantidadPielesAcortar = dataActVisitCuts.QuantityOfSkinToCut,
                A007TipoActa = 1,
                A007CiudadDepartamento = dataActVisitCuts.City,
                A007RepresentanteNombre = dataActVisitCuts.EstablishmentRepresentative,
                A007RepresentanteIdentificacion = dataActVisitCuts.RepresentativeDocument,
                A007VisitaNumero = dataActVisitCuts.VisitNumber,
                A007VisitaNumero1 = dataActVisitCuts.VisitNumber1,
                A007VisitaNumero2 = dataActVisitCuts.VisitNumber2,
                A007PrecintoAdjunto = rutaDocumento
            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.CrearActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosActaVisita, ipAddress);

                if(responses.Response is decimal numeroDecimal && numeroDecimal > 0 && !responses.Error)
                {
                    await actaVisitasCortesRepository.IngresarSalvoConductosExcel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), dataActVisitCuts.ExcelSealFile.Base64String ?? "", numeroDecimal);
                }

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
        /// <param name="dataActVisitCuts"></param>
        /// <returns></returns>
        [HttpPut("EditActVisitIdentfiable")]
        public async Task<IActionResult> EditarActaVisita(EditVisitReportAct dataActVisitCuts)
        {

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            var rutaDocumento = "";

            await actaVisitasCortesRepository.IncrementarCantidadCupoPrecintos(dataActVisitCuts.VisitReportId);            

            string dateNowString = DateTime.Now.ToString("yyyyMMddHHmmss");
            dataActVisitCuts.ExcelSealFile.FileName = $"{dateNowString}_Cupos de Precintos.xlsx";


            rutaDocumento = await ValidarYGuardarArchivoFTP(dataActVisitCuts.ExcelSealFile, dataActVisitCuts.EstablishmentID);

            await actaVisitasCortesRepository.EliminarDocsSalvoConductos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), dataActVisitCuts.VisitReportId, ipAddress);
            await actaVisitasCortesRepository.IngresarSalvoConductosExcel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), dataActVisitCuts.ExcelSealFile.Base64String ?? "", dataActVisitCuts.VisitReportId);

            var datosActaVisita = new CupostT007ActaVisitum()
            {
                PkT007codigo = dataActVisitCuts.VisitReportId,
                A007codigoEntidad = dataActVisitCuts.EstablishmentID,
                A007codigoPrecintoymarquilla = dataActVisitCuts.SealIdentification,
                A007fechaModificacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaActa = dataActVisitCuts.Date,
                A007estadoRegistro = 72,
                A007estadoPieles = dataActVisitCuts.SkinStatus ?? "",
                A007cantidadPielesAcortar = dataActVisitCuts.AmountOfSkinToCut,
                A007CiudadDepartamento = dataActVisitCuts.City,
                A007RepresentanteNombre = dataActVisitCuts.EstablishmentRepresentative,
                A007RepresentanteIdentificacion = dataActVisitCuts.RepresentativeDocument,
                A007VisitaNumero = dataActVisitCuts.VisitNumber,
                A007VisitaNumero1 = dataActVisitCuts.VisitNumber1,
                A007VisitaNumero2 = dataActVisitCuts.VisitNumber2,
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
        //// <summary>
        /// 
        /// </summary>
        /// <param name="dataActVisitCuts"></param>
        /// <returns></returns>
        [HttpPost("CreateActVisitCutsIrregulars")]
        public async Task<IActionResult> CrearActaVisitaIrregular(VisitCutsRegistration dataActVisitCuts)
        {
            var rutaDocumento = await ValidarYGuardarArchivoFTP(dataActVisitCuts.ExcelSealFile, dataActVisitCuts.EstablishmentID);
            
            var datosActaVisita = new CupostT007ActaVisitum()
            {
                A007codigoEntidad = dataActVisitCuts.EstablishmentID,
                A007codigoPrecintoymarquilla = dataActVisitCuts.IdentificationSeal,
                A007fechaModificacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaCreacion = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                A007fechaActa = dataActVisitCuts.Date,
                A007estadoRegistro = 72,
                A007estadoPieles = dataActVisitCuts.SkinStatus ?? "",
                A007cantidadPielesAcortar = dataActVisitCuts.QuantityOfSkinToCut,
                A007CiudadDepartamento = dataActVisitCuts.City,
                A007RepresentanteNombre = dataActVisitCuts.EstablishmentRepresentative,
                A007RepresentanteIdentificacion = dataActVisitCuts.RepresentativeDocument,
                A007TipoActa = 2,
                A007VisitaNumero = dataActVisitCuts.VisitNumber,
                A007VisitaNumero1 = dataActVisitCuts.VisitNumber1,
                A007VisitaNumero2 = dataActVisitCuts.VisitNumber2,
                A007PrecintoAdjunto = rutaDocumento
            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.CrearActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datosActaVisita, ipAddress);

                if (responses.Response is decimal numeroDecimal && numeroDecimal > 0 && !responses.Error)
                {
                    await actaVisitasCortesRepository.IngresarSalvoConductosExcel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), dataActVisitCuts.ExcelSealFile.Base64String ?? "", numeroDecimal);
                }

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
        /// <param name="typeSkinCutIdentifiable"></param>
        /// <returns></returns>
        [HttpPost("InsertTypeSkinIdentfiable")]
        public async Task<IActionResult> InsertTipoPielIdentificable(IdentifiableSkinCutsType typeSkinCutIdentifiable)
        {
            var datosTipoPiel = new CupostT008CortePiel()
            {
                A008codigoActaVisita = typeSkinCutIdentifiable.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = "",
                A008tipoPiel = typeSkinCutIdentifiable.SkinType ?? "",
                A008cantidad = typeSkinCutIdentifiable.Quantity,
                A008total = 0,
                A008areaPromedio = "",
                A008TipoCorteParteCode = 1 //Tipo parte identificable

            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="typePartIdentifiable"></param>
        /// <returns></returns>
        [HttpPost("InsertPartTypeIdentfiable")]
        public async Task<IActionResult> InsertTipoParteIdentificable(IdentifiableSkinPartsType typePartIdentifiable)
        {
            var datosTipoParte = new CupostT008CortePiel()
            {
                A008codigoActaVisita = typePartIdentifiable.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = typePartIdentifiable.SkinPartType ?? "",
                A008tipoPiel = "",
                A008cantidad = typePartIdentifiable.Quantity,
                A008total = 0,
                A008areaPromedio = "",
                A008TipoCorteParteCode = 2 //Tipo parte identificable

            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="typeSkinIrregular"></param>
        /// <returns></returns>
        [HttpPost("InsertPartSkinIrregular")]
        public async Task<IActionResult> InsertTipoPielIrregular(IrregularSkinTypes typeSkinIrregular)
        {
            var datosTipoPielIrregular = new CupostT008CortePiel()
            {
                A008codigoActaVisita = typeSkinIrregular.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = "",
                A008tipoPiel = typeSkinIrregular.IrregularSkinType ?? "",
                A008cantidad = typeSkinIrregular.SkinTypeQuantity,
                A008total = typeSkinIrregular.TotalAreaForSkinType,
                A008areaPromedio = (typeSkinIrregular.AverageAreaForSkinType == null)  ? "" : typeSkinIrregular.AverageAreaForSkinType,
                A008TipoCorteParteCode = 3 //Tipo piel irregular
            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="typeSkinIrregular"></param>
        /// <returns></returns>
        [HttpPost("InsertIrregularPartType")]
        public async Task<IActionResult> InsertTipoParteIrregular(IrregularPartTypes typeSkinIrregular)
        {
            var datosTipoParteIrregular = new CupostT008CortePiel()
            {
                A008codigoActaVisita = typeSkinIrregular.VisitReportCode,
                A008tipoCorte = "",
                A008tipoParte = typeSkinIrregular.PartType ?? "",
                A008tipoPiel = "",
                A008cantidad = typeSkinIrregular.PartTypeQuantity,
                A008total = typeSkinIrregular.TotalAreaForPartType,
                A008areaPromedio = "",
                A008TipoCorteParteCode = 4 //Tipo fraccion irregular

            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="documentOriginSkin"></param>
        /// <returns></returns>
        [HttpPost("InsertRecordVisitSkinOrigin")]
        public async Task<IActionResult> InsertActaVisitaOrigenPiel(VisitReportSkinOriginDocument documentOriginSkin)
        {
            var datosdocOrigenPiel = new CupostT015ActaVisitaDocumentoOrigenPiel()
            {
                A015CodigoActaVisita = documentOriginSkin.VisitReportCode,
                A015DocumentoOrigenPielNumero = documentOriginSkin.SkinOriginDocumentNumber

            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="dataResolution"></param>
        /// <returns></returns>
        [HttpPost("InsertActVisitResolutionNumber")]
        public async Task<IActionResult> InsertActaVisitaResolucionNumero(VisitReportResolutionNumber dataResolution)
        {
            var datosResolucionDB = new CupostT016ActaVisitaResolucion()
            {
                A016CodigoActaVisita = dataResolution.VisitReportCode,
                A016ResolucionNumero = dataResolution.ResolutionNumber

            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
            
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
        /// <param name="dataSalvoConducto"></param>
        /// <returns></returns>
        [HttpPost("InsertActVisitExceptDuct")]
        public async Task<IActionResult> InsertActaVisitaSalvoConducto(VisitReportSafeConduct dataSalvoConducto)
        {
            var datosSalvoConductoContext = new CupostT017ActaVisitaSalvoConducto()
            {
                A017CodigoActaVisita = dataSalvoConducto.VisitReportCode,
                A017SalvoConductoNumero = dataSalvoConducto.SafeConductNumber

            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="documentActVisit"></param>
        /// <returns></returns>
        [HttpPost("InsertFileActVisit")]
        public async Task<IActionResult> InsertarActaVisitaArchivos(VisitReportDocument documentActVisit)
        {
            var obtenerRuta = await GuardarArchivoFtp(documentActVisit);

            var datosDocumentoActaVisita = new CupostT018ActaVisitaDocumentos()
            {
                A018CodigoActaVisita = documentActVisit.Code,
                A018RutaDocumento = obtenerRuta,
                A018NombreArchivo = documentActVisit.FileName ?? ""


            };

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

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
        /// <param name="typeEstablishment"></param>
        /// <returns></returns>
        [HttpGet("ConsultBussinesByType")]
        public async Task<IActionResult> ConsultarEstablecimientosPorTipo(decimal typeEstablishment)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarEstablecimientosPorTipo(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), typeEstablishment);
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
        /// <param name="idEstablishment"></param>
        /// <returns></returns>
        [HttpGet("ConsultActsEstablishmentsById")]
        public async Task<IActionResult> ConsultarActasEstablecimientosPorId(decimal idEstablishment)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarActasEstablecimientosPorId(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idEstablishment);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultActVisitById")]
        public async Task<IActionResult> ConsultarActaVisitaPorId(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarActaVisitaporId(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultNumberSkinoOrigin")]
        public async Task<IActionResult> ConsultarDocumentosOrigenPiel(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDocumentosOrigenPiel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultNumberResolutionActVisit")]
        public async Task<IActionResult> ConsultarDocumentosResolucionActaVisita(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDocumentosResolucion(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultNumberExceptDuctActVisit")]
        public async Task<IActionResult> ConsultarDocumentosSalvoConductoActaVisita(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarDocumentosSavoConductos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultSkinTypeIdentfiableActVisit")]
        public async Task<IActionResult> ConsultarTipoPielIdentificableActaVisita(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoPielidentificablelActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultIdentfiablePartTypeActVisit")]
        public async Task<IActionResult> ConsultarTipoParteIdentificableActaVisita(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoParteIdentificable(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultIrregularSkinTypeActVisit")]
        public async Task<IActionResult> ConsultarTipoPielIrregularActaVisita(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoPielIrregularActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultIrregularPartTypeActVisit")]
        public async Task<IActionResult> ConsultarTipoParteIrregularActaVisita(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarTipoParteIrregular(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultFilePDFActVisit")]
        public async Task<IActionResult> ConsultarArchivoPDF(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarArchivosActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpGet("ConsultFileExcelSeals")]
        public async Task<IActionResult> ConsultarExcelPrecinto(decimal idActaVisit)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ConsultarArchivoPrecintoActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit);
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
        /// <param name="excelBase64File"></param>
        /// <returns></returns>
        [HttpPost("ValidateDataExcelSeals")]
        public async Task<IActionResult> ValidarDatosExcelPrecintos(ExcelSealsFile excelBase64File)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.ValidarDatosExcelPrecintos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), excelBase64File.Base64Excel, excelBase64File.NIT);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNumberSkinOriginActVisit")]
        public async Task<IActionResult> EliminarDocsOrigenPielActaVisita(decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarDocumentosOrigenPiel(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpDelete("DeleteResolutionsNumberActVisit")]
        public async Task<IActionResult> EliminarDocsResolucionActaVisita(decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarDocResolucionActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpDelete("DeleteExceptDuctsNumerActVisita")]
        public async Task<IActionResult> EliminarDocsSalvoConductosActaVisita(decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarDocsSalvoConductos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpDelete("DeletePartSkinsActVisit")]
        public async Task<IActionResult> DeleteTiposPielesActaVisita(decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarTiposPielIdentificables(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpDelete("DeletePartsTypeActVisit")]
        public async Task<IActionResult> DeleteTiposPartesActaVisita(decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarTiposPartesIdentificables(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpPut("DisableActVisit")]
        public async Task<IActionResult> InhabilitarActaVisita([FromQuery] decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.DesHabilitarActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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
        /// <param name="idActaVisit"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFileActVisit")]
        public async Task<IActionResult> BorrarArchivosActaVisita(decimal idActaVisit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var responses = await actaVisitasCortesRepository.EliminarArchivosActaVisita(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), idActaVisit, ipAddress);
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

            #pragma warning disable S2589
            if (requestStream != null)
                requestStream.Close();

            return uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExcelPrecinct"></param>
        /// <param name="idCompany"></param>
        /// <returns></returns>
        private async Task<string> ValidarYGuardarArchivoFTP(VisitReportDocument fileExcelPrecinct, decimal idCompany)
        {
            var rutaDocumento = "";
            var modificacionCantidadCupo = await actaVisitasCortesRepository.ModificarCantidadCupoPrecintos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), fileExcelPrecinct.Base64String ?? "",  idCompany);

            var validacionPrecintos = await actaVisitasCortesRepository.ActualizarNumeroPrecinto(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), fileExcelPrecinct.Base64String ?? "", idCompany, StringHelper.estadoInactivo);           

            if (modificacionCantidadCupo && !validacionPrecintos.Error)
                rutaDocumento = await GuardarArchivoFtp(fileExcelPrecinct);

            return rutaDocumento;
        }
    }
}
