using Repository.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestUnit.API.Models
{
    public class CupostT003NovedadTest
    {
        [Fact]
        public void CupostT003Novedad()
        {
            CupostT003Novedad datos = new CupostT003Novedad();
            datos.PkT003codigo = 1;
            datos.A003codigoEmpresa = 1;
            datos.A003codigoUsuarioCreacion = 1;
            datos.A003codigoUsuarioModificacion = 1;
            datos.A003codigoParametricaTiponovedad = 1;
            datos.A003codigoParametricaDisposicionEspecimen = 1;
            datos.A003estadoRegistro = 1;
            datos.A003estadoEmpresa = 1;
            datos.A003estadoEmisionPermisosCITES = 1;
            datos.A003fechaRegistroNovedad = DateTime.Now;
            datos.A003fechaCreacion = DateTime.Now;
            datos.A003fechaModificacion = DateTime.Now;
            datos.A003saldoProduccionDisponible = 100;
            datos.A003cuposDisponibles = 50;
            datos.A003inventarioDisponible = 25;
            datos.A003numeroCupospendientesportramitar = 10;
            datos.A003codigoEmpresaTraslado = 2;
            datos.A003observaciones = "Observaciones";
            datos.A003otroCual = "Otro cual";
            datos.A003observacionesDetalle = "Observaciones detalle";

            var type = Assert.IsType<CupostT003Novedad>(datos);
            Assert.NotNull(type);
        }
    }
}