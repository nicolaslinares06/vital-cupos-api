using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace WebServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WSUpdatingInformationQuotasSealsTagsController : Controller
	{
        private readonly ILogger<WSUpdatingInformationQuotasSealsTagsController> _logger;
        private readonly WSUpdatingInformationQuotasSealsTags WS_UpdatingInformationQuotasSealsTagsController;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="jwtAuthenticationManager"></param>
		/// <param name="logger"></param>
		public WSUpdatingInformationQuotasSealsTagsController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<WSUpdatingInformationQuotasSealsTagsController> logger)
		{
			WS_UpdatingInformationQuotasSealsTagsController = new WSUpdatingInformationQuotasSealsTags(context, jwtAuthenticationManager);
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
		public IActionResult UpdatingInformationQuotasSealsTags([FromQuery] int nit, int cupos, string nombreCientifico, int idnombreCientifico, string nombreComun,
			decimal numeroInicialPrecinto, decimal numeroFinalPrecinto, decimal numeroInicialMarquilla, decimal numeroFinalMarquilla)
		{
            try
			{
				_logger.LogInformation("Consult method called");
                var r = WS_UpdatingInformationQuotasSealsTagsController.UpdatingInformationQuotasSealsTags(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), "",
                 nit, cupos, nombreCientifico, idnombreCientifico, nombreComun, numeroInicialPrecinto, numeroFinalPrecinto, numeroInicialMarquilla, numeroFinalMarquilla);
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
