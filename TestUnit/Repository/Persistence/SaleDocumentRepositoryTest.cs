using API.Helpers;
using Repository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Microsoft.Extensions.Configuration;
using Repository.Helpers.Models;

namespace TestUnit.API
{
	public class SaleDocumentRepositoryTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private readonly SaleDocumentRepository repository;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
        readonly string ipAddress = "1";
		private readonly ClaimsIdentity user;
		
		public static SupportDocuments? documentoEnviar;

		public SaleDocumentRepositoryTest()
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
			repository = new SaleDocumentRepository(_context, jwtAuthenticationManager);
		}

		[Fact]
		public void GetSaleDocumentId()
		{
			int code = 1;
			var r = repository.GetSaleDocumentId(user, code, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void GetSaleDocuments()
		{
			string? typeDocument = null;
			string? documentNumber = null;
			string? numberCartaVenta = null;
			var r = repository.GetSaleDocuments(user, ipAddress, typeDocument, documentNumber, numberCartaVenta);
			Assert.True(r != null);

            typeDocument = "95"; 
            documentNumber = "999737334";
            numberCartaVenta = "1111";
            r = repository.GetSaleDocuments(user, ipAddress, typeDocument, documentNumber, numberCartaVenta);
            Assert.True(r != null);

            typeDocument = "95";
            documentNumber = "999737334";
            numberCartaVenta = null;
            r = repository.GetSaleDocuments(user, ipAddress, typeDocument, documentNumber, numberCartaVenta);
            Assert.True(r != null);

            typeDocument = null;
            documentNumber = null;
            numberCartaVenta = "1111";
            r = repository.GetSaleDocuments(user, ipAddress, typeDocument, documentNumber, numberCartaVenta);
            Assert.True(r != null);
        }

		[Fact]
		public void GetQuotas()
		{
			string documentNumber = "897564231";
			var r = repository.GetQuotas(user, documentNumber, ipAddress);
			Assert.True(r != null);
		}

        [Fact]
        public void UpdateQuotasCompanySells()
        {
            Quota datos = new Quota();
            datos.Code = 1;
            datos.NumberResolution = 1;
            datos.QuotasGrant = 1;
            datos.QuotasAdvantageCommercialization = 1;
            datos.QuotasRePoblation = "1";
            datos.QuotasAvailable = 1;
            datos.ProductionDate = DateTime.Now;
            datos.YearProduction = 1;
            datos.SpeciesCode = 1;
            datos.SpeciesName = "1";
            datos.QuotasSold = 1;
            datos.InitialNumeration = 1;
            datos.FinalNumeration = 1;
            datos.CompanyCode = 1;
            datos.InitialNumerationRePoblation = 1;
            datos.FinalNumerationRePoblation = 1;
            datos.InitialNumerationSeal = 1;
            datos.FinalNumerationSeal = 1;

            try
            {
                repository.UpdateQuotasCompanySells(user, datos, ipAddress);
                // El método se ejecutó sin errores, no se generó una excepción
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // El método generó una excepción, puedes agregar aserciones adicionales si es necesario
                Assert.True(false, $"El método generó una excepción: {ex.Message}");
            }
        }

        [Fact]
        public void UpdateQuotasInventory()
        {

            List<Inventory> list = new List<Inventory>();

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

            list.Add(datos);

            try
            {
                repository.UpdateQuotasInventory(user, list, ipAddress);
                // El método se ejecutó sin errores, no se generó una excepción
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // El método generó una excepción, puedes agregar aserciones adicionales si es necesario
                Assert.True(false, $"El método generó una excepción: {ex.Message}");
            }
        }

        [Fact]
		public void GetInventory()
		{
            string documentNumber = "1"; 
			string? code = "40163";


            var r = repository.GetInventory(user, documentNumber, ipAddress, code);
			Assert.True(r != null);

            r = repository.GetInventory(user, documentNumber, ipAddress, null);
            Assert.True(r != null);
        }

        [Fact]
        public void BuscadorPrecintos()
        {
            var datos = new NumbersSeals
            {
                initial = 100,
                final = 200,
                initialRep = 300,
                finalRep = 400
            };

            var r = repository.BuscadorPrecintos(datos);
            Assert.True(r != null);
        }

        [Fact]
        public void GetQuotasNumeraciones()
        {
            var empresa = _context.CupostT001Empresas.FirstOrDefault(x => x.A001estadoRegistro != 0);
            var cupo = _context.CupostT026FacturaCompraCupo.FirstOrDefault(x => x.A026CodigoCupo != 0);
            var r = repository.GetQuotasNumeraciones(user, Convert.ToInt32(cupo?.A026CodigoCupo ?? 0), Convert.ToString(empresa?.A001nit ?? 0), ipAddress);
            Assert.True(r != null);
        }

        [Fact]
        public void ValidateNumbers()
        {
            var datos = new Seal
            {
                initialNumber = 1000,
                finalNumber = 2000,
                quotaCode = 123
            };

            var r = repository.ValidateNumbers(user, datos);
            Assert.True(r != null);

            var cupo = _context.CupostT026FacturaCompraCupo.FirstOrDefault(x => x.Pk_T026Codigo != 0);
			datos.initialNumber = Convert.ToInt32(cupo?.A026NumeracionInicial ?? 0);
			datos.finalNumber = Convert.ToInt32(cupo?.A026NumeracionFinal ?? 0);
			datos.quotaCode = Convert.ToInt32(cupo?.A026CodigoCupo ?? 0);

            r = repository.ValidateNumbers(user, datos);
            Assert.True(r != null);
        }

        [Fact]
		public void GetSpecies()
		{
			var r = repository.GetSpecies(user);
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
            var r = repository.SaveSaleDocument(user, datos, ipAddress);
			Assert.True(r != null);

            r = repository.SaveSaleDocument(user, new SaleDocumentModel(), ipAddress);
            Assert.True(r != null);
        }

		[Fact]
		public void ValidateCompany()
		{
			decimal company = 1;
			decimal typeDocument = 1;
			string documentNumber = "0";
			var r = repository.ValidateCompany(user, company, typeDocument, documentNumber);
			Assert.True(r != null);
		}

		[Fact]
		public void SearchCompany()
		{
			string number = "1";
			decimal typeDocument = 1;
			decimal companyCode = 1;
			var r = repository.SearchCompany(user, number, typeDocument, companyCode);
			Assert.True(r != null);
		}

		[Fact]
		public void DeleteSaleDocument()
		{
            var dato = _context.CupostT004FacturacompraCartaventa.FirstOrDefault(x => x.A004estadoRegistro != 0);

            string id = Convert.ToString(dato?.PkT004codigo ?? 0);
			var r = repository.DeleteSaleDocument(user, id, ipAddress);
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
			var r = repository.UpdateSaleDocument(user, datos, ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void SaveQuota()
		{
			var r = repository.SaveQuota(user, new List<Quota>(), ipAddress);
			Assert.True(r != null);
		}

		[Fact]
		public void ValidateDocumentAction()
		{
			bool actionEdit = true;
			string code = "1";
			var r = repository.ValidateDocumentAction(user, new List<SupportDocuments>(), code, actionEdit, 0);
			Assert.True(r != null);
		}

		[Fact]
		public void GetQuotasByCode()
		{
			decimal code = 1;
			var r = repository.GetQuotasByCode(user, code, ipAddress);
			Assert.True(r != null);
		}
	}
}
