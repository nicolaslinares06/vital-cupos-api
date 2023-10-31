using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class CupostT014ResolucionTest
    {
        [Fact]
        public void CupostT014Resolucion()
        {
            CupostT014Resolucion datos = new CupostT014Resolucion();
            datos.PkT014codigo = 0;
            datos.A014codigoUsuarioCreacion = 0;
            datos.A014codigoUsuarioModificacion = 0;
            datos.A014codigoDocumentoSoporte = 0;
            datos.A014estadoRegistro = 0;
            datos.A014fechaCreacion = DateTime.Now;
            datos.A014fechaModificacion = DateTime.Now;
            datos.A014fechaResolucion = DateTime.Now;
            datos.A014fechaInicio = DateTime.Now;
            datos.A014fechaFin = DateTime.Now;
            datos.A014numeroResolucion = 0;
            datos.A014objetoResolucion = "";
            datos.A014codigoEmpresa = 0;

            var type = Assert.IsType<CupostT014Resolucion>(datos);
            Assert.NotNull(type);
        }
    }
}
