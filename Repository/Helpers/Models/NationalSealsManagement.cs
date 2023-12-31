using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{

    public class SettledNationalSealsManagement
    {
        public decimal code { get; set; }
        public string? codeSettled { get; set; }
        public DateTime? date { get; set; }
    }

    public class Especies
    {
        public int pkT005Codigo { get; set; }
        public string? a005Nombre { get; set; }
    }

    public class GenerateSealCodes
    {
        public string? code { get; set; }
        public decimal codeSpecies { get; set; }
        public string? initialNumber { get; set; }
        public string? finalNumber { get; set; }
        public decimal color { get; set; }
        public decimal amount { get; set; }
        public decimal worth { get; set; }
        public string? observations { get; set; }
        public string? tipoSolicitud { get; set; }
    }

    public class ReturnSettledNationalSealsManagement

    {
        public decimal code { get; set; }
        public string observations { get; set; } = "";

    }

    public class DocumentInformation
    {
        public decimal? codigo { get; set; }
        public string? nombreArchivo { get; set; }
        public string? url { get; set; }
    }

    public class Files
    {
        public decimal? codigo { get; set; }
        public string? adjuntoBase64 { get; set; }
        public string? nombreAdjunto { get; set; }
        public string? tipoAdjunto { get; set; }
    }

    public class DesistNationalSealsManagement

    {
        public decimal code { get; set; }
        public decimal state { get; set; }
        public Files file { get; set; } = new Files();
        public string? observations { get; set; }

    }
    public class SignApplicationDocument

    {
        public int code { get; set; }
        public decimal initialNumbering { get; set; }
        public decimal finalNumbering { get; set; }
        public int amount { get; set; }
    }

    public class SaveRequestSeals
    {
        public decimal code { get; set; }
        public decimal amount { get; set; }
        public decimal speciesSubspecies { get; set; }
        public decimal initialCode { get; set; }
        public decimal finalCode { get; set; }
        public decimal lengthMinor { get; set; }
        public decimal lengthGreater { get; set; }
        public DateTime consignMentdate { get; set; }
        public string? observations { get; set; }
        public string? response { get; set; }
        public bool consignment { get; set; }
        public Files? documentAttached { get; set; }
        public Files? documentSupportConsignment { get; set; }
        public Files? documentSupport { get; set; }
    }
    public class ConsultCodes
    {
        public decimal A019Cantidad { get; set; }
    }
    public class NitEmpresa
    {
        public decimal a001nit { get; set; }

    }
    public class ColorPrecinto
    {
        public string? a008valor { get; set; }
    }

    public class CodigosInternos
    {
        public decimal? inicial { get; set; }
        public decimal? final { get; set; }
        public decimal? valorConsignacion { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? numeroCupos {get; set;}
        public string? carta { get; set; }
        public decimal? resolucion { get; set; }
        public string? zoocriadero { get; set; }

    }
    public class TableMin
    {
        public List<SafeGua>? listGuar { get; set; }
        public List<Cut>? corte { get; set; }
    }

    public class SafeGua 
    {
        public decimal codigoSalvo  { get; set; }   
    }
    public class Cut
    {
        public string? fechaActa { get; set; }
        public decimal? cantidad { get; set; }
        public string? FractionType { get; set; }
    }
    public class MailApproval
    {
        public decimal? codigonumeraciones { get; set; }
        public decimal? code { get; set; }
        public string? numberradication { get; set; }
        public DateTime? filingDate { get; set; }
        public string? establishment { get; set; }
        public string? nit { get; set; }
        public string? city { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public int? amount { get; set; }
        public string? color { get; set; }
        public string? initialNumbering { get; set; }
        public string? finalNumbering { get; set; }
        public string? cantCut { get; set; }
        public string? areaCut { get; set; }
        public string? tipoPart { get; set; }
        public string? safeGuard { get; set; }
        public string? initialInternalCoding { get; set; }
        public string? finalInternalCoding { get; set; }
        public int? subtotal { get; set; }
        public int? consignmentValue { get; set; }
        public DateTime? sendDate { get; set; }
        public string? analyst { get; set; }
        public string? zoo { get; set; }
    }
        
}
