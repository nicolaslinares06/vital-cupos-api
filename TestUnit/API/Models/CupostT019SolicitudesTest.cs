using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class CupostT019SolicitudesTest
	{
		[Fact]
		public void CupostT019Solicitudes()
		{
			CupostT019Solicitudes datos = new CupostT019Solicitudes();
			datos.Pk_T019Codigo = 1;
			datos.A019CodigoCiudad = 1;
			datos.A019DireccionEntrega = "1";
			datos.A019FechaSolicitud = DateTime.Now;
			datos.A019FechaConsignacion = DateTime.Now;
			datos.A019CodigoEspecieExportar = 1;
			datos.A019Cantidad = 1;
			datos.A019LongitudMenor = 1;
			datos.A019LongitudMayor = 1;
			datos.A019CodigoUsuarioCreacion = 1;
			datos.A019CodigoUsuarioModificacion = 1;
			datos.A019FechaCreacion = DateTime.Now;
			datos.A019FechaModificacion = DateTime.Now;
			datos.A019EstadoRegistro = 1;
			datos.A019EstadoSolicitud = 1;
			datos.A019NumeroRadicacion = "1";
			datos.A019FechaRadicacion = DateTime.Now;
			datos.A019FechaCambioEstado = DateTime.Now;
			datos.A019ObservacionesDesistimiento = "1";
			datos.A019Observaciones = "1";
			datos.A019Respuesta = "1";
			datos.A019CodigoEmpresa = 1;
			datos.A019AnalistaAsignacion = 1;
			datos.A019ValorConsignacion = 1;
			datos.A019NumeroRadicacionSalida = "1";
			datos.A019FechaRadicacionSalida = DateTime.Now;

			var type = Assert.IsType<CupostT019Solicitudes>(datos);
			Assert.NotNull(type);
		}
	}
}
