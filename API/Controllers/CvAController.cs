using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Helpers.Models;
using Repository.Persistence.Repository;
using System.Reflection.Metadata;
using System.Security.Claims;
using Web.Models;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvAController : ControllerBase
    {
        private readonly ILogger<CvAController> _logger;
        private readonly Cvrepository CvModel;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public CvAController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<CvAController> logger)
        {
            CvModel = new Cvrepository(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentTypeCV"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet("Search")]
        public IActionResult Buscar(decimal documentTypeCV, string documentId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.Buscar(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), documentTypeCV, documentId);
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
        /// <param name="documentTypeCV"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet("Situation")]
        public IActionResult Situacion(decimal documentTypeCV, string documentId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.Situacion(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), documentTypeCV, documentId);
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
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet("QuotaResolution")]
        public IActionResult Resolucioncupos(string documentId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.Resolucioncupos(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), documentId);
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
        [HttpGet("ConsultCertificateshj")]
        public IActionResult ConsultCertificateshj()
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultCertificateshj(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity());
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
        /// <param name="certificateId"></param>
        /// <returns></returns>
        [HttpGet("ConsultCertificateshj2")]
        public IActionResult ConsultCertificateshj2(decimal certificateId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultCertificateshj2(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), certificateId);
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
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet("FishQuery")]
        public IActionResult ConsultPeces(decimal documentId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultPeces(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), documentId);
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
        /// <param name="nit"></param>
        /// <returns></returns>
        [HttpGet("SalesDocument")]
        public IActionResult DocumentoVenta(decimal nit)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.DocumentoVenta(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), nit);
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
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        [HttpGet("ConsultOneQuota2")]
        public IActionResult ConsultOneQuota(decimal quotaCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultOneQuota2(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), quotaCode);
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
        /// <param name="docId"></param>
        /// <returns></returns>
        [HttpGet("ConsultDocument2")]
        public IActionResult ConsultDocument2(decimal docId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultDocument2(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), docId);
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
        /// <param name="docId"></param>
        /// <returns></returns>
        [HttpGet("ConsultDocumentid")]
        public IActionResult ConsultDocumentid(decimal docId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultDocumentid(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), docId);
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
        /// <param name="companyCode"></param>
        /// <param name="situationId"></param>
        /// <returns></returns>
        [HttpGet("QuerySituationId")]
        public IActionResult ConsultSituacionid(decimal companyCode, decimal situationId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultSituacionid(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), companyCode, situationId);
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
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpGet("QueryNoveltySituation")]
        public IActionResult ConsultSituacionnovedad(decimal companyCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultSituacionnovedad(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), companyCode);
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
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpGet("QueryLatestNoveltySituation")]
        public IActionResult ConsultSituacionnovedadultima(decimal companyCode)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultSituacionnovedadultima(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), companyCode);
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
        /// <param name="situationId"></param>
        /// <returns></returns>
        [HttpGet("QuerySituationPDF")]
        public IActionResult ConsultSituacionpdf(decimal situationId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.ConsultSituacionpdf(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), situationId);
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
        /// <param name="resolutionId"></param>
        /// <returns></returns>
        [HttpGet("QueryFishPDF")]
        public IActionResult Consultpecespdf(decimal resolutionId)
        {
            try
            {
                _logger.LogInformation("method called");
                var r = CvModel.Consultpecespdf(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), resolutionId);
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
