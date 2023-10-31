using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Persistence.Repository;
using Repository.Helpers.Models;
using Web.Models;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotasReportsController : ControllerBase
    {
        private readonly ILogger<QuotasReportsController> _logger;
        private readonly QuotasReportsRepository QuotasReportRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public QuotasReportsController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<QuotasReportsController> logger)
        {
            QuotasReportRepository = new QuotasReportsRepository(context, jwtAuthenticationManager);
            _logger = logger;
        }

        /// <summary>
        /// consultar resoluciones
        /// </summary>
        /// <param name="resolutionNumber"></param>
        /// <param name="BussinesName"></param>
        /// <param name="BussinesNit"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultResolutions")]
        public IActionResult ConsultResolutions(string? resolutionNumber, string? BussinesName, string? BussinesNit, string? fromDate, string? toDate)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = QuotasReportRepository.ConsultResolutions(identity, resolutionNumber, BussinesName, BussinesNit, fromDate, toDate);
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
