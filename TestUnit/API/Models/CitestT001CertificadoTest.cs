using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CitestT001CertificadoTest
	{
		[Fact]
		public void CitestT001Certificado()
		{
			CitestT001Certificado datos = new CitestT001Certificado();
			datos.PkT001codigo = 1;
			datos.A001codigoDocumentoActoadministrativoProcedencia = 1;
			datos.A001codigoEmpresa = 1;
			datos.A001codigoUsuarioCreacion = 1;
			datos.A001codigoUsuarioModificacion = 1;
			datos.A001codigoCiudadDestino = 1;
			datos.A001codigoCiudadEmbarque = 1;
			datos.A001codigoUsuarioEvaluadorpuerto = 1;
			datos.A001codigoEntidadExportador = 1;
			datos.A001codigoParametricaTipoEmbarque = 1;
			datos.A001codigoParametricaTipoPermiso = 1;
			datos.A001codigoParametricaTipoSolictud = 1;
			datos.A001codigoPaisDestino = 1;
			datos.A001codigoPersonaDestinatario = 1;
			datos.A001codigoPersonaApoderado = 1;
			datos.A001codigoPersonaTitular = 1;
			datos.A001codigoPuertoentrada = 1;
			datos.A001codigoPuertosalida = 1;
			datos.A001codigoPuertosalidaOtro = null!;
			datos.A001codigoDocumentoPermiso = 1;
			datos.A001estadoRegistro = 1;
			datos.A001fechaCreacion = DateTime.Now;
			datos.A001fechaModificacion = DateTime.Now;
			datos.A001fechaVencimiento = DateTime.Now;
			datos.A001fechaSolicitud = DateTime.Now;
			datos.A001fechaRadicacionRespuesta = DateTime.Now;
			datos.A001fechaRadicacionSolicitud = DateTime.Now;
			datos.A001fechaRespuesta = DateTime.Now;
			datos.A001fechaEmbarque = DateTime.Now;
			datos.A001fechaArribo = DateTime.Now;
			datos.A001firmaAprobado = null!;
			datos.A001finalidad = null!;
			datos.A001modotransporte = null!;
			datos.A001numeroRadicacion = 1;
			datos.A001direccionDestino = null!;
			datos.A001observaciones = null!;
			datos.A001NumeroCertificado = 1;
			datos.A001codigoParametricaTipoCertificado = 1;
			datos.A001AutoridadEmiteCertificado = "1";
			datos.A001codigoCiudadDestinoNavigation = null!;
			datos.A001codigoCiudadEmbarqueNavigation = null!;
			datos.A001codigoDocumentoPermisoNavigation = null!;
			datos.A001codigoEmpresaNavigation = null!;
			datos.A001codigoEntidadExportadorNavigation = null!;
			datos.A001codigoPaisDestinoNavigation = null!;
			datos.A001codigoParametricaTipoEmbarqueNavigation = null!;
			datos.A001codigoParametricaTipoPermisoNavigation = null!;
			datos.A001codigoParametricaTipoSolictudNavigation = null!;
			datos.A001codigoParametricaTipoCertificadoNavigation = null!;
			datos.A001codigoPersonaApoderadoNavigation = null!;
			datos.A001codigoPersonaDestinatarioNavigation = null!;
			datos.A001codigoPersonaTitularNavigation = null!;
			datos.A001codigoPuertoentradaNavigation = null!;
			datos.A001codigoPuertosalidaNavigation = null!;
			datos.AdmintT016RlUsuarioCertificados = null!;
			datos.CitestT002Subpartidaarancelaria = null!;
			datos.CitestT005Recaudos = null!;
			datos.CitestT006Informacionespecimen = null!;
			datos.CitestT007Salvoconductomovilizacions = null!;
			datos.CitestT009ActaSeguimientos = null!;
			datos.CitestT010RlCertificadoDocumentos = null!;
			datos.CitestT011RlCertificadoEvaluacions = null!;

			var type = Assert.IsType<CitestT001Certificado>(datos);
			Assert.NotNull(type);
		}
	}
}
