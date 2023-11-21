using API.Helpers;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using NHibernate.Impl;
using NHibernate.Linq;
using Org.BouncyCastle.Utilities.Net;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    public class ResolutionRegister : IResolutionRegister
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public ResolutionRegister(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// consultar datos de la entidad
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public Responses ConsultEntityDates(ClaimsIdentity identity, decimal documentType, string nitBussines, decimal? entityType)
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
                        join parametricaEstado in _context.AdmintT008Parametricas
                        on empresa.A001estadoRegistro equals parametricaEstado.PkT008codigo
                        from persona in _context.CitestT003Personas.DefaultIfEmpty()
                        where persona.PkT003codigo == empresa.A001codigoPersonaRepresentantelegal
                        where (documentType == 95 && empresa.A001nit == Convert.ToDecimal(nitBussines)
                        && (entityType == null || empresa.A001codigoParametricaTipoEntidad == entityType)) 
                        || (persona.A003codigoParametricaTipoIdentificacion == documentType && persona.A003identificacion == nitBussines)
                        select new
                        {
                            codigoEmpresa = empresa.PkT001codigo,
                            idtipoEntidad = parametrica.PkT008codigo,
                            nombreEntidad = parametrica.A008valor,
                            nombreEmpresa = empresa.A001nombre,
                            nit = empresa.A001nit,
                            telefono = empresa.A001telefono,
                            correo = empresa.A001correo,
                            idciudad = ciudad.PkT004codigo,
                            ciudad = ciudad.A004nombre,
                            iddepartamento = departamento.PkT003codigo,
                            departamento = departamento.A003nombre,
                            idpais = pais.PkT002codigo,
                            pais = pais.A002nombre,
                            direccion = empresa.A001direccion,
                            idestado = parametricaEstado.PkT008codigo,
                            estado = parametricaEstado.A008valor
                        };

                return ResponseManager.generaRespuestaGenerica("", query, token, false);
           
        }

        /// <summary>
        /// consultar cupos 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        public Responses ConsultQuotas(ClaimsIdentity identity, string nitBussines, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var bs = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == nitBussines).FirstOrDefault();

            var query = from cupos in _context.CuposV001ResolucionCupos
                        select cupos;
            object qry = new object();
            if (bs != null)
            {
                qry = from cupos in query
                        where cupos.codigoEmpresa == bs.PkT001codigo
                        select new
                        {
                            cupos.codigoCupo,
                            cupos.autoridadEmiteResolucion,
                            cupos.codigoZoocriadero,
                            cupos.numeroResolucion,
                            cupos.fechaResolucion,
                            cupos.fechaRegistroResolucion,
                            cupos.fechaRadicado,
                            cupos.cuposOtorgados,
                            cupos.cuposPorAnio,
                            cupos.cuposTotal,
                            cupos.fechaProduccion,
                            cupos.cuposAprovechamientoComercializacion,
                            cupos.cuotaRepoblacion,
                            cupos.cuposDisponibles,
                            cupos.codigoEspecie,
                            cupos.numeroInternoInicialCuotaRepoblacion,
                            cupos.numeroInternoFinalCuotaRepoblacion,
                            cupos.NumeroInternoInicial,
                            cupos.numeroInternoFinal
                        };
                var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Metodos met = new Metodos(_context);
                met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smOtorgamientoCupos, null, null, null, 1, null, null);
            }
            return ResponseManager.generaRespuestaGenerica("", qry, token, false);
        }

        /// <summary>
        /// consultar cupos por numero de resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ResolutionNumbre"></param>
        /// <returns></returns>
        public Responses SearchQuotas(ClaimsIdentity identity, decimal? ResolutionNumbre, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from cupos in _context.CuposV001ResolucionCupos
                        select new
                        {
                            cupos.numeroResolucion,
                            cupos.autoridadEmiteResolucion,
                            cupos.codigoZoocriadero,
                            cupos.fechaResolucion,
                            cupos.fechaRegistroResolucion,
                            cupos.fechaRadicado,
                            cupos.cuposOtorgados,
                            cupos.cuposPorAnio,
                            cupos.cuposTotal,
                            cupos.fechaProduccion,
                            cupos.cuposAprovechamientoComercializacion,
                            cupos.cuotaRepoblacion,
                            cupos.cuposDisponibles,
                            cupos.codigoEspecie

                        };
            var query2 = from cupo in query
                         where cupo.numeroResolucion == ResolutionNumbre
                         select cupo;

            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smOtorgamientoCupos, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", query2, token, false);
        }

        /// <summary>
        /// consultar inventario
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultInventory(ClaimsIdentity identity, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from cartaVenta in _context.CupostT004FacturacompraCartaventa 
                        join empresa in _context.CupostT001Empresas on  cartaVenta.A004codigoEntidadVende equals empresa.PkT001codigo
                        join cupos in _context.CupostT002Cupos on cartaVenta.A004codigoCupo equals cupos.PkT002codigo
                        join especie in _context.CupostT005Especieaexportars on cupos.PkT002codigo equals especie.A005codigoCupo
                        where cupos.A002estadoRegistro== StringHelper.estadoActivo
                        where cartaVenta.A004estadoRegistro== StringHelper.estadoActivo
                        select new
                        {
                            especie=Convert.ToDecimal(especie.A005codigoEspecie),
                            codigo = cartaVenta.PkT004codigo,
                            numeroCartaVendeFactura = cartaVenta.A004numeroCartaVenta,
                            quienVende = empresa.A001nombre,
                            fechaVenta = cartaVenta.A004fechaVenta,
                            disponibilidadInventario = cartaVenta.A004disponibilidadInventario,
                            fechaProduccion = cupos.A002fechaProduccion,
                            inventarioDisponible = cartaVenta.A004totalCuposVendidos
                        };
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smOtorgamientoCupos, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        /// <summary>
        /// consultar tipos de marcaje
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEntityTypes(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from parametrica in _context.AdmintT008Parametricas
                        where parametrica.A008parametrica == "TIPO EMPRESA"
                        select new
                        {
                            code = parametrica.PkT008codigo,
                            name = parametrica.A008valor
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        /// <summary>
        /// consultar tipos de marcaje
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultMarkingType(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from parametrica in _context.AdmintT008Parametricas
                        where parametrica.A008parametrica== "TIPO DE MARCAJE"
                        select new 
                        { 
                            code=parametrica.PkT008codigo,
                            name=parametrica.A008valor 
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        /// <summary>
        /// consultar pago de repoblacion
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultRepoblationPay(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from parametrica in _context.AdmintT008Parametricas
                        where parametrica.A008parametrica == "PAGO CUOTA REPOBLACIÓN"
                        select new
                        {
                            code = parametrica.PkT008codigo,
                            name = parametrica.A008valor
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }

        /// <summary>
        /// consultar tipos de especimenes
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
                        where especimenes.A005reino== "Animalia"
                        select new
                        {
                            id = especimenes.PkT005codigo,
                            text= especimenes.A005nombreCientifico
                        };

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
        
        /// <summary>
        /// consultar un cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        public Responses ConsultOneQuota(ClaimsIdentity identity, decimal quotaCode, string ipAddress)
        {
            try
            {

                Metodos met = new Metodos(_context);
                var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var token = jwtAuthenticationManager.generarJWT(identity);
                if (token == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
                }
                var cupos = _context.CupostT002Cupos.Where(p => p.PkT002codigo == quotaCode && p.A002estadoRegistro==StringHelper.estadoActivo).FirstOrDefault();
                CupoGuardar resolucionInfo = new CupoGuardar();
                ExportSpecimens especiesExportar = new ExportSpecimens();
                if (cupos != null)
                {
                    resolucionInfo.issuingAuthority = cupos.A002AutoridadEmiteResolucion;
                    resolucionInfo.zoocriaderoCode = cupos.A002CodigoZoocriadero;
                    resolucionInfo.resolutionNumber = cupos.A002numeroResolucion;
                    resolucionInfo.resolutionDate = cupos.A002fechaResolucion;
                    resolucionInfo.resolutionRegistrationDate = cupos.A002fechaRegistroResolucion;
                    resolucionInfo.observations = cupos.A002observaciones;

                    List<SupportDocuments> documentos = new List<SupportDocuments>();

                    Metodos metodos = new Metodos(_context);
                    var rlDocCupo = _context.CupostT023RlCupoDocumento.Where(p => p.A023CodigoCupo == quotaCode && p.A023EstadoRegistro == StringHelper.estadoActivo).ToList();


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

                    foreach (var docCode in rlDocCupo)
                    {

                        var doc = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == docCode.A023CodigoDocuemento && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                        if (doc != null)
                        {
                            documentos.Add(metodos.CargarArchivoFtp(doc, usuraio, clave));
                            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smOtorgamientoCupos, null, null, null, 1, null, "doc");
                        }
                    }


                    var esp = _context.CupostT005Especieaexportars.Where(p => p.A005codigoCupo == quotaCode && p.A005estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                    if (esp != null)
                    {

                        especiesExportar.quotaCode = esp.A005codigoCupo;
                        especiesExportar.year = esp.A005añoProduccion;
                        especiesExportar.quotas = Convert.ToDecimal(esp.A005cupoAprovechamientoOtorgados);
                        especiesExportar.id = esp.PkT005codigo;
                        especiesExportar.totalQuotas = cupos.A002cuposTotal;
                        especiesExportar.availableQuotas = cupos.A002cuposDisponibles;
                        especiesExportar.repopulationQuota = cupos.A002cuotaRepoblacion;
                        especiesExportar.markingTypeParametricCode = esp.A005codigoParametricaTipoMarcaje;
                        especiesExportar.speciesCode = esp.A005codigoEspecie;
                        especiesExportar.repopulationQuotaPaymentParametricCode = esp.A005codigoParametricaPagoCuotaDerepoblacion;
                        especiesExportar.filingDate = esp.A005fechaRadicado;
                        especiesExportar.specimenType = esp.A005nombreEspecie;
                        especiesExportar.productionYear = esp.A005añoProduccion;
                        especiesExportar.batchCode = esp.A005marcaLote;
                        especiesExportar.markingConditions = esp.A005condicionesMarcaje;
                        especiesExportar.parentalPopulationMale = esp.A005poblacionParentalMacho;
                        especiesExportar.parentalPopulationFemale = esp.A005poblacionParentalHembra;
                        especiesExportar.totalParentalPopulation = esp.A005poblacionParentalTotal;
                        especiesExportar.populationFromIncubator = esp.A005poblacionSalioDeIncubadora;
                        especiesExportar.populationAvailableForQuotas = esp.A005poblacionDisponibleParaCupos;
                        especiesExportar.individualsForRepopulation = esp.A005individuosDestinadosARepoblacion;
                        especiesExportar.grantedUtilizationQuotas = Convert.ToDecimal(esp.A005cupoAprovechamientoOtorgados);
                        especiesExportar.replacementRate = esp.A005tasaReposicion;
                        especiesExportar.mortalityNumber = esp.A005numeroMortalidad;
                        especiesExportar.repopulationQuotaForUtilization = esp.A005cuotaRepoblacionParaAprovechamiento;
                        especiesExportar.grantedPaidUtilizationQuotas = esp.A005cupoAprovechamientoOtorgadosPagados;
                        especiesExportar.observations = esp.A005observaciones;
                        especiesExportar.speciesRegisteredForCommercialization = true;
                        especiesExportar.repopulationQuotaPaid = especiesExportar.repopulationQuotaPaymentParametricCode == "0" ? false : true;
                        especiesExportar.initialRepopulationQuotaInternalNumber = esp.A005NumeroInternoInicialCuotaRepoblacion.ToString();
                        especiesExportar.finalRepopulationQuotaInternalNumber = esp.A005NumeroInternoFinalCuotaRepoblacion.ToString();
                        especiesExportar.initialInternalNumber = cupos.A002rangoCodigoInicial;
                        especiesExportar.finalInternalNumber = cupos.A002rangoCodigoFin;
                        especiesExportar.supportingDocuments = documentos;

                    }
                }
                var qry3 = new
                {
                    resolucionInfo,
                    especiesExportar
                };

                met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smOtorgamientoCupos, null, null, null, 1, null, null);

                return ResponseManager.generaRespuestaGenerica("", qry3, token, false);
            }
            catch(Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }

        /// <summary>
        /// editar resolucion cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses EditDeleteResolutionQuota(ClaimsIdentity identity, SaveResolutionQuotas datas, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            try
            {
                Metodos met = new Metodos(_context);
                List<string> valorAnterior = new List<string>();
                List<string> valorActual = new List<string>();
                List<string> campos = new List<string>();

                var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                #region edicion de informacion

                if (datas.dataToSave != null && datas.newExportSpeciesData != null)
                {

                    var cupos = _context.CupostT002Cupos.Where(p => p.PkT002codigo == datas.dataToSave.quotaCode && p.A002estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

                    if (cupos != null)
                    {
                        var query2 = _context.CupostT002Cupos.Where(p => p.A002estadoRegistro == StringHelper.estadoActivo && p.A002numeroResolucion == cupos.A002numeroResolucion).ToList();

                        foreach (var c in query2)
                        {
                            #region auditoria
                            
                            var codUsuarioModificacion = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == cupos.A002codigoUsuarioModificacion).FirstOrDefault();
                            var userNew = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();

                            if (codUsuarioModificacion != null)
                            {
                                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, codUsuarioModificacion.A012primerNombre + " " + codUsuarioModificacion.A012segundoNombre + " " + codUsuarioModificacion.A012primerApellido + " " + codUsuarioModificacion.A012segundoApellido, userNew.A012primerNombre + " " + userNew.A012segundoNombre + " " + userNew.A012primerApellido + " " + userNew.A012segundoApellido, "A002codigoUsuarioModificacion");
                                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002fechaModificacion.ToString(), DateTime.Now.ToString(), "A002fechaModificacion");
                            }
                            else
                            {
                                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, "", userNew.A012primerNombre + " " + userNew.A012segundoNombre + " " + userNew.A012primerApellido + " " + userNew.A012segundoApellido, "A002codigoUsuarioModificacion");
                                met.validarCampoEditadoAud(valorAnterior, valorActual, campos, "", DateTime.Now.ToString(), "A002fechaModificacion");
                            }

                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002numeroResolucion.ToString(), datas.dataToSave.resolutionNumber.ToString(), "A002numeroResolucion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002fechaResolucion.ToString(), datas.dataToSave.resolutionDate.ToString(), "A002fechaResolucion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002fechaRegistroResolucion.ToString(), datas.dataToSave.resolutionRegistrationDate.ToString(), "A002fechaRegistroResolucion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002AutoridadEmiteResolucion.ToString(), datas.dataToSave.issuingAuthority.ToString() ?? "", "A002AutoridadEmiteResolucion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002observaciones == null ? "" : cupos.A002observaciones.ToString(), datas.dataToSave.observations == null ? "" : datas.dataToSave.observations.ToString(), "A002observaciones");
                            
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002CodigoZoocriadero, datas.dataToSave.zoocriaderoCode, "A002CodigoZoocriadero");
                            
                            #endregion

                            c.A002fechaResolucion = datas.dataToSave.resolutionDate;
                            c.A002fechaRegistroResolucion = datas.dataToSave.resolutionRegistrationDate;
                            c.A002CodigoZoocriadero = datas.dataToSave.zoocriaderoCode;
                            c.A002observaciones = datas.dataToSave.observations == null ? "" : datas.dataToSave.observations;
                            c.A002AutoridadEmiteResolucion = datas.dataToSave.issuingAuthority ?? "";
                            c.A002numeroResolucion = datas.dataToSave.resolutionNumber;
                            c.A002codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value); ;
                            c.A002fechaModificacion = DateTime.Now;
                        }
                        _context.SaveChanges();

                        var fechaVacia = new DateTime();

                        #region auditoria

                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002cuotaRepoblacion.ToString(), datas.newExportSpeciesData[0].repopulationQuota.ToString(), "A002cuotaRepoblacion");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002fechaProduccion.Value.Year.ToString(), datas.newExportSpeciesData[0].productionYear.ToString(), "A002fechaProduccion");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002cuposTotal.ToString(), datas.newExportSpeciesData[0].totalQuotas.ToString(), "A002cuposTotal");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002cuposDisponibles.ToString(), datas.newExportSpeciesData[0].availableQuotas.ToString(), "A002cuposDisponibles");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002cuposAsignados.ToString(), datas.newExportSpeciesData[0].grantedUtilizationQuotas.ToString(), "A002cuposAsignados");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002rangoCodigoInicial.ToString(), datas.newExportSpeciesData[0].initialInternalNumber.ToString(), "A002rangoCodigoInicial");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002rangoCodigoFin.ToString(), datas.newExportSpeciesData[0].finalInternalNumber.ToString(), "A002rangoCodigoFin");
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, cupos.A002fechaRadicadoSolicitud.ToString(), datas.newExportSpeciesData[0].filingDate == fechaVacia ? "" : datas.newExportSpeciesData[0].filingDate.ToString(), "A002fechaRadicadoSolicitud");

                        #endregion

                        cupos.A002numeroResolucion = datas.dataToSave.resolutionNumber;
                        cupos.A002fechaResolucion = datas.dataToSave.resolutionDate;
                        cupos.A002fechaRegistroResolucion = datas.dataToSave.resolutionRegistrationDate;
                        cupos.A002cuotaRepoblacion = datas.newExportSpeciesData[0].repopulationQuota;
                        cupos.A002fechaProduccion = new DateTime(Convert.ToInt32(datas.newExportSpeciesData[0].productionYear), 01, 01);
                        cupos.A002cuposTotal = datas.newExportSpeciesData[0].totalQuotas;
                        cupos.A002cuposDisponibles = datas.newExportSpeciesData[0].availableQuotas;
                        cupos.A002AutoridadEmiteResolucion = datas.dataToSave.issuingAuthority ?? "";
                        cupos.A002CodigoZoocriadero = datas.dataToSave.zoocriaderoCode;
                        cupos.A002cuposAsignados = datas.newExportSpeciesData[0].grantedUtilizationQuotas;
                        cupos.A002observaciones = datas.dataToSave.observations == null ? "" : datas.dataToSave.observations;
                        cupos.A002rangoCodigoInicial = datas.newExportSpeciesData[0].initialInternalNumber;
                        cupos.A002rangoCodigoFin = datas.newExportSpeciesData[0].finalInternalNumber;
                        cupos.A002fechaRadicadoSolicitud = datas.newExportSpeciesData[0].filingDate == fechaVacia ? null : datas.newExportSpeciesData[0].filingDate;
                        cupos.A002codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        cupos.A002fechaModificacion = DateTime.Now;
                        _context.SaveChanges();

                        var especieComercializar = _context.CupostT005Especieaexportars.Where(p => p.A005estadoRegistro == StringHelper.estadoActivo && p.PkT005codigo == datas.newExportSpeciesData[0].id).FirstOrDefault();

                        if (especieComercializar != null)
                        {
                            #region auditoria
                            
                            var codigoEspe = _context.AdmintT005Especimen.Where(p => p.PkT005codigo == Convert.ToDecimal(especieComercializar.A005codigoEspecie)).FirstOrDefault();
                            var codigoEspecieNew = _context.AdmintT005Especimen.Where(p => p.PkT005codigo == Convert.ToDecimal(datas.newExportSpeciesData[0].specimenType)).FirstOrDefault();
                            var codigoUsu = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == especieComercializar.A005codigoUsuarioModificacion).FirstOrDefault();
                            var codParametrica = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == Convert.ToDecimal(especieComercializar.A005codigoParametricaPagoCuotaDerepoblacion)).FirstOrDefault();
                            var codParaNew = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == Convert.ToDecimal(datas.newExportSpeciesData[0].repopulationQuotaPaymentParametricCode)).FirstOrDefault();
                            var codUserNew = _context.AdmintT012Usuarios.Where(p =>p.PkT012codigo == Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();

                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005codigoParametricaTipoMarcaje.ToString(), datas.newExportSpeciesData[0].markingTypeParametricCode.ToString(), "A005codigoParametricaTipoMarcaje"); 
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, codigoEspe.A005nombreCientifico, datas.newExportSpeciesData[0].specimenType == null ? "" : codigoEspecieNew.A005nombreCientifico, "A005codigoEspecie");
                            //met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005codigoParametricaPagoCuotaDerepoblacion == null ? "" : codParametrica.A008valor, datas.newExportSpeciesData[0].repopulationQuotaPaymentParametricCode == null ? "" : codParaNew.A008valor, "A005codigoParametricaPagoCuotaDerepoblacion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005fechaRadicado == null ? "" : especieComercializar.A005fechaRadicado.ToString(), datas.newExportSpeciesData[0].filingDate == fechaVacia ? "" : datas.newExportSpeciesData[0].filingDate.ToString(), "A005fechaRadicado");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005nombreEspecie == null ? "" : especieComercializar.A005nombreEspecie.ToString(), datas.newExportSpeciesData[0].specimenType == null ? "" : datas.newExportSpeciesData[0].specimenType.ToString(), "A005nombreEspecie");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005añoProduccion.ToString(), datas.newExportSpeciesData[0].productionYear.ToString(), "A005añoProduccion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005marcaLote == null ? "" : especieComercializar.A005marcaLote.ToString(), datas.newExportSpeciesData[0].batchCode == null ? "" : datas.newExportSpeciesData[0].batchCode.ToString(), "A005marcaLote");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005condicionesMarcaje == null ? "" : especieComercializar.A005condicionesMarcaje.ToString(), datas.newExportSpeciesData[0].markingConditions == null ? "" : datas.newExportSpeciesData[0].markingConditions.ToString(), "A005condicionesMarcaje");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005poblacionParentalMacho.ToString(), datas.newExportSpeciesData[0].parentalPopulationMale.ToString(), "A005poblacionParentalMacho");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005poblacionParentalHembra.ToString(), datas.newExportSpeciesData[0].parentalPopulationFemale.ToString(), "A005poblacionParentalHembra");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005poblacionParentalTotal.ToString(), datas.newExportSpeciesData[0].totalParentalPopulation.ToString(), "A005poblacionParentalTotal");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005poblacionSalioDeIncubadora == null ? "" : especieComercializar.A005poblacionSalioDeIncubadora.ToString(), datas.newExportSpeciesData[0].populationFromIncubator == null ? "" : datas.newExportSpeciesData[0].populationFromIncubator.ToString(), "A005poblacionSalioDeIncubadora");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005poblacionDisponibleParaCupos.ToString(), datas.newExportSpeciesData[0].populationAvailableForQuotas.ToString(), "A005poblacionDisponibleParaCupos");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005individuosDestinadosARepoblacion.ToString(), datas.newExportSpeciesData[0].individualsForRepopulation.ToString(), "A005individuosDestinadosARepoblacion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005cupoAprovechamientoOtorgados.ToString(), datas.newExportSpeciesData[0].grantedUtilizationQuotas.ToString(), "A005cupoAprovechamientoOtorgados");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005tasaReposicion == null ? "" : especieComercializar.A005tasaReposicion.ToString(), datas.newExportSpeciesData[0].replacementRate == null ? "" : datas.newExportSpeciesData[0].replacementRate.ToString(), "A005tasaReposicion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005numeroMortalidad == null ? "" : especieComercializar.A005numeroMortalidad.ToString(), datas.newExportSpeciesData[0].mortalityNumber == null ? "" : datas.newExportSpeciesData[0].mortalityNumber.ToString(), "A005numeroMortalidad");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005cuotaRepoblacionParaAprovechamiento == null ? "" : especieComercializar.A005cuotaRepoblacionParaAprovechamiento.ToString(), datas.newExportSpeciesData[0].repopulationQuotaForUtilization == null ? "" : datas.newExportSpeciesData[0].repopulationQuotaForUtilization.ToString(), "A005cuotaRepoblacionParaAprovechamiento");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005cupoAprovechamientoOtorgadosPagados == null ? "" : especieComercializar.A005cupoAprovechamientoOtorgadosPagados.ToString(), datas.newExportSpeciesData[0].grantedPaidUtilizationQuotas == null ? "" : datas.newExportSpeciesData[0].grantedPaidUtilizationQuotas.ToString(), "A005cupoAprovechamientoOtorgadosPagados");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005observaciones == null ? "" : especieComercializar.A005observaciones.ToString(), datas.newExportSpeciesData[0].observations == null ? "" : datas.newExportSpeciesData[0].observations.ToString(), "A005observaciones");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005NumeroInternoInicialCuotaRepoblacion == null ? "" : especieComercializar.A005NumeroInternoInicialCuotaRepoblacion.ToString(), datas.newExportSpeciesData[0].initialRepopulationQuotaInternalNumber == null ? "" : datas.newExportSpeciesData[0].initialRepopulationQuotaInternalNumber.ToString(), "A005NumeroInternoInicialCuotaRepoblacion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005NumeroInternoFinalCuotaRepoblacion == null ? "" : especieComercializar.A005NumeroInternoFinalCuotaRepoblacion.ToString(), datas.newExportSpeciesData[0].finalRepopulationQuotaInternalNumber == null ? "" : datas.newExportSpeciesData[0].finalRepopulationQuotaInternalNumber.ToString(), "A005NumeroInternoFinalCuotaRepoblacion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005fechaModificacion == null ? "" : especieComercializar.A005fechaModificacion.ToString(), DateTime.Now.ToString(), "A005fechaModificacion");
                            met.validarCampoEditadoAud(valorAnterior, valorActual, campos, especieComercializar.A005codigoUsuarioModificacion == null ? "" : codigoUsu.A012primerNombre + " " + codigoUsu.A012segundoNombre + " " + codigoUsu.A012primerApellido + " " + codigoUsu.A012segundoApellido, especieComercializar.A005codigoUsuarioModificacion == null ? "" : codUserNew.A012primerNombre + " " + codUserNew.A012segundoNombre + " " + codUserNew.A012primerApellido + " " + codUserNew.A012segundoApellido, "A005codigoUsuarioModificacion");
                            
                            #endregion

                            especieComercializar.A005codigoParametricaTipoMarcaje = datas.newExportSpeciesData[0].markingTypeParametricCode;
                            especieComercializar.A005codigoEspecie = datas.newExportSpeciesData[0].speciesCode ?? "";
                            especieComercializar.A005codigoParametricaPagoCuotaDerepoblacion = datas.newExportSpeciesData[0].repopulationQuotaPaymentParametricCode;
                            especieComercializar.A005fechaRadicado = datas.newExportSpeciesData[0].filingDate == fechaVacia ? null : datas.newExportSpeciesData[0].filingDate;
                            especieComercializar.A005nombreEspecie = datas.newExportSpeciesData[0].specimenType;
                            especieComercializar.A005añoProduccion = datas.newExportSpeciesData[0].productionYear;
                            especieComercializar.A005marcaLote = datas.newExportSpeciesData[0].batchCode;
                            especieComercializar.A005condicionesMarcaje = datas.newExportSpeciesData[0].markingConditions;
                            especieComercializar.A005poblacionParentalMacho = datas.newExportSpeciesData[0].parentalPopulationMale;
                            especieComercializar.A005poblacionParentalHembra = datas.newExportSpeciesData[0].parentalPopulationFemale;
                            especieComercializar.A005poblacionParentalTotal = datas.newExportSpeciesData[0].totalParentalPopulation;
                            especieComercializar.A005poblacionSalioDeIncubadora = datas.newExportSpeciesData[0].populationFromIncubator;
                            especieComercializar.A005poblacionDisponibleParaCupos = datas.newExportSpeciesData[0].populationAvailableForQuotas;
                            especieComercializar.A005individuosDestinadosARepoblacion = datas.newExportSpeciesData[0].individualsForRepopulation;
                            especieComercializar.A005cupoAprovechamientoOtorgados = datas.newExportSpeciesData[0].grantedUtilizationQuotas.ToString();
                            especieComercializar.A005tasaReposicion = datas.newExportSpeciesData[0].replacementRate;
                            especieComercializar.A005numeroMortalidad = datas.newExportSpeciesData[0].mortalityNumber;
                            especieComercializar.A005cuotaRepoblacionParaAprovechamiento = datas.newExportSpeciesData[0].repopulationQuotaForUtilization;
                            especieComercializar.A005cupoAprovechamientoOtorgadosPagados = datas.newExportSpeciesData[0].grantedPaidUtilizationQuotas;
                            especieComercializar.A005observaciones = datas.newExportSpeciesData[0].observations;
                            especieComercializar.A005NumeroInternoInicialCuotaRepoblacion = Convert.ToDecimal(datas.newExportSpeciesData[0].initialRepopulationQuotaInternalNumber);
                            especieComercializar.A005NumeroInternoFinalCuotaRepoblacion = Convert.ToDecimal(datas.newExportSpeciesData[0].finalRepopulationQuotaInternalNumber);
                            especieComercializar.A005fechaModificacion = DateTime.Now;
                            especieComercializar.A005codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        }

                        _context.SaveChanges();

                        if (datas.newExportSpeciesData[0].deletedSupportingDocuments != null && datas.newExportSpeciesData[0].deletedSupportingDocuments.Count > 0)
                        {
                            foreach (var doc in datas.newExportSpeciesData[0].deletedSupportingDocuments)
                            {
                                var docEliminar = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == doc.code && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                                
                                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 4, docEliminar, docEliminar.A009documento);
                                if (docEliminar != null)
                                {
                                    docEliminar.A009estadoRegistro = StringHelper.estadoInactivo;
                                    docEliminar.A009codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                                    docEliminar.A009fechaModificacion = DateTime.Now;
                                }
                                _context.SaveChanges();

                                var rlDocEliminar = _context.CupostT023RlCupoDocumento.Where(p => p.A023CodigoCupo == datas.dataToSave.quotaCode && p.A023CodigoDocuemento == doc.code && p.A023EstadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                                if (rlDocEliminar != null)
                                {
                                    rlDocEliminar.A023EstadoRegistro = StringHelper.estadoInactivo;
                                    rlDocEliminar.A023UsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                                    rlDocEliminar.A023FechaModificacion = DateTime.Now;
                                }
                                _context.SaveChanges();
                            }

                        }

                        if (datas.newExportSpeciesData[0].newSupportingDocuments != null && datas.newExportSpeciesData[0].newSupportingDocuments.Count > 0)
                        {
                            Metodos metodos = new Metodos(_context);

                            foreach (var doc in datas.newExportSpeciesData[0].newSupportingDocuments)
                            {
                                var uri = metodos.GuardarArchivoFtp(doc);

                                AdmintT009Documento newDoc = new AdmintT009Documento();
                                newDoc.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                                newDoc.A009codigoParametricaTipoDocumento = 25;
                                newDoc.A009codigoPlantilla = 1;
                                newDoc.A009estadoRegistro = StringHelper.estadoActivo;
                                newDoc.A009fechaCreacion = DateTime.Now;
                                newDoc.A009firmaDigital = "firma";
                                newDoc.A009documento = doc.attachmentName != null ? doc.attachmentName : "";
                                newDoc.A009descripcion = doc.attachmentName != null ? doc.attachmentName : "";
                                newDoc.A009url = uri;

                                _context.AdmintT009Documentos.Add(newDoc);
                                _context.SaveChanges();
                                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 2, newDoc, newDoc.A009documento);
                                CupostT023RlCupoDocumento newDocCupo = new CupostT023RlCupoDocumento();
                                newDocCupo.A023CodigoDocuemento = newDoc.PkT009codigo;
                                newDocCupo.A023CodigoCupo = datas.dataToSave.quotaCode;
                                newDocCupo.A023FechaCreacion = DateTime.Now;
                                newDocCupo.A023UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                                newDocCupo.A023EstadoRegistro = StringHelper.estadoActivo;

                                _context.CupostT023RlCupoDocumento.Add(newDocCupo);
                                _context.SaveChanges();
                            }

                        }
                    }
                }
                #endregion
                
                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, valorAnterior, valorActual, campos, 3, null, null);
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
            }
            catch (Exception exp)
            {
                return ResponseManager.generaRespuestaGenerica(exp.Message, "", "", true);
            }
        }

        /// <summary>
        /// guardar nueva resolucion
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public Responses saveResolutionQuota( ClaimsIdentity identity, SaveResolutionQuotas datas, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            #region guardar nueva informacion

            if (datas.newExportSpeciesData != null && datas.dataToSave!=null)
            {

                var bs = _context.CupostT001Empresas.Where(p => p.A001nit == datas.dataToSave.companyNit).FirstOrDefault();
                var fechaVacia = new DateTime();

                List<CupostT005Especieaexportar> especiesComercializar = new List<CupostT005Especieaexportar>();

                if (bs != null)
                {
                    var especieRepetida = ValidateQuotaSpeciesRepeats(datas.newExportSpeciesData, bs);
                    if(especieRepetida > 0)
                        return ResponseManager.generaRespuestaGenerica("No se puede guardar, hay duplicacion de especie y año de producción", false, token, false);

                    foreach (var cupo in datas.newExportSpeciesData)
                    {
                        CupostT002Cupo cupoNuevo = new CupostT002Cupo();
                        CuposAuditoria cupoAudit = new CuposAuditoria();

                        cupoNuevo.A002AutoridadEmiteResolucion = datas.dataToSave.issuingAuthority !=null ? datas.dataToSave.issuingAuthority : "";
                        cupoNuevo.A002numeroResolucion = Convert.ToDecimal(datas.dataToSave.resolutionNumber);
                        cupoNuevo.A002fechaResolucion = datas.dataToSave.resolutionDate;
                        cupoNuevo.A002fechaRegistroResolucion = datas.dataToSave.resolutionRegistrationDate;
                        cupoNuevo.A002CodigoZoocriadero = datas.dataToSave.zoocriaderoCode;
                        cupoNuevo.A002observaciones = datas.dataToSave.observations;
                        cupoNuevo.A002fechaProduccion = new DateTime(Convert.ToInt32(cupo.productionYear), 01, 01);
                        cupoNuevo.A002fechaRadicadoSolicitud = cupo.filingDate == fechaVacia ? null : cupo.filingDate;
                        cupoNuevo.A002cuposAsignados = cupo.grantedUtilizationQuotas;
                        cupoNuevo.A002cuposDisponibles = cupo.availableQuotas;
                        cupoNuevo.A002cuposTotal = cupo.totalQuotas;
                        cupoNuevo.A002rangoCodigoInicial = cupo.initialInternalNumber;
                        cupoNuevo.A002rangoCodigoFin = cupo.finalInternalNumber;
                        cupoNuevo.A002cuotaRepoblacion = cupo.repopulationQuota;
                        cupoNuevo.A002fechaCreacion = DateTime.Now;
                        cupoNuevo.A002codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
                        cupoNuevo.A002codigoEmpresa = bs.PkT001codigo;
                        cupoNuevo.A002estadoRegistro = StringHelper.estadoActivo;

                        _context.CupostT002Cupos.Add(cupoNuevo);
                        _context.SaveChanges();

                        var codEmpresa = _context.CupostT001Empresas.Where(p => p.A001nit == Convert.ToDecimal(datas.dataToSave.companyNit)).FirstOrDefault();
                        var codUsuario = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == cupoNuevo.A002codigoUsuarioCreacion).FirstOrDefault();

                        cupoAudit.A002AutoridadEmiteResolucion = datas.dataToSave.issuingAuthority != null ? datas.dataToSave.issuingAuthority : "";
                        cupoAudit.A002numeroResolucion = Convert.ToDecimal(datas.dataToSave.resolutionNumber);
                        cupoAudit.A002fechaResolucion = datas.dataToSave.resolutionDate;
                        cupoAudit.A002fechaRegistroResolucion = datas.dataToSave.resolutionRegistrationDate;
                        cupoAudit.A002CodigoZoocriadero = datas.dataToSave.zoocriaderoCode;
                        cupoAudit.A002observaciones = datas.dataToSave.observations;
                        cupoAudit.A002fechaProduccion = new DateTime(Convert.ToInt32(cupo.productionYear), 01, 01);
                        cupoAudit.A002fechaRadicadoSolicitud = cupo.filingDate == fechaVacia ? null : cupo.filingDate;
                        cupoAudit.A002cuposAsignados = cupo.grantedUtilizationQuotas;
                        cupoAudit.A002cuposDisponibles = cupo.availableQuotas;
                        cupoAudit.A002cuposTotal = cupo.totalQuotas;
                        cupoAudit.A002rangoCodigoInicial = cupo.initialInternalNumber;
                        cupoAudit.A002rangoCodigoFin = cupo.finalInternalNumber;
                        cupoAudit.A002cuotaRepoblacion = cupo.repopulationQuota;
                        cupoAudit.A002fechaCreacion = DateTime.Now;
                        cupoAudit.A002codigoUsuarioCreacion = codUsuario.A012primerNombre + " " + codUsuario.A012segundoNombre + " " + codUsuario.A012primerApellido + " " + codUsuario.A012segundoApellido;
                        cupoAudit.A002codigoEmpresa = codEmpresa.A001nombre;
                        cupoAudit.A002estadoRegistro = StringHelper.estadoActivo;

                        met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 2, cupoAudit, cupoNuevo.PkT002codigo.ToString());
                        

                        cupo.quotaCode = cupoNuevo.PkT002codigo;

                        Metodos metodos = new Metodos(_context);

                        if (cupo.supportingDocuments != null)
                        {

                            foreach (var documentosSoporte in cupo.supportingDocuments)
                            {
                                var uri = metodos.GuardarArchivoFtp(documentosSoporte);

                                AdmintT009Documento newDoc = new AdmintT009Documento();
                                newDoc.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                                newDoc.A009codigoParametricaTipoDocumento = 25;
                                newDoc.A009codigoPlantilla = 1;
                                newDoc.A009estadoRegistro = StringHelper.estadoActivo;
                                newDoc.A009fechaCreacion = DateTime.Now;
                                newDoc.A009firmaDigital = "firma";
                                newDoc.A009documento = documentosSoporte.attachmentName != null ? documentosSoporte.attachmentName : "";
                                newDoc.A009descripcion = documentosSoporte.attachmentName != null ? documentosSoporte.attachmentName : "";
                                newDoc.A009url = uri;

                                _context.AdmintT009Documentos.Add(newDoc);
                                _context.SaveChanges();

                                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 2, newDoc, newDoc.A009documento);

                                CupostT023RlCupoDocumento newDocCupo = new CupostT023RlCupoDocumento();
                                newDocCupo.A023CodigoDocuemento = newDoc.PkT009codigo;
                                newDocCupo.A023CodigoCupo = cupoNuevo.PkT002codigo;
                                newDocCupo.A023FechaCreacion = DateTime.Now;
                                newDocCupo.A023UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                                newDocCupo.A023EstadoRegistro = StringHelper.estadoActivo;

                                _context.CupostT023RlCupoDocumento.Add(newDocCupo);
                                _context.SaveChanges();
                            }
                        }

                        especiesComercializar.Add(guardarNuevaEspecieExportar(cupo, identity));
                    }
                }
                foreach (var especies in especiesComercializar)
                {
                    _context.CupostT005Especieaexportars.Add(especies);
                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 2, especies, especies.PkT005codigo.ToString());
                }
            }
            _context.SaveChanges();

            #endregion

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);

        }

        /// <summary>
        /// guardar nueva especie aexportar
        /// </summary>
        /// <param name="especieExportar"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public CupostT005Especieaexportar guardarNuevaEspecieExportar(ExportSpecimens especieExportar, ClaimsIdentity identity)
        {
            var fechaVacia = new DateTime();
            CupostT005Especieaexportar newEspecieExportar = new CupostT005Especieaexportar();

            newEspecieExportar.A005codigoCupo = especieExportar.quotaCode;
            newEspecieExportar.A005codigoParametricaTipoMarcaje = especieExportar.markingTypeParametricCode;
            if (especieExportar.speciesCode != null)
            {
                newEspecieExportar.A005codigoEspecie = especieExportar.speciesCode;
            }
            newEspecieExportar.A005codigoParametricaPagoCuotaDerepoblacion = especieExportar.repopulationQuotaPaymentParametricCode;
            newEspecieExportar.A005fechaRadicado = especieExportar.filingDate == fechaVacia ? null : especieExportar.filingDate;
            newEspecieExportar.A005nombreEspecie = especieExportar.specimenType;
            newEspecieExportar.A005añoProduccion = especieExportar.productionYear;
            newEspecieExportar.A005marcaLote = especieExportar.batchCode;
            newEspecieExportar.A005condicionesMarcaje = especieExportar.markingConditions;
            newEspecieExportar.A005poblacionParentalMacho = especieExportar.parentalPopulationMale;
            newEspecieExportar.A005poblacionParentalHembra = especieExportar.parentalPopulationFemale;
            newEspecieExportar.A005poblacionParentalTotal = especieExportar.totalParentalPopulation;
            newEspecieExportar.A005poblacionSalioDeIncubadora = especieExportar.populationFromIncubator;
            newEspecieExportar.A005poblacionDisponibleParaCupos = especieExportar.populationAvailableForQuotas;
            newEspecieExportar.A005individuosDestinadosARepoblacion = especieExportar.individualsForRepopulation;
            newEspecieExportar.A005cupoAprovechamientoOtorgados = especieExportar.grantedUtilizationQuotas.ToString();
            newEspecieExportar.A005tasaReposicion = especieExportar.replacementRate;
            newEspecieExportar.A005numeroMortalidad = especieExportar.mortalityNumber;
            newEspecieExportar.A005cuotaRepoblacionParaAprovechamiento = especieExportar.repopulationQuotaForUtilization;
            newEspecieExportar.A005cupoAprovechamientoOtorgadosPagados = especieExportar.grantedPaidUtilizationQuotas;
            newEspecieExportar.A005observaciones = especieExportar.observations;
            newEspecieExportar.A005NumeroInternoInicialCuotaRepoblacion = Convert.ToDecimal(especieExportar.initialRepopulationQuotaInternalNumber);
            newEspecieExportar.A005NumeroInternoFinalCuotaRepoblacion = Convert.ToDecimal(especieExportar.finalRepopulationQuotaInternalNumber);
            newEspecieExportar.A005fechaCreacion = DateTime.Now;
            newEspecieExportar.A005codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            newEspecieExportar.A005estadoRegistro = StringHelper.estadoActivo;


            return newEspecieExportar;
        }

        /// <summary>
        /// deshabilitar resolucion cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="quotaCode"></param>
        /// <returns></returns>
        public Responses DisableResolution(ClaimsIdentity identity, decimal quotaCode, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var datoEliminar = _context.CupostT002Cupos.Where(p => p.PkT002codigo == quotaCode && p.A002estadoRegistro==StringHelper.estadoActivo).FirstOrDefault();
            if (datoEliminar != null)
            {
                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 4, datoEliminar, datoEliminar.PkT002codigo.ToString());
                datoEliminar.A002estadoRegistro = StringHelper.estadoInactivo;
                datoEliminar.A002fechaModificacion = DateTime.Now;
                datoEliminar.A002codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            _context.SaveChanges();

            var docsRlEliminar = _context.CupostT023RlCupoDocumento.Where(p => p.A023CodigoCupo == quotaCode && p.A023EstadoRegistro == StringHelper.estadoActivo).ToList();

            foreach(var docRl in docsRlEliminar)
            {
                docRl.A023EstadoRegistro = StringHelper.estadoInactivo;
                docRl.A023FechaModificacion = DateTime.Now;
                docRl.A023UsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                _context.SaveChanges();

                var docEliminar = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == docRl.A023CodigoDocuemento && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                if (docEliminar != null)
                {
                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 4, docEliminar, docEliminar.A009documento);
                    docEliminar.A009estadoRegistro = StringHelper.estadoInactivo;
                    docEliminar.A009fechaModificacion = DateTime.Now;
                    docEliminar.A009codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                }
                _context.SaveChanges();
            }

            var especiesExportarEliminar = _context.CupostT005Especieaexportars.Where(p => p.A005codigoCupo == quotaCode && p.A005estadoRegistro==StringHelper.estadoActivo).FirstOrDefault();
            if (especiesExportarEliminar != null)
            {
                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smOtorgamientoCupos, null, null, null, 4, especiesExportarEliminar, especiesExportarEliminar.PkT005codigo.ToString());
                especiesExportarEliminar.A005estadoRegistro = StringHelper.estadoInactivo;
                especiesExportarEliminar.A005fechaModificacion = DateTime.Now;
                especiesExportarEliminar.A005codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }

            _context.SaveChanges();
            return ResponseManager.generaRespuestaGenerica(StringHelper.msgEliminadoExitoso, true, token, false);
        }

        private int ValidateQuotaSpeciesRepeats(List<ExportSpecimens> especies, CupostT001Empresa empresa)
        {
            int conteo = 0;

            if(especies.Any())
            {
                foreach(var item in especies)
                {


                    conteo = (from especie in _context.CupostT005Especieaexportars
                              join cupoEmpresa in _context.CupostT002Cupos
                              on especie.A005codigoCupo equals cupoEmpresa.PkT002codigo
                              where especie.A005codigoEspecie == item.speciesCode &&
                                  especie.A005añoProduccion == item.productionYear &&
                                  cupoEmpresa.A002codigoEmpresa == empresa.PkT001codigo
                              select especie).Count();

                }
            }

            return conteo;

        }

    }
}
