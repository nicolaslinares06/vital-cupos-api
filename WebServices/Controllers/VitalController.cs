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
    public class VitalController : Controller
    {
        private readonly Usuario usuario;
        private string storedToken;
        private AuthenticationResult storedUser;

        public VitalController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            usuario = new Usuario(context, jwtAuthenticationManager);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] ReqLogin req)
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = address.AddressList.FirstOrDefault()?.ToString() ?? string.Empty;
            var r = usuario.Authenticate(req, ipAddress);
            storedToken = r.Token ?? "";

            return Ok(storedToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("AuthUsers")]
        public IActionResult AuthUsers([FromBody] ReqLogin req)
        {
            if (req.user == "26032011" && req.password == "contraseña_esperada")
            {
                var result = new AuthenticationResult
                {
                    Code = "05",
                    Status = "Success",
                    Permissions = "AFL",
                    ID = 429,
                    User = "26032011",
                    Name = "USUARIO ANLA",
                    Document = "26032011",
                    EMail = "verdeneruda@gmail.com",
                    LastLogin = DateTime.Now,
                    Active = "T",
                    Enabled = "T",
                    Module = "AFL",
                    Url = "http://test-sunl-api.minambiente.gov.co/api/",
                    Token = Guid.NewGuid(),
                    UrlError = "http://vital.minambiente.gov.co/"
                };

                ValidateTokenAuth(storedToken, storedUser);

                storedUser = result;

                return Ok(storedUser);
            }
            else
            {
                var errorResult = new
                {
                    message = "Usuario no encontrado"
                };

                return NotFound(errorResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private IActionResult ValidateTokenAuth(string token, AuthenticationResult user)
        {
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(user.Code))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }

    public class AuthenticationResult
    {
        public string Code { get; set; } = "";
        public string Status { get; set; } = "";
        public string Permissions { get; set; } = "";
        public int ID { get; set; }
        public string User { get; set; } = "";
        public string Name { get; set; } = "";
        public string Document { get; set; } = "";
        public string EMail { get; set; } = "";
        public DateTime LastLogin { get; set; }
        public string Active { get; set; } = "";
        public string Enabled { get; set; } = "";
        public string Module { get; set; } = "";
        public string Url { get; set; } = "";
        public Guid Token { get; set; }
        public string UrlError { get; set; } = "";
    }

}
