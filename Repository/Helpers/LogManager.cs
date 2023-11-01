using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers
{
    public class LogManager
    {
        private readonly DBContext _context;

        private List<string> pvalorAnterior = new List<string>();
        private List<string> pvalorActual = new List<string>();
        private List<string> pcampos = new List<string>();

        public LogManager(DBContext context)
        {
            this._context = context;
        }

        //TransaccionId 1 Consultar, 2 Crear, 3 Editar, 4 Eliminar, 5 Ver Detalle
        internal void crearAuditoria(string ipAddress, string codigoUsuario, int transaccionId, string modulo, object valorAnterior, object valorActual, object campos, object clase, string indicativoRegistro="")
        {
            var now = DateTime.Now; //el campo fecha ayuda a identificar la unicidad de los logs y su detalle

            AdmintT013Auditorium aud = crearDatosComunes(ipAddress, codigoUsuario, transaccionId, modulo, now, indicativoRegistro);

            //Solo creacion y/o eliminacion tengo valor actual con el objeto clase entrante
            if ((clase != null) && ((transaccionId == 2) || (transaccionId == 4)))
            {
                PropertyInfo[]? lst = null;

                if (clase.GetType() == typeof(AdmintT012Usuario)) lst = typeof(AdmintT012Usuario).GetProperties();
                if (clase.GetType() == typeof(AdmintT014RlRolModuloPermiso)) lst = typeof(AdmintT014RlRolModuloPermiso).GetProperties();
                if (clase.GetType() == typeof(AdmintT005Especiman)) lst = typeof(AdmintT005Especiman).GetProperties();
                if (clase.GetType() == typeof(AdmintT015RlUsuarioRol)) lst = typeof(AdmintT015RlUsuarioRol).GetProperties();
                if (clase.GetType() == typeof(AdmintT011Rol)) lst = typeof(AdmintT011Rol).GetProperties();
                if (clase.GetType() == typeof(AdmintT008Parametrica)) lst = typeof(AdmintT008Parametrica).GetProperties();
                if (clase.GetType() == typeof(CitestT008Estado)) lst = typeof(CitestT008Estado).GetProperties();
                if (clase.GetType() == typeof(AdmintT007AdminTecnica)) lst = typeof(AdmintT007AdminTecnica).GetProperties();
                if (clase.GetType() == typeof(AdmintT009Documento)) lst = typeof(AdmintT009Documento).GetProperties();
                if (clase.GetType() == typeof(CupostT005Especieaexportar)) lst = typeof(CupostT005Especieaexportar).GetProperties();
                if (clase.GetType() == typeof(CupostT002Cupo)) lst = typeof(CupostT002Cupo).GetProperties();
                if (clase.GetType() == typeof(CupostT004FacturacompraCartaventum)) lst = typeof(CupostT004FacturacompraCartaventum).GetProperties();
                if (clase.GetType() == typeof(CupostT026FacturaCompraCupo)) lst = typeof(CupostT026FacturaCompraCupo).GetProperties();
                if (clase.GetType() == typeof(CupostT025FacturaCompraCartaVentaDocumento)) lst = typeof(CupostT025FacturaCompraCartaVentaDocumento).GetProperties();
                if (clase.GetType() == typeof(CupostT019Solicitudes)) lst = typeof(CupostT019Solicitudes).GetProperties();
                if (clase.GetType() == typeof(CupostT027NumeracionesSolicitud)) lst = typeof(CupostT027NumeracionesSolicitud).GetProperties();
                if (clase.GetType() == typeof(CupostT020RlSolicitudesDocumento)) lst = typeof(CupostT020RlSolicitudesDocumento).GetProperties();

                if (lst != null)
                {
                    foreach (PropertyInfo oProperty in lst)
                    {
                        string nombreAtributo = oProperty.Name;
                        string valor = oProperty.GetValue(clase)?.ToString() ?? "";

                        if ((valor != "") && (!valor.Contains("System"))) //evito los valores de clase de system que no son atributos reales
                        {
                            AdmintT013Auditorium audAux = crearDatosComunes(ipAddress, codigoUsuario, transaccionId, modulo, now);

                            if (transaccionId == 2) //crear
                            {
                                audAux.A013estadoAnterior = "";
                                audAux.A013estadoActual = valor;
                            }
                            if (transaccionId == 4) //eliminar
                            {
                                audAux.A013estadoAnterior = valor;
                                audAux.A013estadoActual = "";
                            }
                            audAux.A013camposModificados = nombreAtributo;
                            audAux.A013registroModificado = indicativoRegistro;

                            _context.AdmintT013Auditoria.Add(audAux);
                            _ = _context.SaveChanges();
                        }
                        
                    }
                }

                
            }
            else
            {
                if ((valorActual != null) && ((transaccionId == 2) || (transaccionId == 3) || (transaccionId == 4)))
                {
                    string[] camposAux = campos as string[] ??  Array.Empty<string>(); 
                    string[] valorActualAux = valorActual as string[] ??  Array.Empty<string>(); 
                    string[] valorAnteriorAux = valorAnterior as string[] ??  Array.Empty<string>();
                    List<AdmintT013Auditorium> audList = new List<AdmintT013Auditorium>();
                    //debe entrar solo si es 2, 3 o 4
                    if(camposAux != null)
                    {
                        for (int i = 0; i < camposAux.Count(); i++)
                        {
                            var audAux = crearDatosComunes(ipAddress, codigoUsuario, transaccionId, modulo, now);

                            if (transaccionId == 2) //crear
                            {
                                audAux.A013estadoAnterior = "";
                                audAux.A013estadoActual = valorActualAux != null ? valorActualAux[i] : "";
                            }
                            if (transaccionId == 3) //editar
                            {
                                audAux.A013estadoAnterior = valorAnteriorAux != null ? valorAnteriorAux[i] : "";
                                audAux.A013estadoActual = valorActualAux != null ? valorActualAux[i] : "";
                            }
                            if (transaccionId == 4) //eliminar
                            {
                                audAux.A013estadoAnterior = valorAnteriorAux != null ? valorAnteriorAux[i] : "";
                                audAux.A013estadoActual = "";
                            }
                            audAux.A013camposModificados = camposAux[i];
                            audAux.A013registroModificado = indicativoRegistro;

                            audList.Add(audAux);

                        }
                    }

                    foreach(var au in audList)
                    {
                        _context.AdmintT013Auditoria.Add(au);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    _context.AdmintT013Auditoria.Add(aud);
                    _ = _context.SaveChanges();
                }
            }     
        }

        internal AdmintT013Auditorium crearDatosComunes(string ipAddress, string codigoUsuario, int transaccionId, string modulo, DateTime now, string indicativoRegistro="")
        {
            var rol = _context.AdmintT015RlUsuarioRols.Where(a => a.A015codigoUsuario == decimal.Parse(codigoUsuario)).FirstOrDefault();
            var submodulo = _context.AdmintT010Modulos.Where(a => a.A010modulo == modulo).First();
            ipAddress = ipAddress != null ? ipAddress : ":1";

            AdmintT013Auditorium aud = new AdmintT013Auditorium();
            aud.A013codigoUsuarioCreacion = Int32.Parse(codigoUsuario);
            aud.A013fechaCreacion = now;
            aud.A013fechaHora = now;
            aud.A013estadoRegistro = StringHelper.estadoActivo;
            aud.A013codigoUsuarioAuditado = Int32.Parse(codigoUsuario);
            if(rol != null)
            {
                aud.A013codigoRol = rol.A015codigoRol;
            }
            aud.A013codigoModulo = submodulo.PkT010codigo.ToString();
            switch (transaccionId)
            {
                case 1: 

                    if(indicativoRegistro == "doc")
                    {
                        aud.A013accion = "Consultar documentos";
                    }
                    else
                    {
                        aud.A013accion = "Consultar";
                    }   

                    break;
                case 2: aud.A013accion = "Crear"; break;
                case 3: aud.A013accion = "Editar"; break;
                case 4: aud.A013accion = "Eliminar"; break;
                case 5: aud.A013accion = "Ver Detalle"; break;
            }
            aud.A013ip = ipAddress;

            return aud;
        }

        public static void getValues(object Object, ref List<string> valores)
        {
            valores = new List<string>();
            Type myType = Object.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop.GetValue(Object, null);
                if (propValue != null)
                {
                    if (!propValue.ToString().Contains("System.Collections.Generic.HashSet"))
                        valores.Add(propValue.ToString().Trim());
                }
                else
                {
                    valores.Add("");
                }
            }
        }

        public void getPropertyInfo(object Object)
        {
            List<string> valores = new List<string>();
            Type myType = Object.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            /*Antes*/
            foreach (PropertyInfo prop in props)
            {
                if (!prop.PropertyType.Name.Contains("ICollection"))
                    valores.Add(prop.Name);
            }

            pcampos = valores;

            /*Antes*/
            getValues(Object, ref pvalorAnterior);
        }

        public bool getDifferences(object Object)
        {
            /*Despues*/
            getValues(Object, ref pvalorActual);

            for (int i = pvalorActual.Count - 1; i >= 0; i--)
            {
                if (pvalorActual[i] == pvalorAnterior[i])
                {
                    pvalorActual.RemoveAt(i);
                    pvalorAnterior.RemoveAt(i);
                    pcampos.RemoveAt(i);
                }
            }

            /*Si hay diferencias se guarda*/
            return pvalorActual.Count > 0;
        }
    }
}
