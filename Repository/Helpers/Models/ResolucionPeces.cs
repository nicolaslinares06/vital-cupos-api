using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Helpers.Models
{
    public class PermitResolution
    {
        /// <summary>
        /// 
        /// </summary>
        public class ResolucionPermisos
        {
           public decimal? resolutionCode { get; set; } 
           public decimal? companyCode {get;set;}
           public decimal? resolutionNumber {get;set;}
           public DateTime resolutionDate {get;set;}
           public DateTime startDate {get;set;}
           public DateTime endDate {get;set;}
           public SupportDocuments? attachment {get;set;}
           public string? resolutionObject {get;set;}
        }
    }
}
