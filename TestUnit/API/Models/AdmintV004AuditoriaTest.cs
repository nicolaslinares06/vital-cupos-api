using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class AdmintV004AuditoriaTest
    {
        [Fact]
        public void AdmintV004Auditoria()
        {
            AdmintV004Auditoria datos = new AdmintV004Auditoria();
            datos.pkT013codigo = 1;
            datos.fecha = DateTime.Now;
            datos.usuarioAuditado = "";
            datos.rol = "";
            datos.modulo = "";
            datos.accion = "";
            datos.ip = "";

            var type = Assert.IsType<AdmintV004Auditoria>(datos);
            Assert.NotNull(type);
        }
    }
}
