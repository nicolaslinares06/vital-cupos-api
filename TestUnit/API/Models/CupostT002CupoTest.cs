using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT002CupoTest
	{
		[Fact]
		public void CupostT002Cupo()
		{
			CupostT002Cupo datos = new CupostT002Cupo();
			datos.PkT002codigo = 1;
			datos.A002codigoEmpresa = 1;
			datos.A002codigoUsuarioCreacion = 1;
			datos.A002codigoUsuarioModificacion = 1;
			datos.A002codigoDocumentoCarta = 1;
			datos.A002codigoDocumentoResolucion = 1;
			datos.A002codigoDocumentoConsignacion = 1;
			datos.A002estadoRegistro = 1;
			datos.A002fechaVigencia = DateTime.Now;
			datos.A002fechaProduccion = DateTime.Now;
			datos.A002fechaCreacion = DateTime.Now;
			datos.A002fechaModificacion = DateTime.Now;
			datos.A002fechaResolucion = DateTime.Now;
			datos.A002fechaRegistroResolucion = DateTime.Now;
			datos.A002fechaRadicadoSolicitud = DateTime.Now;
			datos.A002fechaRadicadoRespuesta = DateTime.Now;
			datos.A002observaciones = null!;
			datos.A002cuposAsignados = 1;
			datos.A002cuposDisponibles = 1;
			datos.A002cuposTotal = 1;
			datos.A002precintosymarquillasValorpago = 1;
			datos.A002rangoCodigoInicial = null!;
			datos.A002rangoCodigoFin = null!;
			datos.A002pielLongitudMenor = 1;
			datos.A002pielLongitudMayor = 1;
			datos.A002estadoCupo = null!;
			datos.A002numeroResolucion = 1;
			datos.A002cuotaRepoblacion = null!;
			datos.A002AutoridadEmiteResolucion = "1";
			datos.A002NumeracionInicialPrecintos = null!;
			datos.A002NumeracionFinalPrecintos = null!;
			datos.A002CodigoZoocriadero = "1";
			datos.A002codigoEmpresaNavigation = null!;
			datos.CupostT005Especieaexportars = null;

			var type = Assert.IsType<CupostT002Cupo>(datos);
			Assert.NotNull(type);
		}
	}
}
