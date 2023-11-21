using API.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Web.Models;

namespace Repository.Persistence.Repository
{
    public class Empresa : IEmpresa
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;
        public readonly DocumentManager documentManager;

        public Empresa(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
            this.documentManager = new DocumentManager(context);
        }

        //CU03 Gestion de Empresas
        public Responses Actualizar(ClaimsIdentity identity, EntityRequest empresa, string ipAddress)
        {
            var now = DateTime.UtcNow;

            decimal codigoUsuario = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (empresa != null && empresa.CompanyCode != 0)
            {
                var result = _context.CupostT001Empresas.SingleOrDefault(x => x.PkT001codigo == empresa.CompanyCode);

                if (result != null)
                {
                    /*Se carga auditoria*/
                    this.logManager.getPropertyInfo(result);

                    /*Se actualizan valores*/
                    result.A001codigoParametricaTipoEntidad = empresa.EntityTypeId;
                    result.A001nombre = empresa.CompanyName;
                    result.A001telefono = empresa.Phone;
                    result.A001direccion = empresa.Address;
                    result.A001correo = empresa.Email;
                    result.A001codigoCiudad = empresa.CityId;

                    /*Se evualua igualdad*/
                    /*Si hay diferencias se guarda*/
                    if (this.logManager.getDifferences(result))
                    {
                        result.A001codigoUsuarioModificacion = codigoUsuario;
                        result.A001fechaModificacion = now;
                        _context.Update(result);
                        _context.SaveChanges();

                        /*Se crea auditoria*/
                        logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), 3, ModuleManager.smNovedades, "", "", "", "", result.A001nombre);
                        _context.SaveChanges();
                    }                   

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, result, token, false);
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

