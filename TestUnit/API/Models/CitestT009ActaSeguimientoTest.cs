using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT009ActaSeguimientoTest
	{
		[Fact]
		public void CitestT009ActaSeguimiento()
		{
			CitestT009ActaSeguimiento datos = new CitestT009ActaSeguimiento();
			datos.A009codigo = 1;
			datos.A009codigoCertificado = 1;
			datos.A009codigoUsuarioCreacion = 1;
			datos.A009codigoUsuarioModificacion = 1;
			datos.A009codigoUsuarioEvaluador = 1;
			datos.A009fechaModificacion = DateTime.Now;
			datos.A009fechaCreacion = DateTime.Now;
			datos.A009estadoRegistro = 1;
			datos.A009observaciones = null!;
			datos.A009disposicionesFinales = null!;
			datos.A009codigoCertificadoNavigation = null!;

			var type = Assert.IsType<CitestT009ActaSeguimiento>(datos);
			Assert.NotNull(type);
		}
	}
}
