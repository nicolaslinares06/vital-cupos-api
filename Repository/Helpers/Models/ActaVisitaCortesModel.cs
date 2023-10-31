using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{
    public class ActaVisitaCortesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public class VisitRecordsSearch
        {
            public int EstablishmentId { get; set; }
            public int EstablishmentTypeId { get; set; }
            public DateTime? VisitDate { get; set; }
            public int SearchType { get; set; } = 0;

        }

        public class VisitReportsEstablishments
        {
            public decimal? VisitReportId { get; set; }
            public decimal? ReportTypeId { get; set;}
            public decimal? VisitNumber { get; set;}
            public string? Establishment { get; set;}
            public string? EstablishmentType { get; set;}
            public DateTime Date { get; set;}
            public bool VisitNumberOne { get; set;}
            public bool VisitNumberTwo { get; set;}
            public decimal? RegistrationStatus { get; set;}
            public decimal? CreationDateDecimal { get; set; }
        }

    }
}
