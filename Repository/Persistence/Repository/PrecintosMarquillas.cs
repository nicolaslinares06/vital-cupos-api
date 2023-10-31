using API.Helpers;
using DocumentFormat.OpenXml.Bibliography;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.IRepository;
using System.Security.Claims;


namespace Repository.Persistence.Repository
{
    public class PrecintosMarquillas : IPrecintosMarquillas
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public PrecintosMarquillas(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public Responses Consultar(ClaimsIdentity identity, string ipAddress, string? tipoDocumento,
            string? fechaInicial, string? numero, string? numeroDocumento, string? fechaFinal, string? color,
            string? nombreEmpresa, string? vigencia, int? pagina)
        {
            Metodos met = new Metodos(_context);

            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var pym = from pm in _context.CuposV001Precintosymarquillas
                      select pm;
            fechaInicial = (fechaInicial == "1/1/0001 00:00:00") ? "" : fechaInicial;
            fechaFinal = (fechaFinal == "1/1/0001 00:00:00") ? "" : fechaFinal;

            if (tipoDocumento != null)
            {
                pym = pym.Where(x => x.TIPODOCUMENTO == (tipoDocumento.ToUpper()));
            }
            if (fechaInicial != null && fechaInicial != "")
            {
                pym = pym.Where(x => x.FECHAINICIAL >= Convert.ToDateTime(fechaInicial));
            }
            if (numero != null && numero != "0")
            {
                pym = pym.Where(x => x.NUMERORADICADO == numero);
            }
            if (numeroDocumento != null && numeroDocumento != "0")
            {
                pym = pym.Where(x => x.NUMERO == Convert.ToDecimal(numeroDocumento));
            }
            if (fechaFinal != null && fechaFinal != "")
            {
                pym = pym.Where(x => x.FECHAFINAL <= Convert.ToDateTime(fechaFinal));
            }
            if (color != null)
            {
                pym = pym.Where(x => x.COLOR == (color.ToUpper()));
            }
            if (nombreEmpresa != null)
            {
                pym = pym.Where(x => x.NOMBRE == (nombreEmpresa.ToUpper()));
            }
            if (vigencia != null)
            {
                pym = pym.Where(x => x.VIGENCIA.ToString().Contains(vigencia));
            }

            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smConsultaPrecintosMarquillas, null, null, null, 1, null, null);


            return ResponseManager.generaRespuestaGenerica("", pym.ToList(), token, false);

        }

        public Responses TipoDocumentoEmpresa(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var par = from pr in _context.AdmintT008Parametricas
                      select pr;

            par = par.Where(x => x.A008modulo.Contains("ADMIN") && x.A008parametrica.Contains("TIPO DE DOCUMENTO"));

            return ResponseManager.generaRespuestaGenerica("", par, token, false);

        }

        public Responses Color(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var par = from pr in _context.AdmintT008Parametricas
                      select pr;

            par = par.Where(x => x.A008parametrica.Contains("COLOR") && x.A008estadoRegistro == StringHelper.estadoActivo);

            return ResponseManager.generaRespuestaGenerica("", par, token, false);

        }
    }
}
