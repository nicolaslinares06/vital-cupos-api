using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Persistence.IRepository;
using System.Security.Claims;
using static Repository.Helpers.Models.PaginatioModels;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinatorAssignRequestAnalystGpnController : ControllerBase
    {
        private readonly ILogger<CoordinatorAssignRequestAnalystGpnController> _logger;
        private readonly ICoordinatorAssignRequestAnalystsGpnRepository repositoryCoordinadorPrecinto;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinatorAssignRequestAnalystsGPNRepository"></param>
        /// <param name="logger"></param>
        public CoordinatorAssignRequestAnalystGpnController(ICoordinatorAssignRequestAnalystsGpnRepository coordinatorAssignRequestAnalystsGPNRepository, ILogger<CoordinatorAssignRequestAnalystGpnController> logger)
        {
            this.repositoryCoordinadorPrecinto = coordinatorAssignRequestAnalystsGPNRepository;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestStatusType"></param>
        /// <returns></returns>
        [HttpGet("ConsultRequerimentsSeals")]
        public async Task<IActionResult> ConsultarSolicitudes(string requestStatusType)
        {
            var responses = new Repository.Helpers.Responses();
            
            try
            {
                _logger.LogInformation("method called");
                responses = await repositoryCoordinadorPrecinto.ConsultarSolicitudes(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestStatusType);
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
        /// <param name="requestStatusType"></param>
        /// <returns></returns>
        [HttpPost("ConsultRequerimentsSealsWithPages")]
        public async Task<IActionResult> ConsultarSolicitudesConPagination(decimal requestStatusType, ParamsPaginations parametros)
        {
            var responses = new Repository.Helpers.Responses();

            try
            {
                _logger.LogInformation("method called");
                if(!String.IsNullOrEmpty(parametros.FilterCriterium))
                     responses = await repositoryCoordinadorPrecinto.ConsultarSolicitudesPorFiltro(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestStatusType, parametros);
                else
                    responses = await repositoryCoordinadorPrecinto.ConsultarSolicitudesPagination(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestStatusType, parametros);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultUsersAnalists")]
        public async Task<IActionResult> ConsultarAnalistas(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarAnalistas(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="requestCode"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        [HttpGet("ConsultRequerimentFile")]
        public async Task<IActionResult> ConsultarArchivoPrecinto(decimal requestCode, decimal documentType)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarDocumentoSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode, documentType);
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
        /// <param name="requestCode"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        [HttpGet("ConsultFiles")]
        public async Task<IActionResult> ConsultarArchivos(decimal requestCode, decimal documentType)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarDocumentosSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode, documentType);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultRequerimentSeal")]
        public async Task<IActionResult> ConsultarDatosSolicitudCupo(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarDatosSolicitudCupo(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultUserAnalistRequeriment")]
        public async Task<IActionResult> ConsultarAnalistaSolicitud(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarAnalistaSolicitudAsignado(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultRequerimentUnit")]
        public async Task<IActionResult> ConsultarDatosSolicitud(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarDatosSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultRequerimenWithdrawal")]
        public async Task<IActionResult> ConsultaSolicitudDesistido(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarDatosDesistimientoSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultSealNumbers")]
        public async Task<IActionResult> ConsultarNumeracionesSolicitud(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarNumeracionesSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="requestCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultTypesFractions")]
        public async Task<IActionResult> ConsultarTiposFraccionesSolicitud(decimal requestCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ConsultarTiposFraccionesSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestCode);
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
        /// <param name="assignmentAnalyst"></param>
        /// <returns></returns>
        [HttpPut("UpdateIdAnalyst")]
        public async Task<IActionResult> ActualizarIdAnalista(AnalystAssignmentUpdate assignmentAnalyst)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ActualizarIdAnalistaSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), assignmentAnalyst.RequestCode, assignmentAnalyst.AnalystId);
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
        /// <param name="requestStatus"></param>
        /// <returns></returns>
        [HttpPut("UpdateRequerimentState")]
        public async Task<IActionResult> ActualizarEstadoSolicitud(RequestStatusUpdate requestStatus)
        {
            try
            {
                _logger.LogInformation("method called");
                var responses = await repositoryCoordinadorPrecinto.ActualizarEstadoSolicitud(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), requestStatus.RequestId, requestStatus.RequestStatus ?? "");
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
        public class AnalystAssignmentUpdate
        {
            public decimal AnalystId { get; set; }
            public decimal RequestCode { get; set; }

        }
        /// <summary>
        /// 
        /// </summary>
        public class RequestStatusUpdate
        {
            public decimal RequestId { get; set; }
            public string? RequestStatus { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public class PropsEnvioCorreoCoordinador
        {
            public decimal IdSolicitud { get; set; }
            public string? Asunto { get; set; }
            public string? Correo { get; set; }
            public string? Body { get; set; }
        }
    }
}
