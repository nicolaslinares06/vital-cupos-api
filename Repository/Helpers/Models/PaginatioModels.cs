using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{
    public class PaginatioModels
    {
        public class ParamsPaginations
        {
            public int QuantityRecords { get; set; }
            public int QuantityPages { get; set; }
            public int TotalQuantity { get; set; }
            public int PageNumber { get; set; }
            public int QuantityRecordsForpage { get; set; } = 5;
            public string FilterCriterium { get; set; } = string.Empty;
        }
    }
}
