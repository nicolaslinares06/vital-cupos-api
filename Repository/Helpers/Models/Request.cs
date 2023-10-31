using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Helpers.Models
{
    public class ReqLogin
    {
        public string user { get; set; } = "";
        public string password { get; set; } = "";
    }

    public class ReqSimpleUser
    {
        public string? user { get; set; }
    }

    public class ReqUser
    {
        public decimal code { get; set; }
        public decimal? cityAddress { get; set; }
        public decimal? codeParametricDocumentType { get; set; }
        public decimal? codeParametricUserType { get; set; }
        public string? dependence { get; set; }
        public bool? acceptsTerms { get; set; }
        public bool? acceptsProcessingPersonalData { get; set; }
        public decimal identification { get; set; }
        public string? firstName { get; set; }
        public string? secondName { get; set; }
        public string? firstLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? login { get; set; }
        public string? address { get; set; }
        public decimal? phone { get; set; }
        public string? email { get; set; }
        public string? celular { get; set; }
        public string? password { get; set; }
        public string? digitalSignature { get; set; }
        public DateTime? contractStartDate { get; set; }
        public DateTime? contractFinishDate { get; set; }
        public bool? registrationStatus { get; set; }
        public string? rol { get; set; }
    }

    public class UserReq
    {
        public string? A012primerNombre { get; set; }
        public string? A012segundoNombre { get; set; }
        public string? A012primerApellido { get; set; }
        public string? A012segundoApellido { get; set; }
        public decimal PkT012codigo { get; set; }
        public string? A012login { get; set; }
        public string? Roles { get; set; }
        public string? A008valor { get; set; }
        public decimal A012telefono { get; set; }
        public DateTime? A012fechaExpiracontraseña { get; set; }
        public decimal A012identificacion { get; set; }
        public string? A012Modulo { get; set; }
        public string? A012contrasena { get; set; }
    }

    public class ReqDocumentType
    {
        public decimal id { get; set; }
        public string? type { get; set; }
    }

    public class ReqChangePassword
    {
        public string user { get; set; } = "";
        public string password { get; set; } = "";
        public string newPassword { get; set; } = "";
        public bool? acceptsTerms { get; set; }
        public bool? acceptsProcessingPersonalData { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReqId
    {
        public decimal id { get; set; }
    }

    public class ReqEstados
    {
        public decimal pkT008codigo { get; set; }
        public decimal a008posicion { get; set; }
        public string? a008codigoParametricaEstado { get; set; }
        public string? a008descripcion { get; set; }
        public string? a008etapa { get; set; }
        public string? a008estadoRegistro { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReqEstado
    {
        public decimal id { get; set; }
        public decimal position { get; set; }
        public string idEstate { get; set; } = "";
        public string? description { get; set; }
        public string? stage { get; set; }
        public bool estate { get; set; }
    }

    public class ReqRoles
    {
        public decimal id { get; set; }
        public string? estado { get; set; }
        public string? name { get; set; }
        public string? cargo { get; set; }
        public string? descripcion { get; set; }
    }

    public class ReqRol
    {
        public int rolId { get; set; }
        public string name { get; set; } = "";
        public string position { get; set; } = "";
        public string description { get; set; } = "";
        public bool estate { get; set; }
    }

    public class RolModPermition
    {
        public int rolId { get; set; }
        public int moduleId { get; set; }
        public bool consult { get; set; }
        public bool create { get; set; }
        public bool update { get; set; }
        public bool delete { get; set; }
        public bool see { get; set; }
        public string? name { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReqAssignment
    {
        public decimal id { get; set; }
        public decimal? identification { get; set; }
        public string? firstName { get; set; }
        public string? secondName { get; set; }
        public string? firstLastname { get; set; }
        public string? secondLastname { get; set; }
        public string? rolId { get; set; }
        public decimal estate { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ReqAssignmentUpdate
    {
        public decimal id { get; set; }
        public string? statusRequest { get; set; }
    }

    public class TechnicalAdminReq
    {
        public decimal code { get; set; }
        public string name { get; set; } = "";
        public string value { get; set; } = "";
        public string description { get; set; } = "";
        public bool registrationStatus { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class EntityRequest
    {
        public decimal? CompanyCode { get; set; }
        public decimal? DocumentTypeId { get; set; }
        public decimal EntityTypeId { get; set; }
        public string CompanyName { get; set; } = "";
        public decimal NIT { get; set; }
        public decimal CityId { get; set; }
        public string Address { get; set; } = "";
        public decimal Phone { get; set; }
        public string Email { get; set; } = "";
        public string? BusinessRegistration { get; set; }
    }
    public class ReqModulos
    {
        public decimal rolId { get; set; }
        public string? moduleId { get; set; }
        public bool consult { get; set; }
        public bool create { get; set; }
        public bool update { get; set; }
        public bool delete { get; set; }
        public bool see { get; set; }
    }

    public class ModulosReq
    {
        public decimal id { get; set; }
        public string? name { get; set; }
    }

    public class NoveltiesRequest
    {
        public decimal code { get; set; }
        public decimal companyCode { get; set; }
        public decimal typeOfNoveltyId { get; set; }
        public decimal companyStatusId { get; set; }
        public decimal CITESPermitIssuanceStatusId { get; set; }
        public DateTime noveltyRegistrationDate { get; set; }
        public string? observations { get; set; }
        public decimal? availableProductionBalance { get; set; }
        public decimal? availableQuotas { get; set; }
        public decimal? availableInventory { get; set; }
        public decimal? pendingQuotasToProcess { get; set; }
        public decimal? specimenDispositionId { get; set; }
        public decimal? zooCompanyId { get; set; }
        public string? otherDescription { get; set; }
        public string? detailedObservations { get; set; }
        public List<Archivo>? documents { get; set; }
        public List<Archivo> documentsToDelete { get; set; } = new List<Archivo>();
    }
    /// <summary>
    /// 
    /// </summary>
    public class AdminStatesReq
    {
        public string stage { get; set; } = "";
        public string description { get; set; } = "";
        public decimal state { get; set; }
    }

    public class ReqDocs
    {
        public decimal id { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SaveDocumentRequest
    {
        public decimal id { get; set; }
        public SupportDocuments document { get; set; } = new SupportDocuments();
    }

    public class AceptarCondiciones
    {
        public bool A012aceptaTerminos { get; set; }
        public bool A012aceptaTratamientoDatosPersonales { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class UpdateDocument
    {
        public decimal id { get; set; }
        public string? documentChanges { get; set; }
    }

    public class ReqAudit
    {
        public string? nombre { get; set; }
        public string? accion { get; set; }
        public DateTime fecha { get; set; }
        public string? subModulo { get; set; }
        public string? ip { get; set; }
        public string? rol { get; set; }
    }

    public class VitalReq
    {
        public decimal? code { get; set; }
        public string? status { get; set; }
        public string? permissions { get; set; }
        public string? message { get; set; }
        public decimal? ID { get; set; }
        public string? User { get; set; }
        public string? Name { get; set; }
        public decimal? Document { get; set; }
        public string? EMail { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Active { get; set; }
        public string? Enabled { get; set; }
        public string? Module { get; set; }
        public string? Url { get; set; }
        public string? Token { get; set; }
        public string? UrlError { get; set; }
        public decimal rol { get; set; }
    }
}
