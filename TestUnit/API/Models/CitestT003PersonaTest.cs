using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT003PersonaTest
	{
		[Fact]
		public void CitestT003Persona()
		{
			CitestT003Persona datos = new CitestT003Persona();
			datos.PkT003codigo = 1;
			datos.A003codigoCiudad = 1;
			datos.A003codigoUsuarioCreacion = 1;
			datos.A003codigoUsuarioModificacion = 1;
			datos.A003codigoPuertoEntrada = 1;
			datos.A003codigoPuertoSalida = 1;
			datos.A003codigoParametricaTipoIdentificacion = 1;
			datos.A003aceptaTerminosycondiciones = true;
			datos.A003aceptaTratamientoDatosPersonales = true;
			datos.A003estadoRegistro = 1;
			datos.A003fechaCreacion = DateTime.Now;
			datos.A003fechaModificacion = DateTime.Now;
			datos.A003nombres = null!;
			datos.A003apellidos = "1";
			datos.A003identificacion = null!;
			datos.A003direccion = null!;
			datos.A003telefono = null!;
			datos.A003correoElectronico = null!;
			datos.A003fax = "1";
			datos.A003segundoNombre = null!;
			datos.A003segundoApellido = null!;
			datos.A003codigoCiudadNavigation = null!;
			datos.A003codigoParametricaTipoIdentificacionNavigation = null!;
			datos.A003codigoPuertoEntradaNavigation = null!;
			datos.CitestT001CertificadoA001codigoPersonaApoderadoNavigations = null!;
			datos.CitestT001CertificadoA001codigoPersonaDestinatarioNavigations = null!;
			datos.CitestT001CertificadoA001codigoPersonaTitularNavigations = null!;
			datos.CitestT007SalvoconductomovilizacionA007codigoPersonaSalvoconductoNavigations = null!;
			datos.CitestT007SalvoconductomovilizacionA007codigoPersonaTransportadorNavigations = null!;

			var type = Assert.IsType<CitestT003Persona>(datos);
			Assert.NotNull(type);
		}
	}
}
