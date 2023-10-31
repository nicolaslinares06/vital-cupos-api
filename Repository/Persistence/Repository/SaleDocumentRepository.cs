using API.Helpers;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using NHibernate.Id.Insert;
using NHibernate.Linq;
using Org.BouncyCastle.Utilities.Net;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace Repository.Persistence.Repository
{
    public class SaleDocumentRepository : ISaleDocumentRepository
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public SaleDocumentRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// Obtiene el documento de venta por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetSaleDocumentId(ClaimsIdentity identity, int code, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from saleDocument in _context.CupostT004FacturacompraCartaventa
                        join companySeller in _context.CupostT001Empresas
                        on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                        join companyShopper in _context.CupostT001Empresas
                        on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                        where saleDocument.PkT004codigo == decimal.Parse(code.ToString())
                        && saleDocument.A004estadoRegistro == StringHelper.estadoActivo
                        select new
                        {
                            Code = saleDocument.PkT004codigo,
                            Numeration = saleDocument.PkT004codigo,
                            CarteNumber = saleDocument.A004numeroCartaVenta,
                            SaleDate = saleDocument.A004fechaVenta,
                            NumberSold = saleDocument.A004totalCuposVendidos,
                            BusinessSale = companySeller.A001codigoParametricaTipoEntidad,
                            TypeDocumentSeller = StringHelper.tipoDocumentoNit,
                            DocumentNumberSeller = companySeller.A001nit,
                            ReasonSocial = companySeller.A001nombre,
                            InitialBalanceBusiness = saleDocument.A004saldoEntidadVendeInicial,
                            FinalBalanceBusiness = saleDocument.A004saldoEntidadVendeFinal,
                            Observations = saleDocument.A004observacionesVenta,
                            InventoryAvailability = saleDocument.A004disponibilidadInventario,
                            BusinessShopper = companyShopper.A001codigoParametricaTipoEntidad,
                            TypeDocumentShopper = StringHelper.tipoDocumentoNit,
                            DocumentNumberShopper = companyShopper.A001nit,
                            ReasonSocialShopper = companyShopper.A001nombre,
                            InitialBalanceBusinessShopper = saleDocument.A004saldoEntidadCompraInicial,
                            FinalBalanceBusinessShopper = saleDocument.A004saldoEntidadCompraFinal,
                            ObservationsShopper = saleDocument.A004observacionesCompra,
                            CompanySellerCode = saleDocument.A004codigoEntidadVende,
                            CompanyShopperCode = saleDocument.A004codigoEntidadCompra,
                            TypeSpecimenSeller = saleDocument.A004tipoEspecimenEntidadVende,
                            TypeSpecimenShopper = saleDocument.A004tipoEspecimenEntidadCompra,
                            RegistrationDateCarteSale = saleDocument.A004fechaRegistroCartaVenta
                        };
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", query, token, false);

        }

        /// <summary>
        /// Obtiene los documentos de venta
        /// </summary>
        /// <param name="typeDocument"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetSaleDocuments(ClaimsIdentity identity, string ipAddress, string? typeDocument, string? documentNumber, string? numberCartaVenta)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = new object();
            if (typeDocument == null && documentNumber == null && numberCartaVenta == null)
            {
                query = from saleDocument in _context.CupostT004FacturacompraCartaventa
                        join companySeller in _context.CupostT001Empresas
                        on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                        join companyShopper in _context.CupostT001Empresas
                        on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                        where saleDocument.A004estadoRegistro == StringHelper.estadoActivo
                        select new
                        {
                            Code = saleDocument.PkT004codigo,
                            Numeration = saleDocument.PkT004codigo,
                            CarteNumber = saleDocument.A004numeroCartaVenta,
                            NitCompanySeller = companySeller.A001nit,
                            ReasonSocial = companySeller.A001nombre,
                            NitCompanyShopper = companyShopper.A001nit,
                            ReasonSocialShopper = companyShopper.A001nombre,
                            SaleDate = saleDocument.A004fechaVenta,
                            RegistrationDateCarteSale = saleDocument.A004fechaRegistroCartaVenta,
                            QuotasSold = saleDocument.A004totalCuposVendidos
                        };
            }
            else
            {
                if (typeDocument != null && documentNumber != null && numberCartaVenta != null)
                {
                    query = from saleDocument in _context.CupostT004FacturacompraCartaventa
                            join companySeller in _context.CupostT001Empresas
                            on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                            join person in _context.CitestT003Personas
                            on companySeller.A001codigoPersonaRepresentantelegal equals person.PkT003codigo
                            join companyShopper in _context.CupostT001Empresas
                            on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                            where ((companySeller.A001nit == decimal.Parse(documentNumber) && Convert.ToDecimal(typeDocument) == StringHelper.tipoDocumentoNit)
                            || (person.A003codigoParametricaTipoIdentificacion == decimal.Parse(typeDocument) && person.A003identificacion == documentNumber.ToString()))
                            && (saleDocument.A004numeroCartaVenta == numberCartaVenta) && (saleDocument.A004estadoRegistro == StringHelper.estadoActivo)
                            select new
                            {
                                Code = saleDocument.PkT004codigo,
                                Numeration = saleDocument.PkT004codigo,
                                CarteNumber = saleDocument.A004numeroCartaVenta,
                                NitCompanySeller = companySeller.A001nit,
                                ReasonSocial = companySeller.A001nombre,
                                NitCompanyShopper = companyShopper.A001nit,
                                ReasonSocialShopper = companyShopper.A001nombre,
                                SaleDate = saleDocument.A004fechaVenta,
                                RegistrationDateCarteSale = saleDocument.A004fechaRegistroCartaVenta,
                                QuotasSold = saleDocument.A004totalCuposVendidos
                            };
                }
                else if (typeDocument != null && documentNumber != null && numberCartaVenta == null)
                {
                    query = from saleDocument in _context.CupostT004FacturacompraCartaventa
                            join companySeller in _context.CupostT001Empresas
                            on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                            join person in _context.CitestT003Personas
                            on companySeller.A001codigoPersonaRepresentantelegal equals person.PkT003codigo
                            join companyShopper in _context.CupostT001Empresas
                            on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                            where ((companySeller.A001nit == decimal.Parse(documentNumber) && Convert.ToDecimal(typeDocument) == StringHelper.tipoDocumentoNit)
                            || (person.A003codigoParametricaTipoIdentificacion == decimal.Parse(typeDocument) && person.A003identificacion == documentNumber.ToString()))
                            && (saleDocument.A004estadoRegistro == StringHelper.estadoActivo)
                            select new
                            {
                                Code = saleDocument.PkT004codigo,
                                Numeration = saleDocument.PkT004codigo,
                                CarteNumber = saleDocument.A004numeroCartaVenta,
                                NitCompanySeller = companySeller.A001nit,
                                ReasonSocial = companySeller.A001nombre,
                                NitCompanyShopper = companyShopper.A001nit,
                                ReasonSocialShopper = companyShopper.A001nombre,
                                SaleDate = saleDocument.A004fechaVenta,
                                RegistrationDateCarteSale = saleDocument.A004fechaRegistroCartaVenta,
                                QuotasSold = saleDocument.A004totalCuposVendidos
                            };
                }
                else if (typeDocument == null && documentNumber == null && numberCartaVenta != null)
                {
                    query = from saleDocument in _context.CupostT004FacturacompraCartaventa
                            join companySeller in _context.CupostT001Empresas
                            on saleDocument.A004codigoEntidadVende equals companySeller.PkT001codigo
                            join person in _context.CitestT003Personas
                            on companySeller.A001codigoPersonaRepresentantelegal equals person.PkT003codigo
                            join companyShopper in _context.CupostT001Empresas
                            on saleDocument.A004codigoEntidadCompra equals companyShopper.PkT001codigo
                            where (saleDocument.A004numeroCartaVenta == numberCartaVenta) && (saleDocument.A004estadoRegistro == StringHelper.estadoActivo)
                            select new
                            {
                                Code = saleDocument.PkT004codigo,
                                Numeration = saleDocument.PkT004codigo,
                                CarteNumber = saleDocument.A004numeroCartaVenta,
                                NitCompanySeller = companySeller.A001nit,
                                ReasonSocial = companySeller.A001nombre,
                                NitCompanyShopper = companyShopper.A001nit,
                                ReasonSocialShopper = companyShopper.A001nombre,
                                SaleDate = saleDocument.A004fechaVenta,
                                RegistrationDateCarteSale = saleDocument.A004fechaRegistroCartaVenta,
                                QuotasSold = saleDocument.A004totalCuposVendidos
                            };
                }
            }
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }


        /// <summary>
        /// Obtiene los cupos por numero documento de la empresa o representante legal
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetQuotas(ClaimsIdentity identity, string documentNumber, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var company = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == documentNumber).FirstOrDefault();

            List<Quota>? quotaList = new List<Quota>();
            Metodos metodo = new Metodos(_context);
            if (company != null)
            {
                var quotas = _context.CuposV001ResolucionCupos.Where(p => p.codigoEmpresa == company.PkT001codigo).ToList();
                foreach (var quota in quotas)
                {
                    quotaList.Add(metodo.addQuotaToList(quota, quota.NumeroInternoInicial, quota.numeroInternoFinal, quota.numeroInternoInicialCuotaRepoblacion, quota.numeroInternoFinalCuotaRepoblacion));
                }
            }
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", quotaList, token, false);
        }

        /// <summary>
        /// Obtiene las numeraciones por numero documento de la empresa y codigo cupo
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetQuotasNumeraciones(ClaimsIdentity identity, int codigoCupo, string documentNumber, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var company = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == documentNumber).FirstOrDefault();

            List<NumbersSeals> numeraciones = new List<NumbersSeals>();
            if (company != null)
            {
                var compra = _context.CupostT026FacturaCompraCupo.Where(p => p.A026CodigoCupo == codigoCupo).ToList();
                foreach (var numeracion in compra)
                {
                    NumbersSeals num = new NumbersSeals();
                    num.initial = Convert.ToInt32(numeracion.A026NumeracionInicial);
                    num.final = Convert.ToInt32(numeracion.A026NumeracionFinal);
                    numeraciones.Add(num);
                    if(numeracion.A026NumeracionInicialRepoblacion !=0 && numeracion.A026NumeracionFinalRepoblacion!=0 && numeracion.A026NumeracionInicialRepoblacion !=null && numeracion.A026NumeracionFinalRepoblacion != null)
                    {
                        NumbersSeals num2 = new NumbersSeals();
                        num2.initial = Convert.ToInt32(numeracion.A026NumeracionInicialRepoblacion);
                        num2.final = Convert.ToInt32(numeracion.A026NumeracionFinalRepoblacion);
                        numeraciones.Add(num2);
                    }
                    
                }
            }
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", numeraciones, token, false);
        }

        /// <summary>
        /// Valida si estan disponibvles las numeraciones
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses ValidateNumbers(ClaimsIdentity identity, Seal numbers)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var validacion = true;
            List<CupostT026FacturaCompraCupo > val = new List<CupostT026FacturaCompraCupo>();

            var noDisp = _context.CupostT026FacturaCompraCupo.Where(p => (p.A026NumeracionInicial<= numbers.initialNumber && p.A026NumeracionFinal>=numbers.initialNumber) || (p.A026NumeracionInicial <= numbers.finalNumber && p.A026NumeracionFinal >= numbers.finalNumber)).ToList();
           
            if (noDisp.Count > 0)
            {
                val = noDisp.Where(p => p.A026CodigoCupo == numbers.quotaCode).ToList();
            }

            if (val.Count == 0)
            {
                var noDispRep = _context.CupostT026FacturaCompraCupo.Where(p => ((p.A026NumeracionInicialRepoblacion <= numbers.initialNumber && p.A026NumeracionFinalRepoblacion >= numbers.initialNumber) || (p.A026NumeracionInicialRepoblacion <= numbers.finalNumber && p.A026NumeracionFinalRepoblacion >= numbers.finalNumber)) && (p.A026CodigoCupo == numbers.quotaCode)).ToList();
                
                List<CupostT026FacturaCompraCupo> valRep = new List<CupostT026FacturaCompraCupo>();

                if (noDisp.Count > 0)
                {
                    valRep = noDispRep.Where(p => p.A026CodigoCupo == numbers.quotaCode).ToList();
                }

                if (valRep.Count > 0)
                {
                    validacion = false;
                }
            }
            else
            {
                validacion = false;
            }

            return ResponseManager.generaRespuestaGenerica("", validacion, token, false);
        }

        /// <summary>
        /// Obtiene el inventario
        /// </summary>
        /// <returns></returns>
        public Responses GetInventory(ClaimsIdentity identity, string documentNumber, string ipAddress, string? code = null)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            List<Inventory>? inventoryList = new List<Inventory>();

            var persona = _context.CitestT003Personas.Where(p => p.A003identificacion == documentNumber && p.A003estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();

            CupostT001Empresa companySeller = new CupostT001Empresa();
            if (persona != null)
            {
                companySeller = _context.CupostT001Empresas.Where(p => p.A001codigoPersonaRepresentantelegal == persona.PkT003codigo && p.A001estadoRegistro == StringHelper.estadoActivo).FirstOrDefault() ?? new CupostT001Empresa();
            }
            else
            {
                companySeller = _context.CupostT001Empresas.Where(p => p.A001nit.ToString() == documentNumber ).FirstOrDefault() ?? new CupostT001Empresa();
            }


            List<CupostT004FacturacompraCartaventum> sale = new List<CupostT004FacturacompraCartaventum>();
            if (code == null)
            {
                sale = _context.CupostT004FacturacompraCartaventa.Where(p => p.A004codigoEntidadCompra == companySeller.PkT001codigo && p.A004estadoRegistro == StringHelper.estadoActivo).ToList();
            }
            else
            {
                sale = _context.CupostT004FacturacompraCartaventa.Where(p => p.A004codigoEntidadCompra == companySeller.PkT001codigo && p.PkT004codigo == Convert.ToDecimal(code) && p.A004estadoRegistro == StringHelper.estadoActivo).ToList();
            }
            Metodos metodo = new Metodos(_context);
            foreach (var saleDocument in sale)
            {
                var invoice = _context.CupostT026FacturaCompraCupo.Where(p => p.A026CodigoFacturaCompra == saleDocument.PkT004codigo).ToList();

                foreach (var quotaInvoice in invoice)
                {
                    var quotas = _context.CupostT002Cupos.Where(p => p.PkT002codigo == quotaInvoice.A026CodigoCupo && p.A002estadoRegistro == StringHelper.estadoActivo).ToList();
                    foreach (var quota in quotas)
                    {
                        var specieExport = _context.CupostT005Especieaexportars.Where(p => p.A005codigoCupo == quota.PkT002codigo && p.A005estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                        inventoryList.Add(metodo.addInventoryToList(quota, saleDocument, companySeller, quotaInvoice, specieExport));
                    }
                }
            }
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, null);

            return ResponseManager.generaRespuestaGenerica("", inventoryList, token, false);
        }

        /// <summary>
        /// Obtienen las especies
        /// </summary>
        /// <returns></returns>
        public Responses GetSpecies(ClaimsIdentity identity)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                var query = from species in _context.AdmintT005Especimen
                            where species.A005estadoRegistro == StringHelper.estadoActivo
                            select new
                            {
                                Code = species.PkT005codigo,
                                CommonName = species.A005nombreComun,
                                ScientificName = species.A005nombreCientifico
                            };
                return ResponseManager.generaRespuestaGenerica("", query, token, false);
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", "", true);
            }
        }

        /// <summary>
        /// Guarda el documento de venta
        /// </summary>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        public Responses SaveSaleDocument(ClaimsIdentity identity, SaleDocumentModel saleDocument, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            
            try
            {
                if (saleDocument == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No se pudo guardar, error SaleDocument object null", "", "", true);
                }
                else
                {
                    CupostT004FacturacompraCartaventum factura = new CupostT004FacturacompraCartaventum();
                    SaleDocumentAuditoria codFactura = new SaleDocumentAuditoria();


                    factura.A004numeroCartaVenta = saleDocument.CarteNumber;
                    factura.A004fechaVenta = saleDocument.SaleDate;
                    factura.A004totalCuposVendidos = saleDocument.NumberSold;
                    factura.A004codigoEntidadVende = saleDocument.CompanySellerCode;
                    factura.A004saldoEntidadVendeInicial = saleDocument.InitialBalanceBusiness;
                    factura.A004saldoEntidadVendeFinal = saleDocument.FinalBalanceBusiness;
                    factura.A004codigoParametricaTipoCartaventa = saleDocument.TypeCarte;
                    factura.A004observacionesVenta = saleDocument.Observations;
                    factura.A004disponibilidadInventario = saleDocument.InventoryAvailability;
                    factura.A004codigoEntidadCompra = saleDocument.CompanyShopperCode;
                    factura.A004saldoEntidadCompraInicial = saleDocument.InitialBalanceBusinessShopper;
                    factura.A004saldoEntidadCompraFinal = saleDocument.FinalBalanceBusinessShopper;
                    factura.A004observacionesCompra = saleDocument.ObservationsShopper;
                    factura.A004codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    factura.A004codigoDocumentoSoporte = 0;
                    factura.A004codigoDocumentoFactura = 0;
                    factura.A004fechaCreacion = DateTime.Now;
                    factura.A004fechaRegistroCartaVenta = saleDocument.RegistrationDateCarteSale;
                    factura.A004estadoRegistro = StringHelper.estadoActivo;
                    factura.A004totalCuposObtenidos = 0;
                    factura.A004tipoEspecimenEntidadVende = saleDocument.TypeSpecimenSeller;
                    factura.A004tipoEspecimenEntidadCompra = saleDocument.TypeSpecimenShopper;

                    _context.CupostT004FacturacompraCartaventa.Add(factura);

                    _context.SaveChanges();

                    var codEntidadVende = _context.CupostT001Empresas.Where(p => p.PkT001codigo == factura.A004codigoEntidadVende).FirstOrDefault();
                    var codEntidadCompra = _context.CupostT001Empresas.Where(p => p.PkT001codigo == factura.A004codigoEntidadCompra).FirstOrDefault();
                    var codUsuarioCreacion = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == factura.A004codigoUsuarioCreacion).FirstOrDefault();
                    var CodParametrica = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == factura.A004codigoParametricaTipoCartaventa).FirstOrDefault();

                    codFactura.A004numeroCartaVenta = saleDocument.CarteNumber;
                    codFactura.A004fechaVenta = saleDocument.SaleDate;
                    codFactura.A004totalCuposVendidos = saleDocument.NumberSold;
                    codFactura.A004codigoEntidadVende = codEntidadVende != null ? codEntidadVende.A001nombre : "";
                    codFactura.A004saldoEntidadVendeInicial = saleDocument.InitialBalanceBusiness;
                    codFactura.A004saldoEntidadVendeFinal = saleDocument.FinalBalanceBusiness;
                    codFactura.A004codigoParametricaTipoCartaventa = CodParametrica != null ? CodParametrica.A008valor : "";
                    codFactura.A004observacionesVenta = saleDocument.Observations;
                    codFactura.A004disponibilidadInventario = saleDocument.InventoryAvailability;
                    codFactura.A004codigoEntidadCompra = codEntidadCompra != null? codEntidadCompra.A001nombre : "";
                    codFactura.A004saldoEntidadCompraInicial = saleDocument.InitialBalanceBusinessShopper;
                    codFactura.A004saldoEntidadCompraFinal = saleDocument.FinalBalanceBusinessShopper;
                    codFactura.A004observacionesCompra = saleDocument.ObservationsShopper;
                    codFactura.A004codigoUsuarioCreacion = codUsuarioCreacion != null ? codUsuarioCreacion.A012primerNombre + " " + codUsuarioCreacion.A012segundoNombre + " " + codUsuarioCreacion.A012primerApellido + " " + codUsuarioCreacion.A012segundoApellido : "";
                    codFactura.A004codigoDocumentoSoporte = 0;
                    codFactura.A004codigoDocumentoFactura = 0;
                    codFactura.A004fechaCreacion = DateTime.Now;
                    codFactura.A004fechaRegistroCartaVenta = saleDocument.RegistrationDateCarteSale;
                    codFactura.A004estadoRegistro = StringHelper.estadoActivo;
                    codFactura.A004totalCuposObtenidos = 0;
                    codFactura.A004tipoEspecimenEntidadVende = saleDocument.TypeSpecimenSeller;
                    codFactura.A004tipoEspecimenEntidadCompra = saleDocument.TypeSpecimenShopper;

                   

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, codFactura, factura.PkT004codigo.ToString());
                    

                    if (saleDocument.Quotas != null)
                    {
                        SaveQuota(identity, saleDocument.Quotas, ipAddress);
                    }

                    if (saleDocument.QuotasInventory != null)
                    {
                        UpdateQuotasInventory(identity, saleDocument.QuotasInventory, ipAddress);
                    }

                    ValidateDocumentAction(identity, saleDocument.SupportDocuments, ipAddress, false);
                    return ResponseManager.generaRespuestaGenerica("Se guardo correctamente", "", token, true);
                }

            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", "", true);
            }
        }

        /// <summary>
        /// Valida si la empresa se encuentra registrada
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <param name="business"></param>
        /// <param name="typeDocument"></param>
        /// <returns></returns>
        public Responses ValidateCompany(ClaimsIdentity identity, decimal company = 0, decimal typeDocument = 0, string documentNumber = "0")
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from business in _context.CupostT001Empresas
                        join person in _context.CitestT003Personas
                        on business.A001codigoPersonaRepresentantelegal equals person.PkT003codigo
                        where ((business.A001codigoParametricaTipoEntidad == company && typeDocument == 95 && business.A001nit == decimal.Parse(documentNumber))
                        || (person.A003codigoParametricaTipoIdentificacion == typeDocument && person.A003identificacion == documentNumber.ToString()))
                        && (business.A001estadoRegistro == StringHelper.estadoActivo)
                        select new
                        {
                            reasonSocial = business.A001nombre
                        };

            return ResponseManager.generaRespuestaGenerica("Validación exitosa", query, token, false);
        }

        /// <summary>
        /// Filtra empresa por numero de documento
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Responses SearchCompany(ClaimsIdentity identity, string number, decimal typeDocument = 0, decimal companyCode = 0)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from company in _context.CupostT001Empresas
                        join person in _context.CitestT003Personas
                        on company.A001codigoPersonaRepresentantelegal equals person.PkT003codigo
                        where ((company.A001nit == decimal.Parse(number) && typeDocument == 95 && company.A001codigoParametricaTipoEntidad == companyCode)
                        || (person.A003codigoParametricaTipoIdentificacion == typeDocument && person.A003identificacion == number.ToString()))
                        && (company.A001estadoRegistro == StringHelper.estadoActivo)
                        select new
                        {
                            code = company.PkT001codigo,
                            reasonSocial = company.A001nombre,
                        };

            return ResponseManager.generaRespuestaGenerica("Busqueda completada", query, token, false);
        }

        /// <summary>
        /// Elimina un documento de venta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Responses DeleteSaleDocument(ClaimsIdentity identity, string id, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var qry = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == decimal.Parse(id) && p.A004estadoRegistro == 72);
            foreach (var saleDocument in qry)
            {
                saleDocument.A004estadoRegistro = StringHelper.estadoInactivo; //Inactivo
                met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 4, saleDocument, saleDocument.PkT004codigo.ToString());
            }
            _context.SaveChanges();
            return ResponseManager.generaRespuestaGenerica("Se elimino correctamente", "", token, false);
        }

        /// <summary>
        /// Actualiza el documento de venta
        /// </summary>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses UpdateSaleDocument(ClaimsIdentity identity, SaleDocumentModel saleDocument, string ipAddress)
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
                var documentoVenta = _context.CupostT004FacturacompraCartaventa.Where(p => p.PkT004codigo == saleDocument.Code && p.A004estadoRegistro == StringHelper.estadoActivo).FirstOrDefault();
                if (documentoVenta != null)
                {
                    #region auditoria

                    var codEntidadVende = _context.CupostT001Empresas.Where(p => p.PkT001codigo == documentoVenta.A004codigoEntidadVende).FirstOrDefault();
                    var codEntidadVendeNew = _context.CupostT001Empresas.Where(p => p.PkT001codigo == saleDocument.CompanySellerCode).FirstOrDefault();
                    var codEntidadCompra = _context.CupostT001Empresas.Where(p => p.PkT001codigo == documentoVenta.A004codigoEntidadCompra).FirstOrDefault();
                    var codEntidadCompraNew = _context.CupostT001Empresas.Where(p => p.PkT001codigo == saleDocument.CompanyShopperCode).FirstOrDefault();
                    var codParametrica = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == documentoVenta.A004codigoParametricaTipoCartaventa).FirstOrDefault();
                    var codParametricaNew = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == saleDocument.TypeCarte).FirstOrDefault();
                    var codUsuModificacion = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == documentoVenta.A004codigoUsuarioModificacion).FirstOrDefault();
                    var codUsuModificacionNew = _context.AdmintT012Usuarios.Where(p => p.PkT012codigo == codigoUsuario).FirstOrDefault();


                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004numeroCartaVenta.ToString(), saleDocument.CarteNumber.ToString(), "A004numeroCartaVenta");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004fechaVenta.ToString(), saleDocument.SaleDate.ToString(), "A004fechaVenta");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004totalCuposVendidos.ToString(), saleDocument.NumberSold.ToString(), "A004totalCuposVendidos");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, codEntidadVende.A001nombre, codEntidadVendeNew.A001nombre, "A004codigoEntidadVende");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004saldoEntidadVendeInicial.ToString(), saleDocument.InitialBalanceBusiness.ToString(), "A004saldoEntidadVendeInicial");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004saldoEntidadVendeFinal.ToString(), saleDocument.FinalBalanceBusiness.ToString(), "A004saldoEntidadVendeFinal");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, codParametrica.A008valor, codParametricaNew.A008valor, "A004codigoParametricaTipoCartaventa");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004observacionesVenta.ToString(), saleDocument.Observations.ToString(), "A004observacionesVenta");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004disponibilidadInventario.ToString(), saleDocument.InventoryAvailability.ToString(), "A004disponibilidadInventario");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, codEntidadCompra.A001nombre, codEntidadCompraNew.A001nombre, "A004codigoEntidadCompra");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004saldoEntidadCompraInicial.ToString(), saleDocument.InitialBalanceBusinessShopper.ToString(), "A004saldoEntidadCompraInicial");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004saldoEntidadCompraFinal.ToString(), saleDocument.FinalBalanceBusinessShopper.ToString(), "A004saldoEntidadCompraFinal");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004observacionesCompra.ToString(), saleDocument.ObservationsShopper.ToString(), "A004observacionesCompra");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004codigoUsuarioModificacion == null ? "" : codUsuModificacion.A012primerNombre + " " + codUsuModificacion.A012segundoNombre + " " + codUsuModificacion.A012primerApellido + " " + codUsuModificacion.A012segundoApellido, codUsuModificacionNew.A012primerNombre + " " + codUsuModificacionNew.A012segundoNombre + " " + codUsuModificacionNew.A012primerApellido + " " + codUsuModificacionNew.A012segundoApellido, "A004codigoUsuarioModificacion");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004fechaModificacion.ToString(), DateTime.Now.ToString(), "A004fechaModificacion");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004fechaRegistroCartaVenta.ToString(), saleDocument.RegistrationDateCarteSale.ToString(), "A004fechaRegistroCartaVenta");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004tipoEspecimenEntidadVende.ToString(), saleDocument.TypeSpecimenSeller.ToString(), "A004tipoEspecimenEntidadVende");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, documentoVenta.A004tipoEspecimenEntidadCompra.ToString(), saleDocument.TypeSpecimenShopper.ToString(), "A004tipoEspecimenEntidadCompra");

                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, valorAnterior, valorActual, campos, 3, null, null);
                    
                    #endregion

                    documentoVenta.A004numeroCartaVenta = saleDocument.CarteNumber;
                    documentoVenta.A004fechaVenta = saleDocument.SaleDate;
                    documentoVenta.A004totalCuposVendidos = saleDocument.NumberSold;
                    documentoVenta.A004codigoEntidadVende = saleDocument.CompanySellerCode;
                    documentoVenta.A004saldoEntidadVendeInicial = saleDocument.InitialBalanceBusiness;
                    documentoVenta.A004saldoEntidadVendeFinal = saleDocument.FinalBalanceBusiness;
                    documentoVenta.A004codigoParametricaTipoCartaventa = saleDocument.TypeCarte;
                    documentoVenta.A004observacionesVenta = saleDocument.Observations;
                    documentoVenta.A004disponibilidadInventario = saleDocument.InventoryAvailability;
                    documentoVenta.A004codigoEntidadCompra = saleDocument.CompanyShopperCode;
                    documentoVenta.A004saldoEntidadCompraInicial = saleDocument.InitialBalanceBusinessShopper;
                    documentoVenta.A004saldoEntidadCompraFinal = saleDocument.FinalBalanceBusinessShopper;
                    documentoVenta.A004observacionesCompra = saleDocument.ObservationsShopper;
                    documentoVenta.A004codigoDocumentoSoporte = 0;
                    documentoVenta.A004codigoDocumentoFactura = 0;
                    documentoVenta.A004totalCuposObtenidos = 0;
                    documentoVenta.A004codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    documentoVenta.A004fechaModificacion = DateTime.Now;
                    documentoVenta.A004fechaRegistroCartaVenta = saleDocument.RegistrationDateCarteSale;
                    documentoVenta.A004tipoEspecimenEntidadVende = saleDocument.TypeSpecimenSeller;
                    documentoVenta.A004tipoEspecimenEntidadCompra = saleDocument.TypeSpecimenShopper;
                    _context.SaveChanges();
                }
                ValidateDocumentAction(identity, saleDocument.SupportDocuments, ipAddress, true, saleDocument.Code, saleDocument.SupportDocumentsRemoved);
                return ResponseManager.generaRespuestaGenerica("La información se guardo con éxito", "", token, false);
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message, "", "", true);
            }
        }

        /// <summary>
        /// Guarda el documento de venta
        /// </summary>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        public Responses SaveQuota(ClaimsIdentity identity, List<Quota> quotas, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                Metodos met = new Metodos(_context);
                var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (quotas == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No se pudo guardar, error SaleDocument object null", "", "", true);
                }
                else
                {
                    decimal saleDocumentSaveId = decimal.Parse(_context.CupostT004FacturacompraCartaventa.OrderByDescending(p => p.PkT004codigo).Select(r => r.PkT004codigo).First().ToString());

                    foreach (var quota in quotas)
                    {
                        CupostT026FacturaCompraCupo compra = new CupostT026FacturaCompraCupo();
                        compra.A026CodigoFacturaCompra = saleDocumentSaveId;
                        compra.A026CodigoCupo = quota.Code;
                        compra.A026NumeracionInicial = quota.InitialNumeration;
                        compra.A026NumeracionFinal = quota.FinalNumeration;
                        compra.A026NumeracionInicialRepoblacion = quota.InitialNumerationRePoblation;
                        compra.A026NumeracionFinalRepoblacion = quota.FinalNumerationRePoblation;
                        compra.A026NumeracionInicialPrecintos = quota.InitialNumerationSeal;
                        compra.A026NumeracionFinalPrecintos = quota.FinalNumerationSeal;
                        compra.A026CuposDisponibles = quota.QuotasSold;
                        compra.A026CantidadEspecimenesComprados = quota.QuotasSold;

                        _context.CupostT026FacturaCompraCupo.Add(compra);

                        _context.SaveChanges();

                        met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, compra, compra.Pk_T026Codigo.ToString());

                        UpdateQuotasCompanySells(identity, quota, ipAddress);
                    }
                    return ResponseManager.generaRespuestaGenerica("Se guardo correctamente cupos", "", "", false);
                }

            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", "", true);
            }
        }

        /// <summary>
        /// Valida la accion de documentos adjuntos para guardar, actualizar o eliminar el archivo
        /// </summary>
        /// <param name="supportDocuments"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses ValidateDocumentAction(ClaimsIdentity identity, List<SupportDocuments> supportDocuments, string ipAddress, bool actionEdit, decimal code = 0, List<SupportDocuments>? supportDocumentsRemoved = null)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                if (supportDocuments != null)
                {
                    if (!actionEdit)
                    {
                        foreach (var document in supportDocuments)
                        {
                            SaveDocuments(identity, document, ipAddress);
                        }
                    }
                    else
                    {
                        if (supportDocumentsRemoved != null)
                        {
                            UpdateDocumentsRemoved(identity, supportDocumentsRemoved, ipAddress);
                        }

                        foreach (var document in supportDocuments)
                        {
                            if (document.tempAction == "add")
                            {
                                SaveDocuments(identity, document, ipAddress);
                            }
                            else
                            {
                                UpdateDocument(identity, document, ipAddress);
                            }
                        }
                    }
                    return ResponseManager.generaRespuestaGenerica("Se guardo correctamente", "", token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica("No se pudo guardar", "", token, true);
                }
            }
            catch (Exception ex)
            {

                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", "", true);
            }
        }

        /// <summary>
        /// Guarda el documento soporte
        /// </summary>
        /// <param name="document"></param>
        [ExcludeFromCodeCoverage]
        public void SaveDocuments(ClaimsIdentity identity, SupportDocuments document, string ipAddress)
        {

            decimal saleDocumentSaveId = decimal.Parse(_context.CupostT004FacturacompraCartaventa.OrderByDescending(p => p.PkT004codigo).Select(r => r.PkT004codigo).First().ToString());

            Metodos metodo = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var uri = metodo.GuardarArchivoFtp(document);

            AdmintT009Documento docNuevo = new AdmintT009Documento();
            docNuevo.A009fechaCreacion = DateTime.Now;
            docNuevo.A009codigoUsuarioCreacion = 1;
            docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
            docNuevo.A009codigoParametricaTipoDocumento = 25;
            docNuevo.A009firmaDigital = "firma";
            docNuevo.A009codigoPlantilla = 1;
            docNuevo.A009documento = document.attachmentName ?? "";
            docNuevo.A009descripcion = document.attachmentName ?? "";
            docNuevo.A009url = uri;

            _context.AdmintT009Documentos.Add(docNuevo);
            _context.SaveChanges();
            metodo.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, docNuevo, docNuevo.PkT009codigo.ToString());

            CupostT025FacturaCompraCartaVentaDocumento facturaCompraCartaVentaDocumento = new CupostT025FacturaCompraCartaVentaDocumento();
            facturaCompraCartaVentaDocumento.A025CodigoDocumento = docNuevo.PkT009codigo;
            facturaCompraCartaVentaDocumento.A025CodigoFacturaCompraCartaVenta = saleDocumentSaveId;
            facturaCompraCartaVentaDocumento.A025FechaCreacion = DateTime.Now;
            facturaCompraCartaVentaDocumento.A025UsuarioCreacion = 1;
            facturaCompraCartaVentaDocumento.A025EstadoRegistro = StringHelper.estadoActivo;

            _context.CupostT025FacturaCompraCartaVentaDocumento.Add(facturaCompraCartaVentaDocumento);
            _context.SaveChanges();
            metodo.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, facturaCompraCartaVentaDocumento, facturaCompraCartaVentaDocumento.Pk_T025Codigo.ToString());

        }

        /// <summary>
        ///Actualiza el document soporte
        /// </summary>
        /// <param name="document"></param>
        /// <param name="code"></param>
        [ExcludeFromCodeCoverage]
        public void UpdateDocument(ClaimsIdentity identity, SupportDocuments document, string ipAddress)
        {           

            Metodos metodo = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var uri = metodo.GuardarArchivoFtp(document);

            AdmintT009Documento docNuevo = new AdmintT009Documento();
            docNuevo.A009fechaCreacion = DateTime.Now;
            docNuevo.A009codigoUsuarioCreacion = 1;
            docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
            docNuevo.A009codigoParametricaTipoDocumento = 25;
            docNuevo.A009firmaDigital = "firma";
            docNuevo.A009codigoPlantilla = 1;
            docNuevo.A009documento = document.attachmentName ?? "";
            docNuevo.A009descripcion = document.attachmentName ?? "";
            docNuevo.A009url = uri;

            _context.AdmintT009Documentos.Add(docNuevo);
            _context.SaveChanges();
            metodo.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, docNuevo, docNuevo.PkT009codigo.ToString());

            CupostT025FacturaCompraCartaVentaDocumento facturaCompraCartaVentaDocumento = new CupostT025FacturaCompraCartaVentaDocumento();
            facturaCompraCartaVentaDocumento.A025CodigoDocumento = docNuevo.PkT009codigo;
            facturaCompraCartaVentaDocumento.A025CodigoFacturaCompraCartaVenta = document.code;
            facturaCompraCartaVentaDocumento.A025FechaCreacion = DateTime.Now;
            facturaCompraCartaVentaDocumento.A025UsuarioCreacion = 1;
            facturaCompraCartaVentaDocumento.A025EstadoRegistro = StringHelper.estadoActivo;

            _context.CupostT025FacturaCompraCartaVentaDocumento.Add(facturaCompraCartaVentaDocumento);
            _context.SaveChanges();
            metodo.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, facturaCompraCartaVentaDocumento, facturaCompraCartaVentaDocumento.Pk_T025Codigo.ToString());
        }

        /// <summary>
        /// Obtiene el documento soporte
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public Responses GetSupportDocument(ClaimsIdentity identity, decimal code, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var documents = from saleDocumentsAttachments in _context.CupostT025FacturaCompraCartaVentaDocumento
                            join saleDocument in _context.CupostT004FacturacompraCartaventa
                            on saleDocumentsAttachments.A025CodigoFacturaCompraCartaVenta equals saleDocument.PkT004codigo
                            join document in _context.AdmintT009Documentos
                            on saleDocumentsAttachments.A025CodigoDocumento equals document.PkT009codigo
                            where saleDocument.PkT004codigo == code
                            where saleDocument.A004estadoRegistro == StringHelper.estadoActivo
                            where document.A009estadoRegistro == StringHelper.estadoActivo
                            select new
                            {
                                document.PkT009codigo,
                                document.A009documento,
                                document.A009url
                            };
            List<AdmintT009Documento> documentos = new List<AdmintT009Documento>();

            foreach (var doc in documents)
            {
                documentos.Add(new AdmintT009Documento
                {
                    PkT009codigo = doc.PkT009codigo,
                    A009documento = doc.A009documento,
                    A009url = doc.A009url
                });
            }

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

            List<SupportDocuments> docSoporte = new List<SupportDocuments>();

            Metodos metodo = new Metodos(_context);


            foreach (var docs in documentos)
            {
                docSoporte.Add(metodo.CargarArchivoFtp(docs, usuraio, clave));
            }
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, "doc");


            return ResponseManager.generaRespuestaGenerica("", docSoporte, token, false);
        }

        /// <summary>
        /// Actualiza documentos soporte eliminados
        /// </summary>
        /// <param name="documentsRemoved"></param>
        [ExcludeFromCodeCoverage]
        public void UpdateDocumentsRemoved(ClaimsIdentity identity, List<SupportDocuments> documentsRemoved, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            foreach (var document in documentsRemoved)
            {
                AdmintT009Documento? documentUpdate = _context.AdmintT009Documentos.FirstOrDefault(item => item.PkT009codigo == document.code);
                if (documentUpdate != null)
                {
                    documentUpdate.A009estadoRegistro = StringHelper.estadoInactivo;
                    documentUpdate.A009fechaModificacion = DateTime.Now;
                    _context.SaveChanges();
                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 4, documentUpdate, documentUpdate.PkT009codigo.ToString());

                    CupostT025FacturaCompraCartaVentaDocumento? saleDocumentsAttachments = _context.CupostT025FacturaCompraCartaVentaDocumento.FirstOrDefault(item => item.A025CodigoDocumento == document.code);

                    if (saleDocumentsAttachments != null)
                    {
                        saleDocumentsAttachments.A025EstadoRegistro = StringHelper.estadoInactivo;
                        saleDocumentsAttachments.A025FechaModificacion = DateTime.Now;
                        _context.SaveChanges();
                        met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 4, saleDocumentsAttachments, saleDocumentsAttachments.Pk_T025Codigo.ToString());
                    }
                }
            }

        }

        /// <summary>
        /// Obtiene los cupos por codigo de documento de venta
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetQuotasByCode(ClaimsIdentity identity, decimal code, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var quotasSaleDocumentQuery = from quotasInvoice in _context.CupostT026FacturaCompraCupo
                                          join quota in _context.CupostT002Cupos
                                          on quotasInvoice.A026CodigoCupo equals quota.PkT002codigo
                                          join specieExport in _context.CupostT005Especieaexportars
                                          on quota.PkT002codigo equals specieExport.A005codigoCupo
                                          where quotasInvoice.A026CodigoFacturaCompra == code
                                          select new
                                          {
                                              Code = quota.PkT002codigo,
                                              NumberResolution = quota.A002numeroResolucion,
                                              QuotasGrant = quota.A002cuposAsignados,
                                              QuotasAdvantageCommercialization = specieExport.A005cupoAprovechamientoOtorgados,
                                              QuotasRePoblation = quota.A002cuotaRepoblacion,
                                              QuotasAvailable = quota.A002cuposDisponibles,
                                              QuotasSold = quotasInvoice.A026CantidadEspecimenesComprados,  //preguntar por campo cupos vendidos
                                              YearProduction = quota.A002fechaProduccion == null ? 0 : decimal.Parse(quota.A002fechaProduccion.Value.Year.ToString()),
                                              SpeciesName = specieExport.A005nombreEspecie,
                                              SpeciesCode = specieExport.A005codigoEspecie,
                                              InitialNumeration = quotasInvoice.A026NumeracionInicial,
                                              FinalNumeration = quotasInvoice.A026NumeracionFinal,
                                              InitialNumerationRePoblation = quotasInvoice.A026NumeracionInicialRepoblacion,
                                              FinalNumerationRePoblation = quotasInvoice.A026NumeracionFinalRepoblacion,
                                              InitialNumerationSeal = quotasInvoice.A026NumeracionInicialPrecintos,
                                              FinalNumerationSeal = quotasInvoice.A026NumeracionFinalPrecintos
                                          };
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Metodos met = new Metodos(_context);
            met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, "doc");

            return ResponseManager.generaRespuestaGenerica("", quotasSaleDocumentQuery, token, false);
        }

        /// <summary>
        /// Actualiza cupos vendidos de la empresa que vende
        /// </summary>
        /// <param name="quota"></param>
        public void UpdateQuotasCompanySells(ClaimsIdentity identity, Quota quota, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            List<string> valorAnterior = new List<string>();
            List<string> valorActual = new List<string>();
            List<string> campos = new List<string>();
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (quota != null)
            {
                CupostT002Cupo? resolutionQuota = _context.CupostT002Cupos.FirstOrDefault(item => item.PkT002codigo == quota.Code);
                if (resolutionQuota != null)
                {


                    if (quota.InitialNumeration != null && quota.FinalNumeration != null)
                    {
                        var nuevoValor = resolutionQuota.A002cuposDisponibles - quota.QuotasSold;
                        met.validarCampoEditadoAud(valorAnterior, valorActual, campos, resolutionQuota.A002cuposDisponibles.ToString(), nuevoValor.ToString(), "A002cuposDisponibles");
                        resolutionQuota.A002cuposDisponibles = nuevoValor;
                    }
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, resolutionQuota.A002fechaModificacion.ToString() ?? "", DateTime.Now.ToString(), "A002fechaModificacion");
                    met.validarCampoEditadoAud(valorAnterior, valorActual, campos, resolutionQuota.A002codigoUsuarioModificacion.ToString() ?? "", codigoUsuario.ToString(), "A002codigoUsuarioModificacion");
                    resolutionQuota.A002fechaModificacion = DateTime.Now;
                    resolutionQuota.A002codigoUsuarioModificacion = Convert.ToDecimal(codigoUsuario);
                    _context.SaveChanges();
                    met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, valorAnterior, valorActual, campos, 3, null, null);
                }
            }
        }

        /// <summary>
        /// Actualiza cupos de inventario
        /// </summary>
        /// <param name="quotasInventory"></param>
        public void UpdateQuotasInventory(ClaimsIdentity identity, List<Inventory> quotasInventory, string ipAddress)
        {
            Metodos met = new Metodos(_context);
            var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (quotasInventory != null)
            {
                List<RangosNumeros> numerosSeleccionados = new List<RangosNumeros>();
                
                foreach (var quota in quotasInventory)
                {
                    var val = numerosSeleccionados.Where(p => p.code == quota.Code).FirstOrDefault();
                    if (val!=null)
                    {
                        for (var i = quota.InitialNumeration; i <= quota.FinalNumeration; i++)
                        {
                            int number = Convert.ToInt32(i);
                            val.numeros.Add(number);
                        }
                    }
                    else
                    {
                        RangosNumeros rango = new RangosNumeros();
                        List<int> numTemp = new List<int>();
                        rango.code = Convert.ToInt32(quota.Code);
                        for (var i = quota.InitialNumeration; i <= quota.FinalNumeration; i++)
                        {
                            int number = Convert.ToInt32(i);
                            numTemp.Add(number);
                        }
                        rango.numeros = numTemp;
                        numerosSeleccionados.Add(rango);
                    }
                }


                foreach (var quota in quotasInventory)
                {
                    CupostT026FacturaCompraCupo? fac = _context.CupostT026FacturaCompraCupo.FirstOrDefault(item => item.Pk_T026Codigo == quota.Code);
                    var selected= numerosSeleccionados.Where(p=>p.code==quota.Code).FirstOrDefault();
                    List<int> numTemp = new List<int>();
                    for (var i = fac.A026NumeracionInicial; i <= fac.A026NumeracionFinal; i++)
                    {
                        if (!selected.numeros.Contains(Convert.ToInt32(i)))
                        {
                            numTemp.Add(Convert.ToInt32(i));
                        }
                    }
                    selected.numerosLibres = numTemp;
                }
                
                if (numerosSeleccionados.Count > 0)
                {
                    foreach (var libre in numerosSeleccionados)
                    {
                        var contador = 0;
                        var contador2 = 0;
                        int[] numeraciones = libre.numerosLibres.ToArray();
                        List<NumeracionesNuevas> numeracionesNuevasTemp = new List<NumeracionesNuevas>();
                        for (var i = 0; i < numeraciones.Length; i++)
                        {
                            if (numeracionesNuevasTemp.Count == 0 || (numeraciones[i] - 1) != contador2)
                            {
                                contador++;
                                NumeracionesNuevas numeracionNueva = new NumeracionesNuevas();
                                numeracionNueva.codigo = contador;
                                List<int> numerosTemp = new List<int>();
                                numerosTemp.Add(numeraciones[i]);
                                numeracionNueva.numeros= numerosTemp;
                                numeracionesNuevasTemp.Add(numeracionNueva);
                                contador2 = numeraciones[i];
                            }
                            else if ((numeraciones[i] - 1) == contador2)
                            {
                                contador2 = numeraciones[i];
                                var numeracionAgregada = numeracionesNuevasTemp.Where(p => p.codigo == contador).FirstOrDefault();
                                numeracionAgregada.numeros.Add(numeraciones[i]);
                            }
                        }
                        libre.numeracionesNuevas = numeracionesNuevasTemp;
                    }
                }

                foreach(var numLibres in numerosSeleccionados)
                {
                    CupostT026FacturaCompraCupo? fac = _context.CupostT026FacturaCompraCupo.FirstOrDefault(item => item.Pk_T026Codigo == numLibres.code);
                    if (fac != null)
                    {
                        foreach(var numNuevas in numLibres.numeracionesNuevas)
                        {
                            Seal numeraciones = new Seal();

                            NumbersSeals numerosSeal = new NumbersSeals();

                            numerosSeal.initial = numNuevas.numeros.Min();
                            numerosSeal.final = numNuevas.numeros.Max();

                            numeraciones = BuscadorPrecintos(numerosSeal);

                            CupostT026FacturaCompraCupo newFac = new CupostT026FacturaCompraCupo();

                            newFac.A026CodigoFacturaCompra = fac.A026CodigoFacturaCompra;
                            newFac.A026CodigoCupo = fac.A026CodigoCupo;
                            newFac.A026NumeracionInicial = numNuevas.numeros.Min();
                            newFac.A026NumeracionFinal = numNuevas.numeros.Max();
                            newFac.A026NumeracionInicialPrecintos = numeraciones.initialNumber;
                            newFac.A026NumeracionFinalPrecintos = numeraciones.finalNumber;
                            newFac.A026CuposDisponibles = numNuevas.numeros.Max() - numNuevas.numeros.Min() + 1;
                            newFac.A026CantidadEspecimenesComprados = numNuevas.numeros.Max() - numNuevas.numeros.Min() + 1;

                            _context.CupostT026FacturaCompraCupo.Add(newFac);
                            _context.SaveChanges();
                            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, newFac, newFac.Pk_T026Codigo.ToString());
                        }
                    }
                }

                foreach (var quota in quotasInventory)
                {
                    CupostT004FacturacompraCartaventum? saleDocument = _context.CupostT004FacturacompraCartaventa.FirstOrDefault(item => item.A004numeroCartaVenta == quota.NumberSaleCarte);
                    decimal saleDocumentSaveId = decimal.Parse(_context.CupostT004FacturacompraCartaventa.OrderByDescending(p => p.PkT004codigo).Select(r => r.PkT004codigo).First().ToString());

                    if (saleDocument != null)
                    {
                        CupostT026FacturaCompraCupo newFacComp = new CupostT026FacturaCompraCupo();

                        newFacComp.A026CodigoFacturaCompra = saleDocumentSaveId;
                        newFacComp.A026CodigoCupo = quota.quotaCode;
                        newFacComp.A026NumeracionInicial = quota.InitialNumeration;
                        newFacComp.A026NumeracionFinal = quota.FinalNumeration;
                        newFacComp.A026NumeracionInicialRepoblacion = quota.InitialNumerationRePoblation;
                        newFacComp.A026NumeracionFinalRepoblacion = quota.FinalNumerationRePoblation;
                        newFacComp.A026NumeracionInicialPrecintos = quota.InitialNumerationSeal;
                        newFacComp.A026NumeracionFinalPrecintos = quota.FinalNumerationSeal;
                        newFacComp.A026CuposDisponibles = quota.InventorySold;
                        newFacComp.A026CantidadEspecimenesComprados = quota.InventorySold;

                        _context.CupostT026FacturaCompraCupo.Add(newFacComp);
                        _context.SaveChanges();

                        met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 2, newFacComp, newFacComp.Pk_T026Codigo.ToString());

                        CupostT026FacturaCompraCupo? fac = _context.CupostT026FacturaCompraCupo.FirstOrDefault(item => item.Pk_T026Codigo == quota.Code);
                        if (fac != null)
                        {
                            met.Auditoria(ipAddress, codigoUsuario, ModuleManager.smCompraVenta, null, null, null, 4, fac, fac.Pk_T026Codigo.ToString());
                            _context.CupostT026FacturaCompraCupo.Remove(fac);
                            _context.SaveChanges();
                        }
                    }

                }
            }
        }

        /// <summary>
        /// buscar precintos
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Responses SearchSeals(ClaimsIdentity identity, NumbersSeals data, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                Seal numeraciones = new Seal();

                numeraciones = BuscadorPrecintos(data);
                var codigoUsuario = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Metodos met = new Metodos(_context);
                met.Auditoria(ipAddress, Convert.ToInt32(codigoUsuario), ModuleManager.smCompraVenta, null, null, null, 1, null, "doc");
                return ResponseManager.generaRespuestaGenerica("", numeraciones, token, false);
            }
            catch (Exception ex)
            {

                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", "", true);
            }

        }

        /// <summary>
        /// buscador precintos
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Seal BuscadorPrecintos(NumbersSeals data)
        {
            Seal numeraciones = new Seal();

            var num = _context.CupostT027NumeracionesSolicitud.Where(p => data.initial == p.A027NumeroInternoInicial && data.final == p.A027NumeroInternoFinal).FirstOrDefault();

            if (num != null)
            {
                numeraciones.initialNumber = num.A027NumeroInicialPrecintos;
                numeraciones.finalNumber = num.A027NumeroFinalPrecintos;
            }
            else
            {
                var numRep = _context.CupostT027NumeracionesSolicitud.Where(p => data.initialRep == p.A027NumeroInternoInicial && data.finalRep == p.A027NumeroInternoFinal).FirstOrDefault();

                if (numRep != null)
                {
                    numeraciones.initialNumber = numRep.A027NumeroInicialPrecintos;
                    numeraciones.finalNumber = numRep.A027NumeroFinalPrecintos;
                }
                else
                {
                    var num1 = _context.CupostT027NumeracionesSolicitud.Where(p => data.initial >= p.A027NumeroInternoInicial && data.final <= p.A027NumeroInternoFinal).FirstOrDefault();

                    if (num1 != null)
                    {
                        var cantidadInicial = data.initial - num1.A027NumeroInternoInicial;
                        var cantidadFinal = num1.A027NumeroInternoFinal - data.final;

                        numeraciones.initialNumber = num1.A027NumeroInicialPrecintos + cantidadInicial;
                        numeraciones.finalNumber = num1.A027NumeroFinalPrecintos - cantidadFinal;
                    }
                    else
                    {
                        var numRep1 = _context.CupostT027NumeracionesSolicitud.Where(p => data.initialRep >= p.A027NumeroInternoInicial && data.finalRep <= p.A027NumeroInternoFinal).FirstOrDefault();
                        if (numRep1 != null)
                        {
                            var cantidadInicial = data.initial - numRep1.A027NumeroInternoInicial;
                            var cantidadFinal = numRep1.A027NumeroInternoFinal - data.final;

                            numeraciones.initialNumber = numRep1.A027NumeroInicialPrecintos + cantidadInicial;
                            numeraciones.finalNumber = numRep1.A027NumeroFinalPrecintos - cantidadFinal;
                        }
                        else
                        {
                            var num2 = _context.CupostT027NumeracionesSolicitud.Where(p => data.initial <= p.A027NumeroInternoInicial && data.final <= p.A027NumeroInternoFinal && data.final >= p.A027NumeroInternoInicial).FirstOrDefault();
                            if (num2 != null)
                            {
                                var cantidadFinal = num2.A027NumeroInternoFinal - data.final;
                                numeraciones.initialNumber = num2.A027NumeroInicialPrecintos;
                                numeraciones.finalNumber = num2.A027NumeroFinalPrecintos - cantidadFinal;
                            }
                            else
                            {
                                var numRep2 = _context.CupostT027NumeracionesSolicitud.Where(p => data.initialRep <= p.A027NumeroInternoInicial && data.finalRep <= p.A027NumeroInternoFinal && data.finalRep >= p.A027NumeroInternoInicial).FirstOrDefault();
                                if (numRep2 != null)
                                {
                                    var cantidadFinal = numRep2.A027NumeroInternoFinal - data.final;

                                    numeraciones.initialNumber = numRep2.A027NumeroInicialPrecintos;
                                    numeraciones.finalNumber = numRep2.A027NumeroFinalPrecintos - cantidadFinal;
                                }
                                else
                                {
                                    var num3 = _context.CupostT027NumeracionesSolicitud.Where(p => data.initial >= p.A027NumeroInternoInicial && data.final >= p.A027NumeroInternoFinal && data.initial <= p.A027NumeroInternoFinal).FirstOrDefault();
                                    if (num3 != null)
                                    {
                                        var cantidadInicial = data.initial - num3.A027NumeroInternoInicial;
                                        numeraciones.initialNumber = num3.A027NumeroInicialPrecintos + cantidadInicial;
                                        numeraciones.finalNumber = num3.A027NumeroFinalPrecintos;
                                    }
                                    else
                                    {
                                        var numRep3 = _context.CupostT027NumeracionesSolicitud.Where(p => data.initialRep >= p.A027NumeroInternoInicial && data.finalRep >= p.A027NumeroInternoFinal && data.initialRep <= p.A027NumeroInternoFinal).FirstOrDefault();
                                        if (numRep3 != null)
                                        {
                                            var cantidadInicial = data.initial - numRep3.A027NumeroInternoInicial;
                                            numeraciones.initialNumber = numRep3.A027NumeroInicialPrecintos + cantidadInicial;
                                            numeraciones.finalNumber = numRep3.A027NumeroFinalPrecintos;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } 

            return numeraciones;
        }
    }
}
