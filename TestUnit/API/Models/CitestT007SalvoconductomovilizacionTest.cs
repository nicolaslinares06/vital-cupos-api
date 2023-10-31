using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT007SalvoconductomovilizacionTest
	{
		[Fact]
		public void CitestT007Salvoconductomovilizacion()
		{
			CitestT007Salvoconductomovilizacion datos = new CitestT007Salvoconductomovilizacion();
			datos.PkT007codigo = 1;
			datos.A007codigoCertificado = 1;
			datos.A007codigoPersonaTransportador = 1;
			datos.A007codigoPersonaSalvoconducto = 1;
			datos.A007codigoParametricaTipoVehiculo = 1;
			datos.A007codigoParametricaTipoRuta = 1;
			datos.A007codigoParametricaMedioTransporte = null!;
			datos.A007codigoUsuarioCreacion = 1;
			datos.A007codigoUsuarioModificacion = 1;
			datos.A007codigoCiudad = 1;
			datos.A007estadoRegistro = 1;
			datos.A007empresaTransportadora = null!;
			datos.A007fechaCreacion = DateTime.Now;
			datos.A007fechaModificacion = DateTime.Now;
			datos.A007fechaVencimientoSalvoconducto = DateTime.Now;
			datos.A007finalidadMovilizacion = null!;
			datos.A007numeroSalvoconducto = 1;
			datos.A007placaVehiculo = null!;
			datos.A007codigoCertificadoNavigation = null!;
			datos.A007codigoCiudadNavigation = null!;
			datos.A007codigoParametricaTipoRutaNavigation = null!;
			datos.A007codigoParametricaTipoVehiculoNavigation = null!;
			datos.A007codigoPersonaSalvoconductoNavigation = null!;
			datos.A007codigoPersonaTransportadorNavigation = null!;

			var type = Assert.IsType<CitestT007Salvoconductomovilizacion>(datos);
			Assert.NotNull(type);
		}
	}
}
