using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT001PuertoTest
	{
		[Fact]
		public void AdmintT001Puerto()
		{
			AdmintT001Puerto datos = new AdmintT001Puerto();
			datos.PkTcodigo = 1;
			datos.A001codigoUsuarioCreacion = 1;
			datos.A001codigoUsuarioModificacion = 1;
			datos.A001codigoCiudad = 1;
			datos.A001direccion = null!;
			datos.A001estadoEstrategia = null!;
			datos.A001fechaCreacion = DateTime.Now;
			datos.A001fechaModificacion = DateTime.Now;
			datos.A001modoTransporte = null!;
			datos.A001nombre = null!;
			datos.A001codigoCiudadNavigation = null!;
			datos.CitestT001CertificadoA001codigoPuertoentradaNavigations = new List<CitestT001Certificado>();
			datos.CitestT001CertificadoA001codigoPuertosalidaNavigations = new List<CitestT001Certificado>();
			datos.CitestT003Personas = new List<CitestT003Persona>();

			var type = Assert.IsType<AdmintT001Puerto>(datos);
			Assert.NotNull(type);
		}
	}
}
