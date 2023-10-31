using API.Helpers;
using DocumentFormat.OpenXml.Math;
using Repository.Helpers;
using System.Security.Claims;

namespace Repository.Persistence.Repository
{
	public class WSUpdatingInformationQuotasSealsTags
	{
		private readonly DBContext _context;
		private readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly LogManager logManager;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="jwtAuthenticationManager"></param>
		public WSUpdatingInformationQuotasSealsTags(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
		{
			_context = context;
			this.jwtAuthenticationManager = jwtAuthenticationManager;
			this.logManager = new LogManager(context);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="identity"></param>
		/// <param name="ipAddress"></param>
		/// <param name="nombreCientifico"></param>
		/// <param name="idnombreCientifico"></param>
		/// <param name="nombreComun"></param>
		/// <param name="numeroInicialPrecinto"></param>
		/// <param name="numeroFinalPrecinto"></param>
		/// <param name="numeroInicialMarquilla"></param>
		/// <param name="numeroFinalMarquilla"></param>
		/// <returns></returns>
		public Responses UpdatingInformationQuotasSealsTags(ClaimsIdentity identity, string ipAddress, int nit, int cupos, string nombreCientifico,
			int idnombreCientifico, string nombreComun, decimal? numeroInicialPrecinto, decimal? numeroFinalPrecinto, decimal numeroInicialMarquilla, decimal numeroFinalMarquilla)
		{
			List<string> valorAnterior = new List<string>();
			List<string> valorActual = new List<string>();
			List<string> campos = new List<string>();

			string? codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
			var token = jwtAuthenticationManager.generarJWT(identity);

			if (token == null)
			{
				return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
			}

			var query = _context.WSV001CheckQuotasSealsLabels.FirstOrDefault(x => x.NIT == nit && x.CUPOS == cupos && x.NOMBRECIENTIFICO == nombreCientifico && x.ID == idnombreCientifico &&
			x.NOMBRECOMUN == nombreComun && x.NUMERACIONINICIALPRECINTOS == numeroInicialPrecinto && x.NUMERACIONFINALPRECINTOS == numeroFinalPrecinto &&
			x.NUMERACIONINICIALMARQUILLA == numeroInicialMarquilla && x.NUMERACIONFINALMARQUILLA == numeroFinalMarquilla);

			if(query != null)
			{
                var result = _context.CupostT002Cupos.FirstOrDefault(x => x.PkT002codigo == query.ID);

                if (result != null)
                {

                    if (result.A002cuposAsignados != cupos)
                    {
                        valorAnterior.Add(Convert.ToString(result.A002cuposAsignados) ?? "");
                        valorActual.Add(Convert.ToString(cupos));
                        campos.Add("A002CUPOS_ASIGNADOS");
                    }

                    result.A002cuposAsignados = cupos;

                    if (result.A002NumeracionInicialPrecintos != numeroInicialPrecinto)
                    {
                        valorAnterior.Add(Convert.ToString(result.A002NumeracionInicialPrecintos) ?? "");
                        valorActual.Add(Convert.ToString(numeroInicialPrecinto) ?? "");
                        campos.Add("A002_NUMERACION_INICIAL_PRECINTOS");
                    }

                    result.A002NumeracionInicialPrecintos = numeroInicialPrecinto;

                    if (result.A002NumeracionFinalPrecintos != numeroInicialPrecinto)
                    {
                        valorAnterior.Add(Convert.ToString(result.A002NumeracionFinalPrecintos) ?? "");
                        valorActual.Add(Convert.ToString(numeroFinalPrecinto) ?? "");
                        campos.Add("A002_NUMERACION_FINAL_PRECINTOS");
                    }

                    result.A002NumeracionFinalPrecintos = numeroFinalPrecinto;

                    _context.Update(result);
                    _ = _context.SaveChanges();
                    logManager.crearAuditoria(ipAddress, codigoUsuario, 2, ModuleManager.smParametricas, "", "", "", "", "");
                    _ = _context.SaveChanges();

                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
				}
				else
				{
                    return ResponseManager.generaRespuestaGenerica(StringHelper.msgArchivoNoEncontrado, true, token, false);

                }
            }
			else
			{
				return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoEncontradoEditar, true, token, true);
			}
		}
	}
}
