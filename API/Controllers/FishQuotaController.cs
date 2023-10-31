using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.Repository;
using System.Security.Claims;
using static Repository.Persistence.Repository.ExceptionMiddleware;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishQuotaController : ControllerBase
    {
        private readonly ILogger<FishQuotaController> _logger;
        private readonly FishQuotaRepository FishQuotaRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        /// <param name="logger"></param>
        public FishQuotaController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager, ILogger<FishQuotaController> logger)
        {
            FishQuotaRepository = new FishQuotaRepository(context, jwtAuthenticationManager);
            _logger = logger;   
        }
        /// <summary>
        /// Obtiene cuotas para peces
        /// </summary>
        /// <param name="numberResolution"></param>
        /// <param name="vigency"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetFishesQuotas")]
        public IActionResult GetFishesQuotas(string? initialValidityDate, string? finalValidityDate, decimal numberResolution = 0)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = FishQuotaRepository.GetFishesQuotas(identity, initialValidityDate, finalValidityDate, numberResolution);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene cuotas para peces por code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetFishQuotaByCode")]
        public IActionResult GetFishQuotaByCode(decimal code)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();
            
            try
            {
                _logger.LogInformation("method called");
                r = FishQuotaRepository.GetFishQuotaByCode(identity, code);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// Guarda cuotas para peces
        /// </summary>
        /// <param name="fishQuota"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveFishQuota")]
        public IActionResult SaveFishQuota([FromBody] FishQuota fishQuota)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = FishQuotaRepository.SaveFishQuota(identity, fishQuota);
                return Ok(r);
            }
            catch (Exception ex)
            {
                //Manejo de excepciones
                                throw new MyCustomException($"An error occurred in the method {this.ControllerContext.ActionDescriptor.ActionName} of the controller {this.ControllerContext.ActionDescriptor.ControllerName}. {ex.Message}");
            }
        }
        /// <summary>
        /// Elimina cuotas para peces por code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("DeleteFishQuota")]
        public IActionResult DeleteFishQuota(int code)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = FishQuotaRepository.DeleteFishQuota(identity, code);
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
        /// <param name="fishQuota"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateFishQuota")]
        public IActionResult UpdateFishQuota([FromBody] FishQuota fishQuota)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity();
            Responses r = new Responses();

            try
            {
                _logger.LogInformation("method called");
                r = FishQuotaRepository.UpdateFishQuota(identity, fishQuota);
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
                r = FishQuotaRepository.GetSpecies(identity);
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
                r = FishQuotaRepository.GetSupportDocument(identity, code);
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
