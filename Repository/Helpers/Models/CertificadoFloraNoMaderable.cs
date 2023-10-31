namespace Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CertificateData
    {
        public decimal? code {get; set;}
        public string? issuingAuthority { get; set; }
        public string? certificateNumber {get; set;}
        public DateTime certificationDate {get; set;}
        public DateTime certificationValidity {get; set;}
        public string? permissionType {get; set;}
        public string specimenProductImpExpType { get; set; } = "";
        public string? certificateRemarks {get; set;}
        public decimal? companyNit { get; set;}
        public List<SupportDocuments>? supportingDocuments { get; set; }
        public List<SupportDocuments>? newSupportingDocuments { get; set; }
        public List<SupportDocuments>? deletedSupportingDocuments { get; set; }
    }

    public class SupportDocuments
    {
        public decimal? code { get; set; }
        public string? base64Attachment { get; set; }
        public string? attachmentName { get; set; }
        public string? attachmentType { get; set; }
        public string? tempAction { get; set; }
    }
}
