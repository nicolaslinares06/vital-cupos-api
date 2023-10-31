using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Claims;
using System.Net;
using Org.BouncyCastle.Utilities.Net;
using static Repository.Helpers.Models.ReportesPrecintosModels;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SealsLabelsController : Controller
    {
        private readonly ILogger<SealsLabelsController> _logger;
        private readonly PrecintosMarquillas precintosMarquillas;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public SealsLabelsController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<SealsLabelsController> logger)
        {
            precintosMarquillas = new PrecintosMarquillas(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="initialDate"></param>
        /// <param name="number"></param>
        /// <param name="documentNumber"></param>
        /// <param name="finalDate"></param>
        /// <param name="color"></param>
        /// <param name="companyName"></param>
        /// <param name="validity"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Consult")]
        public IActionResult Consult(FiltrosPrecintosMarquillas filtros)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            int page = 1;

            try
            {
                _logger.LogInformation("method called");
                var r = precintosMarquillas.Consultar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, filtros.documentType, filtros.initialDate.ToString(), filtros.number, filtros.documentNumber,    filtros.finalDate.ToString(), filtros.color, filtros.companyName, filtros.validity, page);
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
        [HttpGet("CompanyDocumentType")]
        public IActionResult CompanyDocumentType()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;

            try
            {
                _logger.LogInformation("method called");
                var r = precintosMarquillas.TipoDocumentoEmpresa(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
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
        [HttpGet("Colors")]
        public IActionResult Colors()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            
            try
            {
                _logger.LogInformation("method called");
                var r = precintosMarquillas.Color(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress);
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
