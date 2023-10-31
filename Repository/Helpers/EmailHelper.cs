using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Repository.Helpers.Models;
using System.Net;
using System.Net.Mail;

namespace Repository.Helpers
{
    public static class EmailHelper
    {
        public static void enviaEmailSMTP(string correoPara, string asunto, string body)
        {
            try
            {
                MailMessage message = new MailMessage("leon.omar82@gmail.com", correoPara, asunto, body);

                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("leon.omar82@gmail.com", "test1234");

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                // Registra o notifica cualquier otra excepción
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }

        public static async Task enviaEmailSendGridAsync(string correoPara, string nombre, string asunto, string body)
        {

            MailjetClient client = new MailjetClient("a24b22f5a7b1f8a2b86514cce373af73", "d1cf2712bbd60d8ba545ea89bc63bdf0");
            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "leon.omar82@gmail.com")
            .Property(Send.FromName, "Administrador")
            .Property(Send.Subject, asunto)
            .Property(Send.TextPart, "Creación de cuenta")
            .Property(Send.HtmlPart, "<h3>Estimado usuario,</h3><br />" + body)
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", correoPara},
                 {"Name", nombre }
                }
             });

            MailjetResponse response = await client.PostAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

        }

        public static async Task sendApprovalMail(string correoPara, string nombre, string asunto, string body, Files document)
        {
            string[] valores = document.adjuntoBase64 != null ? document.adjuntoBase64.Split("data:application/pdf;base64,") : new string[0];

            string cuerpo = "Cordial saludo,<br /><br />" +
                            "De manera atenta, remito para lo de sus competencias la numeración de unidades de<br />" +
                            "marcaje asignadas por este Ministerio para la identificación de especímenes de la fauna<br />" +
                            "silvestre y los soportes de pago respectivos, de conformidad con la normativa nacional.<br /><br />";

            MailjetClient client = new MailjetClient("a24b22f5a7b1f8a2b86514cce373af73", "d1cf2712bbd60d8ba545ea89bc63bdf0");
            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "leon.omar82@gmail.com")
            .Property(Send.FromName, "Administrador")
            .Property(Send.Subject, asunto)
            .Property(Send.TextPart, "Creación de cuenta")
            .Property(Send.HtmlPart, cuerpo + "<br />" + body + "<br />Permanecemos atentos de sus inquietudes u observaciones a través de este correo <br />" +
                            "electrónico.<br /><br />" +
                            "Atentamente,")
            .Property(
                Send.Attachments,
                new JArray {
                    new JObject {
                        {"Content-type", document.tipoAdjunto},
                        {"Filename", document.nombreAdjunto},
                        {"content", valores[1]}
                    }
                }
            )
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", correoPara},
                 {"Name", nombre }
                }
             });

            MailjetResponse response = await client.PostAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

        }

    }
}
