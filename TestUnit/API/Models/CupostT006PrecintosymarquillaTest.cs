using Repository.Models;
using Repository.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT006PrecintosymarquillaTest
	{
		[Fact]
		public void CupostT006Precintosymarquilla()
		{
			CupostT006Precintosymarquilla datos = new CupostT006Precintosymarquilla();
			datos.PkT006codigo = 1;
			datos.A006codigoPrecintoymarquilla = "1";
			datos.A006codigoEspecieExportar = 1;
			datos.A006codigoUsuarioCreacion = 1;
			datos.A006codigoUsuarioModificacion = 1;
			datos.A006codigoParametricaTipoPrecintomarquilla = 1;
			datos.A006codigoParametricaColorPrecintosymarquillas = 1;
			datos.A006estadoRegistro = 1;
			datos.A006fechaCreacion = DateTime.Now;
			datos.A006fechaModificacion = DateTime.Now;
			datos.A006observacion = null!;
			datos.A006numeroInicial = null!;
			datos.A006numeroFinal = null!;
			datos.A006codigoEspecieExportarNavigation = null;

			var type = Assert.IsType<CupostT006Precintosymarquilla>(datos);
			Assert.NotNull(type);
		}
	}
}
