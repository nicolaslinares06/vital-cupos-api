using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace TestUnit.Helpers.Models
{
	public class RequestTest
	{
		[Fact]
		public void ReqDocumentType()
		{
			ReqDocumentType datos = new ReqDocumentType();
			datos.id = 1;
			datos.type = "1";

			var type = Assert.IsType<ReqDocumentType>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void ReqEstados()
		{
			ReqEstados datos = new ReqEstados();
			datos.pkT008codigo = 1;
			datos.a008posicion = 1;
			datos.a008codigoParametricaEstado = "1";
			datos.a008descripcion = "1";
			datos.a008etapa = "1";
			datos.a008estadoRegistro = "1";

			var type = Assert.IsType<ReqEstados>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void ReqRoles()
		{
			ReqRoles datos = new ReqRoles();
			datos.id = 1;
			datos.estado = "1";
			datos.name = "1";
			datos.cargo = "1";
			datos.descripcion = "1";

			var type = Assert.IsType<ReqRoles>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void ReqModulos()
		{
			ReqModulos datos = new ReqModulos();
			datos.rolId = 1;
			datos.moduleId = "1";
			datos.consult = true;
			datos.create = true;
			datos.update = true;
			datos.delete = true;
			datos.see = true;

			var type = Assert.IsType<ReqModulos>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void ModulosReq()
		{
			ModulosReq datos = new ModulosReq();
			datos.id = 1;
			datos.name = "1";

			var type = Assert.IsType<ModulosReq>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void ReqDocs()
		{
			ReqDocs datos = new ReqDocs();
			datos.id = 1;
			datos.name = "1";
			datos.url = "1";

			var type = Assert.IsType<ReqDocs>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void AceptarCondiciones()
		{
			AceptarCondiciones datos = new AceptarCondiciones();
			datos.A012aceptaTerminos = true;
			datos.A012aceptaTratamientoDatosPersonales = true;

			var type = Assert.IsType<AceptarCondiciones>(datos);
			Assert.NotNull(type);
		}

        [Fact]
        public void ReqAudit()
        {
            ReqAudit datos = new ReqAudit();
            datos.nombre = "1";
            datos.accion = "1";
            datos.fecha = DateTime.Now;
            datos.subModulo = "1";
            datos.ip = "1";
            datos.rol = "1";

            var type = Assert.IsType<ReqAudit>(datos);
            Assert.NotNull(type);
        }
    }
}
