using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using System.Security.Claims;
using Repository.Helpers.Models;
using System.Net;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly Empresa empresa;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public CompanyController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<CompanyController> logger)
        {
            empresa = new Empresa(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult Actualizar([FromBody] EntityRequest req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = empresa.Actualizar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        /// <param name="companyCode"></param>
        /// <param name="noveltyId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultNovedades")]
        public IActionResult ConsultNovedades(decimal companyCode, decimal? noveltyId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = empresa.ConsultaNovedades(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), companyCode, noveltyId);
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
        [HttpPost("Save")]
        public IActionResult Save([FromBody] NoveltiesRequest req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = empresa.RegistroNovedad(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        /// <param name="companyId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("  ")]
        public IActionResult ConsultCupos(decimal companyId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = empresa.ConsultarCupos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), companyId);
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