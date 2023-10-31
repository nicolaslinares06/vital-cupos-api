using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers.Models;
using Repository.Persistence.Repository;
using System.Net;
using System.Security.Claims;

namespace WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WSCheckQuotasSealsLabelsController : Controller
    {
        private readonly WSCheckQuotasSealsLabels WS_CheckQuotasSealsLabels;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        public WSCheckQuotasSealsLabelsController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            WS_CheckQuotasSealsLabels = new WSCheckQuotasSealsLabels(context, jwtAuthenticationManager);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ConsultCheckQuotasSealsLabels")]
        public IActionResult ConsultCheckQuotasSealsLabels([FromQuery] int nit)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            var r = WS_CheckQuotasSealsLabels.ConsultCheckQuotasSealsLabels(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), ipAddress, nit);
            return Ok(r);
        }
    }
}
