using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Security.Claims;

namespace Repository.Persistence.Repository
{
    public class Usuario : IUsuario
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;
        private readonly SettingsHelper setting;

        public Usuario(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
            setting = new SettingsHelper();
        }

        public Responses Authenticate(ReqLogin user, string ipAddress)
        {
            AdmintT012Usuario T12usuario = _context.AdmintT012Usuarios.Where(u => u.A012login.Trim() == user.user.Trim() && u.A012contrasena.Trim() == SecurityManager.Encrypt(user.password.Trim()) && (u.A012fechaFinContrato >= DateTime.Now || u.A012fechaFinContrato == null) && u.A012Modulo.Contains("CUPOS")).FirstOrDefault() ?? new AdmintT012Usuario();

			try
            {
                var parIntentos = _context.AdmintT008Parametricas.Where(x => x.A008parametrica == "NUMERO_MAXIMO_FALLOS_CLAVE").FirstOrDefault();
                T12usuario = _context.AdmintT012Usuarios.Where(u => u.A012login == user.user && u.A012contrasena == SecurityManager.Encrypt(user.password) && u.A012Modulo.Contains("CUPOS")).FirstOrDefault() ?? new AdmintT012Usuario();
                
                if (T12usuario == null)
                {
                    T12usuario = _context.AdmintT012Usuarios.Where(u => u.A012login == user.user && u.A012Modulo == "CUPOS").FirstOrDefault() ?? new AdmintT012Usuario();

                    if (T12usuario != null)
                    {

                        T12usuario.A012cantidadIntentosIngresoincorrecto += 1;

                        if (T12usuario.A012fechaDesbloqueo < DateTime.Now)
                        {
                            T12usuario.A012fechaDesbloqueo = null;
                            T12usuario.A012cantidadIntentosIngresoincorrecto = 1;
                        }

                        if (parIntentos != null && T12usuario.A012cantidadIntentosIngresoincorrecto >= Convert.ToDecimal(parIntentos.A008valor))
                        {
                            if (T12usuario.A012fechaDesbloqueo != null)
                            {
                                string des = (T12usuario.A012fechaDesbloqueo - DateTime.Now).ToString() ?? "";
                                int inicioHora = des.IndexOf(":") + 1;
                                int finHora = des.IndexOf(".");
                                string hora = des.Substring(inicioHora, finHora - inicioHora);

                                return ResponseManager.generaRespuestaGenerica(StringHelper.msgBloqueoUsuario + hora + " Minutos", StringHelper.msgNoAutorizado, "", true);
                            }

                            var par = _context.AdmintT008Parametricas.Where(x => x.A008parametrica == "TIEMPO BLOQUEO CLAVE").FirstOrDefault();
                            
                            if(par != null)
                            {
                                T12usuario.A012fechaDesbloqueo = DateTime.Now.AddMinutes(Convert.ToDouble(par.A008valor));

                                _context.Update(T12usuario);
                                _ = _context.SaveChanges();

                                return ResponseManager.generaRespuestaGenerica(StringHelper.msgBloqueoUsuario + "0" + par.A008valor + ":00 Minutos", StringHelper.msgNoAutorizado, "", true);
                            }
                        }

                        _context.Update(T12usuario);
                        _ = _context.SaveChanges();
                    }

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgValidarUsuarioClave, StringHelper.msgNoAutorizado, "", true);
                }
                else
                {
                    if (T12usuario.A012fechaFinContrato < DateTime.Now)
                        return ResponseManager.generaRespuestaGenerica(StringHelper.msgContratoVencido, StringHelper.msgNoAutorizado, "", true);

                    if (T12usuario.A012fechaDesbloqueo > DateTime.Now)
                    {
                        string des = (T12usuario.A012fechaDesbloqueo - DateTime.Now).ToString() ?? "";
                        int inicioHour = des.IndexOf(":") + 1;
                        int finHour = des.IndexOf(".");
                        string hour = des.Substring(inicioHour, finHour - inicioHour);

                        return ResponseManager.generaRespuestaGenerica(StringHelper.msgBloqueoUsuario + hour + " Minutos", StringHelper.msgNoAutorizado, "", true);
                    }
                    else
                    {
                        T12usuario.A012fechaDesbloqueo = null;
                        T12usuario.A012cantidadIntentosIngresoincorrecto = 0;

                        _context.Update(T12usuario);
                        _ = _context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgAdministrador, "", "", true);
            }
            if (T12usuario.A012estadoRegistro == StringHelper.estadoActivo)
            {
                return ResponseManager.generaRespuestaGenerica("", "", jwtAuthenticationManager.generarJWT(T12usuario.PkT012codigo.ToString()), false);
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica("Usuario con estado: " + T12usuario.A012estadoRegistro, "", "", true);
            }

        }

