using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintT013AuditoriumTest
	{
		[Fact]
		public void AdmintT013Auditorium()
		{
			AdmintT013Auditorium datos = new AdmintT013Auditorium();
			datos.PkT013codigo = 1;
			datos.A013codigoUsuarioCreacion = 1;
			datos.A013codigoUsuarioModificacion = 1;
			datos.A013fechaCreacion = DateTime.Now;
			datos.A013fechaModificacion = DateTime.Now;
			datos.A013estadoRegistro = 1;
			datos.A013codigoUsuarioAuditado = 1;
			datos.A013codigoRol = "1";
			datos.A013codigoModulo = null!;
			datos.A013fechaHora = DateTime.Now;
			datos.A013accion = null!;
			datos.A013ip = null!;
			datos.A013estadoAnterior = "1";
			datos.A013estadoActual = "1";
			datos.A013camposModificados = "1";
			datos.CupostT005EspecieaexportarA005codigoUsuarioCreacionNavigations = new List<CupostT005Especieaexportar>();
			datos.CupostT005EspecieaexportarA005codigoUsuarioModificacionNavigations = new List<CupostT005Especieaexportar>();

			var type = Assert.IsType<AdmintT013Auditorium>(datos);
			Assert.NotNull(type);
		}
	}
}
