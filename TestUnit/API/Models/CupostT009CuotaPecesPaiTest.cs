using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT009CuotaPecesPaiTest
	{
		[Fact]
		public void CupostT009CuotaPecesPai()
		{
			CupostT009CuotaPecesPai datos = new CupostT009CuotaPecesPai();
			datos.PkT009codigo = 1;
			datos.A009codigoUsuarioCreacion = 1;
			datos.A009codigoUsuarioModificacion = 1;
			datos.A009codigoParametricaTipoMaritimo = 1;
			datos.A009codigoDocumentoSoporte = 1;
			datos.A009estadoRegistro = 1;
			datos.A009fechaCreacion = DateTime.Now;
			datos.A009fechaModificacion = DateTime.Now;
			datos.A009fechaResolucion = DateTime.Now;
			datos.A009fechaVigencia = DateTime.Now;
			datos.A009numeroResolucion = 1;
			datos.A009objetoResolucion = "1";
			datos.A009tipo = "1";
			datos.A009fechaInicialVigencia = DateTime.Now;
			datos.A009fechaFinalVigencia = DateTime.Now;
			datos.CupostT010CantidadCuotaPecesPai = null!;

			var type = Assert.IsType<CupostT009CuotaPecesPai>(datos);
			Assert.NotNull(type);
		}
	}
}
