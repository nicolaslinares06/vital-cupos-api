using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Net;

namespace WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly Usuario usuario;
        public AuthenticateController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            usuario = new Usuario(context, jwtAuthenticationManager);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] ReqLogin req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            var r = usuario.Authenticate(req, ipAddress);
            return Ok(r);
        }
    }
}
