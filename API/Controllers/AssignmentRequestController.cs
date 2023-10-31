using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers.Models;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentRequestController : ControllerBase
    {
        private readonly ILogger<AssignmentRequestController> _logger;
        private readonly AssignmentRequest solicitud;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public AssignmentRequestController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<AssignmentRequestController> logger)
        {
            solicitud = new AssignmentRequest(context, jwtAuthenticationManager);
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Consult")]
        public IActionResult Consult([FromQuery] string? user)
        {

            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("Consult method called");
                var r = solicitud.Consultar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), user ?? "Guest", ipAddress);
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
        [HttpGet("ConsultStatus")]
        public IActionResult ConsultStatus()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = solicitud.ConsultarEstados(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
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
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ReqAssignment req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = solicitud.Actualizar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] ReqAssignmentUpdate req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = solicitud.ActualizarEstado(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] ReqId req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = solicitud.Eliminar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Assign")]
        public IActionResult Assign([FromBody] VitalReq req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("Consult method called");

                var r = solicitud.Assign(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("VerifyDocument")]
        public IActionResult VerifyDocument(string document)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("Consult method called");

                var r = solicitud.VerificarDocumento(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), document, ipAddress);
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
