using Repository.Helpers.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API.Models
{
    public class ResolutionRegisterModelTest
    {
        [Fact]
        public void registerPending()
        {
            var cuposAuditoria = new CuposAuditoria
            {
                PkT002codigo = 1,
                A002codigoEmpresa = "Empresa123",
                A002codigoUsuarioCreacion = "Usuario123",
                A002codigoUsuarioModificacion = 2,
                A002codigoDocumentoCarta = 3,
                A002codigoDocumentoResolucion = 4,
                A002codigoDocumentoConsignacion = 5,
                A002estadoRegistro = 6,
                A002fechaVigencia = DateTime.Now,
                A002fechaProduccion = DateTime.Now,
                A002fechaCreacion = DateTime.Now,
                A002fechaModificacion = DateTime.Now,
                A002fechaResolucion = DateTime.Now,
                A002fechaRegistroResolucion = DateTime.Now,
                A002fechaRadicadoSolicitud = DateTime.Now,
                A002fechaRadicadoRespuesta = DateTime.Now,
                A002observaciones = "Observaciones123",
                A002cuposAsignados = 7,
                A002cuposDisponibles = 8,
                A002cuposTotal = 9,
                A002precintosymarquillasValorpago = 10,
                A002rangoCodigoInicial = 11,
                A002rangoCodigoFin = 12,
                A002pielLongitudMenor = 13,
                A002pielLongitudMayor = 14,
                A002estadoCupo = new byte[] { 1, 0, 1 },
                A002numeroResolucion = 15,
                A002cuotaRepoblacion = "CuotaRepoblacion123",
                A002AutoridadEmiteResolucion = "Autoridad123",
                A002NumeracionInicialPrecintos = 16,
                A002NumeracionFinalPrecintos = 17,
                A002CodigoZoocriadero = "Zoocriadero123",
                A002codigoEmpresaNavigation = new CupostT001Empresa(),
                CupostT005Especieaexportars = new List<CupostT005Especieaexportar>(),
            };

            var type = Assert.IsType<CuposAuditoria>(cuposAuditoria);
            Assert.NotNull(type);
        }
    }
}
