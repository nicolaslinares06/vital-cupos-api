using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.Helpers.Models
{
	public class InventoryTest
	{
		[Fact]
		public void Inventory()
		{
			Inventory datos = new Inventory();
			datos.quotaCode = 1;
			datos.Code = 1;
			datos.NumberSaleCarte = "1";
			datos.ReasonSocial = "1";
			datos.SaleDate = DateTime.Now;
			datos.AvailabilityInventory = 1;
			datos.Year = "1";
			datos.AvailableInventory = 1;
			datos.InitialNumeration = 1;
			datos.FinalNumeration = 1;
			datos.InitialNumerationRePoblation = 1;
			datos.FinalNumerationRePoblation = 1;
			datos.InitialNumerationSeal = 1;
			datos.FinalNumerationSeal = 1;
			datos.SpeciesCode = 1;
			datos.SpeciesName = "1";
			datos.InventorySold = 1;

			var type = Assert.IsType<Inventory>(datos);
			Assert.NotNull(type);
		}

        [Fact]
        public void rangosNumeros()
        {
            RangosNumeros datos = new RangosNumeros();
            datos.code = 1;
            datos.numeros = new List<int>();
            datos.numerosLibres = new List<int>();
            datos.numeracionesNuevas = null;

            var type = Assert.IsType<RangosNumeros>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void numeracionesNuevas()
        {
            NumeracionesNuevas datos = new NumeracionesNuevas();
            datos.codigo = 1;
            datos.numeros = new List<int>();

            var type = Assert.IsType<NumeracionesNuevas>(datos);
            Assert.NotNull(type);
        }
    }
}
