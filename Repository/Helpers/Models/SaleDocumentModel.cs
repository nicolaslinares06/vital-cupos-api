using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Helpers.Models
{
    public class SaleDocumentModel
    {
        public decimal Code { get; set; }
        public int? Numeration { get; set; }
        public string? CarteNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal NumberSold { get; set; }
        public string? BusinessSale { get; set; }
        public decimal TypeCarte { get; set; }
        public decimal? TypeDocumentSeller { get; set; }
        public string? DocumentNumberSeller { get; set; }
        public string? ReasonSocial { get; set; }
        public decimal InitialBalanceBusiness { get; set; }
        public decimal FinalBalanceBusiness { get; set; }
        public string Observations { get; set; } = "";
        public string? BusinessShopper { get; set; }
        public decimal InventoryAvailability { get; set; }
        public decimal? TypeDocumentShopper { get; set; }
        public string? DocumentNumberShopper { get; set; }
        public string? ReasonSocialShopper { get; set; }
        public decimal InitialBalanceBusinessShopper { get; set; }
        public decimal FinalBalanceBusinessShopper { get; set; }
        public string ObservationsShopper { get; set; } = "";
        public int? Quota { get; set; } 
        public int? Solds { get; set; } 
        public decimal? QuotasSold { get; set; }
        public string? NitCompanySeller { get; set; }
        public string? NitCompanyShopper { get; set; }
        public decimal CompanySellerCode { get; set; }
        public decimal CompanyShopperCode { get; set; }
        public DateTime RegistrationDateCarteSale { get; set; }
        public List<SupportDocuments> SupportDocuments { get; set; } = new List<SupportDocuments>();
        public List<SupportDocuments>? SupportDocumentsRemoved { get; set; }
        public List<Quota>? Quotas { get; set; }
        public List<Inventory>? QuotasInventory { get; set; }
        public string? TypeSpecimenSeller { get; set; }
        public string? TypeSpecimenShopper { get; set; }
    }

    public class Seal
    {
        public decimal initialNumber { get; set; }
        public decimal finalNumber { get; set; }
        public int? quotaCode { get; set; }
    }

    public class NumbersSeals
    {
        public int initial { get; set; }
        public int final { get; set; }
        public int initialRep { get; set; }
        public int finalRep { get; set; }
    }

    public partial class SaleDocumentAuditoria
    {
        public decimal PkT004codigo { get; set; }
        public string? A004codigoUsuarioCreacion { get; set; }
        public decimal? A004codigoUsuarioModificacion { get; set; }
        public string? A004codigoParametricaTipoCartaventa { get; set; }
        public string? A004codigoEntidadCompra { get; set; }
        public decimal A004codigoDocumentoSoporte { get; set; }
        public decimal A004codigoDocumentoFactura { get; set; }
        public string? A004codigoEntidadVende { get; set; }
        public DateTime A004fechaCreacion { get; set; }
        public DateTime? A004fechaModificacion { get; set; }
        public decimal A004estadoRegistro { get; set; }
        public DateTime A004fechaVenta { get; set; }
        public decimal A004totalCuposObtenidos { get; set; }
        public decimal A004saldoEntidadVendeInicial { get; set; }
        public decimal A004saldoEntidadVendeFinal { get; set; }
        public string A004observacionesCompra { get; set; } = null!;
        public decimal A004totalCuposVendidos { get; set; }
        public decimal A004saldoEntidadCompraInicial { get; set; }
        public decimal A004saldoEntidadCompraFinal { get; set; }
        public string A004observacionesVenta { get; set; } = null!;
        public decimal? A004codigoCupo { get; set; }
        public DateTime A004fechaRegistroCartaVenta { get; set; }
        public string? A004numeroCartaVenta { get; set; }
        public decimal A004disponibilidadInventario { get; set; }
        public string? A004tipoEspecimenEntidadVende { get; set; }
        public string? A004tipoEspecimenEntidadCompra { get; set; }
        public virtual CupostT001Empresa A004codigoEntidadVendeNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A004codigoParametricaTipoCartaventaNavigation { get; set; } = null!;
    }
}
