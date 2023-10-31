using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Security.Claims;

namespace Repository.Persistence.Repository
{
    public class TechnicalAdmin : IAdminTecnica
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;

        public TechnicalAdmin(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }

        public Responses Consultar(ClaimsIdentity identity, string? valor, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (valor == null)
            {
                valor = "";
            }

            var admin = _context.AdmintT007AdminTecnicas
                .Join(
                _context.AdmintT008Parametricas,
                admtec => admtec.a007estadoRegistro,
                par => par.PkT008codigo,
                (admtec, par) => new
                {
                    admtec.pkT007Codigo,
                    admtec.a007nombre,
                    admtec.a007valor,
                    admtec.a007descripcion,
                    a007estadoRegistro = par.A008valor,
                    admtec.a007modulo
                });
            
            admin = admin.Where(x => x.a007modulo.Contains("CUPOS") && (x.a007descripcion.Contains(valor) || x.a007nombre.Contains(valor)));

            return ResponseManager.generaRespuestaGenerica("", admin.ToList(), token, false);
        }

        public Responses Listar(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var admin = _context.AdmintT007AdminTecnicas.Select(s => new
            {
                s.a007nombre
            }).Distinct();

            return ResponseManager.generaRespuestaGenerica("", admin.ToList(), token, false);
        }

        public Responses Actualizar(ClaimsIdentity identity, TechnicalAdminReq req, string ipAddress)
        {
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.UtcNow;

            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (req != null && req.code != 0)
            {
                var result = _context.AdmintT007AdminTecnicas.SingleOrDefault(x => x.pkT007Codigo == req.code);

                if (result != null)
                {
                    if (req.name != "")
                    {
                        if (result.a007nombre != req.name)
                        {
                            valorAnterior.Add(result.a007nombre);
                            valorActual.Add(req.name);
                            campos.Add("A007nombre");
                        }
                        result.a007nombre = req.name;
                    }
                    if (req.description != null)
                    {
                        if (result.a007descripcion != req.description)
                        {
                            valorAnterior.Add(result.a007descripcion);
                            valorActual.Add(req.description.ToString());
                            campos.Add("A007descripcion");
                        }
                        result.a007descripcion = req.description;
                    }
                    if (req.value != null)
                    {
                        if (result.a007valor != req.value)
                        {
                            valorAnterior.Add(result.a007valor);
                            valorActual.Add(req.value);
                            campos.Add("A007valor");
                        }
                        result.a007valor = req.value;
                    }
                    
                        decimal aux = req.registrationStatus ? StringHelper.estadoActivo : StringHelper.estadoInactivo;
                        if (result.a007estadoRegistro != aux)
                        {
                            valorAnterior.Add(result.a007estadoRegistro.ToString());
                            valorActual.Add(aux.ToString());
                            campos.Add("A007estadoRegistro");
                        }
                        result.a007estadoRegistro = aux;
                    
                    result.a007fechaModificacion = now;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smAdminTecnica, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.a007nombre);
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);

                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgCodigoEstadoNoEncontrado, "", token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCodigoEstadoNoEncontrado, "", token, true);
            }

        }

        public Responses Eliminar(ClaimsIdentity identity, ReqId req, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (req.id != 0)
            {
                var result = _context.AdmintT007AdminTecnicas.SingleOrDefault(x => x.pkT007Codigo == req.id);

                if (result != null) //lo encontro y lo elimina
                {
                    _context.Remove(result);
                    _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smAdminTecnica, "", "", "", result, result.a007nombre);
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgEliminadoExitoso, "", token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEliminar, "", token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEliminar, "", token, true);
            }

        }

        public Responses Crear(ClaimsIdentity identity, TechnicalAdminReq req, string ipAddress)
        {
            string codigoValor = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var now = DateTime.UtcNow;

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            AdmintT007AdminTecnica adminTecnica = new AdmintT007AdminTecnica();
            adminTecnica.a007nombre = req.name;
            adminTecnica.a007descripcion = req.description;
            adminTecnica.a007valor = req.value;
            adminTecnica.a007fechaCreacion = now;
            adminTecnica.a007fechaModificacion = now;
            adminTecnica.a007codigoUsuarioCreacion = Decimal.Parse(codigoValor);
            adminTecnica.a007codigoUsuarioModificacion = Decimal.Parse(codigoValor);
            adminTecnica.a007estadoRegistro = StringHelper.estadoActivo;
            adminTecnica.a007modulo = "CUPOS";

            _context.AdmintT007AdminTecnicas.Add(adminTecnica);
            int r = _context.SaveChanges();

            logManager.crearAuditoria(ipAddress, codigoValor, 2, ModuleManager.smRoles, "", "", "", adminTecnica, adminTecnica.a007nombre);

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
        }

        public Responses ConsultarValoresTecnicos(ClaimsIdentity identity, string ipAddress, string parametro)
        {
            var parametricas = (from r in _context.AdmintT007AdminTecnicas
                                where r.a007nombre.Contains(parametro) && r.a007estadoRegistro == StringHelper.estadoActivo && r.a007modulo.Contains("CUPOS")
                                select new
                                {
                                    etapa = r.a007nombre
                                }).Distinct();

            return ResponseManager.generaRespuestaGenerica("", parametricas.Take(10).ToList(), "", false);
        }
    }
}