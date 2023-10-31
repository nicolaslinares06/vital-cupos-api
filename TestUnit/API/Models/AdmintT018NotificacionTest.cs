using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT018NotificacionTest
	{
		[Fact]
		public void AdmintT018Notificacion()
		{
			AdmintT018Notificacion datos = new AdmintT018Notificacion();
			datos.PkT018Codigo = 1;
			datos.A018fechaCreacion = DateTime.Now;
			datos.A018fechaModificacion = DateTime.Now;
			datos.A018codigoUsuarioCreacion = 1;
			datos.A018codigoUsuarioModificacion = 1;
			datos.A018estadoRegistro = 1;
			datos.A018correoEnvioNotificacion = null!;
			datos.A018fechaEnvioNotificacion = DateTime.Now;
			datos.A018notificacionAsunto = null!;
			datos.A018notificacionMensaje = null!;

			var type = Assert.IsType<AdmintT018Notificacion>(datos);
			Assert.NotNull(type);
		}
	}
}
