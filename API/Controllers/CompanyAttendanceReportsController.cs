﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Persistence.IRepository;
using System.Security.Claims;
using static Repository.Helpers.Models.ReportesEmpresasMarcajeModels;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyAttendanceReportsController : ControllerBase
    {
        private readonly ILogger<CompanyAttendanceReportsController> _logger;
        private readonly IReportesEmpresasMarcaje reportesEmpresasMarcaje;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportesEmpresasMarcaje"></param>
        /// <param name="logger"></param>
        public CompanyAttendanceReportsController(IReportesEmpresasMarcaje reportesEmpresasMarcaje, ILogger<CompanyAttendanceReportsController> logger)
        {
            this.reportesEmpresasMarcaje = reportesEmpresasMarcaje;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("ConsultDataQuotaBussiness")]
        public async Task<IActionResult> ConsultDataQuotaBusiness([FromBody] BusinessFilters filters)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await reportesEmpresasMarcaje.ConsultarDatosEmpresas(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), filters);
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
