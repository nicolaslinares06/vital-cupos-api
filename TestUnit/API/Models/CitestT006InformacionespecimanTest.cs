using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT006InformacionespecimanTest
	{
		[Fact]
		public void CitestT006Informacionespeciman()
		{
			CitestT006Informacionespeciman datos = new CitestT006Informacionespeciman();
			datos.PkT006codigo = 1;
			datos.A006codigoUsuarioCreacion = 1;
			datos.A006codigoUsuarioModificacion = 1;
			datos.A006codigoCertificado = 1;
			datos.A006codigoEspecimen = "1";
			datos.A006codigoDocumentoProcedencia = 1;
			datos.A006codigoDocumentoPermisoPaisOrigen = 1;
			datos.A006codigoParametricaUnidadMedida = 1;
			datos.A006fechaCreacion = DateTime.Now;
			datos.A006fechaModificacion = DateTime.Now;
			datos.A006estadoRegistro = 1;
			datos.A006fechaProcedencia = DateTime.Now;
			datos.A006sexo = null!;
			datos.A007talla = null!;
			datos.A006observaciones = null!;
			datos.A006cantidad = 1;
			datos.A006cantidadRealExportada = null!;
			datos.A006codigoCertificadoNavigation = null!;
			datos.A006codigoDocumentoPermisoPaisOrigenNavigation = null!;
			datos.A006codigoDocumentoProcedenciaNavigation = null!;

			var type = Assert.IsType<CitestT006Informacionespeciman>(datos);
			Assert.NotNull(type);
		}
	}
}
