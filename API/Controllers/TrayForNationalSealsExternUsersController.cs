using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using Repository;
using Repository.Helpers;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using static Repository.Helpers.Models.ModelsAppSettings;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrayForNationalSealsExternUsersController : ControllerBase
    {
        private readonly ILogger<TrayForNationalSealsExternUsersController> _logger;
        private readonly ITrayForNationalSealsExternUsers TrayForNationalSealsExternUsers;

        public TrayForNationalSealsExternUsersController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, IGenericsMethodsHelper genericsMethodsHelper, ILogger<TrayForNationalSealsExternUsersController> logger, IOptions<EstadosCuposSettings> estadosCuposSettings)
        {
            TrayForNationalSealsExternUsers = new TrayForNationalSealsExternUsers(context, jwtAuthenticationManager, genericsMethodsHelper, estadosCuposSettings);
            _logger = logger;
        }

        /// <summary>
        /// consulta tipos solicitud
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultRequestTypes")]
        public IActionResult ConsultRequestTypes()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultRequestTypes(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar empresas
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultBussiness")]
        public IActionResult ConsultBussiness()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultBussiness(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }


        /// <summary>
        /// consultar empresa y representate relacionado a el usuario externo
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultBussinesAndLegalRepresentant")]
        public IActionResult ConsultBussinesAndLegalRepresentant(decimal codeBussines)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultBussinesAndLegalRepresentant(identity, codeBussines);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }


        /// <summary>
        /// consultar ciudades
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultCities")]
        public IActionResult ConsultCities()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultCities(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar ciudades por id departamento
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultCitiesByIdDepartment")]
        public async Task<IActionResult> ConsultCitiesByIdDepartment(decimal idDepartment)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = await TrayForNationalSealsExternUsers.ConsultCitiesByIdDepartment(identity, idDepartment);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar departamentos
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultDepartments")]
        public async Task<IActionResult> ConsultDepartments()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = await TrayForNationalSealsExternUsers.consultDepartments(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// guardar solicitud
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RegisterRequest")]
        public IActionResult RegisterRequest(Requests request)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = TrayForNationalSealsExternUsers.RegisterRequest(identity, request, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar solicitudes radicadas
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultRegisteredRecuest")]
        public IActionResult ConsultRegisteredRecuest(decimal companyCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultRegisteredRecuest(identity, companyCode);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar solicitudes en requerimeinto
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultRequirements")]
        public IActionResult ConsultRequirements(decimal companyCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultRequirements(identity, companyCode);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar solicitudes aprobadas
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultApproved")]
        public IActionResult ConsultApproved(decimal companyCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultApproved(identity, companyCode);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar solicitudes desistidas
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultDesisted")]
        public IActionResult ConsultDesisted(decimal companyCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultDesisted(identity, companyCode);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// radicar solicitud pendiente 
        /// </summary>
        /// <param name="requestPending"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RegisterPending")]
        public IActionResult RegisterPending(RegisterPending requestPending)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.RegisterPending(identity, requestPending);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar una solicitud
        /// </summary>
        /// <param name="codeRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultOnePendientRegister")]
        public IActionResult ConsultOnePendientRegister(decimal codeRequest)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.ConsultOnePendientRegister(identity, codeRequest);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// edicion de la solicitud
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("EditRequest")]
        public IActionResult EditRequest(Requests request)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = TrayForNationalSealsExternUsers.EditRequest(identity, request, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// obtiene cupos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetQuotas")]
        public IActionResult GetQuotas(string documentNumber, string species)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.GetQuotas(identity, documentNumber, species);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// obtiene inventario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetInventory")]
        public IActionResult GetInventory(string documentNumber, string species)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.GetInventory(identity, documentNumber, species);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Consultar numeraciones solicitudes no disponibles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("getNumbersRequest")]
        public IActionResult getNumbersRequest(ConsultUnableNumbersModel data)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = TrayForNationalSealsExternUsers.getNumbersRequest(identity, data);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Valida si estan disponibvles las numeraciones
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("ValidateNumbers")]
        public IActionResult ValidateNumbers(ValidateNumbersModel numbers)
        {
            try
            {
                _logger.LogInformation("method called");
                ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
                var r = TrayForNationalSealsExternUsers.ValidateNumbers(identity, numbers);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                 throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("getActaData")]
        public IActionResult getActaData(string documentNumber)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            var r = TrayForNationalSealsExternUsers.getActaData(identity, documentNumber);
            return Ok(r);
        }

        [Authorize]
        [HttpGet("getFractions")]
        public IActionResult getFractions(int cuttingCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            var r = TrayForNationalSealsExternUsers.getFractions(identity, cuttingCode);
            return Ok(r);
        }

        [Authorize]
        [HttpGet("getSafeguard")]
        public IActionResult getSafeguard(int reportCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            var r = TrayForNationalSealsExternUsers.getSafeguard(identity, reportCode);
            return Ok(r);
        }

    }
}
