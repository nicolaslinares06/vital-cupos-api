using Repository.Helpers;
using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Persistence.IRepository
{
    public interface IFishQuotaRepository
    {
        public Responses GetFishQuotaByCode(ClaimsIdentity identity, decimal code);
        public Responses GetFishesQuotas(ClaimsIdentity identity, string? initialValidityDate, string? finalValidityDate, decimal numberResolution = 0);
        public Responses SaveFishQuota(ClaimsIdentity identity, FishQuota fishQuota);
        public Responses DeleteFishQuota(ClaimsIdentity identity, int id);
        public Responses UpdateFishQuota(ClaimsIdentity identity, FishQuota fishQuota);
        public Responses SaveFishQuotaAmount(ClaimsIdentity identity, List<FishQuotaAmount> fishQuotas, bool actionEdit, decimal code = 0);
        public Responses UpdateFishQuotaAmount(ClaimsIdentity identity, decimal code, List<FishQuotaAmount> fishQuotasAmount, List<FishQuotaAmount> fishQuotaAmountsRemoved);
        public Responses ValidateDocumentAction(ClaimsIdentity identity, List<SupportDocuments> supportDocuments, bool actionEdit, decimal code = 0, List<SupportDocuments>? supportDocumentsRemoved = null);
        public void SaveDocuments(ClaimsIdentity identity, SupportDocuments supportDocuments);
        public void UpdateDocument(ClaimsIdentity identity, SupportDocuments document, decimal code = 0);
        public Responses GetSupportDocument(ClaimsIdentity identity, decimal code);
        public Responses GetSpecies(ClaimsIdentity identity);
        public void UpdateFishQuotaAmountRemoved(ClaimsIdentity identity, decimal code, List<FishQuotaAmount> fishQuotaAmountsRemoved);
        public void UpdateDocumentsRemoved(ClaimsIdentity identity, List<SupportDocuments> documentsRemoved);
        public void UpdateSpeciesName(ClaimsIdentity identity, decimal speciesCode, string speciesName);
    }
}
