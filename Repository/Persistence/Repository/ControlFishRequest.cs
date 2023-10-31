using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Data;
using System.Net;
using System.Security.Claims;
using Web.Models;
using static Repository.Helpers.Models.PermitResolution;

namespace Repository.Persistence.Repository
{
    public class ControlFishRequest : IControlFishRequest
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public ControlFishRequest(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// consultar datos de entidad
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        public Responses ConsultEntityDates(ClaimsIdentity identity, decimal documentType, decimal nitBussines)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from empresa in _context.CupostT001Empresas
                        join parametrica in _context.AdmintT008Parametricas
                        on empresa.A001codigoParametricaTipoEntidad equals parametrica.PkT008codigo
                        join ciudad in _context.AdmintT004Ciudads
                        on empresa.A001codigoCiudad equals ciudad.PkT004codigo
                        join departamento in _context.AdmintT003Departamentos
                        on ciudad.A004codigoDepartamento equals departamento.PkT003codigo
                        join pais in _context.AdmintT002Pais
                        on departamento.A003codigoPais equals pais.PkT002codigo
                        from persona in _context.CitestT003Personas.DefaultIfEmpty()
                        where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                        where persona.A003estadoRegistro == StringHelper.estadoActivo
                        where (documentType == 95 && empresa.A001nit == nitBussines) || (persona.A003codigoParametricaTipoIdentificacion == documentType && persona.A003identificacion == nitBussines.ToString())
                        where empresa.A001estadoRegistro == StringHelper.estadoActivo
                        select new
                        {
                            codigoEmpresa = empresa.PkT001codigo,
                            tipoDocumento = "Nit",
                            nombreEntidad = parametrica.A008valor,
                            nombreEmpresa = empresa.A001nombre,
                            nit = empresa.A001nit,
                            telefono = empresa.A001telefono,
                            correo = empresa.A001correo,
                            ciudad = ciudad.A004nombre,
                            departamento = departamento.A003nombre,
                            pais = pais.A002nombre,
                            direccion = empresa.A001direccion,
                            matriculaMercantil = empresa.A001matriculaMercantil
                        };


            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }

        /// <summary>
        /// consultar resoluciones por empresa
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeBussines"></param>
        /// <returns></returns>
        public Responses ConsultPermitsReslution(ClaimsIdentity identity, decimal codeBussines)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var resoluciones = _context.CupostT014Resolucion.Where(p => p.A014codigoEmpresa == codeBussines && p.A014estadoRegistro == StringHelper.estadoActivo).ToList();

            List<ResolucionPermisos> resolucionesPermiso = new List<ResolucionPermisos>();
            foreach (var resolucion in resoluciones)
            {
                resolucionesPermiso.Add(new ResolucionPermisos
                {
                    resolutionCode = resolucion.PkT014codigo,
                    resolutionNumber = resolucion.A014numeroResolucion,
                    resolutionDate = resolucion.A014fechaResolucion,
                    startDate = resolucion.A014fechaInicio,
                    endDate = resolucion.A014fechaFin,
                    resolutionObject = resolucion.A014objetoResolucion
                });
            }

