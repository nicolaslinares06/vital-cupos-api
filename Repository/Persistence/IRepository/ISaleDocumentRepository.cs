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
    public interface ISaleDocumentRepository
    {
        /// <summary>
        /// Obtiene el documento de venta por codigo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetSaleDocumentId(ClaimsIdentity identity, int code, string ipAddress);

        /// <summary>
        /// Obtiene los documentos de venta
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="typeDocument"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetSaleDocuments(ClaimsIdentity identity, string ipAddress, string? typeDocument = null, string? documentNumber = null, string? numberCartaVenta=null);

        /// <summary>
        /// Obtiene los cupos por numero documento de la empresa o representante legal
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetQuotas(ClaimsIdentity identity, string documentNumber, string ipAddress);

        /// <summary>
        /// Obtiene las numeraciones por numero documento de la empresa y codigo cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codigoCupo"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        Responses GetQuotasNumeraciones(ClaimsIdentity identity, int codigoCupo, string documentNumber, string ipAddress);

        /// <summary>
        /// Valida si estan disponibvles las numeraciones
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        Responses ValidateNumbers(ClaimsIdentity identity, Seal numbers);

        /// <summary>
        /// Obtiene el inventario
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses GetInventory(ClaimsIdentity identity, string documentNumber, string ipAddress, string? code=null);
        
        /// <summary>
        /// Obtiene especies
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses GetSpecies(ClaimsIdentity identity);
        
        /// <summary>
        /// Guarda documento de venta
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        public Responses SaveSaleDocument(ClaimsIdentity identity, SaleDocumentModel saleDocument, string ipAddress);
        
        /// <summary>
        /// Buscar empresa
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="number"></param>
        /// <param name="typeDocument"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public Responses SearchCompany(ClaimsIdentity identity, string number, decimal typeDocument = 0, decimal companyCode = 0);

        /// <summary>
        /// Valida si la empresa se encuentra registrada
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="company"></param>
        /// <param name="typeDocument"></param>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public Responses ValidateCompany(ClaimsIdentity identity, decimal company = 0, decimal typeDocument = 0, string documentNumber = "0");
        
        /// <summary>
        /// eliminar documento de venta
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Responses DeleteSaleDocument(ClaimsIdentity identity, string id, string ipAddress);
        
        /// <summary>
        /// editar documento de venta
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="saleDocument"></param>
        /// <returns></returns>
        public Responses UpdateSaleDocument(ClaimsIdentity identity, SaleDocumentModel saleDocument, string ipAddress);
        
        /// <summary>
        /// guardar cupo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="quotas"></param>
        /// <returns></returns>
        public Responses SaveQuota(ClaimsIdentity identity, List<Quota> quotas, string ipAddress);
        
        /// <summary>
        /// validar documento
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="supportDocuments"></param>
        /// <param name="actionEdit"></param>
        /// <param name="code"></param>
        /// <param name="supportDocumentsRemoved"></param>
        /// <returns></returns>
        public Responses ValidateDocumentAction(ClaimsIdentity identity, List<SupportDocuments> supportDocuments, string ipAddress, bool actionEdit, decimal code = 0, List<SupportDocuments>? supportDocumentsRemoved = null);
        
        /// <summary>
        /// guarda documentos
        /// </summary>
        /// <param name="document"></param>
        public void SaveDocuments(ClaimsIdentity identity, SupportDocuments document, string ipAddress);
        
        /// <summary>
        /// deshabilita documentos
        /// </summary>
        /// <param name="documentsRemoved"></param>
        public void UpdateDocumentsRemoved(ClaimsIdentity identity, List<SupportDocuments> documentsRemoved, string ipAddress);
        
        /// <summary>
        /// actualiza documentos
        /// </summary>
        /// <param name="document"></param>
        public void UpdateDocument(ClaimsIdentity identity, SupportDocuments document, string ipAddress);
        
        /// <summary>
        /// obtiene documentos de soporte
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetSupportDocument(ClaimsIdentity identity, decimal code, string ipAddress);
        
        /// <summary>
        /// obtiene cupos por id
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Responses GetQuotasByCode(ClaimsIdentity identity, decimal code, string ipAddress);
        
        /// <summary>
        /// actualiza 
        /// </summary>
        /// <param name="quota"></param>
        public void UpdateQuotasCompanySells(ClaimsIdentity identity, Quota quota, string ipAddress);
        
        /// <summary>
        /// actualiza
        /// </summary>
        /// <param name="quotasInventory"></param>
        public void UpdateQuotasInventory(ClaimsIdentity identity, List<Inventory> quotasInventory, string ipAddress);

        public Responses SearchSeals(ClaimsIdentity identity, NumbersSeals data, string ipAddress);
    }
}
