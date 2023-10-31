using API.Helpers;
using Microsoft.AspNetCore.Http;
using NHibernate.Driver;
using NHibernate.Linq;
using Repository.Configurations;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Persistence.Repository
{
    public class NonTimberFloraCertificate : INonTimberFloraCertificate
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;


        public NonTimberFloraCertificate(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }

        /// <summary>
        /// consultar autoridades
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultAuthority(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from documentTypes in _context.AdmintT008Parametricas
                        where documentTypes.A008parametrica == StringHelper.autoridadEmite
                        select new
                        {
                            code = documentTypes.PkT008codigo,
                            name = documentTypes.A008valor
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        /// <summary>
        /// consultar tipo producto
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEspecimensProductsType(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from especimensProductsType in _context.AdmintT008Parametricas
                        where especimensProductsType.A008parametrica == "TIPO ESPECIMENES PRODUCTO IMPORTAR EXPORTAR"
                        select new
                        {
                            code = especimensProductsType.PkT008codigo,
                            name = especimensProductsType.A008valor
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }

        /// <summary>
        /// consultar tipos de documento
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultDocumentsTypes(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from documentTypes in _context.AdmintT008Parametricas
                        where documentTypes.A008parametrica == "TIPO DE DOCUMENTO" && documentTypes.A008estadoRegistro == StringHelper.estadoActivo
                        select new
                        {
                            code = documentTypes.PkT008codigo,
                            name = documentTypes.A008valor
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }

        /// <summary>
        /// consultar certificados
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultCertificates(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from certificates in _context.CupostT021CertificadoFloraNoMaderable
                        join empresa in _context.CupostT001Empresas
                        on certificates.A021CodigoEmpresa equals empresa.PkT001codigo
                        where certificates.A021TipoCertificado == StringHelper.tipoCetificadosFloraNoMaderable
                        where certificates.A021EstadoRegistro== StringHelper.estadoActivo
                        select new
                        {
                            codigoCertificado = certificates.Pk_T021Codigo,
                            numeroCertificacion = certificates.A021NumeroCertificado,
                            fechaCertificacion = certificates.A021FechaCertificacion,
                            fechaRegistroCertificacion = certificates.A021FechaRegistroCertificado,
                            nit = empresa.A001nit,
                            vigenciaCertificacion = certificates.A021VigenciaCertificacion
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);

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
                        where (documentType == StringHelper.tipoDocumentoNit && empresa.A001nit == nitBussines) || (persona.A003codigoParametricaTipoIdentificacion == documentType && persona.A003identificacion == nitBussines.ToString())
                        select new
                        {
                            nombreEntidad = parametrica.A008valor,
                            nombreEmpresa = empresa.A001nombre,
                            nit = empresa.A001nit,
                            telefono = empresa.A001telefono,
                            correo = empresa.A001correo,
                            ciudad = ciudad.A004nombre,
                            departamento = departamento.A003nombre,
                            pais = pais.A002nombre,
                            direccion = empresa.A001direccion
                        };


            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }

        /// <summary>
        /// consultar certificados por nit o numero de certificado o ambas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <param name="CertificateNumber"></param>
        /// <returns></returns>
        public Responses ConsultCertificatesForNit(ClaimsIdentity identity, decimal documentType, decimal nitBussines, string CertificateNumber)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            object query = new object();

            if (CertificateNumber!=null && CertificateNumber!="0" && nitBussines!=0)
            {

                query = from certificates in _context.CupostT021CertificadoFloraNoMaderable
                            join empresa in _context.CupostT001Empresas
                            on certificates.A021CodigoEmpresa equals empresa.PkT001codigo
                            from persona in _context.CitestT003Personas.DefaultIfEmpty()
                            where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                            where certificates.A021TipoCertificado == StringHelper.tipoCetificadosFloraNoMaderable
                            where certificates.A021EstadoRegistro == StringHelper.estadoActivo
                            where certificates.A021NumeroCertificado==CertificateNumber
                            where (documentType == StringHelper.tipoDocumentoNit && empresa.A001nit == nitBussines) || (persona.A003codigoParametricaTipoIdentificacion == documentType && persona.A003identificacion == nitBussines.ToString())
                            select new
                            {
                                codigoCertificado = certificates.Pk_T021Codigo,
                                numeroCertificacion = certificates.A021NumeroCertificado,
                                fechaCertificacion = certificates.A021FechaCertificacion,
                                fechaRegistroCertificacion = certificates.A021FechaRegistroCertificado,
                                nit = empresa.A001nit,
                                vigenciaCertificacion = certificates.A021VigenciaCertificacion
                            };

            }
            else if (CertificateNumber == null || CertificateNumber == "0" && nitBussines != 0)
            {
                query = from certificates in _context.CupostT021CertificadoFloraNoMaderable
                            join empresa in _context.CupostT001Empresas
                            on certificates.A021CodigoEmpresa equals empresa.PkT001codigo
                            from persona in _context.CitestT003Personas.DefaultIfEmpty()
                            where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                            where certificates.A021TipoCertificado == StringHelper.tipoCetificadosFloraNoMaderable
                            where certificates.A021EstadoRegistro== StringHelper.estadoActivo
                            where (documentType == StringHelper.tipoDocumentoNit && empresa.A001nit == nitBussines) || (persona.A003codigoParametricaTipoIdentificacion == documentType && persona.A003identificacion == nitBussines.ToString())
                            select new
                            {
                                codigoCertificado = certificates.Pk_T021Codigo,
                                numeroCertificacion = certificates.A021NumeroCertificado,
                                fechaCertificacion = certificates.A021FechaCertificacion,
                                fechaRegistroCertificacion = certificates.A021FechaRegistroCertificado,
                                nit = empresa.A001nit,
                                vigenciaCertificacion = certificates.A021VigenciaCertificacion
                            };
            }else if (CertificateNumber != null && CertificateNumber != "0" && nitBussines == 0)
            {
                query = from certificates in _context.CupostT021CertificadoFloraNoMaderable
                        join empresa in _context.CupostT001Empresas
                        on certificates.A021CodigoEmpresa equals empresa.PkT001codigo
                        from persona in _context.CitestT003Personas.DefaultIfEmpty()
                        where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                        where certificates.A021TipoCertificado == StringHelper.tipoCetificadosFloraNoMaderable
                        where certificates.A021EstadoRegistro == StringHelper.estadoActivo
                        where certificates.A021NumeroCertificado == CertificateNumber
                        select new
                        {
                            codigoCertificado = certificates.Pk_T021Codigo,
                            numeroCertificacion = certificates.A021NumeroCertificado,
                            fechaCertificacion = certificates.A021FechaCertificacion,
                            fechaRegistroCertificacion = certificates.A021FechaRegistroCertificado,
                            nit = empresa.A001nit,
                            vigenciaCertificacion = certificates.A021VigenciaCertificacion
                        };
            }


            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }

        /// <summary>
        /// consultar tipos especimenes
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEspecimensTypes(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from especimenes in _context.AdmintT005Especimen
                        where especimenes.A005reino == "Plantae"
                        select new
                        {
                            id = especimenes.PkT005codigo,
                            text = especimenes.A005nombreCientifico
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        /// <summary>
        /// guardar certificado nuevo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datosGuardar"></param>
        /// <returns></returns>
        public Responses SaveCertificate(ClaimsIdentity identity, CertificateData datosGuardar, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var bs = _context.CupostT001Empresas.Where(p => p.A001nit == datosGuardar.companyNit).FirstOrDefault();

            CupostT021CertificadoFloraNoMaderable certificadoNuevo = new CupostT021CertificadoFloraNoMaderable();
            certificadoNuevo.A021UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            certificadoNuevo.A021FechaCreacion = DateTime.Now;
            certificadoNuevo.A021FechaCertificacion = datosGuardar.certificationDate;
            certificadoNuevo.A021VigenciaCertificacion = datosGuardar.certificationValidity;
            certificadoNuevo.A021TipoCertificado = StringHelper.tipoCetificadosFloraNoMaderable;
            certificadoNuevo.A021AutoridadEmiteCertificado = datosGuardar.issuingAuthority ?? "";
            certificadoNuevo.A021NumeroCertificado = datosGuardar.certificateNumber;
            certificadoNuevo.A021TipoPermiso = datosGuardar.permissionType ?? "";
            certificadoNuevo.A021Observaciones = datosGuardar.certificateRemarks;
            certificadoNuevo.A021EstadoRegistro = StringHelper.estadoActivo;
            certificadoNuevo.A021TipoEspecimenProductoImpExp = datosGuardar.specimenProductImpExpType;
            if (bs != null)
            {
                certificadoNuevo.A021CodigoEmpresa = bs.PkT001codigo;
            }
            certificadoNuevo.A021FechaRegistroCertificado = DateTime.Now;

            _context.CupostT021CertificadoFloraNoMaderable.Add(certificadoNuevo);
            _context.SaveChanges();
            logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smMovimientoFlora, "", "", "", certificadoNuevo, certificadoNuevo.Pk_T021Codigo.ToString());

            List<CupostT022RlCertificadoFloraNoMaderableDocumento> rlCertDocNuevo = new List<CupostT022RlCertificadoFloraNoMaderableDocumento>();

            Metodos metodo = new Metodos(_context);

            if (datosGuardar.supportingDocuments != null)
            {

                foreach (var documento in datosGuardar.supportingDocuments)
                {

                    var uri = metodo.GuardarArchivoFtp(documento);

                    AdmintT009Documento docNuevo = new AdmintT009Documento();
                    docNuevo.A009fechaCreacion = DateTime.Now;
                    docNuevo.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
                    docNuevo.A009codigoParametricaTipoDocumento = 25;
                    docNuevo.A009firmaDigital = "firma";
                    docNuevo.A009codigoPlantilla = 1;
                    docNuevo.A009documento = documento.attachmentName ?? "";
                    docNuevo.A009descripcion = documento.attachmentName ?? "";
                    docNuevo.A009url = uri;

                    _context.AdmintT009Documentos.Add(docNuevo);
                    _context.SaveChanges();
                    logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smMovimientoFlora, "", "", "", docNuevo, docNuevo.A009documento);

                    rlCertDocNuevo.Add(new CupostT022RlCertificadoFloraNoMaderableDocumento
                    {
                        A022EstadoRegistro = StringHelper.estadoActivo,
                        A022FechaCreacion = DateTime.Now,
                        A022UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        A022CodigoCertificadoFloraNoMaderable = certificadoNuevo.Pk_T021Codigo,
                        A022CodigoDocuemento = docNuevo.PkT009codigo
                    });

                }
                foreach (var rlCert in rlCertDocNuevo)
                {
                    _context.CupostT022RlCertificadoFloraNoMaderableDocumento.Add(rlCert);
                    _context.SaveChanges();
                    logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smMovimientoFlora, "", "", "", rlCert, rlCert.Pk_T022Codigo.ToString());
                }
            }

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
        }

        /// <summary>
        /// consultar un certificado por codigo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeCertificate"></param>
        /// <returns></returns>
        public Responses ConsultDatasCertificate(ClaimsIdentity identity, decimal codeCertificate)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var certificado = _context.CupostT021CertificadoFloraNoMaderable.Where(p => p.Pk_T021Codigo == codeCertificate && p.A021EstadoRegistro== StringHelper.estadoActivo).FirstOrDefault();

            var rldocs = _context.CupostT022RlCertificadoFloraNoMaderableDocumento.Where(p => p.A022CodigoCertificadoFloraNoMaderable == codeCertificate && p.A022EstadoRegistro == StringHelper.estadoActivo).ToList();

            List<SupportDocuments> docSoporte = new List<SupportDocuments>();

            Metodos metodo = new Metodos(_context);

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

            foreach (var docs in rldocs)
            {
                var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == docs.A022CodigoDocuemento && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                if (doc != null)
                {
                    docSoporte.Add(metodo.CargarArchivoFtp(doc, usuraio, clave));
                }
            }

            CertificateData certificadoRetornar = new CertificateData();
            if (certificado != null)
            {
                certificadoRetornar.certificationValidity = certificado.A021VigenciaCertificacion;
                certificadoRetornar.issuingAuthority = certificado.A021AutoridadEmiteCertificado;
                certificadoRetornar.certificateRemarks = certificado.A021Observaciones;
                certificadoRetornar.certificationDate = certificado.A021FechaCertificacion;
                certificadoRetornar.certificateNumber = certificado.A021NumeroCertificado;
                certificadoRetornar.permissionType = certificado.A021TipoPermiso;
                certificadoRetornar.specimenProductImpExpType = certificado.A021TipoEspecimenProductoImpExp;
                certificadoRetornar.supportingDocuments = docSoporte;
            }


            return ResponseManager.generaRespuestaGenerica("", certificadoRetornar, token, false);

        }

        /// <summary>
        /// editar certificado
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datosGuardar"></param>
        /// <returns></returns>
        public Responses SaveEditCertificate(ClaimsIdentity identity, CertificateData datosGuardar)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }


            var certificado = _context.CupostT021CertificadoFloraNoMaderable.Where(p => p.Pk_T021Codigo == datosGuardar.code).FirstOrDefault();
            if (certificado != null)
            {
                certificado.A021UsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                certificado.A021FechaModificacion = DateTime.Now;
                certificado.A021FechaCertificacion = datosGuardar.certificationDate;
                certificado.A021VigenciaCertificacion = datosGuardar.certificationValidity;
                certificado.A021AutoridadEmiteCertificado = datosGuardar.issuingAuthority ?? "";
                certificado.A021NumeroCertificado = datosGuardar.certificateNumber;
                certificado.A021TipoPermiso = datosGuardar.permissionType ?? "";
                certificado.A021TipoEspecimenProductoImpExp = datosGuardar.specimenProductImpExpType;
                certificado.A021Observaciones = datosGuardar.certificateRemarks;
            }

            _context.SaveChanges();

            List<CupostT022RlCertificadoFloraNoMaderableDocumento> rlCertDocNuevo = new List<CupostT022RlCertificadoFloraNoMaderableDocumento>();
            Metodos metodo = new Metodos(_context);
            if (datosGuardar.newSupportingDocuments != null && datosGuardar.newSupportingDocuments.Count > 0)
            {
                foreach (var documento in datosGuardar.newSupportingDocuments)
                {

                    var uri = metodo.GuardarArchivoFtp(documento);

                    AdmintT009Documento docNuevo = new AdmintT009Documento();
                    docNuevo.A009fechaCreacion = DateTime.Now;
                    docNuevo.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
                    docNuevo.A009codigoParametricaTipoDocumento = StringHelper.tipoDocumentoAdjuntoOtro;
                    docNuevo.A009firmaDigital = "firma";
                    docNuevo.A009codigoPlantilla = 1;
                    docNuevo.A009documento = documento.attachmentName ?? "";
                    docNuevo.A009descripcion = documento.attachmentName ?? "";
                    docNuevo.A009url = uri;

                    _context.AdmintT009Documentos.Add(docNuevo);
                    _context.SaveChanges();

                    rlCertDocNuevo.Add(new CupostT022RlCertificadoFloraNoMaderableDocumento
                    {
                        A022EstadoRegistro = StringHelper.estadoActivo,
                        A022FechaCreacion = DateTime.Now,
                        A022UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        A022CodigoCertificadoFloraNoMaderable = datosGuardar.code,
                        A022CodigoDocuemento = docNuevo.PkT009codigo
                    });

                }

                if (rlCertDocNuevo.Count > 0)
                {
                    foreach (var rlCert in rlCertDocNuevo)
                    {
                        _context.CupostT022RlCertificadoFloraNoMaderableDocumento.Add(rlCert);
                        _context.SaveChanges();
                    }
                }
            }

            if(datosGuardar.deletedSupportingDocuments!=null && datosGuardar.deletedSupportingDocuments.Count > 0)
            {
                for (int i = 0; i < datosGuardar.deletedSupportingDocuments.Count; i++)
                {
                    SupportDocuments? doc = datosGuardar.deletedSupportingDocuments[i];
                    var document = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == doc.code).FirstOrDefault();
                    if (document != null)
                    {
                        document.A009estadoRegistro = StringHelper.estadoInactivo;
                        document.A009codigoUsuarioModificacion= Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        document.A009fechaModificacion = DateTime.Now;
                    }


                    var rlDocCert = _context.CupostT022RlCertificadoFloraNoMaderableDocumento.Where(p => p.A022CodigoCertificadoFloraNoMaderable == datosGuardar.code && p.A022CodigoDocuemento == doc.code).FirstOrDefault();
                    if (rlDocCert != null)
                    {
                        rlDocCert.A022EstadoRegistro = StringHelper.estadoInactivo;
                        rlDocCert.A022UsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        rlDocCert.A022FechaModificacion = DateTime.Now;
                    }

                    _context.SaveChanges();
                }
            }

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
        }

        /// <summary>
        /// eliminar certificado
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeCertificate"></param>
        /// <returns></returns>
        public Responses DeleteCertificate(ClaimsIdentity identity, decimal codeCertificate)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var certificado = _context.CupostT021CertificadoFloraNoMaderable.Where(p => p.Pk_T021Codigo == codeCertificate).FirstOrDefault();
            if(certificado != null)
            {
                certificado.A021EstadoRegistro = StringHelper.estadoInactivo;
                certificado.A021UsuarioModificacion= Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                certificado.A021FechaModificacion = DateTime.Now;
            }

            _context.SaveChanges();

            var relacion = _context.CupostT022RlCertificadoFloraNoMaderableDocumento.Where(p => p.A022CodigoCertificadoFloraNoMaderable == codeCertificate && p.A022EstadoRegistro == StringHelper.estadoActivo).ToList();
            foreach(var rl in relacion)
            {
                rl.A022EstadoRegistro = StringHelper.estadoInactivo;
                rl.A022UsuarioModificacion= Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                rl.A022FechaModificacion = DateTime.Now;

                var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == rl.A022CodigoDocuemento).FirstOrDefault();
                if (doc != null)
                {
                    doc.A009estadoRegistro = StringHelper.estadoInactivo;
                    doc.A009codigoUsuarioModificacion= Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    doc.A009fechaModificacion = DateTime.Now;
                }
            }
            _context.SaveChanges();

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgEliminadoExitoso, true, token, false);
        }

    }
}