            return ResponseManager.generaRespuestaGenerica("", resolucionesPermiso, token, false);
        }

        /// <summary>
        /// consultar una resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeReslution"></param>
        /// <returns></returns>
        public Responses ConsultOnePermitResolution(ClaimsIdentity identity, decimal codeReslution)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos metodo = new Metodos(_context);

            var resolucion = _context.CupostT014Resolucion.Where(p => p.PkT014codigo == codeReslution && p.A014estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

            ResolucionPermisos resolucionPermiso = new ResolucionPermisos();

            if(resolucion != null)
            {

                var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == resolucion.A014codigoDocumentoSoporte && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
                string usuraio = "";
                string clave = "";
                foreach (var c in query1)
                {
                    switch (c.A008descripcion)
                    {
                        case "USUARIO":
                            usuraio = c.A008valor;
                            break;
                        case "CONTRASEÑA":
                            clave = c.A008valor;
                            break;
                    }
                }
                if (doc != null)
                {
                    var docuemnto = metodo.CargarArchivoFtp(doc, usuraio, clave);
                    resolucionPermiso.resolutionCode = resolucion.PkT014codigo;
                    resolucionPermiso.resolutionNumber = resolucion.A014numeroResolucion;
                    resolucionPermiso.resolutionDate = resolucion.A014fechaResolucion;
                    resolucionPermiso.startDate = resolucion.A014fechaInicio;
                    resolucionPermiso.endDate = resolucion.A014fechaFin;
                    resolucionPermiso.attachment = docuemnto;
                    resolucionPermiso.resolutionObject = resolucion.A014objetoResolucion;
                }

            }
            return ResponseManager.generaRespuestaGenerica("", resolucionPermiso, token, false);
        }

        /// <summary>
        /// guardar nueva resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public Responses SaveResolution(ClaimsIdentity identity, ResolucionPermisos resolution)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos metodo = new Metodos(_context);

            if (resolution.attachment != null)
            {

                var uri = metodo.GuardarArchivoFtp(resolution.attachment);

                AdmintT009Documento docNuevo = new AdmintT009Documento();
                docNuevo.A009fechaCreacion = DateTime.Now;
                docNuevo.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
                docNuevo.A009codigoParametricaTipoDocumento = 25;
                docNuevo.A009firmaDigital = "firma";
                docNuevo.A009codigoPlantilla = 1;
                docNuevo.A009documento = resolution.attachment.attachmentName ?? "";
                docNuevo.A009descripcion = resolution.attachment.attachmentName ?? "";
                docNuevo.A009url = uri;

                _context.AdmintT009Documentos.Add(docNuevo);
                _context.SaveChanges();

                CupostT014Resolucion resolucion = new CupostT014Resolucion();
                resolucion.A014estadoRegistro = StringHelper.estadoActivo;
                resolucion.A014fechaCreacion = DateTime.Now;
                resolucion.A014codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                resolucion.A014codigoEmpresa = resolution.companyCode;
                resolucion.A014numeroResolucion = resolution.resolutionNumber;
                resolucion.A014fechaResolucion = resolution.resolutionDate;
                resolucion.A014fechaInicio = resolution.startDate;
                resolucion.A014fechaFin = resolution.endDate;
                resolucion.A014objetoResolucion = resolution.resolutionObject;
                resolucion.A014codigoDocumentoSoporte = docNuevo.PkT009codigo;

                _context.CupostT014Resolucion.Add(resolucion);
                _context.SaveChanges();
            }
            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
        }

        /// <summary>
        /// editar resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public Responses EditResolution(ClaimsIdentity identity, ResolucionPermisos resolution)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var resolucion = _context.CupostT014Resolucion.Where(p => p.PkT014codigo == resolution.resolutionCode).FirstOrDefault();
            if (resolucion != null)
            {
                var docvalidar = _context.AdmintT009Documentos.Where(x => x.PkT009codigo == resolucion.A014codigoDocumentoSoporte && x.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                if (resolution.attachment != null)
                {
                    Metodos metodo = new Metodos(_context);
                    if (docvalidar == null || docvalidar.A009documento != resolution.attachment.attachmentName)
                    {
                        var uri = metodo.GuardarArchivoFtp(resolution.attachment);
                        if (docvalidar != null)
                        {
                            docvalidar.A009estadoRegistro = StringHelper.estadoInactivo;
                            docvalidar.A009codigoUsuarioModificacion = 1;
                            docvalidar.A009fechaModificacion = DateTime.Now;
                            _context.SaveChanges();
                        }


                        AdmintT009Documento newDoc = new AdmintT009Documento();
                        newDoc.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        newDoc.A009codigoParametricaTipoDocumento = 25;
                        newDoc.A009codigoPlantilla = 1;
                        newDoc.A009estadoRegistro = StringHelper.estadoActivo;
                        newDoc.A009fechaCreacion = DateTime.Now;
                        newDoc.A009firmaDigital = "firma";
                        newDoc.A009documento = resolution.attachment.attachmentName ?? "";
                        newDoc.A009descripcion = resolution.attachment.attachmentName ?? "";
                        newDoc.A009url = uri;

                        _context.AdmintT009Documentos.Add(newDoc);

                        _context.SaveChanges();

                        resolucion.A014codigoDocumentoSoporte = newDoc.PkT009codigo;
                    }
                    resolucion.A014estadoRegistro = StringHelper.estadoActivo;
                    resolucion.A014fechaModificacion = DateTime.Now;
                    resolucion.A014codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    resolucion.A014numeroResolucion = resolution.resolutionNumber;
                    resolucion.A014fechaResolucion = resolution.resolutionDate;
                    resolucion.A014fechaInicio = resolution.startDate;
                    resolucion.A014fechaFin = resolution.endDate;
                    resolucion.A014objetoResolucion = resolution.resolutionObject;

                    _context.SaveChanges();
                }
            }
            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
        }

        /// <summary>
        /// deshabilitar resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeResolution"></param>
        /// <returns></returns>
        public Responses DeleteResolution( ClaimsIdentity identity, decimal codeResolution)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var resolucion = _context.CupostT014Resolucion.Where(p => p.PkT014codigo == codeResolution && p.A014estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
            if (resolucion != null)
            {
                resolucion.A014estadoRegistro = StringHelper.estadoInactivo;
                resolucion.A014fechaModificacion = DateTime.Now;
                resolucion.A014codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == resolucion.A014codigoDocumentoSoporte && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                if (doc != null)
                {
                    doc.A009estadoRegistro = StringHelper.estadoInactivo;
                    doc.A009fechaModificacion = DateTime.Now;
                    doc.A009codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                }
            }

            _context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgEliminadoExitoso, true, token, false);
        }

    }
}
