using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CuposV003SolicitudPrecintosNacionalesTest
	{
		[Fact]
		public void CuposV003SolicitudPrecintosNacionales()
		{
			CuposV003SolicitudPrecintosNacionales datos = new CuposV003SolicitudPrecintosNacionales();
			datos.ID = 1;
			datos.CIUDAD = "1";
			datos.FECHA = DateTime.Now;
			datos.ESTABLECIMIENTO = "1";
			datos.DEPARTAMENTO = "1";
			datos.PRIMERNOMBRE = "1";
			datos.SEGUNDONOMBRE = "1";
			datos.PRIMERAPELLIDO = "1";
			datos.SEGUNDOAPELLIDO = "1";
			datos.TIPOIDENTIFICACION = "1";
			datos.NUMEROIDENTIFICACION = 1;
			datos.DIRECCIONENTREGA = "1";
			datos.CIUDADENTREGA = "1";
			datos.TELEFONO = 1;
			datos.CANTIDAD = 1;
			datos.ESPECIE = "1";
			datos.CORREOELECTRONICO = "1";
			datos.CODIGOINICIAL = 1;
			datos.CODIGOFINAL = 1;
			datos.LONGITUDMENOR = 1;
			datos.LONGITUDMAYOR = 1;
			datos.FECHALEGAL = DateTime.Now;
			datos.OBSERVACIONES = "1";
			datos.RESPUESTA = "1";
			datos.FECHADESISTIMIENTO = DateTime.Now;
			datos.NUMERORADICACION = "1";
			datos.FECHARADICACION = DateTime.Now;
			datos.FECHAESTADO = DateTime.Now;
			datos.OBSERVACIONESDESISTIMIENTO = "1";
			datos.ANALISTA = "1";
			datos.CODIGOESPECIE = 1;
			datos.NUMERORADICACIONSALIDA = "1";
			datos.FECHARADICACIONSALIDA = DateTime.Now;
			datos.VALORCONSIGNACION = 1;
			datos.TIPOSOLICITUD = "";
			datos.CODIGOZOOCRIADERO = "";
			datos.CODIGONUMERACIONES = 1;
			datos.TYPEREQUESTCODE = 1;
			datos.CODIGOCORTEPIELSOLICITUD = 1;
			datos.NITEMPRESA = 1;

			var type = Assert.IsType<CuposV003SolicitudPrecintosNacionales>(datos);
			Assert.NotNull(type);
		}
	}
}
