using API.Controllers;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Persistence.IRepository;
using System.Security.Claims;
using System.Security.Cryptography;
using Web.Models;
using static Repository.Helpers.Models.ReportesPrecintosModels;

public class SealsReportsControllerTests
{
    //Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
    private readonly DBContext _context;
    private readonly SealsReportsController controller;
    private readonly ClaimsIdentity user;
    public static SupportDocuments? documentoEnviar;
    public readonly IReportesPrecintosRepository reportes;

    public SealsReportsControllerTests()
    {
        var authenticationType = "AuthenticationTypes.Federation";

        user = new ClaimsIdentity(authenticationType);
        user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
        user.AddClaim(new Claim("aud", "CUPOS"));
        user.AddClaim(new Claim("exp", "1668005030"));
        user.AddClaim(new Claim("iat", "1668004130"));
        user.AddClaim(new Claim("nbf", "1668004130"));

        _context = new DBContext();

        controller = new SealsReportsController(reportes, new LoggerFactory().CreateLogger<SealsReportsController>());

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, "Administrador")
                }, "someAuthTypeName"))
            }
        };
    }

    [Fact]
    public void ConsultarDatosPrecintos()
    {

        var resolucion = _context.CupostT019Solicitudes.FirstOrDefault(x => x.A019EstadoRegistro != 0);
        var empresa = _context.CupostT001Empresas.FirstOrDefault(x => x.A001estadoRegistro != 0);

        var datos = new SealFilters
        {
            ResolutionNumber = resolucion?.A019NumeroRadicacion ?? "",
            Establishment = 0,
            NIT = empresa?.A001nit ?? 0,
            RadicationDate = DateTime.Now,
            SpecificSearch = 0
        };

        // Act
        var result = controller.ConsultarDatosPrecintos(datos);

        // Assert
        Assert.NotNull(result);
        // Add more assertions as needed
    }

    [Fact]
    public void ConsultarEstablecimientos()
    {
        // Act
        var result = controller.ConsultarEstablecimientos();
        // Assert
        Assert.NotNull(result);
        // Add more assertions as needed
    }
}
