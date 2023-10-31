using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.PaginatioModels;
using static Repository.Helpers.Models.ReportesMarquillasModels;

namespace TestUnit.API.Models
{
    public class ReportesMarquillasModelsTest
    {
        [Fact]
        public void TagsData()
        {
            TagsData datos = new TagsData();
            datos.RadicationNumber = "";
            datos.RadicationDate = DateTime.Now;
            datos.CompanyName = "";
            datos.NIT = 1;
            datos.City = "";
            datos.Address = "";
            datos.Phone = 1;
            datos.Species = "";
            datos.Type = "";
            datos.SpeciesTags = "";
            datos.Amount = 1;
            datos.Color = "";
            datos.InitialNumber = "";
            datos.FinalNumber = "";
            datos.ConsignmentValue = 1;
            datos.Evaluator = "";
            datos.AnswerDate = DateTime.Now;
            datos.InitialNumberTags = "";
            datos.FinalNumberTags = "";

            var type = Assert.IsType<TagsData>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void TagsFilters()
        {
            TagsFilters datos = new TagsFilters();
            datos.DateFrom = DateTime.Now;
            datos.DateTo = DateTime.Now;
            datos.RadicationNumber = "";

            var type = Assert.IsType<TagsFilters>(datos);
            Assert.NotNull(type);
        }
    }
}
