using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Helpers;
using Repository.Models;
using Repository.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using System.Security.Claims;
using Repository.Helpers.Models;
using System.Net;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly Usuario usuario;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public UserController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<UserController> logger)
        {
            usuario = new Usuario(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] ReqLogin req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.Authenticate(req, ipAddress);
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
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ReqChangePassword req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.CambiaContrasena(req, ipAddress);
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
        [HttpPost("SendEmailChangePassword")]
        public IActionResult SendEmailChangePassword([FromBody] ReqSimpleUser req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.enviaEmailRecuperarContrasena(req, ipAddress);
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
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("ConsultTerms")]
        public IActionResult ConsultTerminos([FromQuery] string? login)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.ConsultarTerminos(login, ipAddress);
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
        /// <param name="stringSearch"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Consult")]
        public IActionResult Consult([FromQuery] string? stringSearch)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.ConsultarTodos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, stringSearch);
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
        [HttpGet("ConsultEdit")]
        public IActionResult ConsultEdit([FromQuery] decimal id)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.ConsultarEdit(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, id);
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
        public IActionResult Actualizar([FromBody] ReqUser req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.Actualizar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        [HttpPost("Create")]
        public IActionResult Create([FromBody] ReqUser req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = usuario.Create(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
