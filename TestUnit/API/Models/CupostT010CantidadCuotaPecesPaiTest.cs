using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT010CantidadCuotaPecesPaiTest
	{
		[Fact]
		public void CupostT010CantidadCuotaPecesPai()
		{
			CupostT010CantidadCuotaPecesPai datos = new CupostT010CantidadCuotaPecesPai();
			datos.PkT010codigo = 1;
			datos.A010codigoUsuarioCreacion = 1;
			datos.A010codigoUsuarioModificacion = 1;
			datos.A010codigoCuotaPecesPais = 1;
			datos.A0010codigoParametricaRegion = 1;
			datos.A010codigoEspecimen = 1;
			datos.A010estadoRegistro = 1;
			datos.A010fechaCreacion = DateTime.Now;
			datos.A010fechaModificacion = DateTime.Now;
			datos.A010cuota = 1;
			datos.A010total = 1;
			datos.A010codigoCuotaPecesPaisNavigation = null!;

			var type = Assert.IsType<CupostT010CantidadCuotaPecesPai>(datos);
			Assert.NotNull(type);
		}
	}
}
