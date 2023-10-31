using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Security.Claims;

namespace Repository.Persistence.Repository
{
    public class AssignmentRequest : ISolicitudAsignacion
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;

        public AssignmentRequest(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }

        public Responses Consultar(ClaimsIdentity identity, string nombreUsuario, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (nombreUsuario == null)
            {
                nombreUsuario = "";
            }

            var solicitudes = from u in _context.AdmintV003UsuarioRole
                              select u;

            if (nombreUsuario != null)
            {
                solicitudes = solicitudes.Where(x =>
                    (x.nombre != null && x.nombre.Contains(nombreUsuario)) ||
                    (x.a012segundoNombre != null && x.a012segundoNombre.Contains(nombreUsuario)) ||
                    (x.a012segundoApellido != null && x.a012segundoApellido.Contains(nombreUsuario)) &&
                    (x.a012CodigoParametricaTipoUsuario == StringHelper.tipoUsuarioExterno));
            }

            return ResponseManager.generaRespuestaGenerica("", solicitudes.ToList(), token, false);
        }

        public Responses ConsultarEstados(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var estados = new Dictionary<int, String>()
            {
                { 1, StringHelper.estadoAceptada },
                { 2, StringHelper.estadoRechazada }
            };

            return ResponseManager.generaRespuestaGenerica("", estados, token, false);
        }

        public Responses Actualizar(ClaimsIdentity identity, ReqAssignment usuario, string ipAddress)
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

