using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.PaginatioModels;

namespace TestUnit.API.Models
{
    public class ParamsPaginationsTest
    {
        [Fact]
        public void ParamsPaginations()
        {
            ParamsPaginations datos = new ParamsPaginations();
            datos.QuantityRecords = 1;
            datos.QuantityPages = 1;
            datos.TotalQuantity = 1;
            datos.PageNumber = 1;
            datos.QuantityRecordsForpage = 1;
            datos.FilterCriterium = "";

            var type = Assert.IsType<ParamsPaginations>(datos);
            Assert.NotNull(type);
        }
    }
}
