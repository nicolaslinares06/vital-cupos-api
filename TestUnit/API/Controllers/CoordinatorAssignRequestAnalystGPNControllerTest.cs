using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static API.Controllers.CoordinatorAssignRequestAnalystGpnController;
using static Repository.Helpers.Models.PaginatioModels;

namespace TestUnit.API.Controllers
{
	public class CoordinatorAssignRequestAnalystGPNControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly CoordinatorAssignRequestAnalystGpnController controller;
		private readonly ICoordinatorAssignRequestAnalystsGpnRepository repositoryCoordinadorPrecinto;

        public CoordinatorAssignRequestAnalystGPNControllerTest()
		{
			controller = new CoordinatorAssignRequestAnalystGpnController(repositoryCoordinadorPrecinto, new LoggerFactory().CreateLogger<CoordinatorAssignRequestAnalystGpnController>());

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, "Administrador")
					}, "someAuthTypeName"))
				}
			};

		}

		[Fact]
		public void ConsultarSolicitudes()
		{
			string ConsultRequerimentsSeals = "APROBADO";
			var r = controller.ConsultarSolicitudes(ConsultRequerimentsSeals);
			Assert.True(r != null);
			
			ConsultRequerimentsSeals = "ENVIADA";
			r = controller.ConsultarSolicitudes(ConsultRequerimentsSeals);
			Assert.True(r != null);
		}

        [Fact]
        public void ConsultarSolicitudesConPagination()
        {
            var ConsultRequerimentsSeals = 10166;
            var datos = new ParamsPaginations
            {
                QuantityRecords = 50,
                QuantityPages = 10,
                TotalQuantity = 100,
                PageNumber = 2,
                QuantityRecordsForpage = 20,
                FilterCriterium = "Ejemplo de filtro"
            };

            var r = controller.ConsultarSolicitudesConPagination(ConsultRequerimentsSeals, datos);
            Assert.True(r != null);

            ConsultRequerimentsSeals = 1;
            datos.FilterCriterium = "";

            r = controller.ConsultarSolicitudesConPagination(ConsultRequerimentsSeals, datos);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarArchivos()
        {
            var r = controller.ConsultarArchivos(1, 1);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarNumeracionesSolicitud()
        {
            var r = controller.ConsultarNumeracionesSolicitud(1);
            Assert.True(r != null);
        }

        [Fact]
        public void ConsultarTiposFraccionesSolicitud()
        {
            var r = controller.ConsultarTiposFraccionesSolicitud(1);
            Assert.True(r != null);
        }

        [Fact]
		public void ConsultarAnalistas()
		{
			decimal codigoSolicitud = 1;
			var r = controller.ConsultarAnalistas(codigoSolicitud);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarArchivoPrecinto()
		{
			decimal codigoSolicitud = 1;
			decimal tipoDocumento = 1;
			var r = controller.ConsultarArchivoPrecinto(codigoSolicitud, tipoDocumento);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarDatosSolicitudCupo()
		{
			decimal codigoSolicitud = 1;
			var r = controller.ConsultarDatosSolicitudCupo(codigoSolicitud);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarAnalistaSolicitud()
		{
			decimal codigoSolicitud = 1;
			var r = controller.ConsultarAnalistaSolicitud(codigoSolicitud);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultarDatosSolicitud()
		{
			decimal codigoSolicitud = 1;
			var r = controller.ConsultarDatosSolicitud(codigoSolicitud);
			Assert.True(r != null);
		}

		[Fact]
		public void ConsultaSolicitudDesistido()
		{
			decimal codigoSolicitud = 1;
			var r = controller.ConsultaSolicitudDesistido(codigoSolicitud);
			Assert.True(r != null);
		}

		[Fact]
		public void ActualizarIdAnalista()
		{
			AnalystAssignmentUpdate datos = new AnalystAssignmentUpdate();
			datos.AnalystId = 1;
			datos.RequestCode = 1;
			var r = controller.ActualizarIdAnalista(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ActualizarEstadoSolicitud()
		{
			RequestStatusUpdate datos = new RequestStatusUpdate();
			datos.RequestId = 1;
			datos.RequestStatus = "1";
			var r = controller.ActualizarEstadoSolicitud(datos);
			Assert.True(r != null);
		}


        
        [Fact]
        public void PropsEnvioCorreoCoordinador()
        {
            PropsEnvioCorreoCoordinador datos = new PropsEnvioCorreoCoordinador();
            datos.IdSolicitud = 1;
            datos.Asunto = "1";
            datos.Correo = "1";
            datos.Body = "1";
            
            var type = Assert.IsType<PropsEnvioCorreoCoordinador>(datos);
            Assert.NotNull(type);
        }
    }
}
