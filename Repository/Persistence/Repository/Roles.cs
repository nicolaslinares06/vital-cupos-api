using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repository
{
    public class Roles : IRoles
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;

        public Roles(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }
        public Responses ConsultarTodos(ClaimsIdentity identity, string ipAddress, string? cadenaBusqueda, string? estado)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }
            var usu = _context.AdmintT011Rols
                .Join(
                _context.AdmintT008Parametricas,
                usu => usu.A011estadoRegistro,
                para => para.PkT008codigo,
                (usu, para) => new
                {
                    id = usu.PkT011codigo,
                    name = usu.A011nombre,
                    cargo = usu.A011cargo,
                    estado = para.A008valor,
                    descripcion = usu.A011descripcion,
                    usu.A011modulo,
                });

            usu = usu.Where(x => x.A011modulo.Contains("CUPOS"));
            
            if (!String.IsNullOrEmpty(cadenaBusqueda) || !String.IsNullOrEmpty(estado))
            {
                if (!String.IsNullOrEmpty(estado) && !String.IsNullOrEmpty(cadenaBusqueda))
                {
                    usu = usu.Where(s => (s.name.Contains(cadenaBusqueda) || s.cargo.Contains(cadenaBusqueda)) && (s.estado == estado));
                }
                else
                {
                    if (!String.IsNullOrEmpty(estado))
                    {
                        usu = usu.Where(s => (s.estado == estado));
                    }
                    else
                    {
                        usu = usu.Where(s => s.name.Contains(cadenaBusqueda ?? "") || s.cargo.Contains(cadenaBusqueda ?? ""));
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("", usu, token, false);

        }
        public Responses ConsultarRoles(ClaimsIdentity identity, string ipAddress)
        {

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var roles = _context.AdmintT011Rols.Select(s => new
            {
                id = s.PkT011codigo,
                name = s.A011nombre,
                cargo = s.A011cargo,
                s.A011estadoRegistro,
                s.A011modulo
            });
            
            roles = roles.Where(x => x.A011estadoRegistro == StringHelper.estadoActivo && x.A011modulo.Contains("CUPOS"));

            return ResponseManager.generaRespuestaGenerica("", roles, token, false);

        }

        public Responses ConsultarRolesAsignar(ClaimsIdentity identity, string ipAddress)
        {
            var roles = from rols in _context.AdmintT011Rols
                        where rols.A011modulo.Contains("CITES") && rols.A011estadoRegistro == StringHelper.estadoActivo && rols.A011tipoUsuario == "EXTERNO"
                        select new
                        {
                            id = rols.PkT011codigo,
                            name = rols.A011nombre,
                            cargo = rols.A011cargo,
                            rols.A011estadoRegistro,
                            rols.A011modulo
                        };

            return ResponseManager.generaRespuestaGenerica("", roles, "", false);

        }
        public Responses Consultar(ClaimsIdentity identity, int rol, string cargo, bool estado, string ipAddress)
        {
            List<RolModPermition> permisos = new List<RolModPermition>();

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (rol == 0)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgRolObligatorio, "", token, true);
            }

            //Traemos todos los modulos
            var modulos = _context.AdmintT010Modulos.Where(s => s.A010estadoRegistro == StringHelper.estadoActivo && s.A010aplicativo.Contains("CUPOS"));
            //Traemos los valores configurados
            var funcionalidades = _context.AdmintV002Roles.Where(s => s.A014codigoRol == rol && s.A011estadoRegistro == StringHelper.estadoActivo).ToList();

            if (_context.AdmintT011Rols.Where(r => r.PkT011codigo == rol).FirstOrDefault() == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoRol, "", token, true);
            }

            if (cargo != null)
            {
                _ = funcionalidades.Where(s => s.A011cargo.Contains(cargo));
            }

            foreach (AdmintT010Modulo mod in modulos)
            {
                RolModPermition per = new RolModPermition();
                var func = funcionalidades.Where(s => s.PkT010codigo == mod.PkT010codigo).ToList();
                per.moduleId = (int)mod.PkT010codigo;
                per.rolId = rol;
                per.name = mod.A010descripcion;

                if (func != null)
                {
                    //workaround
                    foreach (AdmintV002Role f in func)
                    {
                        per.delete = f.A014eliminar;
                        per.create = f.A014crear;
                        per.consult = f.A014consultar;
                        per.update = f.A014actualizar;
                        per.see = f.A014verDetalle;
                    }
                }
                else
                {
                    per.delete = false;
                    per.create = false;
                    per.consult = false;
                    per.update = false;
                    per.see = false;
                }
                permisos.Add(per);
            }
            return ResponseManager.generaRespuestaGenerica("", permisos, token, false);

        }

        public Responses ActualizarFuncionalidades(ClaimsIdentity identity, RolModPermition permisos, string ipAddress)
        {
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (permisos != null && permisos.rolId != 0 && permisos.moduleId != 0)
            {
                var result = _context.AdmintT014RlRolModuloPermisos.SingleOrDefault(x => x.A014codigoRol == permisos.rolId && x.A014codigoModulo == permisos.moduleId);
                var resultModule = _context.AdmintT010Modulos.SingleOrDefault(s => s.PkT010codigo == permisos.moduleId);

                if (resultModule != null)
                {
                    if (resultModule.A010descripcion != permisos.name)
                    {
                        valorAnterior.Add(resultModule.A010descripcion);
                        valorActual.Add(permisos.name);
                        campos.Add("A010descripcion");
                    }
                    resultModule.A010descripcion = permisos.name;
                }

                _context.Update(resultModule);
                _ = _context.SaveChanges();

                if (result != null) //lo encontro y lo actualiza
                {
                    if (result.A014actualizar != permisos.update)
                    {
                        valorAnterior.Add(result.A014actualizar.ToString());
                        valorActual.Add(permisos.update.ToString());
                        campos.Add("A014actualizar");
                    }
                    result.A014actualizar = permisos.update;

                    if (result.A014consultar != permisos.consult)
                    {
                        valorAnterior.Add(result.A014consultar.ToString());
                        valorActual.Add(permisos.consult.ToString());
                        campos.Add("A014consultar");
                    }
                    result.A014consultar = permisos.consult;

                    if (result.A014crear != permisos.create)
                    {
                        valorAnterior.Add(result.A014crear.ToString());
                        valorActual.Add(permisos.create.ToString());
                        campos.Add("A014crear");
                    }
                    result.A014crear = permisos.create;

                    if (result.A014eliminar != permisos.delete)
                    {
                        valorAnterior.Add(result.A014eliminar.ToString());
                        valorActual.Add(permisos.delete.ToString());
                        campos.Add("A014eliminar");
                    }
                    result.A014eliminar = permisos.delete;

                    if (result.A014verDetalle != permisos.see)
                    {
                        valorAnterior.Add(result.A014verDetalle.ToString());
                        valorActual.Add(permisos.see.ToString());
                        campos.Add("A014verDetalle");
                    }
                    result.A014verDetalle = permisos.see;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    var rol = _context.AdmintT011Rols.Where(x => x.PkT011codigo == result.A014codigoRol).FirstOrDefault();
                    var modulo = _context.AdmintT010Modulos.Where(s => s.PkT010codigo == result.A014codigoModulo).FirstOrDefault();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smRolFuncionalidades, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), null, rol.A011nombre + ", " + modulo.A010descripcion);
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);

                }
                else //lo agrega
                {
                    //siempre y cuando el rol exista y la funcionalidad
                    var rol = _context.AdmintT011Rols.SingleOrDefault(x => x.PkT011codigo == permisos.rolId);
                    if (rol == null)
                    {
                        return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
                    }
                    var modulo = _context.AdmintT010Modulos.SingleOrDefault(x => x.PkT010codigo == permisos.moduleId);
                    if (modulo == null)
                    {
                        return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
                    }

                    var now = DateTime.Now;

                    AdmintT014RlRolModuloPermiso per = new AdmintT014RlRolModuloPermiso();
                    per.A014codigoRol = permisos.rolId;
                    per.A014codigoModulo = permisos.moduleId;
                    per.A014verDetalle = permisos.see;
                    per.A014eliminar = permisos.delete;
                    per.A014crear = permisos.create;
                    per.A014actualizar = permisos.update;
                    per.A014consultar = permisos.consult;
                    per.A014codigoUsuarioCreacion = Decimal.Parse(codigoUsuario);
                    per.A014codigoUsuarioModificacion = Decimal.Parse(codigoUsuario);
                    per.A014estadoRegistro = StringHelper.estadoActivo;
                    per.A014fechaCreacion = now;
                    per.A014fechaModificacion = now;

                    _context.AdmintT014RlRolModuloPermisos.Add(per);
                    int r = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRolFuncionalidades, "", "", "", per, rol.A011nombre + ", " + modulo.A010descripcion);

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }

        public Responses Crear(ClaimsIdentity identity, ReqRol rol, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var now = DateTime.Now;

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var result = _context.AdmintT011Rols.SingleOrDefault(x => x.A011nombre == rol.name && x.A011modulo == "CUPOS");

            if (result != null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgYaExisteElRol + rol.name, "", token, true);
            }

            AdmintT011Rol adminRol = new AdmintT011Rol();
            adminRol.A011nombre = rol.name;
            adminRol.A011descripcion = rol.description;
            adminRol.A011cargo = rol.position;
            adminRol.A011fechaCreacion = now;
            adminRol.A011fechaModificacion = now;
            adminRol.A011codigoUsuarioCreacion = Decimal.Parse(codigoUsuario);
            adminRol.A011codigoUsuarioModificacion = Decimal.Parse(codigoUsuario);
            adminRol.A011estadoRegistro = StringHelper.estadoActivo;
            adminRol.A011modulo = "CUPOS";

            _context.AdmintT011Rols.Add(adminRol);
            int r = _context.SaveChanges();

            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smRoles, "", "", "", adminRol, adminRol.A011nombre);

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);
        }

        public Responses Actualizar(ClaimsIdentity identity, ReqRol rol, string ipAddress)
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

            if (rol.rolId != 0)
            {
                var result = _context.AdmintT011Rols.SingleOrDefault(x => x.PkT011codigo == rol.rolId);

                if (result != null) //lo encontro y lo actualiza
                {
                    result.A011fechaModificacion = now;

                    if (result.A011nombre != rol.name)
                    {
                        var resultRepeat = _context.AdmintT011Rols.SingleOrDefault(x => x.A011nombre == rol.name && x.A011modulo == "CUPOS");

                        if (resultRepeat != null)
                        {
                            return ResponseManager.generaRespuestaGenerica(StringHelper.msgYaExisteElRol + rol.name, "", token, true);
                        }

                        valorAnterior.Add(result.A011nombre);
                        valorActual.Add(rol.name);
                        campos.Add("A011nombre");
                    }
                    result.A011nombre = rol.name;

                    if (result.A011descripcion != rol.description)
                    {
                        valorAnterior.Add(result.A011descripcion);
                        valorActual.Add(rol.name);
                        campos.Add("A011descripcion");
                    }
                    result.A011descripcion = rol.name;

                    if (result.A011cargo != rol.position)
                    {
                        valorAnterior.Add(result.A011cargo);
                        valorActual.Add(rol.position);
                        campos.Add("A011cargo");
                    }
                    result.A011cargo = rol.position;

                    decimal estado = rol.estate ? StringHelper.estadoActivo : StringHelper.estadoInactivo;

                    if (result.A011estadoRegistro != estado)
                    {
                        valorAnterior.Add(result.A011estadoRegistro.ToString());
                        valorActual.Add(estado.ToString());
                        campos.Add("A011estadoRegistro");
                    }
                    result.A011estadoRegistro = estado;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smRoles, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A011nombre);
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
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoId, "", token, true);
            }

        }

        public Responses ConsultarTodosRoles(ClaimsIdentity identity, string ipAddress, string parametro)
        {
            var parametricas = (from r in _context.AdmintT011Rols
                                where r.A011nombre.Contains(parametro) && r.A011estadoRegistro == StringHelper.estadoActivo && r.A011modulo.Contains("CUPOS")
                                select new
                                {
                                    etapa = r.A011nombre
                                }).Distinct();

            return ResponseManager.generaRespuestaGenerica("", parametricas.Take(10).ToList(), "", false);
        }
    }
}
