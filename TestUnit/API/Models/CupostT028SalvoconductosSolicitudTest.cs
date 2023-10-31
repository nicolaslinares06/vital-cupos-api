using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class CupostT028SalvoconductosSolicitudTest
    {
        [Fact]
        public void CupostT028SalvoconductosSolicitud()
        {
            CupostT028SalvoconductosSolicitud datos = new CupostT028SalvoconductosSolicitud();
            datos.Pk_T028Codigo = 1;
            datos.A028CodigoActaVisitaSalvoconducto = 1;
            datos.A028CodigoSolicitud = 1;
            datos.A028CodigoUsuarioCreacion = 1;
            datos.A028CodigoUsuarioModificacion = null;
            datos.A028FechaCreacion = DateTime.Now;
            datos.A028FechaModificacion = DateTime.Now;
            datos.A028EstadoRegistro = 0;

            var type = Assert.IsType<CupostT028SalvoconductosSolicitud>(datos);
            Assert.NotNull(type);
        }
    }
}
