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
    public class DocumentManagementController : Controller
    {
        private readonly GestionDocumental gestionDocumental;
        private readonly ILogger<DocumentManagementController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public DocumentManagementController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<DocumentManagementController> logger)
        {
            gestionDocumental = new GestionDocumental(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Consult")]
        public IActionResult Consult([FromQuery] string? searchString)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = gestionDocumental.ConsultarDocumentos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, searchString);
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
        [HttpGet("GetDocument")]
        public IActionResult GetDocument([FromQuery] decimal id)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = gestionDocumental.GetFile(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, id);
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
        /// <param name="document"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveDocument")]
        public IActionResult EditResolution([FromBody] SaveDocumentRequest document)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = gestionDocumental.SaveDocument(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), document);
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
        [HttpGet("ReadDocument")]
        public IActionResult ReadDocument([FromQuery] decimal id)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = gestionDocumental.ReadDocument(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, id);
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
        /// <param name="documentData"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("UpdateDocument")]
        public IActionResult UpdateDocument([FromBody] UpdateDocument documentData)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                
                var r = gestionDocumental.updateDocument(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, documentData);
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
