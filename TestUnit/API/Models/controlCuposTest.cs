using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace TestUnit.API.Models
{
    public class controlCuposTest
    {
        [Fact]
        public void CuposCreation()
        {
            // Arrange
            var cupo = new Cupos
            {
                codigoCupo = 1,
                autoridadEmiteResolucion = "Autoridad123",
                codigoZoocriadero = "Zoocriadero123",
                numeroResolucion = "Resolucion123",
                fechaResolucion = DateTime.Now,
                fechaRegistroResolucion = DateTime.Now,
                fechaRadicado = DateTime.Now,
                cuposOtorgados = 10,
                cuposPorAnio = 5,
                cuposTotal = 15,
                fechaProduccion = DateTime.Now,
                cuposAprovechamientoComercializacion = 8,
                cuotaRepoblacion = "CuotaRepoblacion123",
                cuposDisponibles = 7,
                anioProduccion = 2023,
                observaciones = "Observaciones123",
                nitEmpresa = "NitEmpresa123",
                codigoEspecie = "Especie123",
                numeroInternoFinalCuotaRepoblacion = 2,
                numeroInternoFinal = "NumeroInternoFinal123",
                numeroInternoInicialCuotaRepoblacion = 1,
                NumeroInternoInicial = "NumeroInternoInicial123",
                cuposUtilizados = 6,
                codigoParametricaPagoCuotaDerepoblacion = 3,
                tipoEspecimen = "Especimen123",
                nombreEmnpresa = "NombreEmpresa123",
                tipoEmpresa = 2,
                soporteRepoblacion = true,
            };

            // Act
            var type = Assert.IsType<Cupos>(cupo);
            Assert.NotNull(type);
        }

        [Fact]
        public void ExportSpecimensCreation()
        {
            // Arrange
            ExportSpecimens exportSpecimen = new ExportSpecimens();
            exportSpecimen.PkT005code = 1;
            exportSpecimen.quotaCode = 2;
            exportSpecimen.year = 2023;
            exportSpecimen.quotas = 10;
            exportSpecimen.id = 3;
            exportSpecimen.markingTypeParametricCode = "MarkingType123";
            exportSpecimen.speciesCode = "Species123";
            exportSpecimen.repopulationQuotaPaymentParametricCode = "PaymentCode123";
            exportSpecimen.filingDate = DateTime.Now;
            exportSpecimen.specimenType = "SpecimenType123";
            exportSpecimen.productionYear = 2023;
            exportSpecimen.batchCode = "BatchCode123";
            exportSpecimen.markingConditions = "Conditions123";
            exportSpecimen.parentalPopulationMale = 5;
            exportSpecimen.parentalPopulationFemale = 3;
            exportSpecimen.totalParentalPopulation = 8;
            exportSpecimen.populationFromIncubator = 2;
            exportSpecimen.populationAvailableForQuotas = 6;
            exportSpecimen.individualsForRepopulation = "Individuals123";
            exportSpecimen.grantedUtilizationQuotas = 7;
            exportSpecimen.replacementRate = "Rate123";
            exportSpecimen.mortalityNumber = 1;
            exportSpecimen.repopulationQuotaForUtilization = true;
            exportSpecimen.grantedPaidUtilizationQuotas = "PaidQuotas123";
            exportSpecimen.observations = "Observations123";
            exportSpecimen.repopulationQuota = "RepopulationQuota123";
            exportSpecimen.initialRepopulationQuotaInternalNumber = "InitialNumber123";
            exportSpecimen.finalRepopulationQuotaInternalNumber = "FinalNumber123";
            exportSpecimen.initialInternalNumber = 4;
            exportSpecimen.finalInternalNumber = 9;
            exportSpecimen.totalQuotas = 15;
            exportSpecimen.availableQuotas = 12;
            exportSpecimen.repopulationQuotaPaid = true;
            exportSpecimen.speciesRegisteredForCommercialization = true;
            exportSpecimen.supportingDocuments = null;
            exportSpecimen.newSupportingDocuments = null;
            exportSpecimen.deletedSupportingDocuments = null;

            // Act
            var type = Assert.IsType<ExportSpecimens>(exportSpecimen);
            Assert.NotNull(type);
        }
    }
}
