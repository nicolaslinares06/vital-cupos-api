using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ActaVisitaCortesModel;

namespace TestUnit.API.Models
{
    public class SaleDocumentModelTest
    {
        [Fact]
        public void SaleDocumentAuditoriaCreation()
        {
            // Arrange
            var saleDocument = new SaleDocumentAuditoria
            {
                PkT004codigo = 1,
                A004codigoUsuarioCreacion = "User123",
                A004codigoUsuarioModificacion = null,
                A004codigoParametricaTipoCartaventa = "Carta123",
                A004codigoEntidadCompra = "EntidadCompra123",
                A004codigoDocumentoSoporte = 2,
                A004codigoDocumentoFactura = 3,
                A004codigoEntidadVende = "EntidadVende123",
                A004fechaCreacion = DateTime.Now,
                A004fechaModificacion = null,
                A004estadoRegistro = 1,
                A004fechaVenta = DateTime.Now,
                A004totalCuposObtenidos = 10,
                A004saldoEntidadVendeInicial = 5,
                A004saldoEntidadVendeFinal = 8,
                A004observacionesCompra = "ObservacionesCompra123",
                A004totalCuposVendidos = 7,
                A004saldoEntidadCompraInicial = 3,
                A004saldoEntidadCompraFinal = 6,
                A004observacionesVenta = "ObservacionesVenta123",
                A004codigoCupo = null,
                A004fechaRegistroCartaVenta = DateTime.Now,
                A004numeroCartaVenta = "CartaVenta123",
                A004disponibilidadInventario = 20,
                A004tipoEspecimenEntidadVende = "EspecimenVende123",
                A004tipoEspecimenEntidadCompra = "EspecimenCompra123",
            };

            var type = Assert.IsType<SaleDocumentAuditoria>(saleDocument);
            Assert.NotNull(type);
        }
    }
}