        //Se usa cuando es primera vez y se envia un password temporal 
        //en esa pantalla debe haber los 2 campos de contraseña
        //si es primera ves se puede enviar los campos de acepar terminos

        //igual con el link que se envia al crear un usuario nuevo
        //url = urlDomain/Usuario/Nuevo?token=34345434
        public Responses CambiaContrasena(ReqChangePassword user, string ipAddress)
        {
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();

            var result = _context.AdmintT012Usuarios.SingleOrDefault(x => x.A012login == user.user && x.A012contrasena == SecurityManager.Encrypt(user.password) && x.A012Modulo == "CUPOS");

            if (result != null)
            {
                valorAnterior.Add(result.A012contrasena);
                valorActual.Add(SecurityManager.Encrypt(user.password));
                campos.Add("A012contrasena");

                result.A012tokenTemporal = "";
                result.A012contrasena = SecurityManager.Encrypt(user.newPassword);

                if (result.A012estadoRegistro == StringHelper.estadoValidarCorreo)
                {
                    result.A012estadoRegistro = StringHelper.estadoActivo;
                }

                if ((result.A012aceptaTerminos) && (result.A012aceptaTratamientoDatosPersonales))
                {
                    user.acceptsTerms = true;
                    user.acceptsProcessingPersonalData = true;
                }

                if ((user.acceptsTerms == true) && (user.acceptsProcessingPersonalData == true))
                {
                    if (result.A012aceptaTerminos != user.acceptsTerms)
                    {
                        valorAnterior.Add(result.A012aceptaTerminos.ToString());
                        valorActual.Add(user.acceptsTerms.ToString() ?? "");
                        campos.Add("A012aceptaTerminos");
                    }
                    result.A012aceptaTerminos = user.acceptsTerms is bool ? (bool)user.acceptsTerms : false;

                    if (result.A012aceptaTratamientoDatosPersonales != user.acceptsProcessingPersonalData)
                    {
                        valorAnterior.Add(result.A012aceptaTratamientoDatosPersonales.ToString());
                        valorActual.Add(user.acceptsProcessingPersonalData.ToString() ?? "");
                        campos.Add("A012aceptaTratamientoDatosPersonales");
                    }
                    result.A012aceptaTratamientoDatosPersonales = user.acceptsProcessingPersonalData is bool ? (bool)user.acceptsProcessingPersonalData : false;
                }
                else
                {
                    if (!((user.acceptsTerms == false) && (user.acceptsProcessingPersonalData == false)))
                    {
                        return ResponseManager.generaRespuestaGenerica(StringHelper.msgValidarTerminos, "", "", true);
                    }
                }

                _context.Update(result);
                _ = _context.SaveChanges();

                logManager.crearAuditoria(ipAddress, result.PkT012codigo.ToString(), 3, ModuleManager.smGestionUsuarios, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A012login);
                _ = _context.SaveChanges();

                return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", jwtAuthenticationManager.generarJWT(result.PkT012codigo.ToString()), false);
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgValidarUsuarioClave, "", "", true);
            }
        }

        public Responses enviaEmailRecuperarContrasena(ReqSimpleUser usuario, string ip)
        {
            var result = _context.AdmintT012Usuarios.SingleOrDefault(x => x.A012login == usuario.user && x.A012Modulo.Contains("CUPOS"));
            if (result != null)
            {
                var tokenTemporal = Guid.NewGuid().ToString();
                var tempPassword = StringHelper.generaContrasenaAleatoria();
                result.A012tokenTemporal = tokenTemporal;
                result.A012contrasena = SecurityManager.Encrypt(tempPassword);
                result.A012estadoRegistro = StringHelper.estadoValidarCorreo;

                _context.Update(result);
                _ = _context.SaveChanges();

                EmailHelper.enviaEmailSendGridAsync(result.A012correoElectronico, result.A012primerNombre, "Recuperación de Cuenta para CUPOS, PRECINTOS Y MARQUILLAS", "Para continuar con la recuperación de tu cuenta debes dirigirte a " + setting.getPageUrl() + " y utilizar tu usuario: " + result.A012login + " y tu contraseña temporal: " + tempPassword);
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgEmailEnviado, "", "", false);
            }else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgUsuarioNoExiste, "", "", true);
            }
        }

        public Responses Consultar(ClaimsIdentity identity, string CorreoElectronico, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var usuario = _context.AdmintT012Usuarios.Where(x => x.A012correoElectronico == CorreoElectronico).First();

            return ResponseManager.generaRespuestaGenerica("", usuario, token, false);
        }

        public Responses ConsultarTodos(ClaimsIdentity identity, string ipAddress, string? cadenaBusqueda)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var usu = _context.AdmintT008Parametricas
                .Join(
                _context.AdmintT012Usuarios,
                par => par.PkT008codigo,
                usua => usua.A012estadoRegistro,
                (par, usua) => new
                {
                    usua.A012primerNombre,
                    usua.A012segundoNombre,
                    usua.A012primerApellido,
                    usua.A012segundoApellido,
                    usua.PkT012codigo,
                    usua.A012login,
                    par.A008valor,
                    usua.A012telefono,
                    usua.A012fechaExpiracontraseña,
                    usua.A012identificacion,
                    usua.A012Modulo,
                    usua.A012contrasena
                }).Join(
                _context.AdmintT015RlUsuarioRols,
                usus => usus.PkT012codigo,
                usurol => usurol.A015codigoUsuario,
                (usus, usurol) => new
                {
                    usus.A012primerNombre,
                    usus.A012segundoNombre,
                    usus.A012primerApellido,
                    usus.A012segundoApellido,
                    usus.PkT012codigo,
                    usus.A012login,
                    Roles = usurol.A015codigoRol,
                    usus.A008valor,
                    usus.A012telefono,
                    usus.A012fechaExpiracontraseña,
                    usus.A012identificacion,
                    usus.A012Modulo,
                    usus.A012contrasena
                });

            usu = usu.Where(s => s.A012Modulo.Contains("CUPOS"));

            if (!String.IsNullOrEmpty(cadenaBusqueda))
            {
                usu = usu.Where(s => s.A012login.Contains(cadenaBusqueda) || s.A012primerNombre.Contains(cadenaBusqueda) || s.A012primerApellido.Contains(cadenaBusqueda) || s.A012segundoNombre.Contains(cadenaBusqueda) || s.A012segundoApellido.Contains(cadenaBusqueda) || s.A012identificacion.ToString().Contains(cadenaBusqueda));
            }

            List<UserReq> usuarios = new List<UserReq>();
            foreach (var x in usu)
            {
                UserReq usus = new UserReq();
                usus.A012primerNombre = x.A012primerNombre;
                usus.A012segundoNombre = x.A012segundoNombre;
                usus.A012primerApellido = x.A012primerApellido;
                usus.A012segundoApellido = x.A012segundoApellido;
                usus.PkT012codigo = x.PkT012codigo;
                usus.A012login = x.A012login;
                usus.Roles = x.Roles;
                usus.A008valor = x.A008valor;
                usus.A012telefono = x.A012telefono;
                usus.A012fechaExpiracontraseña = x.A012fechaExpiracontraseña;
                usus.A012identificacion = x.A012identificacion;
                usus.A012Modulo = x.A012Modulo ?? "";
                usus.A012contrasena = SecurityManager.Decrypt(x.A012contrasena);

                usuarios.Add(usus);
            }

            return ResponseManager.generaRespuestaGenerica("", usuarios.ToList(), token, false);

        }

        //CU02.1 Gestion de Usuarios
        //Permisos: Solo Administradores
        public Responses Create(ClaimsIdentity identity, ReqUser usuario, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

                if (usuario.firstName != null)
                {
                    if (usuario.firstLastName != null)
                    {
                        if (StringHelper.esEmailValido(usuario.email ?? ""))
                        {
                            var now = DateTime.Now;
                            var clave = StringHelper.generaContrasenaAleatoria();
                            var tokenTemporal = Guid.NewGuid().ToString();

                            var docs = _context.AdmintT012Usuarios.Where(x => x.A012identificacion == usuario.identification && x.A012Modulo.Contains("CUPOS")).FirstOrDefault();

                            if (docs != null)
                            {
                                return ResponseManager.generaRespuestaGenerica("Ya se encuentra registrado un usuario con el numero de documento ingresado", "", token, true);
                            }

                            AdmintT012Usuario usu = new AdmintT012Usuario();
                            usu.A012codigoUsuarioCreacion = Int32.Parse(codigoUsuario);
                            usu.A012fechaExpiracontraseña = now.AddMonths(3); //expiracion a los 3 meses
                            usu.A012fechaCreacion = now;
                            usu.A012estadoRegistro = StringHelper.estadoValidarCorreo; //pendiente por asignación de rol
                            usu.A012aceptaTerminos = usuario.acceptsTerms is bool ? (bool)usuario.acceptsTerms : false;
                            usu.A012aceptaTratamientoDatosPersonales = usuario.acceptsProcessingPersonalData is bool ? (bool)usuario.acceptsProcessingPersonalData : false;
                            usu.A012celular = usuario.celular;
                            usu.A012telefono = usuario.phone is decimal ? (decimal)usuario.phone : 0;
                            usu.A012correoElectronico = usuario.email ?? "";
                            usu.A012direccion = usuario.address is not null ? usuario.address : "";
                            usu.A012fechaInicioContrato = usuario.contractStartDate;
                            usu.A012fechaFinContrato = usuario.contractFinishDate;
                            usu.A012login = usuario.login ?? "";
                            usu.A012primerNombre = usuario.firstName.Trim();
                            usu.A012segundoNombre = usuario.secondName is not null ? usuario.secondName : "";
                            usu.A012primerApellido = usuario.firstLastName.Trim();
                            usu.A012segundoApellido = usuario.SecondLastName is not null ? usuario.SecondLastName : "";
                            usu.A012identificacion = usuario.identification;
                            //Seguridad de acceso
                            usu.A012login = generaLogin(usuario.firstName.Trim(), usuario.firstLastName.Trim());
                            usu.A012contrasena = SecurityManager.Encrypt(clave);
                            usu.A012tokenTemporal = tokenTemporal;
                            usu.A012codigoParametricaTipousuario = StringHelper.tipoUsuarioInterno;
                            usu.A012Modulo = "CUPOS";

                            if (usuario.cityAddress != null)
                            {
                                var ciudad = _context.AdmintT004Ciudads.Where(a => a.PkT004codigo == usuario.cityAddress).FirstOrDefault();
                                if (ciudad == null)
                                {
                                    return ResponseManager.generaRespuestaGenerica("Código de Ciudad no encontrado", "", token, true);
                                }
                                usu.A012codigoCiudadDireccion = usuario.cityAddress;
                            }

                            if (usuario.codeParametricUserType != null)
                            {
                                var parametrica = _context.AdmintT008Parametricas.Where(a => a.PkT008codigo == usuario.codeParametricUserType).FirstOrDefault();
                                if (parametrica == null)
                                {
                                    return ResponseManager.generaRespuestaGenerica("Código de Tipo de Usuario no encontrado", "", token, true);
                                }
                                usu.A012codigoParametricaTipousuario = usuario.codeParametricUserType;
                            }

                            if (usuario.codeParametricDocumentType != null)
                            {
                                var parametrica = _context.AdmintT008Parametricas.Where(a => a.PkT008codigo == usuario.codeParametricDocumentType).FirstOrDefault();
                                if (parametrica == null)
                                {
                                    return ResponseManager.generaRespuestaGenerica("Código de Tipo de Documento no encontrado", "", token, true);
                                }
                                usu.A012codigoParametricaTipoDocumento = usuario.codeParametricDocumentType;
                            }

                            if (usuario.dependence != null)
                            {
                                usu.A012dependencia = usuario.dependence;
                            }

                            _context.AdmintT012Usuarios.Add(usu);
                            int r = _context.SaveChanges();

                            //Asocia un Rol
                            AdmintT015RlUsuarioRol rolUsuario = new AdmintT015RlUsuarioRol();
                            rolUsuario.A015codigoUsuario = usu.PkT012codigo;
                            rolUsuario.A015codigoUsuarioCreacion = Decimal.Parse(codigoUsuario);
                            rolUsuario.A015codigoUsuarioModificacion = Decimal.Parse(codigoUsuario);
                            rolUsuario.A015codigoRol = usuario.rol != null ? usuario.rol: "";
                            rolUsuario.A015estadoRegistro = StringHelper.estadoActivo;
                            rolUsuario.A015fechaCreacion = now;
                            rolUsuario.A015fechaModificacion = now;

                            _context.AdmintT015RlUsuarioRols.Add(rolUsuario);
                            _context.SaveChanges();

                            var rol = _context.AdmintT011Rols.Where(x => x.PkT011codigo.ToString() == rolUsuario.A015codigoRol).FirstOrDefault();

                            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smGestionUsuarios, "", "", "", usu, usu.A012login);
                            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smGestionUsuarios, "", "", "", rolUsuario, rol != null ? rol.A011nombre : "");

                            EmailHelper.enviaEmailSendGridAsync(usuario.email, usuario.firstName, "Creación de Cuenta para CUPOS, PRECINTOS Y MARQUILLAS", "Para continuar con el registro debes dirigirte a " + setting.getPageUrl() + "/Login/Index usando tu usuario: " + usu.A012login + " y contraseña temporal: " + clave);
                            return ResponseManager.generaRespuestaGenerica(StringHelper.msgUsuarioCreadoExitoso, "", token, false);
                        }
                        else
                        {
                            return ResponseManager.generaRespuestaGenerica("El campo de Correo Electrónico no es valido", "", token, true);
                        }
                    }
                    else
                    {
                        return ResponseManager.generaRespuestaGenerica("El campo Primer Apellido no puedes ser vacio", "", token, true);
                    }
                } else
                {
                    return ResponseManager.generaRespuestaGenerica("El campo Primer Nombre no puedes ser vacio", "", token, true);
                }
        }

        public Responses Actualizar(ClaimsIdentity identity, ReqUser usuario, string ipAddress)
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

            int r = 0;
            bool flag = false;
            if (usuario != null && usuario.code != 0)
            {
                var result = _context.AdmintT012Usuarios.SingleOrDefault(x => x.PkT012codigo == usuario.code && x.A012Modulo.Contains("CUPOS"));
                var resultRol = _context.AdmintT015RlUsuarioRols.SingleOrDefault(x => x.A015codigoUsuario == usuario.code);

                if (result != null)
                {
                    if (usuario.password != null)
                    {
                        var _contrasena = SecurityManager.Encrypt(usuario.password);
                        if (result.A012contrasena != usuario.password)
                        {
                            valorAnterior.Add(result.A012contrasena.ToString());
                            valorActual.Add(_contrasena);
                            campos.Add("A012contrasena");
                            flag = true;
                        }
                        result.A012contrasena = _contrasena;
                    }
                    if (usuario.acceptsTerms != null)
                    {
                        if (result.A012aceptaTerminos != usuario.acceptsTerms)
                        {
                            valorAnterior.Add(result.A012aceptaTerminos.ToString());
                            valorActual.Add(usuario.acceptsTerms.ToString() ?? "");
                            campos.Add("A012aceptaTerminos");
                        }
                        result.A012aceptaTerminos = usuario.acceptsTerms is bool && (bool)usuario.acceptsTerms;
                    }
                    if (usuario.acceptsProcessingPersonalData != null)
                    {
                        if (result.A012aceptaTratamientoDatosPersonales != usuario.acceptsProcessingPersonalData)
                        {
                            valorAnterior.Add(result.A012aceptaTratamientoDatosPersonales.ToString());
                            valorActual.Add(usuario.acceptsProcessingPersonalData.ToString() ?? "");
                            campos.Add("A012aceptaTratamientoDatosPersonales");
                        }
                        result.A012aceptaTratamientoDatosPersonales = usuario.acceptsProcessingPersonalData is bool ? (bool)usuario.acceptsProcessingPersonalData : false;
                    }
                    if (usuario.celular != null)
                    {
                        if (result.A012celular != usuario.celular)
                        {
                            valorAnterior.Add(result.A012celular != null ? result.A012celular.ToString() : "");
                            valorActual.Add(usuario.celular.ToString());
                            campos.Add("A012celular");
                        }
                        result.A012celular = usuario.celular;
                    }
                    if (usuario.phone != null)
                    {
                        if (result.A012telefono != usuario.phone)
                        {
                            valorAnterior.Add(result.A012telefono.ToString());
                            valorActual.Add(usuario.phone.ToString() ?? "");
                            campos.Add("A012telefono");
                        }
                        result.A012telefono = usuario.phone is decimal ? (decimal)usuario.phone : 0;
                    }
                    if (usuario.email != null)
                    {
                        if (result.A012correoElectronico != usuario.email)
                        {
                            valorAnterior.Add(result.A012correoElectronico.ToString());
                            valorActual.Add(usuario.email.ToString());
                            campos.Add("A012correoElectronico");
                        }
                        result.A012correoElectronico = usuario.email;
                    }
                    if (usuario.address != null)
                    {
                        if (result.A012direccion != usuario.address)
                        {
                            valorAnterior.Add(result.A012direccion.ToString());
                            valorActual.Add(usuario.address.ToString());
                            campos.Add("A012direccion");
                        }
                        result.A012direccion = usuario.address;
                    }
                    if (usuario.contractStartDate != null)
                    {
                        if (result.A012fechaInicioContrato != usuario.contractStartDate)
                        {
                            valorAnterior.Add(result.A012fechaInicioContrato.ToString() ?? "");
                            valorActual.Add(usuario.contractStartDate.ToString() ?? "");
                            campos.Add("A012fechaInicioContrato");
                        }
                        result.A012fechaInicioContrato = usuario.contractStartDate;
                    }
                    if (usuario.contractFinishDate != null)
                    {
                        if (result.A012fechaFinContrato != usuario.contractFinishDate)
                        {
                            valorAnterior.Add(result.A012fechaFinContrato.ToString() ?? "");
                            valorActual.Add(usuario.contractFinishDate.ToString() ?? "");
                            campos.Add("A012fechaFinContrato");
                        }
                        result.A012fechaFinContrato = usuario.contractFinishDate;
                    }
                    if (usuario.login != null)
                    {
                        if (result.A012login != usuario.login)
                        {
                            valorAnterior.Add(result.A012login.ToString());
                            valorActual.Add(usuario.login.ToString());
                            campos.Add("A012login");
                        }
                        result.A012login = usuario.login;
                    }
                    if (usuario.identification != 0)
                    {
                        if (result.A012identificacion != usuario.identification)
                        {
                            valorAnterior.Add(result.A012identificacion.ToString());
                            valorActual.Add(usuario.identification.ToString());
                            campos.Add("A012identificacion");
                        }
                        result.A012identificacion = usuario.identification;
                    }
                    if (usuario.cityAddress != null)
                    {
                        var ciudad = _context.AdmintT004Ciudads.Where(a => a.PkT004codigo == usuario.cityAddress).FirstOrDefault();
                        if (ciudad == null)
                        {
                            return ResponseManager.generaRespuestaGenerica("Código de Ciudad no encontrado", "", token, true);
                        }
                        if (result.A012codigoCiudadDireccion != usuario.cityAddress)
                        {
                            valorAnterior.Add(result.A012codigoCiudadDireccion.ToString() ?? "");
                            valorActual.Add(usuario.cityAddress.ToString() ?? "");
                            campos.Add("A012codigoCiudadDireccion");
                        }
                        result.A012codigoCiudadDireccion = usuario.cityAddress;
                    }
                    if (usuario.codeParametricUserType != null)
                    {
                        var parametrica = _context.AdmintT008Parametricas.Where(a => a.PkT008codigo == usuario.codeParametricUserType).FirstOrDefault();
                        if (parametrica == null)
                        {
                            return ResponseManager.generaRespuestaGenerica("Código de Tipo de Usuario no encontrado", "", token, true);
                        }
                        if (result.A012codigoParametricaTipousuario != usuario.codeParametricUserType)
                        {
                            valorAnterior.Add(result.A012codigoParametricaTipousuario.ToString() ?? "");
                            valorActual.Add(usuario.codeParametricUserType.ToString() ?? "");
                            campos.Add("A012codigoParametricaTipousuario");
                        }
                        result.A012codigoParametricaTipousuario = usuario.codeParametricUserType;
                    }
                    if (usuario.codeParametricDocumentType != null)
                    {
                        var parametrica = _context.AdmintT008Parametricas.Where(a => a.PkT008codigo == usuario.codeParametricDocumentType).FirstOrDefault();
                        if (parametrica == null)
                        {
                            return ResponseManager.generaRespuestaGenerica("Código de Tipo de Documento no encontrado", "", token, true);
                        }
                        if (result.A012codigoParametricaTipoDocumento != usuario.codeParametricDocumentType)
                        {
                            valorAnterior.Add(result.A012codigoParametricaTipoDocumento.ToString() ?? "");
                            valorActual.Add(usuario.codeParametricDocumentType.ToString() ?? "");
                            campos.Add("A012codigoParametricaTipoDocumento");
                        }
                        result.A012codigoParametricaTipoDocumento = usuario.codeParametricDocumentType;
                    }

                    if (usuario.dependence != null)
                    {
                        if (result.A012dependencia != usuario.dependence)
                        {
                            valorAnterior.Add(result.A012dependencia ?? "");
                            valorActual.Add(usuario.dependence);
                            campos.Add("A012codigoParametricaDependencia");
                        }
                        result.A012dependencia = usuario.dependence;
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
                    if (usuario.firstLastName != null)
                    {
                        if (result.A012primerApellido != usuario.firstLastName)
                        {
                            valorAnterior.Add(result.A012primerApellido.ToString());
                            valorActual.Add(usuario.firstLastName.ToString());
                            campos.Add("A012primer_apellido");
                        }
                        result.A012primerApellido = usuario.firstLastName;
                    }
                    if (usuario.SecondLastName != null)
                    {
                        if (result.A012segundoApellido != usuario.SecondLastName)
                        {
                            valorAnterior.Add(result.A012segundoApellido.ToString());
                            valorActual.Add(usuario.SecondLastName.ToString());
                            campos.Add("A012segundo_apellido");
                        }
                        result.A012segundoApellido = usuario.SecondLastName;
                    }
                    if (usuario.registrationStatus != null)
                    {
                        decimal aux = usuario.registrationStatus == true ? StringHelper.estadoActivo : StringHelper.estadoInactivo;
                        if (result.A012estadoRegistro != aux && result.A012estadoRegistro != StringHelper.estadoValidarCorreo)
                        {
                            valorAnterior.Add(result.A012estadoRegistro.ToString());
                            valorActual.Add(aux.ToString());
                            campos.Add("A012estadoRegistro");

                            result.A012estadoRegistro = aux;
                        }
                    }

                    if (usuario.rol != null)
                    {
                        if (resultRol == null)
                        {
                            AdmintT015RlUsuarioRol rolUsuario = new AdmintT015RlUsuarioRol();
                            rolUsuario.A015codigoUsuario = usuario.code;
                            rolUsuario.A015codigoUsuarioCreacion = Int32.Parse(codigoUsuario);
                            rolUsuario.A015codigoUsuarioModificacion = Int32.Parse(codigoUsuario);
                            rolUsuario.A015estadoRegistro = StringHelper.estadoActivo;
                            rolUsuario.A015fechaCreacion = now;
                            rolUsuario.A015codigoRol = usuario.rol;
                            rolUsuario.A015fechaModificacion = now;
                            rolUsuario.A015estadoSolicitud = StringHelper.estadoAceptada;

                            _context.Update(rolUsuario);
                            _ = _context.SaveChanges();
                        }
                        else
                        {
                            if (resultRol.A015codigoRol != usuario.rol)
                            {
                                valorAnterior.Add(resultRol.A015codigoRol.ToString());
                                valorActual.Add(usuario.rol.ToString());
                                campos.Add("A015codigoRol");
                            }
                            resultRol.A015codigoRol = usuario.rol;
                            resultRol.A015fechaModificacion = now;

                            _context.Update(resultRol);
                            _ = _context.SaveChanges();
                        }
                    }

                    result.A012fechaModificacion = now;

                    _context.Update(result);
                    _ = _context.SaveChanges();

                    if (flag)
                    {
                        EmailHelper.enviaEmailSendGridAsync(result.A012correoElectronico, result.A012primerNombre, "Cambio de contraseña cuenta CUPOS, PRECINTOS Y MARQUILLAS", "Para realizar el ingreso con las credenciales correspondiente debes dirigirte a " + setting.getPageUrl() + "/Login/Index usando tu usuario: " + result.A012login + " y contraseña: " + usuario.password);
                    }

                    logManager.crearAuditoria(ipAddress, codigoUsuario, 3, ModuleManager.smGestionUsuarios, valorAnterior.ToArray(), valorActual.ToArray(), campos.ToArray(), "", result.A012login);
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, "", token, false);

                } else
                {
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
                }
            } else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, "", token, true);
            }

        }

        public Responses ConsultarEdit(ClaimsIdentity identity, string ipAddress, decimal id)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var usu = _context.AdmintT008Parametricas
                .Join(
                _context.AdmintT012Usuarios,
                par => par.PkT008codigo,
                usua => usua.A012estadoRegistro,
                (par, usua) => new
                {
                    usua.A012primerNombre,
                    usua.A012segundoNombre,
                    usua.A012primerApellido,
                    usua.A012segundoApellido,
                    usua.PkT012codigo,
                    usua.A012login,
                    par.A008valor,
                    usua.A012telefono,
                    usua.A012fechaExpiracontraseña,
                    usua.A012identificacion,
                    usua.A012direccion,
                    usua.A012celular,
                    usua.A012fechaInicioContrato,
                    usua.A012fechaFinContrato,
                    usua.A012correoElectronico,
                    usua.A012codigoParametricaTipoDocumento,
                    usua.A012dependencia,
                    usua.A012Modulo
                }).Join(
                _context.AdmintT015RlUsuarioRols,
                usus => usus.PkT012codigo,
                usurol => usurol.A015codigoUsuario,
                (usus, usurol) => new
                {
                    usus.A012primerNombre,
                    usus.A012segundoNombre,
                    usus.A012primerApellido,
                    usus.A012segundoApellido,
                    usus.PkT012codigo,
                    usus.A012login,
                    Roles = usurol.A015codigoRol,
                    usus.A008valor,
                    usus.A012telefono,
                    usus.A012identificacion,
                    usus.A012direccion,
                    usus.A012celular,
                    usus.A012fechaInicioContrato,
                    usus.A012fechaFinContrato,
                    usus.A012correoElectronico,
                    usus.A012codigoParametricaTipoDocumento,
                    usus.A012dependencia,
                    usus.A012Modulo
                }).Join(
                _context.AdmintT008Parametricas,
                usuar => usuar.A012codigoParametricaTipoDocumento,
                para => para.PkT008codigo,
                (usuar, para) => new
                {
                    firstName = usuar.A012primerNombre,
                    secondName = usuar.A012segundoNombre,
                    firstLastName = usuar.A012primerApellido,
                    SecondLastName = usuar.A012segundoApellido,
                    code = usuar.PkT012codigo,
                    login = usuar.A012login,
                    rol = usuar.Roles,
                    estate = usuar.A008valor,
                    phone = usuar.A012telefono,
                    identification = usuar.A012identificacion,
                    address = usuar.A012direccion,
                    celular = usuar.A012celular,
                    contractStartDate = usuar.A012fechaInicioContrato,
                    contractFinishDate = usuar.A012fechaFinContrato,
                    email = usuar.A012correoElectronico,
                    codeParametricDocumentType = para.PkT008codigo,
                    dependence = usuar.A012dependencia,
                    usuar.A012Modulo
                }).Where(s => s.code == id && s.A012Modulo.Contains("CUPOS")).FirstOrDefault();

            return ResponseManager.generaRespuestaGenerica("", usu != null ? usu : "", token, false);

        }

        private string generaLogin(string primerNombre, string primerApellido)
        {
            string login = primerNombre + primerApellido;

            var count = _context.AdmintT012Usuarios.Count(u => u.A012login.Contains(login) && u.A012Modulo.Contains("CUPOS"));

            if (count > 0)
            {
                login = login + count.ToString();
            }

            return login;
        }

        public Responses ConsultarTerminos(string? login, string ipAddress)
        {
            var usuario = _context.AdmintT012Usuarios.Where(x => x.A012login == login && x.A012Modulo.Contains("CUPOS")).FirstOrDefault();

            AceptarCondiciones permisos = new AceptarCondiciones();
            if (usuario != null)
            {
                permisos.A012aceptaTerminos = usuario.A012aceptaTerminos;
                permisos.A012aceptaTratamientoDatosPersonales = usuario.A012aceptaTratamientoDatosPersonales;
            }

            return ResponseManager.generaRespuestaGenerica("", permisos, "", false);
        }
    }
}
