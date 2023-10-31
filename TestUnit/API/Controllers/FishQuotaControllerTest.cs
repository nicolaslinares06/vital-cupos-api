using API.Helpers;
using Repository;
using System.Security.Cryptography;
using System.Security.Claims;
using Web.Models;
using Repository.Helpers.Models;
using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestUnit.API.Controllers
{
	public class FishQuotaControllerTest
	{
		//Se debe colocar un usuario administrador valido para la ejecucion de las pruebas unitarias
		private readonly DBContext _context;
		private  readonly FishQuotaController controller;
		readonly JwtAuthenticationManager jwtAuthenticationManager;
		private readonly ClaimsIdentity user;
		

        public FishQuotaControllerTest()
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
			controller = new FishQuotaController(_context, jwtAuthenticationManager, new LoggerFactory().CreateLogger<FishQuotaController>());

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
		public void GetFishQuotaByCode()
		{
			decimal code = 1;
			var r = controller.GetFishQuotaByCode(code);
			Assert.True(r != null);
		}

		[Fact]
		public void GetFishesQuotas()
		{
			string? initialValidityDate = "10";
			string? finalValidityDate = "1";
			decimal numberResolution = 1;
			var r = controller.GetFishesQuotas(initialValidityDate, finalValidityDate, numberResolution);
			Assert.True(r != null);
		}

		[Fact]
		public void SaveFishQuota()
		{
			FishQuota datos = new FishQuota();
			datos.Code = 1;
			datos.Type = "1";
			datos.NumberResolution = 1;
			datos.ResolutionDate = DateTime.Now;
			datos.ValidityDate = DateTime.Now;
			datos.ValidityYear = 1;
			datos.ResolutionObject = "1";
			datos.Document = 1;
			datos.InitialValidityDate = DateTime.Now;
			datos.FinalValidityDate = DateTime.Now;
			datos.CodeFishQuotaAmount = 1;
			datos.Group = 1;
			datos.GroupName = "1";
			datos.SpeciesCode = 1;
			datos.Quota = 1;
			datos.Total = 1;
			datos.Region = 1;
			datos.SpeciesName = "1";
			datos.FishQuotaAmounts = null;
			datos.FishQuotaAmountsRemoved = null;
			datos.SupportDocuments = new List<SupportDocuments>();
            datos.SupportDocumentsRemoved = null;

			var r = controller.SaveFishQuota(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void DeleteFishQuota()
		{
			int code = 1;

			var r = controller.DeleteFishQuota(code);
			Assert.True(r != null);
		}

		[Fact]
		public void UpdateFishQuota()
		{
			FishQuota datos = new FishQuota();
			datos.Code = 1;
			datos.Type = "1";
			datos.NumberResolution = 1;
			datos.ResolutionDate = DateTime.Now;
			datos.ValidityDate = DateTime.Now;
			datos.ValidityYear = 1;
			datos.ResolutionObject = "1";
			datos.Document = 1;
			datos.InitialValidityDate = DateTime.Now;
			datos.FinalValidityDate = DateTime.Now;
			datos.CodeFishQuotaAmount = 1;
			datos.Group = 1;
			datos.GroupName = "1";
			datos.SpeciesCode = 1;
			datos.Quota = 1;
			datos.Total = 1;
			datos.Region = 1;
			datos.SpeciesName = "1";
			datos.FishQuotaAmounts = null;
			datos.FishQuotaAmountsRemoved = null;
			datos.SupportDocuments = new List<SupportDocuments>();
            datos.SupportDocumentsRemoved = null;

			var r = controller.UpdateFishQuota(datos);
			Assert.True(r != null);
		}

		[Fact]
		public void GetSupportDocument()
		{
			decimal code = 1;
			var r = controller.GetSupportDocument(code);
			Assert.True(r != null);
		}
	}
}