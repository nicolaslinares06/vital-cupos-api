using Repository.Helpers.Models;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Client.Resources;
using Repository.Persistence.Repository;

namespace TestUnit.Helpers.Models
{
    public class EmailHelperTest
    {
        [Fact]
        public void EnviarEmailSendGridAsyncPrecintos()
        {
            string correoPara = "";
            string nombre = "";
            string asunto = "";
            string body = "";

            var r = EmailHelper.enviaEmailSendGridAsync(correoPara, nombre, asunto, body);
            Assert.True(r != null);
        }

        [Fact]
        public void sendApprovalMail()
        {
            string correoPara = "";
            string nombre = "";
            string asunto = "";
            string body = "";

            var ejemploFiles = new Files
            {
                codigo = 1.23m,
                adjuntoBase64 = "Base64DelAdjuntoEjemplo",
                nombreAdjunto = "NombreDelAdjuntoEjemplo",
                tipoAdjunto = "TipoDelAdjuntoEjemplo"
            };


            var r = EmailHelper.sendApprovalMail(correoPara, nombre, asunto, body, ejemploFiles);
            Assert.True(r != null);
        }
    }
}
