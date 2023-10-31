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
    public class RolController : Controller
    {
        private readonly ILogger<RolController> _logger;
        private readonly Roles roles;
        public RolController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<RolController> logger)
        {
            roles = new Roles(context, jwtAuthenticationManager);
            _logger = logger;
        }

        [Authorize]
        [HttpGet("Consult")]
        public IActionResult Consult([FromQuery] string? searchQuery, [FromQuery] string? state)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = roles.ConsultarTodos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, searchQuery, state);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }

        }

        [Authorize]
        [HttpGet("ConsultRols")]
        public IActionResult ConsultRols()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.ConsultarRoles(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
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
        /// <exception cref="Exception"></exception>
        [AllowAnonymous]
        [HttpGet("ConsultRolsAssign")]
        public IActionResult ConsultRolsAssign()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.ConsultarRolesAsignar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("ConsultByRol")]
        public IActionResult ConsultByRol([FromQuery] int rol, [FromQuery] string charge, [FromQuery] bool state)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.Consultar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), rol, charge, state, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("UpdateFunctionality")]
        public IActionResult UpdateFunctionality([FromBody] RolModPermition req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.ActualizarFuncionalidades(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        public IActionResult Create([FromBody] ReqRol req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.Crear(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
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
        public IActionResult Update([FromBody] ReqRol req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.Actualizar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), req, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("ConsultAllRols")]
        public IActionResult ConsultAllRols(string parameter)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = roles.ConsultarTodosRoles(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, parameter);
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
