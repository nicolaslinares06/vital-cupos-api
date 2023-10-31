using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class CupostT005EspecieaexportarTest
    {
        [Fact]
        public void CupostT005Especieaexportar()
        {
            CupostT005Especieaexportar datos = new CupostT005Especieaexportar();
            datos.PkT005codigo = 0;
            datos.A005codigoCupo = 0;
            datos.A005codigoUsuarioCreacion = 0;
            datos.A005codigoUsuarioModificacion = 0;
            datos.A005codigoParametricaTipoMarcaje = "";
            datos.A005codigoEspecie = "";
            datos.A005codigoParametricaPagoCuotaDerepoblacion = null!;
            datos.A005fechaCreacion = DateTime.Now;
            datos.A005fechaModificacion = DateTime.Now;
            datos.A005fechaRadicado = DateTime.Now;
            datos.A005estadoRegistro = null!;
            datos.A005nombreEspecie = null!;
            datos.A005añoProduccion = 0;
            datos.A005marcaLote = null!;
            datos.A005condicionesMarcaje = null!;
            datos.A005poblacionParentalMacho = 0;
            datos.A005poblacionParentalHembra = 0;
            datos.A005poblacionParentalTotal = 0;
            datos.A005poblacionSalioDeIncubadora = 0;
            datos.A005poblacionDisponibleParaCupos = 0;
            datos.A005individuosDestinadosARepoblacion = null!;
            datos.A005cupoAprovechamientoOtorgados = null!;
            datos.A005tasaReposicion = null!;
            datos.A005numeroMortalidad = 0;
            datos.A005cuotaRepoblacionParaAprovechamiento = true;
            datos.A005cupoAprovechamientoOtorgadosPagados = null!;
            datos.A005observaciones = null!;
            datos.A005NumeroInternoInicialCuotaRepoblacion = 0;
            datos.A005NumeroInternoFinalCuotaRepoblacion = 0;
            datos.A005codigoDocumentoSoporte = 0;
            datos.A005codigoCupoNavigation = null!;
            datos.A005codigoUsuarioCreacionNavigation = null!;
            datos.A005codigoUsuarioModificacionNavigation = null!;
            datos.CupostT006Precintosymarquillas = null!;

            var type = Assert.IsType<CupostT005Especieaexportar>(datos);
            Assert.NotNull(type);
        }
    }
}
