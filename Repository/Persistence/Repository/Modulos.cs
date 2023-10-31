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
    public class Modulos : IModulos
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly LogManager logManager;

        public Modulos(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.logManager = new LogManager(context);
        }

        public Responses ConsultarTodos(ClaimsIdentity identity, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var modulos = from r in _context.AdmintT010Modulos
                          select new
                          {
                              id = r.PkT010codigo,
                              name = r.A010descripcion
                          };

            logManager.crearAuditoria(ipAddress, codigoUsuario, 1, ModuleManager.smRolFuncionalidades, "", "", "", "", "");

            return ResponseManager.generaRespuestaGenerica("", modulos, token, false);
        }

        public Responses ConsultarRol(ClaimsIdentity identity, string ipAddress)
        {
            string codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var rol = from r in _context.AdmintV003UsuarioRole
                       where (r.codigoUsuario == Convert.ToDecimal(codigoUsuario))
                       select new
                       {
                           r.codigoRol
                       };

            List<ModulosReq> roles = new List<ModulosReq>();

            if(rol != null)
            {
                foreach (var o in rol)
                {
                    ModulosReq rols = new ModulosReq();

                    var idRol = o.codigoRol;

                    var r = from s in _context.AdmintT011Rols
                            where (s.PkT011codigo == Convert.ToDecimal(idRol))
                            select new
                            {
                                id = s.PkT011codigo,
                                name = s.A011cargo
                            };

                    var firstResult = r.FirstOrDefault();

                    if (firstResult != null)
                    {
                        rols.id = firstResult.id;
                        rols.name = firstResult.name;
                    }

                    roles.Add(rols);
                }
            }
            
            return ResponseManager.generaRespuestaGenerica("", roles, token, false);
        }
    }
}