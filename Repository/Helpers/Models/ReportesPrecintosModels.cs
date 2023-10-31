using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{
    public class ReportesPrecintosModels
    {
        public class SealFilters
        {
            public string? ResolutionNumber { get; set; }
            public decimal Establishment { get; set; } = 0;
            public decimal? NIT { get; set; }
            [DataType(DataType.Date)]
            public DateTime? RadicationDate { get; set; }
            public int SpecificSearch { get; set; } = 0;
        }

        public class SealDataModel
        {
            public string? RadicationNumber { get; set; } = "";
            public string? RadicationDate { get; set; }
            public string? CompanyName { get; set; } = "";
            public decimal? NIT { get; set; } = 0;
            public string? City { get; set; } = "";
            public string? DeliveryAddress { get; set; } = "";
            public string? Telephone { get; set; } = "";
            public string? Species { get; set; } = "";
            public decimal? LesserLength { get; set; } = 0;
            public decimal? GreaterLength { get; set; } = 0;
            public decimal? Quantity { get; set; } = 0;
            public string? Color { get; set; } = "";
            public decimal? ProductionYear { get; set; } = 0;
            public decimal? InitialInternalNumber { get; set; } = 0;
            public decimal? FinalInternalNumber { get; set; } = 0;
            public string? InitialNumber { get; set; } = "";
            public string? FinalNumber { get; set; } = "";
            public decimal? CompanyCode { get; set; } = 0;
            public string? DepositValue { get; set; } = "";
            public string? Analyst { get; set; } = "";
        }

        public class EstablishmentProperties{

            public decimal EstablishmentId  { get; set; }
            public string EstablishmentName { get; set; } = "";
        }

        public class FiltrosPrecintosMarquillas
        {
            public string? documentType { get; set; } = string.Empty;
            public DateTime initialDate { get; set; }
            public string? number { get; set; } = string.Empty;
            public string? documentNumber { get; set; } = string.Empty;
            public DateTime finalDate { get; set; }
            public string? color { get; set; } = string.Empty;
            public string? companyName { get; set; } = string.Empty;
            public string? validity { get; set; } = string.Empty;

        }
    }
}
