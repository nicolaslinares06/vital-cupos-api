using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Security.Claims;
using static Repository.Helpers.Models.ReportesMarquillasModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsReportsController : ControllerBase
    {
        private readonly ReportesMarquillasRepository reportes;
        public TagsReportsController(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            reportes = new ReportesMarquillasRepository(context, jwtAuthenticationManager);
        }

        [Authorize]
        [HttpPost("ConsultDataTags")]
        public IActionResult ConsultarDatosMarquillas(TagsFilters filtros)
        {
            var responses = reportes.ConsultarMarquillas(HttpContext.User.Identity as ClaimsIdentity ?? new ClaimsIdentity(), filtros);
            return Ok(responses);
        }
    }
}