            if (usuario != null && usuario.id != 0)
            {
                var result = _context.AdmintT012Usuarios.SingleOrDefault(x => x.PkT012codigo == usuario.id);

                if (result != null)
                {
                    if (usuario.identification != 0)
                    {
                        if (result.A012identificacion != usuario.identification)
                        {
                            valorAnterior.Add(result.A012identificacion.ToString());
                            valorActual.Add(usuario.identification.ToString() ?? "");
                            campos.Add("A012identificacion");
                        }

                        result.A012identificacion = usuario.identification.HasValue ? usuario.identification.Value : 0;
                    }
                    if (usuario.firstName != null)
                    {
                        if (result.A012primerNombre != usuario.firstName)
                        {
                            valorAnterior.Add(result.A012primerNombre.ToString());
                            valorActual.Add(usuario.firstName.ToString());
                            campos.Add("A012primer_nombre");
                        }
                        result.A012primerNombre = usuario.firstName;
                    }
                    if (usuario.secondName != null)
                    {
                        if (result.A012segundoNombre != usuario.secondName)
                        {
                            valorAnterior.Add(result.A012segundoNombre.ToString());
                            valorActual.Add(usuario.secondName.ToString());
                            campos.Add("A012segundo_nombre");
                        }
                        result.A012segundoNombre = usuario.secondName;
                    }
                    if (usuario.firstLastname != null)
                    {
                        if (result.A012primerApellido != usuario.firstLastname)
                        {
                            valorAnterior.Add(result.A012primerApellido.ToString());
                            valorActual.Add(usuario.firstLastname.ToString());
                            campos.Add("A012primer_apellido");
                        }
                        result.A012primerApellido = usuario.firstLastname;
                    }
                    if (usuario.secondLastname != null)
                    {
                        if (result.A012segundoApellido != usuario.secondLastname)
                        {
                            valorAnterior.Add(result.A012segundoApellido.ToString());
                            valorActual.Add(usuario.secondLastname.ToString());
                            campos.Add("A012segundo_apellido");
                        }
                        result.A012segundoApellido = usuario.secondLastname;
                    }
                    if (usuario.estate != 0)
                    {
                        if (result.A012estadoSolicitud != usuario.estate)
                        {
                            valorAnterior.Add(result.A012segundoApellido.ToString());
                            valorActual.Add(usuario.estate.ToString());
                            campos.Add("A012estadoSolicitud");
                        }
                        result.A012estadoSolicitud = usuario.estate;
                    }
                    if (usuario.rolId != null)
                    {
                        var rolesUsuarios = _context.AdmintT015RlUsuarioRols.SingleOrDefault(x => x.A015codigoUsuario == usuario.id);

                        //Si tiene otro rol se elimina, se mantiene simple rol
                        if (rolesUsuarios != null)
                        {
                            rolesUsuarios.A015codigoRol = usuario.rolId;
                            _context.Update(rolesUsuarios);
                            _context.SaveChanges();
                        }
                        else
                        {
                            //Asocia un Rol
                            AdmintT015RlUsuarioRol rolUsuario = new AdmintT015RlUsuarioRol();
                            rolUsuario.A015codigoUsuario = usuario.id;
                            rolUsuario.A015codigoUsuarioCreacion = Decimal.Parse(codigoUsuario);
                            rolUsuario.A015codigoUsuarioModificacion = Decimal.Parse(codigoUsuario);
                            rolUsuario.A015codigoRol = usuario.rolId;
                            rolUsuario.A015estadoRegistro = StringHelper.estadoActivo;
                            rolUsuario.A015fechaCreacion = now;
                            rolUsuario.A015fechaModificacion = now;

                            _context.AdmintT015RlUsuarioRols.Add(rolUsuario);
                            _context.SaveChanges();
                        }
                    }
                    result.A012fechaModificacion = now;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smSolicitudesRol, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A012login);
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
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
            }

        }

        public Responses ActualizarEstado(ClaimsIdentity identity, ReqAssignmentUpdate solicitud, string ipAddress)
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

            if (solicitud != null && solicitud.id != 0)
            {
                var result = _context.AdmintT015RlUsuarioRols.SingleOrDefault(x => x.PkT0015codigo == solicitud.id);

                if (result != null)
                {
                    if (solicitud.statusRequest != null)
                    {
                        if (result.A015estadoSolicitud != solicitud.statusRequest)
                        {
                            valorAnterior.Add(result.A015estadoSolicitud.ToString());
                            valorActual.Add(solicitud.statusRequest.ToString());
                            campos.Add("A012estadoSolicitud");
                        }
                        result.A015estadoSolicitud = solicitud.statusRequest;
                    }

                    result.A015fechaModificacion = now;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    var rol = _context.AdmintT011Rols.Where(x => x.PkT011codigo.ToString().Contains(result.A015codigoRol)).FirstOrDefault();

                    if (rol != null)
                    {
                        logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smSolicitudesRol, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", rol.A011nombre);
                        _ = _context.SaveChanges();
                    }

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);

                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
            }
        }
        public Responses Eliminar(ClaimsIdentity identity, ReqId solicitud, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (solicitud.id != 0)
            {
                var result = _context.AdmintT012Usuarios.SingleOrDefault(x => x.PkT012codigo == solicitud.id);

                if (result != null) //lo encontro y lo elimina
                {
                    var rolesUsuarios = _context.AdmintT015RlUsuarioRols.SingleOrDefault(x => x.A015codigoUsuario == solicitud.id);

                    if (rolesUsuarios != null)
                    {
                        _context.Remove(rolesUsuarios);
                        _context.SaveChanges();
                    }

                    _context.Remove(result);
                    _context.SaveChanges();

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 4, ModuleManager.smSolicitudesRol, "", "", "", result, result.A012login);
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
        public Responses Assign(ClaimsIdentity identity, VitalReq solicitud, string ipAddress)
        {
            if (solicitud.Name != null)
            {
                if (StringHelper.esEmailValido(solicitud.EMail != null ? solicitud.EMail  : ""))
                {
                    var now = DateTime.Now;
                    var clave = StringHelper.generaContrasenaAleatoria();
                    var tokenTemporal = Guid.NewGuid().ToString();

                    AdmintT012Usuario usu = new AdmintT012Usuario();
                    usu.A012codigoUsuarioCreacion = 1;
                    usu.A012fechaExpiracontraseña = now.AddMonths(3); //expiracion a los 3 meses
                    usu.A012fechaCreacion = now;
                    usu.A012estadoRegistro = StringHelper.estadoValidarCorreo; //pendiente por asignación de rol
                    usu.A012aceptaTerminos = false;
                    usu.A012aceptaTratamientoDatosPersonales = false;
                    usu.A012celular = "";
                    usu.A012telefono = 0;
                    usu.A012correoElectronico = solicitud.EMail != null ? solicitud.EMail : "";
                    usu.A012direccion = "";
                    usu.A012fechaInicioContrato = null;
                    usu.A012fechaFinContrato = null;
                    usu.A012login = "";
                    usu.A012primerNombre = solicitud.Name.Substring(0, solicitud.Name.IndexOf(" ")).Trim();
                    usu.A012segundoNombre = "";
                    usu.A012primerApellido = solicitud.Name.Substring(solicitud.Name.IndexOf(" ")).Trim();
                    usu.A012segundoApellido = "";
                    usu.A012identificacion = solicitud.Document.HasValue ? solicitud.Document.Value : 0;
                    //Seguridad de acceso
                    usu.A012login = generaLogin(usu.A012primerNombre.Trim(), usu.A012primerApellido.Trim());
                    usu.A012contrasena = SecurityManager.Encrypt(clave);
                    usu.A012tokenTemporal = tokenTemporal;
                    usu.A012codigoParametricaTipousuario = StringHelper.tipoUsuarioExterno;
                    usu.A012Modulo = "CUPOS";

                    _context.AdmintT012Usuarios.Add(usu);
                    _context.SaveChanges();

                    //Asocia un Rol
                    AdmintT015RlUsuarioRol rolUsuario = new AdmintT015RlUsuarioRol();
                    rolUsuario.A015codigoUsuario = usu.PkT012codigo;
                    rolUsuario.A015codigoUsuarioCreacion = 1;
                    rolUsuario.A015codigoUsuarioModificacion = 1;
                    rolUsuario.A015codigoRol = solicitud.rol.ToString();
                    rolUsuario.A015estadoRegistro = StringHelper.estadoActivo;
                    rolUsuario.A015fechaCreacion = now;
                    rolUsuario.A015fechaModificacion = now;
                    rolUsuario.A015estadoSolicitud = "Pendiente";

                    _context.AdmintT015RlUsuarioRols.Add(rolUsuario);
                    _context.SaveChanges();

                    var rol = _context.AdmintT011Rols.Where(x => x.PkT011codigo.ToString() == rolUsuario.A015codigoRol).FirstOrDefault();

                    logManager.crearAuditoria(ipAddress, "1", 2, ModuleManager.smSolicitudesRol, "", "", "", usu, usu.A012login);
                    logManager.crearAuditoria(ipAddress, "1", 2, ModuleManager.smSolicitudesRol, "", "", "", rolUsuario, (rol != null ? rol.A011nombre : ""));

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgSolicitudRol, "", "", false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica("El campo de Correo Electrónico no es valido", "", "", true);
                }
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("El campo Primer Nombre no puedes ser vacio", "", "", true);
            }
        }
        private string generaLogin(string primerNombre, string primerApellido)
        {
            string login = primerNombre + primerApellido;

            var count = _context.AdmintT012Usuarios.Count(u => u.A012login.Contains(login) && (u.A012Modulo != null && u.A012Modulo.Contains("CITES")));

            if (count > 0)
            {
                login = login + count.ToString();
            }

            return login;
        }

        public Responses VerificarDocumento(ClaimsIdentity identity, string documento, string ipAddress)
        {
            var result = _context.AdmintV003UsuarioRole.Where(x => x.a012identificacion == Convert.ToDecimal(documento)).FirstOrDefault();
            var result2 = _context.AdmintT012Usuarios.Where(x => result != null && result.codigoUsuario == x.PkT012codigo).FirstOrDefault();

            if (result2 == null)
            {
                return ResponseManager.generaRespuestaGenerica("El Documento no se encuentra Registrado", "", "", false);
            }
            else
            {
                if (result != null && result.a015estadoSolicitud != null)
                {
                    if (result.a015estadoSolicitud.ToUpper() == "PENDIENTE")
                    {
                        return ResponseManager.generaRespuestaGenerica("Usted ya cuenta con la usuario de un rol en curso, espere la confirmación por correo", "", "", true);
                    }
                    else if (result.a015estadoSolicitud.ToUpper() == "RECHAZADO")
                    {
                        return ResponseManager.generaRespuestaGenerica("Usted ya cuenta con una usuario de rol Rechazada, vuelva a solicitar un rol", "", "", true);
                    }
                }

                return ResponseManager.generaRespuestaGenerica("Usted ya cuenta con una cuenta activa para ingresar al el sistema, si no sabe las credenciales solicite un correo con las mismas", "", "", true);
            }
        }
    }
}
