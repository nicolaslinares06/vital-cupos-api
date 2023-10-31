using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class CupostT029CortesPielSolicitudTest
    {
        [Fact]
        public void CupostT029CortesPielSolicitud()
        {
            CupostT029CortesPielSolicitud datos = new CupostT029CortesPielSolicitud();
            datos.Pk_T029Codigo = 1;
            datos.A029CodigoCortePiel = 1;
            datos.A029CodigoSolicitud = 1;
            datos.A029CodigoUsuarioCreacion = 1;
            datos.A029Cantidad = 1;
            datos.A029AreaTotal = 1;
            datos.A029CodigoUsuarioModificacion = 1;
            datos.A029FechaCreacion = DateTime.Now;
            datos.A029FechaModificacion = DateTime.Now;
            datos.A029EstadoRegistro = 1;

            var type = Assert.IsType<CupostT029CortesPielSolicitud>(datos);
            Assert.NotNull(type);
        }
    }
}
