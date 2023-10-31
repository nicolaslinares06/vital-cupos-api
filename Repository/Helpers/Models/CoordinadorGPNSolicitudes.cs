using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{
    public class CoordinadorGpnSolicitudes
    {
        public class NumeracionesSolicitud
        {
            public string Especie { get; set; } = "";
            public decimal? NumeracionInicial { get; set; } = 0;
            public decimal? NumeracionFinal { get; set; } = 0;
        }

        public class ListaSolicitudesCoordinador
        {
            public decimal CodigoSolicitud { get; set; }
            public string? NumeroRadicado { get; set; } = "";
            public string? SolicitudPrecintoNacional { get; set; } = "";
            public string? NombreEntidadSolicitante { get; set; } = "";
            public DateTime? FechaSolicitud { get; set; }
            public DateTime? FechaRadicacion { get; set; }
            public string? Estado { get; set; } = "";
            public string? AccionVisual { get; set; } = "";
            public string? NumeroRadicacionSalida { get; set; } = "";
            public DateTime? FechaRadicacionSalida { get; set; } 
        }


        public class InformacionSolicitud
        {
            public decimal IdSolicitud { get; set; }
            public string? Ciudad { get; set; } = "";
            public string? Establecimiento { get; set; } = "";            
            public DateTime? Fecha { get; set; } = DateTime.Now;
            public string? PrimerNombre { get; set; } = "";
            public string? SegundoNombre { get; set; } = "";
            public string? PrimerApellido { get; set; } = "";
            public string? SegundoApellido { get; set; } = "";
            public string? TipoIdentificacion { get; set; } = "";
            public string? NumeroIdentificacion { get; set; } = "";
            public string? DireccionEntrega { get; set; } = "";
            public string? CiudadEntrega { get; set; } = "";
            public string? Telefonos { get; set; } = "";
            public string? Fax { get; set; } = "";
            public decimal Cantidad { get; set; } = 0;
            public string? EspeciesSubEspecies { get; set; } = "";
            public decimal? CodigoInicialPieles { get; set; } = 0;
            public decimal? CodigoFinalPieles { get; set; } = 0;
            public decimal? LongitudMenor { get; set; } = 0;
            public decimal? LongitudMayor { get; set; } = 0;
            public string? SoporteConsignacionBase64 { get; set; }           
            public DateTime? FechaAsignacion { get; set; } = DateTime.Now;
            public bool EsSoporteConsignacion { get; set; } = true;

            public string? Observaciones { get; set; } = "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500";

            public decimal? Analista { get; set; } = 0;
            public string? NumeroRadicado { get; set; } = "";
            public decimal? Nit { get; set; } = 0;
            public decimal? ValorConsignacion { get; set; } = 0;
            public string? CodigoInicial { get; set; } = "";
            public string? CodigoFinal { get; set; } = "";
            public string? Color { get; set; } = "";
            public string? CorreoElectronico { get; set; } = "";
            public string? DepartamentoEntrega { get; set; } = "";
            public string? Respuesta { get; set; }
            public string? TipoSolicitud { get; set; }
            public decimal? TypeRequestCode { get; set; } = 0;
        }

    }
}
