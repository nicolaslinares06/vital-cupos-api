using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT002SubpartidaarancelariumTest
	{
		[Fact]
		public void CitestT002Subpartidaarancelarium()
		{
			CitestT002Subpartidaarancelarium datos = new CitestT002Subpartidaarancelarium();
			datos.PkT002codigo = 1;
			datos.A002codigoCertificado = 1;
			datos.A002codigoUsuarioCreacion = 1;
			datos.A002codigoUsuarioModificacion = 1;
			datos.A002cantidadTotal = 1;
			datos.A002estadoRegistro = 1;
			datos.A002fechaCreacion = DateTime.Now;
			datos.A002fechaModificacion = DateTime.Now;
			datos.A002subpartidaArancelaria = null!;
			datos.A002descripcionSubpartida = null!;
			datos.A002descripcionProducto = null!;
			datos.A002valorUnitario = 1;
			datos.A002valorFbo = 1;
			datos.A002unidadComercial = null!;
			datos.A002codigoCertificadoNavigation = null!;

			var type = Assert.IsType<CitestT002Subpartidaarancelarium>(datos);
			Assert.NotNull(type);
		}
	}
}
