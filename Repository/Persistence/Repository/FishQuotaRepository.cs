using API.Helpers;
using iTextSharp.text.pdf.codec.wmf;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Persistence.Repository
{
    public class FishQuotaRepository : IFishQuotaRepository
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public FishQuotaRepository(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// Obtiene cuotas para peces por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetFishQuotaByCode(ClaimsIdentity identity, decimal code)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = from fishQuota in _context.CupostT009CuotaPecesPais
                        join fishQuotaAmount in _context.CupostT010CantidadCuotaPecesPais
                        on fishQuota.PkT009codigo equals fishQuotaAmount.A010codigoCuotaPecesPais
                        join specie in _context.AdmintT005Especimen
                        on fishQuotaAmount.A010codigoEspecimen equals specie.PkT005codigo
                        where fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                        && fishQuotaAmount.A010estadoRegistro == StringHelper.estadoActivo
                        && fishQuota.PkT009codigo == code
                        select new
                        {
                            Code = fishQuota.PkT009codigo,
                            NumberResolution = fishQuota.A009numeroResolucion,
                            ResolutionDate = fishQuota.A009fechaResolucion,
                            InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                            FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                            ResolutionObject = fishQuota.A009objetoResolucion,
                            Type = fishQuota.A009tipo,
                            CodeFishQuotaAmount = fishQuotaAmount.PkT010codigo,
                            GroupName = specie.A005familia,
                            Group = fishQuotaAmount.A010codigoEspecimen,
                            SpeciesCode = fishQuotaAmount.A010codigoEspecimen, //Especiment.nombreComun
                            SpeciesName = specie.A005nombreCientifico,
                            speciesNameComun = specie.A005nombreComun,
                            Quota = fishQuotaAmount.A010cuota,
                            Total = fishQuotaAmount.A010total,
                            Region = fishQuotaAmount.A0010codigoParametricaRegion
                        };

            return ResponseManager.generaRespuestaGenerica("Su Busqueda se realizo correctamente", query, token, false);
        }

        /// <summary>
        /// Obtiene cuotas para peces
        /// </summary>
        /// <param name="numberResolution"></param>
        /// <param name="vigency"></param>
        /// <returns></returns>
        public Responses GetFishesQuotas(ClaimsIdentity identity, string? initialValidityDate, string? finalValidityDate, decimal numberResolution = 0)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var query = new Object();
            if (numberResolution == 0 && initialValidityDate == null && finalValidityDate == null)
            {

                query = from fishQuota in _context.CupostT009CuotaPecesPais
                        where fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                        select new
                        {
                            Code = fishQuota.PkT009codigo,
                            NumberResolution = fishQuota.A009numeroResolucion,
                            ResolutionDate = fishQuota.A009fechaResolucion,
                            InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                            FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                            ResolutionObject = fishQuota.A009objetoResolucion
                        };
            }
            else
            {
                DateTime initialDate = new DateTime();
                DateTime finalDate = new DateTime();

                if (initialValidityDate != null)
                {
                    string[] initial = initialValidityDate.Split("/");
                    string[] yearHour = initial[2].Split(" ");
                    initialDate = new DateTime(Int32.Parse(yearHour[0]), Int32.Parse(initial[1]), Int32.Parse(initial[0]));

                }

                if (finalValidityDate != null)
                {
                    string[] final = finalValidityDate.Split("/");
                    string[] yearHourFinal = final[2].Split(" ");
                    finalDate = new DateTime(Int32.Parse(yearHourFinal[0]), Int32.Parse(final[1]), Int32.Parse(final[0]));
                }

                if (numberResolution != 0 && initialValidityDate != null && finalValidityDate != null)
                {
                    query = from fishQuota in _context.CupostT009CuotaPecesPais
                            where (fishQuota.A009numeroResolucion == numberResolution)
                            && (fishQuota.A009fechaInicialVigencia >= initialDate && fishQuota.A009fechaFinalVigencia <= finalDate)
                            && fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                            select new
                            {
                                Code = fishQuota.PkT009codigo,
                                NumberResolution = fishQuota.A009numeroResolucion,
                                ResolutionDate = fishQuota.A009fechaResolucion,
                                InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                                FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                                ResolutionObject = fishQuota.A009objetoResolucion
                            };
                }
                else
                {
                    if (numberResolution != 0 && initialValidityDate == null && finalValidityDate == null)
                    {
                        query = from fishQuota in _context.CupostT009CuotaPecesPais
                                where (fishQuota.A009numeroResolucion == numberResolution)
                                && fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                                select new
                                {
                                    Code = fishQuota.PkT009codigo,
                                    NumberResolution = fishQuota.A009numeroResolucion,
                                    ResolutionDate = fishQuota.A009fechaResolucion,
                                    InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                                    FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                                    ResolutionObject = fishQuota.A009objetoResolucion
                                };
                    }
                    else if(numberResolution == 0 && initialValidityDate != null && finalValidityDate == null)
                    {
                        query = from fishQuota in _context.CupostT009CuotaPecesPais
                                where (fishQuota.A009fechaInicialVigencia >= initialDate)
                                && fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                                select new
                                {
                                    Code = fishQuota.PkT009codigo,
                                    NumberResolution = fishQuota.A009numeroResolucion,
                                    ResolutionDate = fishQuota.A009fechaResolucion,
                                    InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                                    FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                                    ResolutionObject = fishQuota.A009objetoResolucion
                                };
                    }
                    else if (numberResolution != 0 && initialValidityDate != null && finalValidityDate == null)
                    {
                        query = from fishQuota in _context.CupostT009CuotaPecesPais
                                where (fishQuota.A009fechaInicialVigencia >= initialDate && fishQuota.A009numeroResolucion == numberResolution)
                                && fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                                select new
                                {
                                    Code = fishQuota.PkT009codigo,
                                    NumberResolution = fishQuota.A009numeroResolucion,
                                    ResolutionDate = fishQuota.A009fechaResolucion,
                                    InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                                    FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                                    ResolutionObject = fishQuota.A009objetoResolucion
                                };
                    }
                    else
                    {
                        query = from fishQuota in _context.CupostT009CuotaPecesPais
                                where (fishQuota.A009fechaInicialVigencia >= initialDate && fishQuota.A009fechaFinalVigencia <= finalDate)
                                && fishQuota.A009estadoRegistro == StringHelper.estadoActivo
                                select new
                                {
                                    Code = fishQuota.PkT009codigo,
                                    NumberResolution = fishQuota.A009numeroResolucion,
                                    ResolutionDate = fishQuota.A009fechaResolucion,
                                    InitialValidityDate = fishQuota.A009fechaInicialVigencia,
                                    FinalValidityDate = fishQuota.A009fechaFinalVigencia,
                                    ResolutionObject = fishQuota.A009objetoResolucion
                                };
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("Consulta exitosa", query, token, false);
        }

        /// <summary>
        /// Guarda cuotas para peces
        /// </summary>
        /// <param name="fishQuota"></param>
        /// <returns></returns>
        public Responses SaveFishQuota(ClaimsIdentity identity, FishQuota fishQuota)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                if (fishQuota == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No se pudo guardar, error fishQuota object null", "", "", true);
                }
                else
                {
                    _context.CupostT009CuotaPecesPais.Add(
                                new CupostT009CuotaPecesPai
                                {
                                    A009tipo = fishQuota.Type,
                                    A009numeroResolucion = fishQuota.NumberResolution,
                                    A009fechaInicialVigencia = fishQuota.InitialValidityDate,
                                    A009fechaFinalVigencia = fishQuota.FinalValidityDate,
                                    A009fechaResolucion = fishQuota.ResolutionDate,
                                    A009objetoResolucion = fishQuota.ResolutionObject,
                                    A009codigoDocumentoSoporte = fishQuota.Document,
                                    A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                                    A009codigoParametricaTipoMaritimo = 0,
                                    A009estadoRegistro = StringHelper.estadoActivo,
                                    A009fechaCreacion = DateTime.Now
                                }
                    );

                    if(fishQuota.FishQuotaAmounts != null)
                    {
                        _context.SaveChanges();
                        SaveFishQuotaAmount(identity, fishQuota.FishQuotaAmounts, false);
                        ValidateDocumentAction(identity, fishQuota.SupportDocuments, false);

                        return ResponseManager.generaRespuestaGenerica("Se guardo correctamente", "", token, false);
                    }

                    return ResponseManager.generaRespuestaGenerica("", "", token, false);
                }

            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", token, true);
            }
        }

        /// <summary>
        /// Valida la accion de documentos adjuntos para guardar, actualizar o eliminar el archivo
        /// </summary>
        /// <param name="supportDocuments"></param>
        /// <returns></returns>
        public Responses ValidateDocumentAction(ClaimsIdentity identity, List<SupportDocuments> supportDocuments, bool actionEdit, decimal code = 0, List<SupportDocuments>? supportDocumentsRemoved = null)
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
                            SaveDocuments(identity, document);
                        }
                    }
                    else
                    {
                        if (supportDocumentsRemoved != null)
                        {
                            UpdateDocumentsRemoved(identity, supportDocumentsRemoved);
                        }

                        foreach (var document in supportDocuments)
                        {
                            if (document.tempAction == "add")
                            {
                                SaveDocuments(identity, document);
                            }
                            else
                            {
                                UpdateDocument(identity, document, code);
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

                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", token, true);
            }
        }

        /// <summary>
        /// Guarda el documento soporte
        /// </summary>
        /// <param name="supportDocuments"></param>
        public void SaveDocuments(ClaimsIdentity identity, SupportDocuments supportDocuments)
        {
            decimal fishQuotaSaveId = decimal.Parse(_context.CupostT009CuotaPecesPais.OrderByDescending(p => p.PkT009codigo).Select(r => r.PkT009codigo).First().ToString());

            Metodos metodo = new Metodos(_context);

            var uri = metodo.GuardarArchivoFtp(supportDocuments);

            AdmintT009Documento docNuevo = new AdmintT009Documento();
            docNuevo.A009fechaCreacion = DateTime.Now;
            docNuevo.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
            docNuevo.A009codigoParametricaTipoDocumento = 25;
            docNuevo.A009firmaDigital = "firma";
            docNuevo.A009codigoPlantilla = 1;
            docNuevo.A009documento = supportDocuments.attachmentName ?? "";
            docNuevo.A009descripcion = supportDocuments.attachmentName ?? "";
            docNuevo.A009url = uri;

            _context.AdmintT009Documentos.Add(docNuevo);
            _context.SaveChanges();

            CupostT024RlCuotaPecesPaisDocumento cuotaPecesPaisDocumento = new CupostT024RlCuotaPecesPaisDocumento();
            cuotaPecesPaisDocumento.A024CodigoDocumento = docNuevo.PkT009codigo;
            cuotaPecesPaisDocumento.A024CodigoCuotaPecesPais = fishQuotaSaveId;
            cuotaPecesPaisDocumento.A024FechaCreacion = DateTime.Now;
            cuotaPecesPaisDocumento.A024UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            cuotaPecesPaisDocumento.A024EstadoRegistro = StringHelper.estadoActivo;

            _context.CupostT024RlCuotaPecesPaisDocumento.Add(cuotaPecesPaisDocumento);
            _context.SaveChanges();
        }

        /// <summary>
        ///Actualiza el supportDocuments soporte
        /// </summary>
        /// <param name="document"></param>
        /// <param name="code"></param>
        public void UpdateDocument(ClaimsIdentity identity, SupportDocuments document, decimal code = 0)
        {
            
            string uri = _context.AdmintT009Documentos.Where(d => d.A009documento == document.attachmentName && d.A009estadoRegistro == StringHelper.estadoActivo).Select(d => d.A009url).FirstOrDefault() ?? "";

            AdmintT009Documento docNuevo = new AdmintT009Documento();
            docNuevo.A009fechaCreacion = DateTime.Now;
            docNuevo.A009fechaModificacion = DateTime.Now;
            docNuevo.A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            docNuevo.A009estadoRegistro = StringHelper.estadoActivo;
            docNuevo.A009codigoParametricaTipoDocumento = 25;
            docNuevo.A009firmaDigital = "firma";
            docNuevo.A009codigoPlantilla = 1;
            docNuevo.A009documento = document.attachmentName ?? "";
            docNuevo.A009descripcion = document.attachmentName ?? "";
            docNuevo.A009url = uri;
            docNuevo.PkT009codigo = document.code;

            _context.AdmintT009Documentos.Update(docNuevo);
            _context.SaveChanges();

            decimal codeFishQuotaDocument = _context.CupostT024RlCuotaPecesPaisDocumento.Where(d => d.A024CodigoDocumento == document.code && d.A024EstadoRegistro == StringHelper.estadoActivo).Select(d => d.Pk_T024Codigo).FirstOrDefault();

            CupostT024RlCuotaPecesPaisDocumento cuotaPecesPaisDocumento = new CupostT024RlCuotaPecesPaisDocumento();
            cuotaPecesPaisDocumento.A024CodigoDocumento = docNuevo.PkT009codigo;
            cuotaPecesPaisDocumento.A024CodigoCuotaPecesPais = code;
            cuotaPecesPaisDocumento.A024FechaCreacion = DateTime.Now;
            cuotaPecesPaisDocumento.A024FechaModificacion = DateTime.Now;
            cuotaPecesPaisDocumento.A024UsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            cuotaPecesPaisDocumento.A024EstadoRegistro = StringHelper.estadoActivo;
            cuotaPecesPaisDocumento.Pk_T024Codigo = codeFishQuotaDocument;

            _context.CupostT024RlCuotaPecesPaisDocumento.Update(cuotaPecesPaisDocumento);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina cuotas para peces por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Responses DeleteFishQuota(ClaimsIdentity identity, int id)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var qry = _context.CupostT009CuotaPecesPais.Where(p => p.PkT009codigo == id && p.A009estadoRegistro == StringHelper.estadoActivo);
            foreach (var fishQuota in qry)
            {
                fishQuota.A009codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                fishQuota.A009fechaModificacion = DateTime.Now;
                fishQuota.A009estadoRegistro = StringHelper.estadoInactivo; 
            }
            _context.SaveChanges();
            return ResponseManager.generaRespuestaGenerica("Se elimino correctamente", "", token, false);
        }


        /// <summary>
        /// Actualiza cuotas para peces
        /// </summary>
        /// <param name="fishQuota"></param>
        /// <returns></returns>
        public Responses UpdateFishQuota(ClaimsIdentity identity, FishQuota fishQuota)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                if (fishQuota.Code != 0)
                {
                    _context.Update(new CupostT009CuotaPecesPai
                    {
                        PkT009codigo = fishQuota.Code,
                        A009tipo = fishQuota.Type,
                        A009numeroResolucion = fishQuota.NumberResolution,
                        A009fechaInicialVigencia = fishQuota.InitialValidityDate,
                        A009fechaFinalVigencia = fishQuota.FinalValidityDate,
                        A009fechaResolucion = fishQuota.ResolutionDate,
                        A009objetoResolucion = fishQuota.ResolutionObject,
                        A009codigoDocumentoSoporte = fishQuota.Document,
                        A009codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        A009codigoParametricaTipoMaritimo = 0,
                        A009estadoRegistro = StringHelper.estadoActivo,
                        A009fechaCreacion = DateTime.Now,
                        A009fechaModificacion = DateTime.Now
                    });

                    if(fishQuota.FishQuotaAmounts != null && fishQuota.FishQuotaAmountsRemoved != null)
                    {
                        _context.SaveChanges();
                        UpdateFishQuotaAmount(identity, fishQuota.Code, fishQuota.FishQuotaAmounts, fishQuota.FishQuotaAmountsRemoved);
                        ValidateDocumentAction(identity, fishQuota.SupportDocuments, true, fishQuota.Code, fishQuota.SupportDocumentsRemoved);
                        return ResponseManager.generaRespuestaGenerica("La información se guardo con éxito", "", token, false);
                    }

                    return ResponseManager.generaRespuestaGenerica("La información no se guardo con éxito", "", token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica("Error UpdateFishQuota id null", "", token, true);
                }
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message, "", token, true);
            }
        }

        /// <summary>
        /// Guarda cuotas para peces
        /// </summary>
        /// <param name="fishQuota"></param>
        /// <returns></returns>
        public Responses SaveFishQuotaAmount(ClaimsIdentity identity, List<FishQuotaAmount> fishQuotas, bool actionEdit, decimal code = 0)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                if (fishQuotas == null)
                {
                    return ResponseManager.generaRespuestaGenerica("No se pudo guardar, error fishQuota object null", "", token, true);
                }
                else
                {
                    decimal fishQuotaSaveId = 0;
                    if (!actionEdit)
                    {
                        fishQuotaSaveId = decimal.Parse(_context.CupostT009CuotaPecesPais.OrderByDescending(p => p.PkT009codigo).Select(r => r.PkT009codigo).First().ToString());
                    }

                    foreach (var fishQuota in fishQuotas)
                    {
                        if (fishQuota.ActionTemp == 1)
                        {
                            var specie = _context.AdmintT005Especimen.Where(e => e.A005nombreComun == fishQuota.speciesNameComun).FirstOrDefault();
                            if (specie == null)
                            {
                                UpdateSpeciesName(identity, fishQuota.SpeciesCode, fishQuota.speciesNameComun != null ? fishQuota.speciesNameComun : "");
                            }

                            var region = _context.AdmintT008Parametricas.Where(a => a.A008valor == fishQuota.NameRegion).FirstOrDefault();

                            _context.CupostT010CantidadCuotaPecesPais.Add(
                                        new CupostT010CantidadCuotaPecesPai
                                        {
                                            A010codigoEspecimen = fishQuota.SpeciesCode == 0 ? fishQuota.Group : fishQuota.SpeciesCode,
                                            A010cuota = fishQuota.Quota,
                                            A010total = fishQuota.Total,
                                            A010codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                                            A010codigoCuotaPecesPais = !actionEdit ? fishQuotaSaveId : code,
                                            A0010codigoParametricaRegion = region == null ? 0 : region.PkT008codigo,
                                            A010estadoRegistro = StringHelper.estadoActivo,
                                            A010fechaCreacion = DateTime.Now
                                        }
                            );

                            _context.SaveChanges();
                        }
                    }
                    return ResponseManager.generaRespuestaGenerica("Se guardo correctamente", "", token, false);
                }

            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", token, true);
            }
        }

        /// <summary>
        /// Actualiza cantidad cuotas para peces
        /// </summary>
        /// <param name="fishQuota"></param>
        /// <returns></returns>
        public Responses UpdateFishQuotaAmount(ClaimsIdentity identity, decimal code, List<FishQuotaAmount> fishQuotasAmount, List<FishQuotaAmount> fishQuotaAmountsRemoved)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            try
            {
                if (fishQuotaAmountsRemoved != null)
                {
                    UpdateFishQuotaAmountRemoved(identity, code, fishQuotaAmountsRemoved);
                }

                if (fishQuotasAmount != null)
                {
                    foreach (var fishQuota in fishQuotasAmount)
                    {
                        if (fishQuota.ActionTemp == 0)
                        {
                            var region = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == fishQuota.Region).FirstOrDefault();

                            if (fishQuota.CodeFishQuotaAmount != 0)
                            {
                                _context.Update(new CupostT010CantidadCuotaPecesPai
                                {
                                    PkT010codigo = fishQuota.CodeFishQuotaAmount,
                                    A010codigoEspecimen = fishQuota.SpeciesCode == 0 ? fishQuota.Group : fishQuota.SpeciesCode,
                                    A010cuota = fishQuota.Quota,
                                    A010total = fishQuota.Total,
                                    A010codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                                    A010codigoCuotaPecesPais = code,
                                    A0010codigoParametricaRegion = region == null ? 0 : region.PkT008codigo,
                                    A010estadoRegistro = StringHelper.estadoActivo,
                                    A010fechaCreacion = DateTime.Now,
                                    A010fechaModificacion = DateTime.Now
                                });
                                _context.SaveChanges();
                            }
                        }
                    }
                    SaveFishQuotaAmount(identity, fishQuotasAmount, true, code);
                    return ResponseManager.generaRespuestaGenerica("La información se guardo con éxito", "", token, false);
                }
                else
                {
                    return ResponseManager.generaRespuestaGenerica("Error UpdateFishQuota null", "", token, true);
                }
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message, "", token, true);
            }
        }

        /// <summary>
        /// Obtiene especies
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
                                id = species.PkT005codigo,
                                NameFamily = species.A005familia,
                                text = species.A005nombreCientifico,
                                CommonName = species.A005nombreComun
                            };
                return ResponseManager.generaRespuestaGenerica("", query, token, false);
            }
            catch (Exception ex)
            {
                return ResponseManager.generaRespuestaGenerica(ex.Message.ToString(), "", token, true);
            }
        }

        /// <summary>
        /// Actualiza cantidad cuota para peces eliminados
        /// </summary>
        /// <param name="code"></param>
        /// <param name="fishQuotaAmountsRemoved"></param>
        public void UpdateFishQuotaAmountRemoved(ClaimsIdentity identity, decimal code, List<FishQuotaAmount> fishQuotaAmountsRemoved)
        {
            if (fishQuotaAmountsRemoved != null)
            {
                foreach (var fishQuotaAmount in fishQuotaAmountsRemoved)
                {
                    if (fishQuotaAmount.CodeFishQuotaAmount != 0)
                    {
                        var region = _context.AdmintT008Parametricas.Where(p => p.PkT008codigo == fishQuotaAmount.Region).FirstOrDefault();

                        _context.Update(new CupostT010CantidadCuotaPecesPai
                        {
                            PkT010codigo = fishQuotaAmount.CodeFishQuotaAmount,
                            //A010codigoGrupo = fishQuota.CodeGrupe, QUEMADO codigo grupo
                            A010codigoEspecimen = fishQuotaAmount.SpeciesCode,
                            A010cuota = fishQuotaAmount.Quota,
                            A010total = fishQuotaAmount.Total,
                            A010codigoUsuarioCreacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                            A010codigoCuotaPecesPais = code,
                            A0010codigoParametricaRegion = region == null ? 0 : region.PkT008codigo,
                            A010estadoRegistro = StringHelper.estadoInactivo,
                            A010fechaCreacion = DateTime.Now,
                            A010fechaModificacion = DateTime.Now
                        });
                        _context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el documento soporte
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetSupportDocument(ClaimsIdentity identity, decimal code)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }
            var documents = from fishQuotaDocuments in _context.CupostT024RlCuotaPecesPaisDocumento
                            join fishQuota in _context.CupostT009CuotaPecesPais
                            on fishQuotaDocuments.A024CodigoCuotaPecesPais equals fishQuota.PkT009codigo
                            join document in _context.AdmintT009Documentos
                            on fishQuotaDocuments.A024CodigoDocumento equals document.PkT009codigo
                            where fishQuota.PkT009codigo == code
                            where fishQuota.A009estadoRegistro == StringHelper.estadoActivo
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

            return ResponseManager.generaRespuestaGenerica("", docSoporte, token, false);
        }

        /// <summary>
        /// Actualiza documentos soporte eliminados
        /// </summary>
        /// <param name="documentsRemoved"></param>
        public void UpdateDocumentsRemoved(ClaimsIdentity identity, List<SupportDocuments> documentsRemoved)
        {
            if (documentsRemoved != null)
            {
                var documentCodes = documentsRemoved.Select(document => document.code).ToList();

                var documentUpdates = _context.AdmintT009Documentos.Where(item => documentCodes.Contains(item.PkT009codigo)).ToList();

                foreach (var documentUpdate in documentUpdates)
                {
                    documentUpdate.A009codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    documentUpdate.A009estadoRegistro = StringHelper.estadoInactivo;
                    documentUpdate.A009fechaModificacion = DateTime.Now;

                    CupostT024RlCuotaPecesPaisDocumento? fishQuotaCountryDocument = _context.CupostT024RlCuotaPecesPaisDocumento.FirstOrDefault(item => item.A024CodigoDocumento == documentUpdate.PkT009codigo);

                    if (fishQuotaCountryDocument != null)
                    {
                        fishQuotaCountryDocument.A024UsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        fishQuotaCountryDocument.A024EstadoRegistro = StringHelper.estadoInactivo;
                        fishQuotaCountryDocument.A024FechaModificacion = DateTime.Now;
                    }
                }

                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza el nombre de la especie
        /// </summary>
        /// <param name="speciesCode"></param>
        /// <param name="speciesName"></param>
        public void UpdateSpeciesName(ClaimsIdentity identity, decimal speciesCode, string speciesName)
        {
            if (speciesCode != 0 && speciesName != null)
            {
                AdmintT005Especiman? specie = _context.AdmintT005Especimen.FirstOrDefault(item => item.PkT005codigo == speciesCode);
                if (specie != null)
                {
                    specie.A005fechaModificacion = DateTime.Now;
                    specie.A005codigoUsuarioModificacion = Convert.ToDecimal(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    specie.A005nombre = speciesName;
                    specie.A005nombreComun = speciesName;
                    _context.SaveChanges(true);
                }
            }
        }
    }
}