        public Responses ConsultaNovedades(ClaimsIdentity identity, decimal codigoEmpresa, decimal? idNovedad)
        {
            List<Archivo>? archivos = new List<Archivo>();
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from novedad in _context.CupostT003Novedads
                        join parametrica in _context.AdmintT008Parametricas
                        on novedad.A003codigoParametricaTiponovedad equals parametrica.PkT008codigo
                        join parametricaEstadoEmpresa in _context.AdmintT008Parametricas
                        on novedad.A003estadoEmpresa equals parametricaEstadoEmpresa.PkT008codigo
                        join parametricaEstadoCITES in _context.AdmintT008Parametricas
                        on novedad.A003estadoEmisionPermisosCITES equals parametricaEstadoCITES.PkT008codigo
                        join zoocriadero in _context.CupostT001Empresas
                        on novedad.A003codigoEmpresaTraslado equals zoocriadero.PkT001codigo into zoocriaderoTm
                        from zoo in zoocriaderoTm.DefaultIfEmpty()
                        where (novedad.A003codigoEmpresa == codigoEmpresa && (idNovedad == null || novedad.PkT003codigo == idNovedad))
                        orderby novedad.A003fechaRegistroNovedad ascending
                        select new Novedad()
                        {
                            codigo = novedad.PkT003codigo,
                            codigoEmpresa = codigoEmpresa,
                            idTipoNovedad = novedad.A003codigoParametricaTiponovedad,
                            nombreTipoNovedad = parametrica.A008valor,
                            fechaRegistroNovedad = novedad.A003fechaRegistroNovedad,
                            idEstadoEmpresa = parametricaEstadoEmpresa.PkT008codigo,
                            estadoEmpresa = parametricaEstadoEmpresa.A008valor,
                            idEstadoEmisionCITES = novedad.A003estadoEmisionPermisosCITES,
                            estadoEmisionCITES = parametricaEstadoCITES.A008valor,
                            observaciones = novedad.A003observaciones,
                            saldoProduccionDisponible = novedad.A003saldoProduccionDisponible,
                            cuposDisponibles = novedad.A003cuposDisponibles,
                            inventarioDisponible = novedad.A003inventarioDisponible,
                            numeroCupospendientesportramitar = novedad.A003numeroCupospendientesportramitar,
                            idDisposicionEspecimen = novedad.A003codigoParametricaDisposicionEspecimen,
                            idEmpresaZoo = novedad.A003codigoEmpresaTraslado,
                            NitEmpresaZoo = zoo == null? 0 : zoo.A001nit,
                            otroCual = novedad.A003otroCual,
                            observacionesDetalle = novedad.A003observacionesDetalle,
                            documentos = new List<Archivo>()
                        };

            List<Novedad> lst = (List<Novedad>)query.ToList();

            if (idNovedad != null)
            {
                var documentos = from docNovedad in _context.CupostT012RlNovedadDocumentos
                                join doc in _context.AdmintT009Documentos
                                on docNovedad.A012codigoDocumento equals doc.PkT009codigo
                                where docNovedad.A012codigoNovedad == idNovedad
                                where docNovedad.A012estadoRegistro == StringHelper.estadoActivo
                                where doc.A009estadoRegistro == StringHelper.estadoActivo
                                select new Archivo()
                                {
                                    codigo = doc.PkT009codigo,
                                    nombreAdjunto = doc.A009documento,
                                    urlFTP = doc.A009url,
                                    tipoAdjunto = this.documentManager.getTipoData(doc.A009url)
                                };

                archivos = documentos.ToList();

                if (archivos.Any())
                {
                    foreach (Archivo arc in archivos)
                    {
                        if (arc.urlFTP != null)
                        {
                            arc.adjuntoBase64 = documentManager.ConvertirArchivoToBase64(arc.urlFTP);
                        }
                    }

                    foreach (Novedad nov in lst)
                    {
                        nov.documentos = archivos;
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("", lst, token, false);
        }

        [ExcludeFromCodeCoverage]
        public Responses RegistroNovedad(ClaimsIdentity identity, NoveltiesRequest novedad, string ipAddress)
        {
            var now = DateTime.UtcNow;
            decimal codigoUsuario = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            if (novedad != null)
            {
                var result = _context.CupostT003Novedads.Where(x => x.PkT003codigo == novedad.code).FirstOrDefault();

                if (result == null)
                {
                    result = new Models.CupostT003Novedad();
                }

                /*Se actualiza estado de la Empresa*/
                var empresa = _context.CupostT001Empresas.SingleOrDefault(x => x.PkT001codigo == novedad.companyCode);

                if (empresa != null)
                {
                    empresa.A001estadoRegistro = novedad.companyStatusId;
                    _context.Update(empresa);
                    _context.SaveChanges();
                }

                /*Se carga auditoria*/
                this.logManager.getPropertyInfo(result);

                /*Se crea o actualiza novedad*/
                /*Se setean valores*/
                result.A003codigoEmpresa = novedad.companyCode;
                result.A003estadoRegistro = StringHelper.estadoActivo;
                result.A003estadoEmpresa = novedad.companyStatusId;
                result.A003estadoEmisionPermisosCITES = novedad.CITESPermitIssuanceStatusId;
                result.A003codigoParametricaTiponovedad = novedad.typeOfNoveltyId;
                result.A003fechaRegistroNovedad = novedad.noveltyRegistrationDate;
                result.A003observaciones = novedad.observations;
                result.A003saldoProduccionDisponible = novedad.availableProductionBalance;
                result.A003cuposDisponibles = novedad.availableQuotas;
                result.A003inventarioDisponible = novedad.availableInventory;
                result.A003numeroCupospendientesportramitar = novedad.pendingQuotasToProcess;
                result.A003codigoParametricaDisposicionEspecimen = novedad.specimenDispositionId;
                result.A003codigoEmpresaTraslado = novedad.zooCompanyId;
                result.A003otroCual = novedad.otherDescription;
                result.A003observacionesDetalle = novedad.detailedObservations; 

                /*Se evualua igualdad*/
                /*Si hay diferencias se guarda*/
                if (this.logManager.getDifferences(result))
                {
                    if (result.PkT003codigo == 0)
                    {
                        result.A003codigoUsuarioCreacion = codigoUsuario;
                        result.A003fechaCreacion = now;
                    }
                    else
                    {
                        result.A003codigoUsuarioModificacion = codigoUsuario;
                        result.A003fechaModificacion = now;
                    }

                    /*Se guarda la novedad*/
                    _context.Update(result);
                    _context.SaveChanges();

                    /*En caso de ser novedad de trasaldo, tiponovedad = 71 y DisposicionEspecimenes = 39*/
                    if (result.A003codigoParametricaTiponovedad == 71 && result.A003codigoParametricaDisposicionEspecimen == 39 && novedad.zooCompanyId != null)
                    {
                        /*Se trasladan los Cupos*/
                        List<CupostT002Cupo> cupos = _context.CupostT002Cupos.Where(p => p.A002codigoEmpresa == novedad.companyCode && p.A002estadoRegistro == StringHelper.estadoActivo).ToList();
                        if (cupos.Any())
                        {
                            foreach (CupostT002Cupo cupo in cupos)
                            {
                                cupo.A002codigoEmpresa = (decimal)novedad.zooCompanyId;
                            }

                            _context.UpdateRange(cupos);
                            _context.SaveChanges();
                        }

                        if (novedad.availableQuotas > 0)
                        {
                            /*Se traslada inventario, por medio de una venta de los cupos disponibles de la empresa Origen*/
                            CupostT004FacturacompraCartaventum factura = new CupostT004FacturacompraCartaventum()
                            {
                                A004codigoParametricaTipoCartaventa = 67,  // Tipo de Factura - Compra
                                A004codigoEntidadCompra = (decimal)novedad.zooCompanyId,
                                A004codigoEntidadVende = novedad.companyCode,
                                A004codigoDocumentoSoporte = 0,
                                A004codigoDocumentoFactura = 0,
                                A004codigoUsuarioCreacion = codigoUsuario,
                                A004fechaCreacion = now,
                                A004codigoUsuarioModificacion = null,
                                A004fechaModificacion = null,
                                A004fechaVenta = novedad.noveltyRegistrationDate,
                                A004saldoEntidadCompraInicial = 0,
                                A004saldoEntidadCompraFinal = 0,
                                A004saldoEntidadVendeInicial = 0,
                                A004saldoEntidadVendeFinal = 0,
                                A004totalCuposObtenidos = (decimal)novedad.availableQuotas,
                                A004totalCuposVendidos = (decimal)novedad.availableQuotas,
                                A004disponibilidadInventario = (decimal)novedad.availableQuotas,
                                A004numeroCartaVenta = "",
                                A004observacionesCompra = novedad.observations is not null ? novedad.observations : "",
                                A004observacionesVenta = novedad.observations is not null ? novedad.observations : "",
                                A004estadoRegistro = StringHelper.estadoActivo,
                                A004fechaRegistroCartaVenta = now,
                                A004tipoEspecimenEntidadCompra = null,
                                A004tipoEspecimenEntidadVende = null,
                                A004codigoCupo = 0
                            };

                            _context.CupostT004FacturacompraCartaventa.Add(factura);
                            _context.SaveChanges();
                        }
                    }

                    /*Se crea auditoria*/
                    var empresaAud = _context.CupostT001Empresas.Where(x => x.PkT001codigo == result.A003codigoEmpresa).FirstOrDefault();
                    
                    logManager.crearAuditoria(ipAddress, codigoUsuario.ToString(), (result.PkT003codigo == 0? 2: 3), ModuleManager.smNovedades, "", "", "", "", (empresaAud != null ? empresaAud.A001nombre : ""));
                }

                /*Borra logicamente archivos por borrar */
                if (novedad.documentsToDelete != null)
                {
                    foreach (Archivo arc in novedad.documentsToDelete)
                    {
                        if (arc.codigo != null)
                        {
                            int contDocNove = _context.CupostT012RlNovedadDocumentos.Where(p => p.A012codigoDocumento == arc.codigo && p.A012codigoNovedad == result.PkT003codigo && p.A012estadoRegistro == StringHelper.estadoActivo).Count();
                            if (contDocNove > 0)
                            {
                                ElimiarDocumentoNovedad(identity, novedad.code, (decimal)arc.codigo);
                            }
                        }
                    }
                }

                /*Se guarda DocumentoS Soporte*/
                if (novedad.documents != null)
                {
                    /*Se guardar los Nuevos documentos*/
                    foreach (Archivo arc in novedad.documents)
                    {
                        int contDocNove = _context.CupostT012RlNovedadDocumentos.Where(p => p.A012codigoDocumento == arc.codigo && p.A012codigoNovedad == result.PkT003codigo && p.A012estadoRegistro == StringHelper.estadoActivo).Count();
                        if (contDocNove == 0)
                        {
                            string uri = documentManager.GuardarArchivoFTP(arc);
                            if(String.IsNullOrEmpty(uri))
                                return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, result, token, false);

                            AdmintT009Documento docNuevo = this.documentManager.GuardarDocumento(ipAddress, codigoUsuario, (arc.nombreAdjunto != null ? arc.nombreAdjunto : ""), uri);

                            if (docNuevo.PkT009codigo != null)
                            {
                                decimal codDoc = Convert.ToDecimal(docNuevo.PkT009codigo);
                                CupostT012RlNovedadDocumento docNuevoNovedad = new CupostT012RlNovedadDocumento
                                {
                                    A012codigoNovedad = result.PkT003codigo,
                                    A012codigoDocumento = codDoc,
                                    A012estadoRegistro = StringHelper.estadoActivo,
                                    A012codigoUsuarioCreacion = codigoUsuario,
                                    A012fechaCreacion = now
                                };

                                _context.CupostT012RlNovedadDocumentos.Add(docNuevoNovedad);
                                _context.SaveChanges();
                            }
                        }
                    }
                }

                return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, result, token, false);
            }
            else
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgIntenteNuevamente, "", token, true);
            }
        }

        public Responses ElimiarDocumentoNovedad(ClaimsIdentity identity, decimal idNovedad, decimal idArchivo)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            decimal codigoUsuario = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var rlDocNove = _context.CupostT012RlNovedadDocumentos.Where(p => p.A012codigoDocumento == idArchivo && p.A012codigoNovedad == idNovedad && p.A012estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

            if (rlDocNove != null)
            {
                rlDocNove.A012estadoRegistro = StringHelper.estadoInactivo;
                rlDocNove.A012codigoUsuarioModificacion = codigoUsuario;
                rlDocNove.A012fechaModificacion = DateTime.Now;
                _context.SaveChanges();

                var document = _context.AdmintT009Documentos.Where(p => p.PkT009codigo == rlDocNove.A012codigoDocumento && p.A009estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                if (document != null)
                {
                    document.A009estadoRegistro = StringHelper.estadoInactivo;
                    document.A009codigoUsuarioModificacion = codigoUsuario;
                    document.A009fechaModificacion = DateTime.Now;
                    _context.SaveChanges();
                }
            }

            return ResponseManager.generaRespuestaGenerica("Documento Eliminado", "", token, false);
        }
        public Responses ConsultarCupos(ClaimsIdentity identity, decimal idEmpresa)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            TotalQuotas totales = new TotalQuotas();
            totales.availableQuotas = _context.CupostT002Cupos.Where(p => p.A002codigoEmpresa == idEmpresa && p.A002estadoRegistro == StringHelper.estadoActivo).Sum(p => p.A002cuposDisponibles);
            totales.availableInventory = _context.CupostT004FacturacompraCartaventa.Where(p => p.A004codigoEntidadCompra == idEmpresa && p.A004estadoRegistro == StringHelper.estadoActivo).Sum(p => p.A004disponibilidadInventario);
            totales.pendingQuotasForProcessing = 0;

            return ResponseManager.generaRespuestaGenerica("", totales, token, false);
        }
    }
}