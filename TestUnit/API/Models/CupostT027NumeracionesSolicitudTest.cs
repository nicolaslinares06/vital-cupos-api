using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT027NumeracionesSolicitudTest
	{
		[Fact]
		public void CupostT027NumeracionesSolicitud()
		{
			CupostT027NumeracionesSolicitud datos = new CupostT027NumeracionesSolicitud();
			datos.Pk_T027Codigo = 1;
			datos.A027CodigoSolicitud = 1;
			datos.A027CodigoUsuarioCreacion = 1;
			datos.A027CodigoUsuarioModificacion = 1;
			datos.A027FechaCreacion = DateTime.Now;
			datos.A027FechaModificacion = DateTime.Now;
			datos.A027EstadoRegistro = 1;
			datos.A027NumeroInternoInicial = 1;
			datos.A027NumeroInternoFinal = 1;
			datos.A027OrigenSolicitud = 1;
			datos.A027NumeroInicialPrecintos = 1;
			datos.A027NumeroFinalPrecintos = 1;
			datos.A027NumeroInicialMarquillas = 1;
			datos.A027NumeroFinalMarquillas = 1;
			datos.A027CodigoEmpresaOrigenNumeraciones = 1;

			var type = Assert.IsType<CupostT027NumeracionesSolicitud>(datos);
			Assert.NotNull(type);
		}
	}
}
