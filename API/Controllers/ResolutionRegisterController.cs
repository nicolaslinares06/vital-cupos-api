using API.Helpers;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Persistence.Repository;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using Web.Models;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResolutionRegisterController : ControllerBase
    {
        private readonly ILogger<ResolutionRegisterController> _logger;
        private readonly ResolutionRegister ResolutionRegister;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public ResolutionRegisterController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<ResolutionRegisterController> logger)
        {
            ResolutionRegister = new ResolutionRegister(context, jwtAuthenticationManager);
            _logger = logger;
        }
        /// <summary>
        /// consultar datos entidad o empresa
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultEntityDates")]
        public IActionResult ConsultEntityDates(decimal documentType, string nitBussines, decimal? entityType)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = ResolutionRegister.ConsultEntityDates(identity, documentType, nitBussines, entityType);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar resiolucion cupos
        /// </summary>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultQuotas")]
        public IActionResult ConsultQuotas(string nitBussines)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.ConsultQuotas(identity, nitBussines, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar una resolcuion cupo por numero de resolucion
        /// </summary>
        /// <param name="resolutionNumber"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("SearchQuotas")]
        public IActionResult SearchQuotas(decimal resolutionNumber)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.SearchQuotas(identity, resolutionNumber, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar inventario
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultInventory")]
        public IActionResult ConsultInventory()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.ConsultInventory(identity, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        

        /// <summary>
        /// consultar tipo de marcado
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultEntityTypes")]
        public IActionResult ConsultEntityTypes()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = ResolutionRegister.ConsultEntityTypes(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar tipo de marcado
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultMarkingType")]
        public IActionResult ConsultMarkingType()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = ResolutionRegister.ConsultMarkingType(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar pagos de repoblacion
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultRepoblationPay")]
        public IActionResult ConsultRepoblationPay()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = ResolutionRegister.ConsultRepoblationPay(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar tipos especimenes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultEspecimensTypes")]
        public IActionResult ConsultEspecimensTypes()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = ResolutionRegister.ConsultEspecimensTypes(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar una resolucion cupo
        /// </summary>
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultOneQuota")]
        public IActionResult ConsultOneQuota(decimal quotaCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.ConsultOneQuota(identity, quotaCode, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// editar una resolucion cupo
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("EditDeleteResolutionQuota")]
        public IActionResult EditDeleteResolutionQuota([FromBody] SaveResolutionQuotas data)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.EditDeleteResolutionQuota(identity, data, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// guardar nueva resolucion cupos
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("saveResolutionQuota")]
        public IActionResult saveResolutionQuota([FromBody] SaveResolutionQuotas data)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.saveResolutionQuota(identity, data, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// deshabilitar resolucion cupo
        /// </summary>
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("DisableResolution")]
        public IActionResult DisableResolution(decimal quotaCode)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = ResolutionRegister.DisableResolution(identity, quotaCode, ipAddress);
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
