using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Repository.Helpers.Models;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class SaleDocumentControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly SaleDocumentController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		
		public static SupportDocuments? documentoEnviar;

        public SaleDocumentControllerTest()
		{
			var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
			var authenticationType = "AuthenticationTypes.Federation";

			user = new ClaimsIdentity(authenticationType);
			user.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
			user.AddClaim(new Claim("aud", "CUPOS"));
			user.AddClaim(new Claim("exp", "1668005030"));
			user.AddClaim(new Claim("iat", "1668004130"));
			user.AddClaim(new Claim("nbf", "1668004130"));

			

			_context = new DBContext();

			jwtAuthenticationManager = new JwtAuthenticationManager(key);
			controller = new SaleDocumentController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<SaleDocumentController>());

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, "Administrador")
					}, "someAuthTypeName"))
				}
			};
		}

		[Fact]
		public void GetSaleDocuments()
		{
			string? typeDocument = null;
			string? documentNumber = null;
			string? numberCartaVenta = null;
			var r = controller.GetSaleDocuments(typeDocument, documentNumber, numberCartaVenta);
			Assert.True(r != null);
		}

        [Fact]
        public void GetQuotasNumeraciones()
        {
            int documentNumber = 1;
            string? numberCartaVenta = "";
            var r = controller.GetQuotasNumeraciones(documentNumber, numberCartaVenta);
            Assert.True(r != null);
        }

        [Fact]
        public void ValidateNumbers()
        {
            var datos = new Seal
            {
                initialNumber = 1000.0m,
                finalNumber = 2000.0m,
                quotaCode = 123
            };

            var r = controller.ValidateNumbers(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void GetSaleDocument()
        {
            int typeDocument = 1;
            var r = controller.GetSaleDocuments(typeDocument);
            Assert.True(r != null);
        }

        [Fact]
        public void GetSupportDocument()
        {
            int typeDocument = 1;
            var r = controller.GetSupportDocument(typeDocument);
            Assert.True(r != null);
        }

        [Fact]
        public void SearchSeals()
        {
            var datos = new NumbersSeals
            {
                initial = 100,
                final = 200,
                initialRep = 300,
                finalRep = 400
            };

            var r = controller.SearchSeals(datos);
            Assert.True(r != null);
        }

        [Fact]
		public void GetQuotas()
		{
			string documentNumber = "1";
			var r = controller.GetQuotas(documentNumber);
			Assert.True(r != null);
		}

		[Fact]
		public void GetInventory()
		{
			string documentNumber = "1";
			string? code = null;
			var r = controller.GetInventory(documentNumber, code);
			Assert.True(r != null);
		}

		[Fact]
		public void GetSpecies()
		{
			var r = controller.GetSpecies();
			Assert.True(r != null);
		}

		[Fact]
		public void SaveSaleDocument()
		{
			SaleDocumentModel datos = new SaleDocumentModel();
			datos.Code = 1;
			datos.Numeration = 1;
			datos.CarteNumber = "1";
			datos.SaleDate = DateTime.Now;
			datos.NumberSold = 1;
			datos.BusinessSale = "1";
			datos.TypeCarte = 1;
			datos.TypeDocumentSeller = 1;
			datos.DocumentNumberSeller = "1";
			datos.ReasonSocial = "1";
			datos.InitialBalanceBusiness = 1;
			datos.FinalBalanceBusiness = 1;
			datos.Observations = "1";
			datos.BusinessShopper = "1";
			datos.InventoryAvailability = 1;
			datos.TypeDocumentShopper = 1;
			datos.DocumentNumberShopper = "1";
			datos.ReasonSocialShopper = "1";
			datos.InitialBalanceBusinessShopper = 1;
			datos.FinalBalanceBusinessShopper = 1;
			datos.ObservationsShopper = "1";
			datos.Quota = 1;
			datos.Solds = 1;
			datos.QuotasSold = 1;
			datos.NitCompanySeller = "1";
			datos.NitCompanyShopper = "1";
			datos.CompanySellerCode = 1;
			datos.CompanyShopperCode = 1;
			datos.RegistrationDateCarteSale = DateTime.Now;
			datos.SupportDocuments = new List<SupportDocuments>();
            datos.SupportDocumentsRemoved = null;
			datos.Quotas = null;
			datos.QuotasInventory = null;
			datos.TypeSpecimenSeller = null;
			datos.TypeSpecimenShopper = null;
			var r = controller.SaveSaleDocument(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void ValidateCompany()
		{
			decimal company = 1;
			decimal typeDocument = 1;
			string documentNumber = "0";
			var r = controller.ValidateCompany(company, typeDocument, documentNumber);
			Assert.True(r != null);
		}

		[Fact]
		public void SearchCompany()
		{
			string number = "1";
			decimal typeDocument = 1;
			decimal companyCode = 1;
			var r = controller.SearchCompany(number, typeDocument, companyCode);
			Assert.True(r != null);
		}

		[Fact]
		public void DeleteSaleDocument()
		{
			string id = "1";
			var r = controller.DeleteSaleDocument(id);
			Assert.True(r != null);
		}

		[Fact]
		public void UpdateSaleDocument()
		{
			SaleDocumentModel datos = new SaleDocumentModel();
			datos.Code = 1;
			datos.Numeration = 1;
			datos.CarteNumber = "1";
			datos.SaleDate = DateTime.Now;
			datos.NumberSold = 1;
			datos.BusinessSale = "1";
			datos.TypeCarte = 1;
			datos.TypeDocumentSeller = 1;
			datos.DocumentNumberSeller = "1";
			datos.ReasonSocial = "1";
			datos.InitialBalanceBusiness = 1;
			datos.FinalBalanceBusiness = 1;
			datos.Observations = "1";
			datos.BusinessShopper = "1";
			datos.InventoryAvailability = 1;
			datos.TypeDocumentShopper = 1;
			datos.DocumentNumberShopper = "1";
			datos.ReasonSocialShopper = "1";
			datos.InitialBalanceBusinessShopper = 1;
			datos.FinalBalanceBusinessShopper = 1;
			datos.ObservationsShopper = "1";
			datos.Quota = 1;
			datos.Solds = 1;
			datos.QuotasSold = 1;
			datos.NitCompanySeller = "1";
			datos.NitCompanyShopper = "1";
			datos.CompanySellerCode = 1;
			datos.CompanyShopperCode = 1;
			datos.RegistrationDateCarteSale = DateTime.Now;
			datos.SupportDocuments = new List<SupportDocuments>();
            datos.SupportDocumentsRemoved = null;
			datos.Quotas = null;
			datos.QuotasInventory = null;
			datos.TypeSpecimenSeller = null;
			datos.TypeSpecimenShopper = null;
			var r = controller.UpdateSaleDocument(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void GetQuotasByCode()
		{
			decimal code = 1;
			var r = controller.GetQuotasByCode(code);
			Assert.True(r != null);
		}
    }
}
