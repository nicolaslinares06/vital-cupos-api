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
    public class SealRecordsController : ControllerBase
    {
        private readonly ILogger<SealRecordsController> _logger;
        private readonly IReportesPrecintosRepository reportesPrecintos;

        public SealRecordsController(IReportesPrecintosRepository reportesPrecintos, ILogger<SealRecordsController> logger)
        {
            this.reportesPrecintos = reportesPrecintos;
            _logger = logger;
        }

        [HttpPost("ConsultDataSeals")]
        public async Task<IActionResult> ConsultarDatosPrecintos(SealFilters filters)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await reportesPrecintos.ConsultarDatosPrecintos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), filters);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

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
