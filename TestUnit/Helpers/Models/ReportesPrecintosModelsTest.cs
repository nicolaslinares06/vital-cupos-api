using static Repository.Helpers.Models.ReportesPrecintosModels;

namespace TestUnit.Helpers.Models
{
	public class ReportesPrecintosModelsTest
	{
		[Fact]
		public void DatosPrecintosModel()
		{
			SealDataModel datos = new SealDataModel();
			datos.RadicationNumber = "1";
			datos.RadicationDate = "1";
			datos.CompanyName = "1";
			datos.NIT = 1;
			datos.City = "1";
			datos.DeliveryAddress = "1";
			datos.Telephone = "1";
			datos.Species = "1";
			datos.LesserLength = 1;
			datos.GreaterLength = 1;
			datos.Quantity = 1;
			datos.Color = "1";
			datos.ProductionYear = 1;
			datos.InitialInternalNumber = 1;
			datos.FinalInternalNumber = 1;
			datos.InitialNumber = "1";
			datos.FinalNumber = "1";
			datos.CompanyCode = 1;
			datos.DepositValue = "1";
			datos.Analyst = "1";

			var type = Assert.IsType<SealDataModel>(datos);
			Assert.NotNull(type);
		}

        [Fact]
        public void FiltrosPrecintosMarquillas()
        {
            FiltrosPrecintosMarquillas datos = new FiltrosPrecintosMarquillas();
            datos.documentType = "1";
            datos.initialDate = DateTime.Now;
            datos.number = "1";
            datos.documentNumber = "";
            datos.finalDate = DateTime.Now;
            datos.color = "1";
            datos.companyName = "1";
            datos.validity = "1";

            var type = Assert.IsType<FiltrosPrecintosMarquillas>(datos);
            Assert.NotNull(type);
        }

    }
}
