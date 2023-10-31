using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class CuposV004NumeracionesPrecintosTest
    {
        [Fact]
        public void CuposV004NumeracionesPrecintos()
        {
            CuposV004NumeracionesPrecintos datos = new CuposV004NumeracionesPrecintos();
            datos.ID = 1;
            datos.IDNUMERACION = 1;
            datos.NUMEROINTERNOINICIAL = 1;
            datos.NUMEROINTERNOFINAL = 1;
            datos.A027CODIGO_EMPRESA_ORIGEN_NUMERACIONES = 1;
            datos.A027CODIGO_PARAMETRICA_ORIGEN_SOLICITUD = 1;

            var type = Assert.IsType<CuposV004NumeracionesPrecintos>(datos);
            Assert.NotNull(type);
        }
    }
}
