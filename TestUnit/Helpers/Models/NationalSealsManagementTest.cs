using Repository.Helpers;
using Repository.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.Helpers.Models
{
    public class NationalSealsManagementTest
    {
        [Fact]
        public void SaveRequestSeals()
        {
            SaveRequestSeals datos = new SaveRequestSeals();
            datos.code = 0;
            datos.amount = 0;
            datos.speciesSubspecies = 0;
            datos.initialCode = 0;
            datos.finalCode = 0;
            datos.lengthMinor = 0;
            datos.lengthGreater = 0;
            datos.consignMentdate = DateTime.Now;
            datos.observations = "";
            datos.response = null;
            datos.consignment = true;
            datos.documentAttached = null;
            datos.documentSupportConsignment = null;
            datos.documentSupport = null;

            var type = Assert.IsType<SaveRequestSeals>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void NitEmpresa()
        {
            NitEmpresa datos = new NitEmpresa();
            datos.a001nit = 0;
            
            var type = Assert.IsType<NitEmpresa>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void ConsultCodes()
        {
            ConsultCodes datos = new ConsultCodes();
            datos.A019Cantidad = 0;

            var type = Assert.IsType<ConsultCodes>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void CodigosInternos()
        {
            CodigosInternos datos = new CodigosInternos();
            datos.inicial = 0;
            datos.final = 0;
            datos.carta = "";
            datos.resolucion = 0;

            var type = Assert.IsType<CodigosInternos>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void Especies()
        {
            Especies datos = new Especies();
            datos.pkT005Codigo = 0;
            datos.a005Nombre = "";

            var type = Assert.IsType<Especies>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void CodigosInternosCreation()
        {
            // Arrange
            var codigosInternos = new CodigosInternos
            {
                inicial = 1,
                final = 2,
                valorConsignacion = 100.0m,
                subtotal = 50.0m,
                numeroCupos = 10,
                carta = "Carta123",
                resolucion = 3,
                zoocriadero = "Zoocriadero123",
            };

            // Act
            var type = Assert.IsType<CodigosInternos>(codigosInternos);
            Assert.NotNull(type);
        }

        [Fact]
        public void TableMinCreation()
        {
            // Arrange
            var safeGuaList = new List<SafeGua>
        {
            new SafeGua { codigoSalvo = 1 },
            new SafeGua { codigoSalvo = 2 },
        };

            var cutList = new List<Cut>
        {
            new Cut { fechaActa = "2023-01-01", cantidad = 10, FractionType = "TypeA" },
            new Cut { fechaActa = "2023-02-01", cantidad = 5, FractionType = "TypeB" },
        };

            var tableMin = new TableMin
            {
                listGuar = safeGuaList,
                corte = cutList,
            };

            // Assert
            Assert.All(tableMin.listGuar, item => Assert.IsType<SafeGua>(item));
        }

        [Fact]
        public void MailApprovalCreation()
        {
            // Arrange
            var mailApproval = new MailApproval
            {
                codigonumeraciones = 1,
                code = 2,
                numberradication = "Radication123",
                filingDate = DateTime.Now,
                establishment = "Establishment123",
                nit = "Nit123",
                city = "City123",
                address = "Address123",
                phone = "Phone123",
                amount = 10,
                color = "Color123",
                initialNumbering = "Initial123",
                finalNumbering = "Final123",
                cantCut = "CantCut123",
                areaCut = "AreaCut123",
                tipoPart = "TipoPart123",
                safeGuard = "SafeGuard123",
                initialInternalCoding = "InitialCoding123",
                finalInternalCoding = "FinalCoding123",
                subtotal = 5,
                consignmentValue = 100,
                sendDate = DateTime.Now,
                analyst = "Analyst123",
                zoo = "Zoo123",
            };

            // Act
            var type = Assert.IsType<MailApproval>(mailApproval);
            Assert.NotNull(type);
        }
    }
}
