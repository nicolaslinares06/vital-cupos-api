using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using static API.Controllers.CoordinatorAssignRequestAnalystGpnController;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalSealsManagementController : Controller
    {
        private readonly ILogger<NationalSealsManagementController> _logger;
        private readonly GestionPrencintosNacionales gestionPrencintosNacionales;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public NationalSealsManagementController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<NationalSealsManagementController> logger, IGenericsMethodsHelper genericsMethodsHelper)
        {
            gestionPrencintosNacionales = new GestionPrencintosNacionales(context, jwtAuthenticationManager, genericsMethodsHelper);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultPendingAnalyst")]
        public IActionResult ConsultPendingAnalyst()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultPendingAnalyst(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultNumbers")]
        public IActionResult ConsultNumbers([FromQuery] int codigoSolicitud)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultNumbers(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, codigoSolicitud);
                return Ok(r);
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
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetNumbersSeals")]
        public IActionResult GetNumbersSeals([FromQuery] string requestType)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.GetNumbersSeals(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, requestType);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultRequirementAnalyst")]
        public IActionResult ConsultRequirementAnalyst()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultRequirementAnalyst(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultApprovedAnalyst")]
        public IActionResult ConsultApprovedAnalyst()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultApprovedAnalyst(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultWithdrawalAnalyst")]
        public IActionResult ConsultWithdrawalAnalyst()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultWithdrawalAnalyst(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultAssignedAnalyst")]
        public IActionResult ConsultAssignedAnalyst()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultAssignedAnalyst(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultPendingDirector")]
        public IActionResult ConsultPendingDirector()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultPendingDirector(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsulSignedDirector")]
        public IActionResult ConsulSignedDirector()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsulSignedDirector(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultApprovedDirector")]
        public IActionResult ConsultApprovedDirector()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultApprovedDirector(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("ConsultWithdrawalDirector")]
        public IActionResult ConsultWithdrawalDirector()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultWithdrawalDirector(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("UpdateSettled")]
        public IActionResult UpdateSettled([FromBody] SettledNationalSealsManagement data)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.FileApplication(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), data, ipAddress);
                return Ok(r);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("UpdateSettledDeparture")]
        public IActionResult UpdateSettledDeparture([FromBody] SettledNationalSealsManagement data)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.FileExitRequest(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), data, ipAddress);
                return Ok(r);
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
        [Authorize]
        [HttpGet("Species")]
        public IActionResult Species()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.Species(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
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
        /// <param name="datos"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("GenerateSealCodes")]
        public IActionResult GenerateSealCodes([FromBody] GenerateSealCodes datos)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.GeneratePrecitosCodes(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), datos, ipAddress);
                return Ok(r);
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
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultDetail")]
        public IActionResult ConsultDetail([FromQuery] int id)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultDetail(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, id);
                return Ok(r);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("ReturnSettled")]
        public IActionResult ReturnSettled([FromBody] ReturnSettledNationalSealsManagement data)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ReturnRequest(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), data, ipAddress);
                return Ok(r);
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
        /// <param name="code"></param>
        /// <param name="amount"></param>
        /// <param name="color"></param>
        /// <param name="initialCode"></param>
        /// <param name="finalCode"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GenerateDocument")]
        public IActionResult GenerateDocument([FromQuery] int code, [FromQuery] int amount, [FromQuery] string color, [FromQuery] decimal initialCode, [FromQuery] decimal finalCode, [FromQuery] string requestType)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string tipoCarta = "cartaSolicitud";
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.GenerateDocument(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, code, amount, color, initialCode, finalCode, tipoCarta, requestType);
                return Ok(r);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DesistSettled")]
        public IActionResult DesistSettled([FromBody] DesistNationalSealsManagement data)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.WithdrawRequest(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), data, ipAddress);
                return Ok(r);
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
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("DocumentSeal")]
        public IActionResult DocumentSeal([FromQuery] int code, int type)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.DocumentSeal(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, code, type);
                return Ok(r);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SignApplicationDocument")]
        public IActionResult SignApplicationDocument([FromBody] SignApplicationDocument data)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.SignDocumentRequest(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), data, ipAddress);
                return Ok(r);
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
        /// <param name="code"></param>
        /// <returns></returns>
		[Authorize]
		[HttpGet("ConsultCodes")]
		public IActionResult ConsultCodes([FromQuery] int code)
		{
            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.CheckCodesSeals(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), code);
                return Ok(r);
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
        /// <param name="code"></param>
        /// <returns></returns>
		[Authorize]
        [HttpGet("CheckCodesSealsMin")]
        public IActionResult CheckCodesSealsMin([FromQuery] int code)
        {

            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.CheckCodesSealsMin(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), code);
                return Ok(r);
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
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Authorize]
		[HttpGet("ConsultNumberings")]
		public IActionResult ConsultNumberings([FromQuery] int requestType)
		{
            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.CheckApplicationNumbers(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestType);
                return Ok(r);
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
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultNitCompany")]
        public IActionResult ConsultNitCompany([FromQuery] int requestType)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultNitCompany(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestType);
                return Ok(r);
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
        /// <param name="requestType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultSealColor")]
        public IActionResult ConsultSealColor([FromQuery] int requestType)
        {
            
            try
            {
                _logger.LogInformation("method called");
                var r = gestionPrencintosNacionales.ConsultColorSeal(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestType);
                return Ok(r);
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
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SendEmailtoApproved")]
        public async Task<IActionResult> SendEmailtoApproved([FromBody] List<MailApproval> data)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = await gestionPrencintosNacionales.SendApprovedMail(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), data);
                return Ok(r);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTypesFractionsRequest")]
        public async Task<IActionResult> GetTypesFractionsRequest([FromQuery] int requestCode)
        {
            
            try
            {
                _logger.LogInformation("method called");
                var r = await gestionPrencintosNacionales.GetTypesFractionsRequest(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// consultar puertos
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetEspecimen")]
        public IActionResult GetTEspecimen(decimal code)
        {
            try
            {
                _logger.LogInformation("Consult method called");

                ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
                var r = gestionPrencintosNacionales.GetEspecimen(identity, code);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
    }
}
