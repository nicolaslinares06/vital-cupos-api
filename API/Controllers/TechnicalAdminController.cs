using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalAdminController : ControllerBase
    {
        private readonly ILogger<TechnicalAdminController> _logger;
        private readonly TechnicalAdmin admin;
        public TechnicalAdminController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<TechnicalAdminController> logger)
        {
            admin = new TechnicalAdmin(context, jwtAuthenticationManager);
            _logger = logger;
        }

        [Authorize]
        [HttpGet("Consult")]
        public IActionResult Consult([FromQuery] string? value)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                
                var r = admin.Consultar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), value, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("List")]
        public IActionResult List()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                
                var r = admin.Listar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult Update([FromBody] TechnicalAdminReq req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = admin.Actualizar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] ReqId req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                
                var r = admin.Eliminar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] TechnicalAdminReq req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                
                var r = admin.Crear(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("ConsultTechnicalValues")]
        public IActionResult ConsultTechnicalValues(string parameter)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                var r = admin.ConsultarValoresTecnicos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, parameter);
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