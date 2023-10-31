using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers
{
    public static class ModuleManager
    {
        //Basado en la tabla de Modulos, si algun código cambia en BD se debe hacer aca la actualización
        public const string smAdministracion = "01";
        public const string smRoles = "02";
        public const string smRolFuncionalidades = "03";
        public const string smParametricas = "04";
        public const string smAuditoria = "05";
        public const string smSolicitudesRol = "06";
        public const string smOtorgamientoCupos = "07";
        public const string smCompraVenta = "08";
        public const string smNovedades = "09";
        public const string smMovimientoPeces = "10";
        public const string smMovimientoFlora = "11";
        public const string smReportes = "12";
        public const string smEstado = "13";
        public const string smAdminTecnica = "14";
        public const string smGestiónDocumental = "15";
        public const string smHojaVidaEntidad = "16";
        public const string smRegistrarResolucionCuotasGlobal = "17";
        public const string smConsultaPrecintosMarquillas = "18";
        public const string smBandejaSolicitudPrecintosNacionalesUsuarioExterno = "19";
        public const string smBandejaTrabajoUsuarioInternoCoordinador = "20";
        public const string smBandejaTrabajoValidacionSolicitud = "21";
        public const string smBandejaTrabajoFirmaRespuestaSolicitante = "22";
        public const string smRegistroActaVisitaCortes = "23";
        public const string smGestionarSolicitudPermisosCites = "24";
        public const string smGestionUsuarios = "28";
        public const string smAdministracióndeServicios = "29";
    }
}
