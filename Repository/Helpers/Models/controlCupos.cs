using Microsoft.EntityFrameworkCore.Diagnostics;
using Repository.Models;
using Web.Models;

namespace Web.Models
{
    public class Cupos
    {
        public decimal? codigoCupo { get; set; }
        public string? autoridadEmiteResolucion { get; set; }
        public string? codigoZoocriadero { get; set; }
        public string? numeroResolucion { get; set; }
        public DateTime? fechaResolucion { get; set; }
        public DateTime? fechaRegistroResolucion { get; set; }
        public DateTime? fechaRadicado { get; set; }
        public decimal? cuposOtorgados { get; set; }
        public decimal? cuposPorAnio { get; set; }
        public decimal? cuposTotal { get; set; }
        public DateTime fechaProduccion { get; set; }
        public decimal? cuposAprovechamientoComercializacion { get; set; }
        public string? cuotaRepoblacion { get; set; }
        public decimal? cuposDisponibles { get; set; }
        public decimal? anioProduccion { get; set; }
        public string? observaciones { get; set; }
        public string? nitEmpresa { get; set; }
        public string? codigoEspecie { get; set; }
        public decimal? numeroInternoFinalCuotaRepoblacion { get; set; }
        public string? numeroInternoFinal { get; set; }
        public decimal? numeroInternoInicialCuotaRepoblacion { get; set; }
        public string? NumeroInternoInicial { get; set; }
        public decimal? cuposUtilizados { get; set; }
        public decimal? codigoParametricaPagoCuotaDerepoblacion { get; set; }
        public string? tipoEspecimen { get; set; }
        public string? nombreEmnpresa { get; set; }
        public decimal? tipoEmpresa { get; set; }
        public bool? soporteRepoblacion { get; set; }

    }

    public class ExportSpecimens
    {
        public decimal? PkT005code { get; set; }
        public decimal? quotaCode { get; set; }
        public decimal? year { get; set; }
        public decimal? quotas { get; set; }
        public decimal? id { get; set; }
        public string? markingTypeParametricCode { get; set; }
        public string? speciesCode { get; set; }
        public string? repopulationQuotaPaymentParametricCode { get; set; } = null!;
        public DateTime? filingDate { get; set; }
        public string? specimenType { get; set; } = null!;
        public decimal? productionYear { get; set; }
        public string? batchCode { get; set; } = null!;
        public string? markingConditions { get; set; } = null!;
        public decimal? parentalPopulationMale { get; set; }
        public decimal? parentalPopulationFemale { get; set; }
        public decimal? totalParentalPopulation { get; set; }
        public decimal? populationFromIncubator { get; set; }
        public decimal? populationAvailableForQuotas { get; set; }
        public string? individualsForRepopulation { get; set; } = null!;
        public decimal? grantedUtilizationQuotas { get; set; } = null!;
        public string? replacementRate { get; set; } = null!;
        public decimal? mortalityNumber { get; set; }
        public bool? repopulationQuotaForUtilization { get; set; }
        public string? grantedPaidUtilizationQuotas { get; set; } = null!;
        public string? observations { get; set; } = null!;
        public string? repopulationQuota {get; set;}
        public string? initialRepopulationQuotaInternalNumber { get; set; }
        public string? finalRepopulationQuotaInternalNumber { get; set; }
        public decimal? initialInternalNumber { get; set; }
        public decimal? finalInternalNumber { get; set; }
        public decimal? totalQuotas { get; set; }
        public decimal? availableQuotas { get; set; }
        public bool? repopulationQuotaPaid { get; set; }
        public bool? speciesRegisteredForCommercialization { get; set; }
        public List<SupportDocuments>? supportingDocuments { get; set; }
        public List<SupportDocuments>? newSupportingDocuments { get; set; }
        public List<SupportDocuments>? deletedSupportingDocuments { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SaveResolutionQuotas
    {
        public CupoGuardar? dataToSave { get; set; }
        public List<ExportSpecimens>? newExportSpeciesData { get; set; }
    }

    public class CupoGuardar
    {
        public decimal? quotaCode { get; set; }
        public string? issuingAuthority { get; set; }
        public string? zoocriaderoCode { get; set; }
        public decimal? resolutionNumber { get; set; }
        public DateTime? resolutionDate { get; set; }
        public DateTime? resolutionRegistrationDate { get; set; }
        public string? observations { get; set; }
        public decimal? companyNit { get; set; }
    }

    public class TotalQuotas
    {
        public decimal? availableQuotas { get; set; }
        public decimal? availableInventory { get; set; }
        public decimal? pendingQuotasForProcessing { get; set; }
    }
}
