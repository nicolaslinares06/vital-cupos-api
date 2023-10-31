using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT004CiudadTest
	{
		[Fact]
		public void AdmintT004Ciudad()
		{
			AdmintT004Ciudad datos = new AdmintT004Ciudad();
			datos.PkT004codigo = 1;
			datos.A004codigoUsuarioCreacion = 1;
			datos.A004codigoUsuarioModificacion = 1;
			datos.A004codigoDepartamento = 1;
			datos.A004estadoRegistro = 1;
			datos.A004fechaCreacion = DateTime.Now;
			datos.A004fechaModificacion = DateTime.Now;
			datos.A004nombre = null!;
			datos.A004codigoDepartamentoNavigation = null!;
			datos.AdmintT001Puertos = new List<AdmintT001Puerto>();
			datos.AdmintT012Usuarios = new List<AdmintT012Usuario>();
			datos.CitestT001CertificadoA001codigoCiudadDestinoNavigations = new List<CitestT001Certificado>();
			datos.CitestT001CertificadoA001codigoCiudadEmbarqueNavigations = new List<CitestT001Certificado>();
            datos.CitestT003Personas = new List<CitestT003Persona>();
			datos.CitestT007Salvoconductomovilizacions = new List<CitestT007Salvoconductomovilizacion>();
			datos.CupostT001Empresas = new List<CupostT001Empresa>();

			var type = Assert.IsType<AdmintT004Ciudad>(datos);
			Assert.NotNull(type);
		}
	}
}
