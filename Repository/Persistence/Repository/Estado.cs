using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repository
{
    public class Estado : IEstado
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;

        public Estado(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }

        public Responses Consultar(ClaimsIdentity identity, string? nombre, string? estadoReg, int? codigoEstado, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (nombre == null)
            {
                nombre = "";
            }

            var estado = _context.CitestT008Estados
                .Join(
                _context.AdmintT008Parametricas,
                est => est.A008estadoRegistro,
                par => par.PkT008codigo,
                (est, par) => new
                {
                    est.PkT008codigo,
                    est.A008posicion,
                    est.A008codigoParametricaEstado,
                    est.A008descripcion,
                    est.A008etapa,
                    est.A008modulo,
                    par.A008valor
                }).Join(
                    _context.AdmintT008Parametricas,
                    ant => ant.A008codigoParametricaEstado,
                    par2 => par2.PkT008codigo,
                    (ant, par2) => new
                    {
                        ant.PkT008codigo,
                        ant.A008posicion,
                        A008codigoParametricaEstado = par2.A008valor,
                        ant.A008descripcion,
                        ant.A008etapa,
                        ant.A008modulo,
                        A008estadoRegistro = ant.A008valor
                    });

            estado = estado.Where(e => e.A008modulo == "CUPOS");

            if (!String.IsNullOrEmpty(nombre) || !String.IsNullOrEmpty(estadoReg))
            {
                if (!String.IsNullOrEmpty(nombre) && !String.IsNullOrEmpty(estadoReg))
                {
                    estado = estado.Where(x => (x.A008descripcion.Contains(nombre) || x.A008etapa.Contains(nombre)) && (x.A008estadoRegistro == estadoReg));
                }
                else
                {
                    if (!String.IsNullOrEmpty(nombre))
                    {
                        estado = estado.Where(x => x.A008descripcion.Contains(nombre) || x.A008etapa.Contains(nombre));
                    }
                    else
                    {
                        estado = estado.Where(x => x.A008estadoRegistro == estadoReg);
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("", estado.ToList(), token, false);
        }

        [ExcludeFromCodeCoverage]
        public Responses Actualizar(ClaimsIdentity identity, ReqEstado estado, string ipAddress)
        {
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var now = DateTime.Now;

            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (estado != null && estado.id != 0)
            {
                var result = _context.CitestT008Estados.SingleOrDefault(x => x.PkT008codigo == estado.id);

                if (result != null)
                {
                    if (estado.position != 0)
                    {
                        if (result.A008posicion != estado.position)
                        {
                            valorAnterior.Add(result.A008posicion.ToString());
                            valorActual.Add(estado.position.ToString());
                            campos.Add("A008posicion");
                        }
                        result.A008posicion = estado.position;
                    }
                    if (estado.description != null)
                    {
                        if (result.A008descripcion != estado.description)
                        {
                            valorAnterior.Add(result.A008descripcion);
                            valorActual.Add(estado.description.ToString());
                            campos.Add("A008descripcion");
                        }
                        result.A008descripcion = estado.description;
                    }
                    if (estado.stage != null)
                    {
                        if (result.A008etapa != estado.stage)
                        {
                            valorAnterior.Add(result.A008etapa.ToString());
                            valorActual.Add(estado.stage);
                            campos.Add("A008etapa");
                        }
                        result.A008etapa = estado.stage;
                    }
                    var resp = _context.AdmintT008Parametricas.SingleOrDefault(x => x.A008valor == estado.idEstate);
                    if (result.A008codigoParametricaEstado != (resp != null ? resp.PkT008codigo : 0))
                    {
                        valorAnterior.Add(result.A008codigoParametricaEstado.ToString());
                        valorActual.Add(estado.idEstate.ToString());
                        campos.Add("A012estadoSolicitud");
                    }

                    decimal estados = estado.estate ? StringHelper.estadoActivo : StringHelper.estadoInactivo;

                    if (result.A008estadoRegistro != estados)
                    {
                        valorAnterior.Add(result.A008estadoRegistro.ToString());
                        valorActual.Add(estados.ToString());
                        campos.Add("A008estadoRegistro");
                    }
                    result.A008estadoRegistro = estados;

                    result.A008codigoParametricaEstado = resp != null ? resp.PkT008codigo : 0;
                    result.A008fechaModificacion = now;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smEstado, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A008etapa);
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
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCodigoEstadoNoEncontrado, "", token, true);
            }

        }

        [ExcludeFromCodeCoverage]
        public Responses Eliminar(ClaimsIdentity identity, ReqId estado, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (estado.id != 0)
            {
                var result = _context.CitestT008Estados.SingleOrDefault(x => x.PkT008codigo == estado.id);

                if (result != null) //lo encontro y lo elimina
                {
                    _context.Remove(result);
                    _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smEstado, "", "", "", result, result.A008etapa);
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
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCodigoEstadoNoEncontrado, "", token, true);
            }

        }

        public Responses Crear(ClaimsIdentity identity, AdminStatesReq req, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var now = DateTime.Now;

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var result = _context.CitestT008Estados.Count();

            CitestT008Estado adminEstados = new CitestT008Estado();
            adminEstados.A008codigoUsuarioCreacion = Decimal.Parse(codigoUsuario);
            adminEstados.A008codigoUsuarioModificacion = Decimal.Parse(codigoUsuario);
            adminEstados.A008posicion = result + 1;
            adminEstados.A008codigoParametricaEstado = req.state;
            adminEstados.A008estadoRegistro = StringHelper.estadoActivo;
            adminEstados.A008descripcion = req.description;
            adminEstados.A008etapa = req.stage;
            adminEstados.A008fechaCreacion = now;
            adminEstados.A008fechaModificacion = now;
            adminEstados.A008modulo = "CUPOS";

            _context.CitestT008Estados.Add(adminEstados);
            _context.SaveChanges();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRoles, "", "", "", adminEstados, adminEstados.A008etapa);

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
        }

        public Responses ConsultarEstado (ClaimsIdentity identity, string ipAddress, string parametro)
        {
            var parametricas = (from r in _context.CitestT008Estados
                                where r.A008etapa.Contains(parametro) && r.A008estadoRegistro == StringHelper.estadoActivo && r.A008modulo == "CUPOS"
                                select new
                                {
                                    etapa = r.A008etapa
                                }).Distinct();

            return ResponseManager.generaRespuestaGenerica("", parametricas.Take(10).ToList(), "", false);
        }
    }
}
