using Org.BouncyCastle.Bcpg;
using Web.Models;

namespace Repository.Helpers.Models
{
    public class TrayForNationalSealsExternUsers
    {
        public class Requests
        {
            public decimal companyCode { get; set; }
            public decimal? requestCode { get; set; }
            public DateTime date { get; set; }
            public decimal representativeCity { get; set; }
            public string deliveryAddress { get; set; } = "";
            public decimal quantity { get; set; }
            public decimal? specimens { get; set; }
            public decimal initialSkinCode { get; set; }
            public decimal finalSkinCode { get; set; }
            public decimal minorLength { get; set; }
            public decimal majorLength { get; set; }
            public bool generateSealsForConsignation { get; set; }
            public DateTime representativeDate { get; set; }
            public string? observations { get; set; }
            public string? response { get; set; }
            public string? requestStatus { get; set; }
            public DateTime? registrationDate { get; set; }
            public decimal? requestType { get; set; }
            public DateTime? statusChangeDate { get; set; }
            public string? withdrawalObservations { get; set; }
            public List<Numeration>? numerations { get; set; }
            public SupportDocuments invoiceAttachment { get; set; } = new SupportDocuments();
            public SupportDocuments? letterAttachment { get; set; }
            public List<SupportDocuments>? attachedAnnexes { get; set; }
            public List<SupportDocuments>? attachedAnnexesToDelete { get; set; }
            public List<SupportDocuments>? responseAttachments { get; set; }
            public List<SupportDocuments>? responseAttachmentsToDelete { get; set; }
            public decimal representativeDepartment { get; set; }
            public List<SafeGuardNumbersModel>? safeGuardNumbers { get; set; }
            public List<CuttingSaveModel>? cuttingSave { get; set; }
        }

        public class SafeGuardNumbersModel
        {
            public int id { get; set; }
            public int idCutting { get; set; }
            public string? number { get; set; }
        }

        public class CuttingSaveModel
        {
            public int id { get; set; }
            public int idCutting { get; set; }
            public string? fractionType { get; set; }
            public int amountSelected { get; set; }
            public int totalAreaSelected { get; set; }
        }
        public class Numeration
        {
            public decimal? code { get; set; }
            public decimal initial { get; set; }
            public decimal final { get; set; }
            public decimal? origen { get; set; }
        }
        public class CuttingSafeGuar 
        {
            public List<decimal>? salvoCon { get; set; }
            public List<ValCut>? Cut { get; set; }

        }
        public class ValCut 
        {
            public decimal cuttingCode { get; set; }
            public decimal? cantCut { get; set; }
            public decimal areaCut { get; set; }
            public string? tipoPart { get; set; }
            public string? safeGuard { get; set; }
        }
        public class RepresentaeLegalEmpresas
        {
            public decimal codigoEmpresa {get; set;}
            public decimal ciudad { get; set; }
            public string? primerNombre { get; set; }
            public string? segundoNombre { get; set; }
            public string? primerApellido { get; set; }
            public string? segundoApellido { get; set; }
            public decimal? tipoIdentificacion { get; set; }
            public string? numeroIdentificacion { get; set; }
            public string? telefono { get; set; }
            public string? fax { get; set; }
        }
        public class RequestsOther
        {
            public decimal code { get; set; }
            public string? filingNumber { get; set; }
            public string? sealLabelRequest { get; set; }
            public string? requestingEntityName { get; set; }
            public DateTime requestDate { get; set; }
            public DateTime? filingDate { get; set; }
            public string? status { get; set; }
            public string? observations { get; set; }
            public string? outgoingFilingNumber { get; set; }
            public DateTime? outgoingFilingDate { get; set; }
        }

        public class RegisterPending
        {
            public decimal requestCode { get; set; }
            public DateTime fechaRadicado { get; set; }
            public string? numeroRadicado { get; set; }
        }
        public class SpecimenQuotas
        {
            public decimal quotaCode  { get; set; }
            public string? codigoEspecie { get; set; }
            public decimal? availableQuotas { get; set; }
            public bool validation { get; set; }
        }

        public class ConsultUnableNumbersModel
        {
            public int code { get; set; }
            public int companyNit  { get; set; }
            public bool quota { get; set; }
            public bool inventory { get; set; }
        }
        public class ValidateNumbersModel
        {
            public decimal codeCompany { get; set; }
            public Numbers? numbers { get; set; }
            public decimal origin { get; set; }
        }
        public class Numbers
        {
            public int initial { get; set; }
            public int final { get; set; }
        }
        public class OcupadosRangos
        {
            public int code { get; set; }
            public List<int>? numbers { get; set; }
        }

        public class CuttingReport
        {
            public decimal code { get; set; }
            public DateTime dateVisit { get; set; }
            public DateTime dateRegister { get; set; }
            public string? visitNumber { get; set; }
        }

        public class Cutting
        {
            public decimal? code { get; set; }
            public string? fractionsType { get; set; }
            public decimal? amount { get; set; }
            public string? totalArea { get; set; }
        }

        public class Safeguard
        {
            public decimal? code { get; set; }
            public decimal codSafeguard { get; set; }
            
        }

    }
}
