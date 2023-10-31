using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WSConsultInformationQuotasSealsTagsController : Controller
    {
        private readonly ILogger<WSConsultInformationQuotasSealsTagsController> _logger;
        private readonly WSConsultInformationQuotasSealsTags WS_ConsultInformationQuotasSealsTagsController;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public WSConsultInformationQuotasSealsTagsController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<WSConsultInformationQuotasSealsTagsController> logger)
        {
            WS_ConsultInformationQuotasSealsTagsController = new WSConsultInformationQuotasSealsTags(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
		/// 
		/// </summary>
		/// <param name="nit"></param>
		/// <param name="cupos"></param>
		/// <param name="nombreCientifico"></param>
		/// <param name="idnombreCientifico"></param>
		/// <param name="nombreComun"></param>
		/// <param name="numeroInicialPrecinto"></param>
		/// <param name="numeroFinalPrecinto"></param>
		/// <param name="numeroInicialMarquilla"></param>
		/// <param name="numeroFinalMarquilla"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[Authorize]
        [HttpGet("UpdatingInformationQuotasSealsTags")]
        public IActionResult UpdatingInformationQuotasSealsTags([FromQuery] decimal cuposOcupados)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());

            try
            {
                _logger.LogInformation("Consult method called");
                var r = WS_ConsultInformationQuotasSealsTagsController.ConsultInformationQuotasSealsTags(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), cuposOcupados);
                return Ok(r);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the Consult method.");
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
    }
}
