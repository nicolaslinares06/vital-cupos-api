using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT005RecaudoTest
	{
		[Fact]
		public void AdmintT011Rol()
		{
			CitestT005Recaudo datos = new CitestT005Recaudo();
			datos.PkT005codigo = 1;
			datos.A005codigoDocumentoSoportetransferencia = 1;
			datos.A005codigoParametricaTipodocumento = null!;
			datos.A005codigoParametricaTipoPago = 1;
			datos.A005codigoUsuarioCreacion = 1;
			datos.A005codigoUsuarioModificacion = 1;
			datos.A005codigoCertificado = 1;
			datos.A005banco = null!;
			datos.A005estadoRegistro = 1;
			datos.A005fechaCreacion = DateTime.Now;
			datos.A005fechaModificacion = DateTime.Now;
			datos.A005fechaConsignacion = DateTime.Now;
			datos.A005numeroCuenta = 1;
			datos.A005valor = 1;
			datos.A005observaciones = null!;
			datos.A005codigoCertificadoNavigation = null!;
			datos.A005codigoDocumentoSoportetransferenciaNavigation = null!;
			datos.A005codigoParametricaTipoPagoNavigation = null!;

			var type = Assert.IsType<CitestT005Recaudo>(datos);
			Assert.NotNull(type);
		}
	}
}
