using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.CoordinadorGpnSolicitudes;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace TestUnit.Helpers.Models
{
    public class CoordinadorGPNSolicitudesTest
    {
        [Fact]
        public void NumeracionesSolicitud()
        {
            NumeracionesSolicitud datos = new NumeracionesSolicitud();
            datos.Especie = "1";
            datos.NumeracionInicial = 1;
            datos.NumeracionFinal = 1;

            var type = Assert.IsType<NumeracionesSolicitud>(datos);
            Assert.NotNull(type);
        }
        [Fact]
        public void ListaSolicitudesCoordinador()
        {
            ListaSolicitudesCoordinador datos = new ListaSolicitudesCoordinador();
            datos.CodigoSolicitud = 1;
            datos.NumeroRadicado = "1";
            datos.SolicitudPrecintoNacional = "1";
            datos.NombreEntidadSolicitante = "1";
            datos.FechaSolicitud = DateTime.Now;
            datos.FechaRadicacion = DateTime.Now;
            datos.Estado = "1";
            datos.AccionVisual = "1";
            datos.NumeroRadicacionSalida = "1";
            datos.FechaRadicacionSalida = DateTime.Now;

            var type = Assert.IsType<ListaSolicitudesCoordinador>(datos);
            Assert.NotNull(type);
        }
        [Fact]
        public void InformacionSolicitud()
        {
            InformacionSolicitud datos = new InformacionSolicitud();
            datos.IdSolicitud = 1;
            datos.Ciudad = "1";
            datos.Establecimiento = "1";
            datos.Fecha = DateTime.Now;
            datos.PrimerNombre = "1";
            datos.SegundoNombre = "1";
            datos.PrimerApellido = "1";
            datos.SegundoApellido = "1";
            datos.TipoIdentificacion = "1";
            datos.NumeroIdentificacion = "1";
            datos.DireccionEntrega = "1";
            datos.CiudadEntrega = "1";
            datos.Telefonos = "1";
            datos.Fax = "1";
            datos.Cantidad = 1;
            datos.EspeciesSubEspecies = "1";
            datos.CodigoInicialPieles = 1;
            datos.CodigoFinalPieles = 1;
            datos.LongitudMenor = 1;
            datos.LongitudMayor = 1;
            datos.SoporteConsignacionBase64 = null;
            datos.FechaAsignacion = DateTime.Now;
            datos.EsSoporteConsignacion = true;
            datos.Observaciones = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de textodatos. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500";
            datos.Analista = 1;
            datos.NumeroRadicado = "1";
            datos.Nit = 1;
            datos.ValorConsignacion = 1;
            datos.CodigoInicial = "1";
            datos.CodigoFinal = "1";
            datos.Color = "1";
            datos.Respuesta = "1";
            datos.TipoSolicitud = "1";

            var type = Assert.IsType<InformacionSolicitud>(datos);
            Assert.NotNull(type);
        }
    }
}
