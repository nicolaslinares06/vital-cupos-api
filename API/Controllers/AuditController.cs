using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using System.Security.Claims;
using System.Net;
using DocumentFormat.OpenXml.Presentation;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly ILogger<AuditController> _logger;
        private readonly Audit auditoria;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public AuditController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<AuditController> logger)
        {
            auditoria = new Audit(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Consult")]
        public IActionResult Consult([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? page)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var result = auditoria.Consultar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), startDate, endDate, ipAddress, page);
                return Ok(result);
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
        /// <param name="date"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultDetails")]
        public IActionResult ConsultDetails([FromQuery] DateTime date)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var result = auditoria.ConsultarDetalle(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), date, ipAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
    }
}
