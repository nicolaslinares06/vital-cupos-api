using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using Web.Models;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonTimberFloraCertificateController : ControllerBase
    {
        private readonly ILogger<NonTimberFloraCertificateController> _logger;
        private readonly NonTimberFloraCertificate NonTimberFloraCertificate;
        public NonTimberFloraCertificateController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<NonTimberFloraCertificateController> logger)
        {
            NonTimberFloraCertificate = new NonTimberFloraCertificate(context, jwtAuthenticationManager);
            _logger = logger;
        }

        /// <summary>
        /// consultar autoridades
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultAuthority")]
        public IActionResult ConsultAuthority()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultAuthority(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar tipos productos
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultEspecimensProductsType")]
        public IActionResult ConsultEspecimensProductsType()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultEspecimensProductsType(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar tipos de documentos
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultDocumentsTypes")]
        public IActionResult ConsultDocumentsTypes()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultDocumentsTypes(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar certificados
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultCertificates")]
        public IActionResult ConsultCertificates()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultCertificates(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar datos de la entidad
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultEntityDates")]
        public IActionResult ConsultEntityDates(decimal documentType, decimal nitBussines)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultEntityDates(identity, documentType, nitBussines);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar certificado por nit, numero certificado o ambas
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <param name="CertificateNumber"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultCertificatesForNit")]
        public IActionResult ConsultCertificatesForNit(decimal documentType, decimal nitBussines=0, string CertificateNumber = "0")
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultCertificatesForNit(identity, documentType, nitBussines, CertificateNumber);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar tipos de especimenes
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
                r = NonTimberFloraCertificate.ConsultEspecimensTypes(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// guardra nuevo certificado
        /// </summary>
        /// <param name="datosGuardar"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveCertificate")]
        public IActionResult SaveCertificate([FromBody] CertificateData datosGuardar)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.SaveCertificate(identity, datosGuardar, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// consultar un certificado por codigo
        /// </summary>
        /// <param name="codeCertificate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultDatasCertificate")]
        public IActionResult ConsultDatasCertificate(decimal codeCertificate)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.ConsultDatasCertificate(identity, codeCertificate);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// editar certificado
        /// </summary>
        /// <param name="dataToSave"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveEditCertificate")]
        public IActionResult SaveEditCertificate([FromBody] CertificateData dataToSave)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.SaveEditCertificate(identity, dataToSave);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// deshabilitar certificado
        /// </summary>
        /// <param name="codeCertificate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteCertificate")]
        public IActionResult DeleteCertificate([FromBody] decimal codeCertificate)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = NonTimberFloraCertificate.DeleteCertificate(identity, codeCertificate);
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
