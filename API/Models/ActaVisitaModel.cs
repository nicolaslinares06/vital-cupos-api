namespace API.Models
{
    public class ActaVisitaModel
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class VisitCutsRegistration
    {
        public decimal VisitNumber { get; set; }
        public bool VisitNumber1 { get; set; }
        public bool VisitNumber2 { get; set; }
        public int EstablishmentType { get; set; }
        public decimal EstablishmentID { get; set; }
        public int QuantityOfSkinToCut { get; set; }
        public int IdentificationSeal { get; set; }
        public string? SkinStatus { get; set; } = "";
        public decimal CitesAuthorityOfficer { get; set; } = 0;
        public decimal RepresentativeDocument { get; set; }
        public string? EstablishmentRepresentative { get; set; } = "";
        public decimal City { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public VisitReportDocument ExcelSealFile { get; set; } = new VisitReportDocument();
    }
    /// <summary>
    /// 
    /// </summary>
    public class EditVisitReportAct
    {
        public decimal VisitReportId { get; set; } = 0;
        public decimal VisitNumber { get; set; }
        public decimal EstablishmentType { get; set; }
        public string? EstablishmentTypeName { get; set; } = "";
        public decimal EstablishmentID { get; set; }
        public string? EstablishmentName { get; set; } = "";
        public decimal AmountOfSkinToCut { get; set; }
        public decimal SealIdentification { get; set; }
        public string? SkinStatus { get; set; } = "";
        public decimal CitesAuthorityOfficial { get; set; }
        public decimal RepresentativeDocument { get; set; }
        public string? EstablishmentRepresentative { get; set; } = "";
        public decimal City { get; set; }
        public DateTime Date { get; set; }
        public string DateFormat { get; set; } = "";
        public decimal VisitReportType { get; set; }
        public int SkinStatusInt { get; set; }
        public bool VisitNumber1 { get; set; }
        public bool VisitNumber2 { get; set; }
        public string CitesAuthorityOfficialName { get; set; } = "";
        public VisitReportDocument ExcelSealFile { get; set; } = new VisitReportDocument();
    }
    /// <summary>
    /// 
    /// </summary>
    public class IdentifiableSkinCutsType
    {
        public string? SkinType { get; set; } = "";
        public int Quantity { get; set; }
        public int VisitReportCode { get; set; } = 0;
    }
    /// <summary>
    /// 
    /// </summary>
    public class IdentifiableSkinPartsType
    {
        public string? SkinPartType { get; set; } = "";
        public int Quantity { get; set; }
        public int VisitReportCode { get; set; } = 0;
    }
    /// <summary>
    /// 
    /// </summary>
    public class IrregularSkinTypes
    {
        public string? IrregularSkinType { get; set; } = "";
        public string? AverageAreaForSkinType { get; set; } = "";
        public decimal SkinTypeQuantity { get; set; }
        public decimal TotalAreaForSkinType { get; set; }
        public decimal VisitReportCode { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class IrregularPartTypes
    {
        public string? PartType { get; set; } = "";
        public decimal PartTypeQuantity { get; set; }
        public decimal TotalAreaForPartType { get; set; }
        public decimal VisitReportCode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
        public class VisitReportSkinOriginDocument
        {
            public decimal VisitReportCode { get; set; }
            public string SkinOriginDocumentNumber { get; set; } = "";
        }
    /// <summary>
    /// 
    /// </summary>
    public class VisitReportResolutionNumber
    {
        public decimal VisitReportCode { get; set; }
        public decimal ResolutionNumber { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class VisitReportSafeConduct
    {
        public decimal VisitReportCode { get; set; }
        public decimal SafeConductNumber { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VisitReportDocument
    {
        public decimal Code { get; set; } = 0;
        public string? FileName { get; set; } = "";
        public string? Base64String { get; set; } = "";
        public string? FileType { get; set; } = "";
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExcelSealsFile
    {
        public string Base64Excel { get; set; } = "";
        public decimal NIT { get; set; }
    }
}
