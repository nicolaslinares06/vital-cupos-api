using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
	public class AdmintV001UsuarioTest
	{
		[Fact]
		public void AdmintV001Usuario()
		{
			AdmintV001Usuario datos = new AdmintV001Usuario();
			datos.PkT012codigo = 1;
			datos.Roles = "1";
			datos.A012identificacion = 1;
			datos.A012direccion = null!;
			datos.A012telefono = 1;
			datos.A012correoElectronico = null!;
			datos.A012Celular = "1";
			datos.A012estadoRegistro = 1;
			datos.A012fechaCreacion = DateTime.Now;
			datos.A012fechaModificacion = DateTime.Now;
			datos.A012fechaExpiracontraseña = DateTime.Now;
			datos.A012fechaFinContrato = DateTime.Now;
			datos.A012fechaInicioContrato = DateTime.Now;
			datos.A012primerNombre = null!;
			datos.A012segundoNombre = null!;
			datos.A012segundoApellido = null!;
			datos.A012primerApellido = null!;
			datos.A012login = null!;

			var type = Assert.IsType<AdmintV001Usuario>(datos);
			Assert.NotNull(type);
		}
	}
}
