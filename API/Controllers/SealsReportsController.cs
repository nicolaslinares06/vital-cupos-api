using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Persistence.IRepository;
using System.Security.Claims;
using static Repository.Helpers.Models.ReportesPrecintosModels;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SealsReportsController : ControllerBase
    {
        private readonly ILogger<SealsReportsController> _logger;
        private readonly IReportesPrecintosRepository reportesPrecintos;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportesPrecintos"></param>
        /// <param name="logger"></param>
        public SealsReportsController(IReportesPrecintosRepository reportesPrecintos, ILogger<SealsReportsController> logger)
        {
            this.reportesPrecintos = reportesPrecintos;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        [HttpPost("ConsultDataSeals")]
        public async Task<IActionResult> ConsultarDatosPrecintos(SealFilters filtros)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await reportesPrecintos.ConsultarDatosPrecintos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), filtros);
                return Ok(responses);
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
        [HttpGet("ConsultBussinesSeals")]
        public async Task<IActionResult> ConsultarEstablecimientos()
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await reportesPrecintos.ConsultarEstablecimientos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity());
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
    }
}
