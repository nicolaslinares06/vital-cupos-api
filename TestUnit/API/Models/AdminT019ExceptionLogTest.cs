    using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class AdminT019ExceptionLogTest
    {
        [Fact]
        public void AdminT019ExceptionLog()
        {
            AdminT019ExceptionLog datos = new AdminT019ExceptionLog();
            datos.PkT019codigo = 1;
            datos.A019Timestamp = DateTime.Now;
            datos.A019Mensaje = "";
            datos.A019Detalles = "";
            datos.A019Fuente = "";
            datos.A019Tipo = "";
            datos.A019StackTrace = "";
            datos.A019Modulo = "";

            var type = Assert.IsType<AdminT019ExceptionLog>(datos);
            Assert.NotNull(type);
        }
    }
}
