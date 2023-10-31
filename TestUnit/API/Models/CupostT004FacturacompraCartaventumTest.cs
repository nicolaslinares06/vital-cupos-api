using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT004FacturacompraCartaventumTest
	{
		[Fact]
		public void CupostT004FacturacompraCartaventum()
		{
			CupostT004FacturacompraCartaventum datos = new CupostT004FacturacompraCartaventum();
			datos.PkT004codigo = 1;
			datos.A004codigoUsuarioCreacion = 1;
			datos.A004codigoUsuarioModificacion = 1;
			datos.A004codigoParametricaTipoCartaventa = 1;
			datos.A004codigoEntidadCompra = 1;
			datos.A004codigoDocumentoSoporte = 1;
			datos.A004codigoDocumentoFactura = 1;
			datos.A004codigoEntidadVende = 1;
			datos.A004fechaCreacion = DateTime.Now;
			datos.A004fechaModificacion = DateTime.Now;
			datos.A004estadoRegistro = 1;
			datos.A004fechaVenta = DateTime.Now;
			datos.A004totalCuposObtenidos = 1;
			datos.A004saldoEntidadVendeInicial = 1;
			datos.A004saldoEntidadVendeFinal = 1;
			datos.A004observacionesCompra = null!;
			datos.A004totalCuposVendidos = 1;
			datos.A004saldoEntidadCompraInicial = 1;
			datos.A004saldoEntidadCompraFinal = 1;
			datos.A004observacionesVenta = null!;
			datos.A004codigoCupo = 1;
			datos.A004fechaRegistroCartaVenta = DateTime.Now;
			datos.A004numeroCartaVenta = "1";
			datos.A004disponibilidadInventario = 1;
			datos.A004tipoEspecimenEntidadVende = "1";
			datos.A004tipoEspecimenEntidadCompra = "1";
			datos.A004codigoEntidadVendeNavigation = null!;
			datos.A004codigoParametricaTipoCartaventaNavigation = null!;

			var type = Assert.IsType<CupostT004FacturacompraCartaventum>(datos);
			Assert.NotNull(type);
		}
	}
}
