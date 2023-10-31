using API.Helpers;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repository
{
    public class Audit : IAuditoria
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public Audit(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public Responses Consultar(ClaimsIdentity identity, DateTime fechaInicio, DateTime fechaFinal, string ipAddress, int? pagina)
        {
            fechaFinal = fechaFinal.Date + new TimeSpan(23, 59, 59);

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var audi = from aud in _context.AdmintV004Auditoria
                       where aud.fecha >= fechaInicio && aud.fecha <= fechaFinal
                       select new
                       {
                           nombre = aud.usuarioAuditado,
                           accion = aud.accion,
                           fecha = aud.fecha,
                           subModulo = aud.modulo,
                           ip = aud.ip,
                           rol = aud.rol
                       };
            try
            {
                return ResponseManager.generaRespuestaGenerica("", audi.ToList(), token, false);
            }
            catch (Exception)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgCamposIncompletos, "", token, true);
            }

        }

        public Responses ConsultarDetalle(ClaimsIdentity identity, DateTime fecha, string ipAddress)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }
            var auditoria = _context.AdmintT013Auditoria.Select(s => new
            {
                s.A013estadoAnterior,
                s.A013estadoActual,
                s.A013camposModificados,
                s.A013fechaHora,
                s.A013registroModificado
            });
            auditoria = auditoria.Where(s => s.A013fechaHora == fecha);

            return ResponseManager.generaRespuestaGenerica("", auditoria.ToList(), token, false);
        }
    }
}
