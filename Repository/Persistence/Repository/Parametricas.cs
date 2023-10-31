using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Security.Claims;

namespace Repository.Persistence.Repository
{
    public class Parametricas : IParametricas
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;

        public Parametricas(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }

        public Responses Consultar(ClaimsIdentity identity, string tabla, string? ipAddress)
        {

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (tabla == null)
            {
                tabla = "";
            }

            var parametricas = from r in _context.AdmintT008Parametricas
                               where r.A008parametrica.Contains(tabla)
                               group r by r.A008parametrica into g
                               select new
                               {
                                   tablas = g.Key
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas.ToList(), token, false);

        }

        public Responses Consultar(ClaimsIdentity identity, string? ipAddress, string? cadenaBusqueda, int? pagina)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }
            var par = from p in _context.AdmintT008Parametricas
                      select p;

            if (!String.IsNullOrEmpty(cadenaBusqueda))
            {
                par = par.Where(x => (x.A008parametrica.Contains(cadenaBusqueda) || x.A008valor.Contains(cadenaBusqueda)) && x.A008modulo == "CUPOS");
            }
            else
            {
                par = par.Where(x => x.A008modulo == "CUPOS");
            }

            return ResponseManager.generaRespuestaGenerica("", par.ToList(), token, false);
        }

        public Responses Crear(ClaimsIdentity identity, ReqParametric parametrica, string? ipAddress)
        {
            string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var now = DateTime.UtcNow;

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var result = _context.AdmintT008Parametricas.SingleOrDefault(x => x.A008parametrica == parametrica.name && x.A008valor == parametrica.value);

            if (result != null)
            {
                return ResponseManager.generaRespuestaGenerica("Ya existe el valor " + parametrica.value + " para la tabla " + parametrica.name + ", validar.", "", token, true);
            }

            if ((parametrica.name != null) && (parametrica.value != null) && (parametrica.estate != null))
            {
                AdmintT008Parametrica par = new AdmintT008Parametrica();
                par.A008parametrica = parametrica.name;
                par.A008valor = parametrica.value;
                par.A008descripcion = parametrica.description;
                par.A008modulo = "CUPOS";
                par.A008fechaCreacion = now;
                par.A008fechaModificacion = now;
                par.A008codigoUsuarioCreacion = Decimal.Parse(codigoUsuario);
                par.A008codigoUsuarioModificacion = Decimal.Parse(codigoUsuario);
                par.A008estadoRegistro = parametrica.estate == 72 ? 72 : 73;

                _context.AdmintT008Parametricas.Add(par);
                _context.SaveChanges();

                logManager.crearAuditoria(ipAddress ?? "", codigoUsuario, 2, ModuleManager.smParametricas, "", "", "", par, par.A008valor);

                return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }

        public Responses Actualizar(ClaimsIdentity identity, ReqParametric parametrica, string? ipAddress)
        {
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (parametrica.code != null)
            {
                var result = _context.AdmintT008Parametricas.SingleOrDefault(x => x.PkT008codigo == parametrica.code);

                if (result != null) //lo encontro y lo actualiza
                {
                    result.A008fechaModificacion = now;

                    if (result.A008parametrica != parametrica.name)
                    {
                        valorAnterior.Add(result.A008parametrica);
                        valorActual.Add(parametrica.name ?? "");
                        campos.Add("A008parametrica");
                    }
                    result.A008parametrica = parametrica.name ?? "";

                    if (result.A008descripcion != parametrica.description)
                    {
                        valorAnterior.Add(result.A008descripcion ?? "");
                        valorActual.Add(parametrica.description ?? "");
                        campos.Add("A008descripcion");
                    }
                    result.A008descripcion = parametrica.description;

                    if (result.A008valor != parametrica.value)
                    {
                        valorAnterior.Add(result.A008valor);
                        valorActual.Add(parametrica.value ?? "");
                        campos.Add("A008valor");
                    }
                    result.A008valor = parametrica.value ?? "";

                    decimal estado = parametrica.estate == 72 ? 72 : 73;

                    if (result.A008estadoRegistro != estado)
                    {
                        valorAnterior.Add(Convert.ToString(result.A008estadoRegistro));
                        valorActual.Add(Convert.ToString(estado));
                        campos.Add("A011estadoRegistro");
                    }
                    result.A008estadoRegistro = estado;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress ?? "", codigoUsuario, 3, ModuleManager.smParametricas, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A008valor ?? "");
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }

        public Responses ActivarInactivar(ClaimsIdentity identity, ReqParametric parametrica, string? ipAddress)
        {
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (parametrica.code != null)
            {
                var result = _context.AdmintT008Parametricas.SingleOrDefault(x => x.PkT008codigo == parametrica.code);

                if (result != null)
                {
                    result.A008fechaModificacion = now;

                    decimal estado = parametrica.estate == 72 ? 72 : 73;

                    if (result.A008estadoRegistro != estado)
                    {
                        valorAnterior.Add(Convert.ToString(result.A008estadoRegistro));
                        valorActual.Add(Convert.ToString(estado));
                        campos.Add("A011estadoRegistro");
                    }
                    result.A008estadoRegistro = estado;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress ?? "", codigoUsuario, 3, ModuleManager.smParametricas, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A008valor);
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }


        public Responses ConsultarTipoDocumento(ClaimsIdentity identity, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT008Parametricas
                               where r.A008parametrica == "TIPO DE DOCUMENTO " && r.A008estadoRegistro == StringHelper.estadoActivo
                               select new
                               {
                                   id = r.PkT008codigo,
                                   type = r.A008valor
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);

        }

        public Responses ConsultarParametrica(ClaimsIdentity identity, string parametrica, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT008Parametricas
                               where r.A008parametrica == parametrica && r.A008estadoRegistro == StringHelper.estadoActivo
                               orderby r.A008valor ascending
                               select new
                               {
                                   id = r.PkT008codigo,
                                   type = r.A008valor
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);

        }

        public Responses ConsultarPaises(ClaimsIdentity identity, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT002Pais
                               where r.A002estadoRegistro == StringHelper.estadoActivo
                               orderby r.A002nombre ascending
                               select new
                               {
                                   codigo = r.PkT002codigo,
                                   nombre = r.A002nombre
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);
        }

        public Responses ConsultarDepartamentos(ClaimsIdentity identity, int idpais, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT003Departamentos
                               where r.A003codigoPais == idpais && r.A003estadoRegistro == StringHelper.estadoActivo
                               orderby r.A003nombre ascending
                               select new
                               {
                                   codigo = r.PkT003codigo,
                                   nombre = r.A003nombre
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);
        }

        public Responses ConsultarCiudades(ClaimsIdentity identity, int iddepartamento, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT004Ciudads
                               where r.A004codigoDepartamento == iddepartamento && r.A004estadoRegistro == StringHelper.estadoActivo
                               orderby r.A004nombre ascending
                               select new
                               {
                                   codigo = r.PkT004codigo,
                                   nombre = r.A004nombre
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);
        }

        public Responses ConsultarEstadoCertificado(ClaimsIdentity identity, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT008Parametricas
                               where r.A008parametrica == "ESTADO CERTIFICADO" && r.A008estadoRegistro == StringHelper.estadoActivo
                               select new
                               {
                                   codigo = r.PkT008codigo,
                                   nombre = r.A008valor
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);
        }

        public Responses ConsultarDependencia(ClaimsIdentity identity, string? ipAddress)
        {
            var parametricas = from r in _context.AdmintT008Parametricas
                               where r.A008parametrica == "DEPENDENCIA" && r.A008estadoRegistro == StringHelper.estadoActivo
                               select new
                               {
                                   codigo = r.PkT008codigo,
                                   nombre = r.A008valor
                               };

            return ResponseManager.generaRespuestaGenerica("", parametricas, "", false);
        }

        public Responses ConsultarParametros(ClaimsIdentity identity, string? ipAddress, string parametro)
        {
            var parametricas = (from r in _context.AdmintT008Parametricas
                               where r.A008parametrica.Contains(parametro) && r.A008estadoRegistro == StringHelper.estadoActivo
                               select new
                               {
                                    r.A008parametrica
                               }).Distinct();

            return ResponseManager.generaRespuestaGenerica("", parametricas.Take(10).ToList(), "", false);
        }

    }
}
