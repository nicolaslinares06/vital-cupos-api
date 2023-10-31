using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class Wsv001CheckQuotasSealsLabels
    {
        public decimal? NIT { get; set; }
        public string? NOMBRECIENTIFICO { get; set; }
        public decimal ID { get; set; }
        public string? NOMBRECOMUN { get; set; }
        public decimal? CUPOS { get; set; }
        public decimal? SALDO { get; set; }
        public DateTime? VIGENCIA { get; set; }
        public decimal? NUMERACIONINICIALPRECINTOS { get; set; }
        public decimal? NUMERACIONFINALPRECINTOS { get; set; }
        public decimal? NUMERACIONINICIALMARQUILLA { get; set; }
        public decimal? NUMERACIONFINALMARQUILLA { get; set; }
        
    }
}
