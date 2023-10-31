using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsultCitesPermitController : Controller
    {
        // Placeholder data for demonstration purposes
        private static readonly List<PermitData> PermitDatabase = new List<PermitData>()
        {
            // Sample permit data
            new PermitData
            {
                SalvoconductoNumber = "ABC123",
                IdentificationNumber = 123456789,
                Authority = "Environmental Authority",
                IssuanceDate = "2023-07-05",
                DestinationDepartment = "Destination Department",
                OriginDepartment = "Origin Department",
                OriginMunicipality = "Origin Municipality",
                PermitType = "Permit Type",
                InternalNumber = 12345,
                Species = "Species",
                Specimens = "Specimens",
                ScientificName = "Scientific Name",
                CommonName = "Common Name",
                ProductType = "Product Type",
                UnitOfMeasure = "Unit of Measure",
                AvailableQuantity = 10,
                Description = "Additional description",
                ValidityFrom = "2023-07-01",
                ValidityTo = "2023-07-31",
                PermitHolderName = "Permit Holder Name",
                PermitHolderIdentification = 123456789,
                PermitHolderMunicipality = "Permit Holder Municipality",
                ResourceClass = "Resource Class",
                RouteDepartment = "Departure and Arrival Departments",
                RouteMunicipality = "Departure and Arrival Municipalities",
                TransportationMedium = "Transportation Medium",
                TransportationType = "Transportation Type",
                TransportationIdentification = 987654321,
                Transporter = "Transporter",
                TransporterIdentification = 987654321
            },
            new PermitData
            {
                SalvoconductoNumber = "DEF456",
                IdentificationNumber = 987654321,
                Authority = "Environmental Authority 2",
                IssuanceDate = "2023-07-10",
                DestinationDepartment = "Destination Department 2",
                OriginDepartment = "Origin Department 2",
                OriginMunicipality = "Origin Municipality 2",
                PermitType = "Permit Type 2",
                InternalNumber = 54321,
                Species = "Species 2",
                Specimens = "Specimens 2",
                ScientificName = "Scientific Name 2",
                CommonName = "Common Name 2",
                ProductType = "Product Type 2",
                UnitOfMeasure = "Unit of Measure 2",
                AvailableQuantity = 5,
                Description = "Additional description 2",
                ValidityFrom = "2023-07-15",
                ValidityTo = "2023-07-31",
                PermitHolderName = "Permit Holder Name 2",
                PermitHolderIdentification = 987654321,
                PermitHolderMunicipality = "Permit Holder Municipality 2",
                ResourceClass = "Resource Class 2",
                RouteDepartment = "Departure and Arrival Departments 2",
                RouteMunicipality = "Departure and Arrival Municipalities 2",
                TransportationMedium = "Transportation Medium 2",
                TransportationType = "Transportation Type 2",
                TransportationIdentification = 123456789,
                Transporter = "Transporter 2",
                TransporterIdentification = 123456789
            }
        };
        [AllowAnonymous]
        // POST: /ConsultCitesPermit
        [HttpPost]
        public IActionResult ConsultCitesPermit(string salvoconductoNumber, int identificationNumber)
        {
            // Perform lookup based on salvoconductoNumber and identificationNumber
            var permitData = PermitDatabase.FirstOrDefault(p =>
                p.SalvoconductoNumber == salvoconductoNumber &&
                p.IdentificationNumber == identificationNumber);

            if (permitData == null)
            {
                // No matching record found
                return NotFound();
            }

            // Return the permit data
            return Json(permitData);
        }

        // Class representing permit data
        public class PermitData
        {
            public string SalvoconductoNumber { get; set; } = "";
            public int IdentificationNumber { get; set; }
            public string Authority { get; set; } = "";
            public string IssuanceDate { get; set; } = "";
            public string DestinationDepartment { get; set; } = "";
            public string OriginDepartment { get; set; } = "";
            public string OriginMunicipality { get; set; } = "";
            public string PermitType { get; set; } = "";
            public int InternalNumber { get; set; }
            public string Species { get; set; } = "";
            public string Specimens { get; set; } = "";
            public string ScientificName { get; set; } = "";
            public string CommonName { get; set; } = "";
            public string ProductType { get; set; } = "";
            public string UnitOfMeasure { get; set; } = "";
            public int AvailableQuantity { get; set; } = 0;
            public string Description { get; set; } = "";
            public string ValidityFrom { get; set; } = "";
            public string ValidityTo { get; set; } = "";
            public string PermitHolderName { get; set; } = "";
            public int PermitHolderIdentification { get; set; }
            public string PermitHolderMunicipality { get; set; } = "";
            public string ResourceClass { get; set; } = "";
            public string RouteDepartment { get; set; } = "";
            public string RouteMunicipality { get; set; } = "";
            public string TransportationMedium { get; set; } = "";
            public string TransportationType { get; set; } = "";
            public int TransportationIdentification { get; set; }
            public string Transporter { get; set; } = "";
            public int TransporterIdentification { get; set; }
        }
    }
}
