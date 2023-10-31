using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Persistence.Repository;
using Repository.Helpers.Models;
using Web.Models;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDocumentController : ControllerBase
    {
        private readonly ILogger<SaleDocumentController> _logger;
        private readonly SaleDocumentRepository SaleDocumentRepository;
        public SaleDocumentController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<SaleDocumentController> logger)
        {
            SaleDocumentRepository = new SaleDocumentRepository(context, jwtAuthenticationManager);
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el documento de venta por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetSaleDocumentId")]
        public IActionResult GetSaleDocuments(int code)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.GetSaleDocumentId(identity, code, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }

        }

        /// <summary>
        /// Obtiene los documentos de venta
        /// </summary>
        /// <param name="typeDocument"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetSaleDocuments")]
        public IActionResult GetSaleDocuments(string? typeDocument = null, string? documentNumber = null, string? numberCartaVenta=null)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.GetSaleDocuments(identity, ipAddress, typeDocument, documentNumber, numberCartaVenta);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los cupos por numero documento de la empresa o representante legal
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetQuotas")]
        public IActionResult GetQuotas(string documentNumber)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.GetQuotas(identity, documentNumber, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene las numeraciones por numero documento de la empresa y codigo cupo
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetQuotasNumeraciones")]
        public IActionResult GetQuotasNumeraciones(int codigoCupo, string documentNumber)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.GetQuotasNumeraciones(identity, codigoCupo, documentNumber, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Valida si estan disponibvles las numeraciones
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("ValidateNumbers")]
        public IActionResult ValidateNumbers(Seal numbers)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = SaleDocumentRepository.ValidateNumbers(identity, numbers);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el inventario
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetInventory")]
        public IActionResult GetInventory(string documentNumber, string? code=null)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = SaleDocumentRepository.GetInventory(identity, documentNumber, (code != null ? code : ""));
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene las especies
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetSpecies")]
        public IActionResult GetSpecies()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = SaleDocumentRepository.GetSpecies(identity);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Guarda el documento de venta
        /// </summary>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveSaleDocument")]
        public IActionResult SaveSaleDocument([FromBody] SaleDocumentModel saleDocument)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.SaveSaleDocument(identity, saleDocument, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Valida si la empresa se encuentra registrada
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <param name="business"></param>
        /// <param name="typeDocument"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ValidateCompany")]
        public IActionResult ValidateCompany(decimal company = 0, decimal typeDocument = 0, string documentNumber = "0")
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = SaleDocumentRepository.ValidateCompany(identity, company, typeDocument, documentNumber);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Filtra Empresa por numero de documento
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("SearchCompany")]
        public IActionResult SearchCompany(string number, decimal typeDocument = 0, decimal companyCode = 0)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = SaleDocumentRepository.SearchCompany(identity, number, typeDocument, companyCode);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un documento de venta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("DeleteSaleDocument")]
        public IActionResult DeleteSaleDocument(string id)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.DeleteSaleDocument(identity, id, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza el documento de venta
        /// </summary>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateSaleDocument")]
        public IActionResult UpdateSaleDocument([FromBody] SaleDocumentModel saleDocument)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.UpdateSaleDocument(identity, saleDocument, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el documento soporte
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetSupportDocument")]
        public IActionResult GetSupportDocument(decimal code)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.GetSupportDocument(identity, code, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los cupos por codigo de documento de venta
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetQuotasByCode")]
        public IActionResult GetQuotasByCode(decimal code)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.GetQuotasByCode(identity, code, ipAddress);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("SearchSeals")]
        public IActionResult SearchSeals(NumbersSeals data)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                var address = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = address.AddressList[1]?.ToString() ?? string.Empty;
                r = SaleDocumentRepository.SearchSeals(identity, data, ipAddress);
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
