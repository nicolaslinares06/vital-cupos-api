using API.Helpers;
using Repository.Helpers;
using System.Security.Claims;

namespace Repository.Persistence.Repository
{
    public class WSCheckQuotasSealsLabels : IWSCheckQuotasSealsLabels
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jwtAuthenticationManager"></param>
        public WSCheckQuotasSealsLabels(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="ipAddress"></param>
        /// <param name="nit"></param>
        /// <returns></returns>
        public Responses ConsultCheckQuotasSealsLabels(ClaimsIdentity identity, string ipAddress, int nit)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);

            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var query = from cqsl in _context.WSV001CheckQuotasSealsLabels
                        select cqsl;

            query = query.Where(x => x.NIT == nit);

            return ResponseManager.generaRespuestaGenerica("", query, token, false);
        }
    }
}
