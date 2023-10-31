using API.Helpers;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repository
{
    public class WSConsultInformationQuotasSealsTags
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        /// <summary>
        /// Constructor de la clase WSConsultInformationQuotasSealsTags.
        /// </summary>
        /// <param name="context">El contexto de la base de datos.</param>
        /// <param name="jwtAuthenticationManager">El administrador de autenticación JWT.</param>
        public WSConsultInformationQuotasSealsTags(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
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
        public Responses ConsultInformationQuotasSealsTags(ClaimsIdentity identity, decimal cuposOcupados)
        {
			var token = jwtAuthenticationManager.generarJWT(identity);

			if (token == null)
				return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);

			var result = from cupo in _context.CupostT002Cupos
						 from especie in _context.CupostT005Especieaexportars
						 from especimen in _context.AdmintT005Especimen
						 from empresa in _context.CupostT001Empresas
						 where cupo.A002codigoEmpresa == empresa.PkT001codigo
						 where cupo.PkT002codigo == especie.A005codigoCupo
						 where especie.A005codigoEspecie == especimen.PkT005codigo.ToString()
						 select new
						 {
							 NombreCientifico = especimen.A005nombreCientifico,
							 NombreComun = especimen.A005nombreComun,
							 Cupo = cupo.A002cuposDisponibles,
							 saldo = cupo.A002cuposDisponibles - cuposOcupados,
							 Vigencia = cupo.A002fechaVigencia,
							 NumeroInicialPrecintos = cupo.A002NumeracionInicialPrecintos,
							 NumeroFinalPrecintos = cupo.A002NumeracionFinalPrecintos,
							 NumeroInicialMarquillas = 0,
							 NumeroFinalMarquillas = 0
						 };

			return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, result, token, false);
		}
	}
}
