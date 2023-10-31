using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.TrayForNationalSealsExternUsers;

namespace TestUnit.Helpers.Models
{
	public class TrayForNationalSealsExternUsersTest
	{
		[Fact]
		public void Requests()
		{
			Requests datos = new Requests();
			datos.companyCode = 1;
			datos.requestCode = 1;
			datos.date = DateTime.Now;
			datos.representativeCity = 1;
			datos.deliveryAddress = "1";
			datos.quantity = 1;
			datos.specimens = 1;
			datos.initialSkinCode = 1;
			datos.finalSkinCode = 1;
			datos.minorLength = 1;
			datos.majorLength = 1;
			datos.generateSealsForConsignation = true;
			datos.representativeDate = DateTime.Now;
			datos.observations = "1";
			datos.response = "1";
			datos.requestStatus = "1";
			datos.registrationDate = DateTime.Now;
			datos.statusChangeDate = DateTime.Now;
			datos.withdrawalObservations = "1";
			datos.numerations = null!;
			datos.invoiceAttachment = null!;
			datos.letterAttachment = null!;
			datos.attachedAnnexes = null!;
			datos.attachedAnnexesToDelete = null!;
			datos.responseAttachments = null!;
			datos.responseAttachmentsToDelete = null!;
			datos.representativeDepartment = 1;
			datos.safeGuardNumbers = null!;

			var type = Assert.IsType<Requests>(datos);
			Assert.NotNull(type);
		}

		[Fact]
		public void SafeGuardNumbersModelCreation()
		{
			// Arrange
			var safeGuardNumbers = new SafeGuardNumbersModel
			{
				id = 1,
				idCutting = 2,
				number = "Number123",
			};

			var type = Assert.IsType<SafeGuardNumbersModel>(safeGuardNumbers);
			Assert.NotNull(type);
		}

		[Fact]
		public void CuttingSaveModelCreation()
		{
			// Arrange
			var cuttingModel = new CuttingSaveModel
			{
				id = 1,
				idCutting = 2,
				fractionType = "FractionType123",
				amountSelected = 10,
				totalAreaSelected = 100,
			};

			var type = Assert.IsType<CuttingSaveModel>(cuttingModel);
			Assert.NotNull(type);
		}

		[Fact]
		public void numeracion()
		{
			Numeration datos = new Numeration();
			datos.initial = 1;
			datos.final = 1;
			datos.origen = 1;

			var type = Assert.IsType<Numeration>(datos);
			Assert.NotNull(type);
		}


		[Fact]
		public void solicitudes()
		{
			RequestsOther datos = new RequestsOther();
			datos.code = 1;
			datos.filingNumber = "1";
			datos.sealLabelRequest = "1";
			datos.requestingEntityName = "1";
			datos.requestDate = DateTime.Now;
			datos.filingDate = DateTime.Now;
			datos.status = "1";
			datos.observations = "1";
			datos.outgoingFilingNumber = "1";
			datos.outgoingFilingDate = DateTime.Now;

			var type = Assert.IsType<RequestsOther>(datos);
			Assert.NotNull(type);
		}

        [Fact]
        public void OcupadosRangosCreation()
		{
			// Arrange
			var ocupadosRangos = new OcupadosRangos
			{
				code = 1,
				numbers = new List<int> { 10, 20, 30, 40, 50 },
			};

			var type = Assert.IsType<OcupadosRangos>(ocupadosRangos);
			Assert.NotNull(type);
		}

		[Fact]
		public void CuttingReportCreation()
		{
			// Arrange
			var cuttingReport = new CuttingReport
			{
				code = 1,
				dateVisit = DateTime.Now,
				dateRegister = DateTime.Now,
				visitNumber = "VisitNumber123",
			};

			var type = Assert.IsType<CuttingReport>(cuttingReport);
			Assert.NotNull(type);
		}

		[Fact]
		public void CuttingCreation()
		{
			// Arrange
			var cutting = new Cutting
			{
				code = 1,
				fractionsType = "FractionType123",
				amount = 10,
				totalArea = "TotalArea123",
			};

			var type = Assert.IsType<Cutting>(cutting);
			Assert.NotNull(type);
		}

		[Fact]
		public void registerPending()
		{
			RegisterPending datos = new RegisterPending();
			datos.requestCode = 1;
			datos.fechaRadicado = DateTime.Now;
			datos.numeroRadicado = "1";

			var type = Assert.IsType<RegisterPending>(datos);
			Assert.NotNull(type);
		}

        [Fact]
        public void SafeguardCreation()
        {
            // Arrange
            var safeguard = new Safeguard
            {
                code = 1,
                codSafeguard = 2,
            };

            var type = Assert.IsType<Safeguard>(safeguard);
            Assert.NotNull(type);
        }

		[Fact]
		public void EspecimenQuotas()
		{
			SpecimenQuotas datos = new SpecimenQuotas();
			datos.quotaCode  = 1;
			datos.codigoEspecie = "1";
			datos.availableQuotas = 1;
			datos.validation = true;

			var type = Assert.IsType<SpecimenQuotas>(datos);
			Assert.NotNull(type);
		}
	}
}
