using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class CuposV005ActaVisitaCortesTest
    {
        [Fact]
        public void CuposV005ActaVisitaCortes()
        {
            CuposV005ActaVisitaCortes datos = new CuposV005ActaVisitaCortes();
            datos.ActaVisitaId = 1;
            datos.TipoActaId = 1;
            datos.VisitaNumero = 1;
            datos.Establecimiento = "Establecimiento";
            datos.TipoEstablecimiento = "TipoEstablecimiento";
            datos.FechaActaVisita = DateTime.Now;
            datos.VisitaUno = true;
            datos.VisitaDos = true;
            datos.EstadoRegistro = 1;
            datos.FechaCreacionDecimal = 1;
            datos.EntidadId = 1;
            datos.TipoEstablecimientoId = 1;

            var type = Assert.IsType<CuposV005ActaVisitaCortes>(datos);
            Assert.NotNull(type);
        }
    }
}
