using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tsp;
using Repository;
using Repository.Helpers;
using Repository.Persistence.Repository;
using System.Security.Claims;
using Web.Models;
using static Repository.Helpers.Models.PermitResolution;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlFishRequestController : ControllerBase
    {
        private readonly ILogger<ControlFishRequestController> _logger;
        private readonly ControlFishRequest controlFishRequest;

        public ControlFishRequestController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<ControlFishRequestController> logger)
        {
            controlFishRequest = new ControlFishRequest(context, jwtAuthenticationManager);
            _logger = logger;   
        }

        /// <summary>
        /// consultar datos entidad
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultEntityDates")]
        public IActionResult ConsultEntityDates(decimal documentType, decimal nitBussines)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = controlFishRequest.ConsultEntityDates(identity, documentType, nitBussines);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar resoluciones por empresa
        /// </summary>
        /// <param name="codeBussines"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultPermitsReslution")]
        public IActionResult ConsultPermitsReslution(decimal codeBussines)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = controlFishRequest.ConsultPermitsReslution(identity, codeBussines);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar resolucion por codigo
        /// </summary>
        /// <param name="codeReslution"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultOnePermitResolution")]
        public IActionResult ConsultOnePermitResolution(decimal codeReslution)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = controlFishRequest.ConsultOnePermitResolution(identity, codeReslution);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// guardar nueva resolucuion
        /// </summary>
        /// <param name="resolution"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveResolution")]
        public IActionResult SaveResolution([FromBody] ResolucionPermisos resolution)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = controlFishRequest.SaveResolution(identity, resolution);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// editar resolucion
        /// </summary>
        /// <param name="resolution"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("EditResolution")]
        public IActionResult EditResolution([FromBody] ResolucionPermisos resolution)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = controlFishRequest.EditResolution(identity, resolution);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// deshabilitar resolucion
        /// </summary>
        /// <param name="codeResolution"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteResolution")]
        public IActionResult DeleteResolution([FromBody] decimal codeResolution)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = controlFishRequest.DeleteResolution(identity, codeResolution);
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
