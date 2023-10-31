using API.Helpers;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Repository.Helpers;
using Repository.Helpers.Models;
using Repository.Models;
using Repository.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Persistence.Repository
{
    public class GestionDocumental : IGestionDocumental
    {
        private readonly DBContext _context;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public GestionDocumental(DBContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public Responses ConsultarDocumentos(ClaimsIdentity identity, string ipAddress, string? cadenaBusqueda)
        {
            if (String.IsNullOrEmpty(cadenaBusqueda)) { cadenaBusqueda = ""; }

            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica(StringHelper.msgNoAutorizado, "", "", true);
            }

            var reqDocs = from r in _context.AdmintT006PlantillaDocumentos
                          where r.A006nombre.Contains(cadenaBusqueda) && r.A006estadoRegistro == StringHelper.estadoActivo
                          select new
                          {
                              id = r.PkT006codigo,
                              name = r.A006nombre,
                              date = r.A006fechaModificacion,
                              estate = r.A006estadoRegistro,
                              url = r.A006plantillaUrl
                          };

            return ResponseManager.generaRespuestaGenerica("", reqDocs, token, false);

        }

        public Responses SaveDocument(ClaimsIdentity identity, SaveDocumentRequest documento)
        {
            var token = jwtAuthenticationManager.generarJWT(identity);
            if (token == null)
            {
                return ResponseManager.generaRespuestaGenerica("No Autorizado", "", "", true);
            }

            var plantilla = _context.AdmintT006PlantillaDocumentos.SingleOrDefault(x => x.PkT006codigo == documento.id);

            if (plantilla != null)
            {
                var uri = GuardarArchivoFtp(documento.document);

                plantilla.A006nombre = documento.document.attachmentName ?? "";
                plantilla.A006descripcion = documento.document.attachmentName;
                plantilla.A006plantillaUrl = uri;

                _context.Update(plantilla);
                _ = _context.SaveChanges();
            }

            return ResponseManager.generaRespuestaGenerica(StringHelper.msgGuardadoExitoso, true, token, false);
        }

        [ExcludeFromCodeCoverage]
        public Responses GetFile(ClaimsIdentity identity, string ipAddress, decimal id)
        {
            SupportDocuments docuemnto = new SupportDocuments();
            var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            string usuraio = "";
            string clave = "";
            foreach (var c in query1)
            {
                switch (c.A008descripcion)
                {
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }

            var resp = from s in _context.AdmintT006PlantillaDocumentos
                       where s.PkT006codigo == id
                       select new
                       {
                           s.A006plantillaUrl,
                           s.A006nombre
                       };


            string url = "";
            string name = "";

            if (resp != null)
            {
                var consulta = resp.FirstOrDefault();

                if (consulta != null)
                {
                    url = consulta.A006plantillaUrl;
                    name = consulta.A006nombre;

                    if (!String.IsNullOrEmpty(url))
                    {
                        bool esPdf = url.Contains(".pdf", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esDocx = url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esJpeg = url.Contains(".jpeg", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esPng = url.Contains(".png", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esJpg = url.Contains(".jpg", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esXlsx = url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

#pragma warning disable SYSLIB0014
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        request.Credentials = new NetworkCredential(usuraio, clave);

                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        Stream responseStream = response.GetResponseStream();

                        var base64 = ConvertToBase64(responseStream);

                        string tipoAdjun = "";

                        if (esPdf)
                        {
                            tipoAdjun = "application/pdf";
                        }
                        else if (esDocx)
                        {
                            tipoAdjun = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        }
                        else if (esJpeg)
                        {
                            tipoAdjun = "image/jpeg";
                        }
                        else if (esPng)
                        {
                            tipoAdjun = "image/png";
                        }
                        else if (esJpg)
                        {
                            tipoAdjun = "image/jpg";
                        }
                        else if (esXlsx)
                        {
                            tipoAdjun = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        }

                        docuemnto.code = id;
                        docuemnto.base64Attachment = "data:" + tipoAdjun + ";base64," + base64;
                        docuemnto.attachmentName = name;
                        docuemnto.attachmentType = tipoAdjun;

                        return ResponseManager.generaRespuestaGenerica("", docuemnto, "", false);
                    }

                    docuemnto.code = id;
                    docuemnto.base64Attachment = "";
                    docuemnto.attachmentName = name;
                    docuemnto.attachmentType = "";
                }
            }

            return ResponseManager.generaRespuestaGenerica("No existe ningun documento", docuemnto, "", true);
        }

        public string ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public string GuardarArchivoFtp(SupportDocuments documento)
        {
            var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            string urlAdjjunto = "";
            string Puerto = "";
            string usuraio = "";
            string clave = "";
            foreach (var c in query1)
            {
                switch (c.A008descripcion)
                {
                    case "URL":
                        urlAdjjunto = c.A008valor;
                        break;
                    case "PUERTO":
                        Puerto = c.A008valor;
                        break;
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }
            Stream? requestStream = null;

            string eliminar = "data:" + documento.attachmentType + ";base64,";
            string SinData = documento.base64Attachment?.Replace(eliminar, String.Empty) ?? "";

            byte[] buffer = Convert.FromBase64String(SinData);

            string uri = "ftp://" + urlAdjjunto + ":" + Puerto + "/CUPOS/docs/" + documento.attachmentName;

#pragma warning disable SYSLIB0014
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.ContentLength = buffer.Length;
            request.EnableSsl = false;
            request.Credentials = new NetworkCredential(usuraio, clave);

            requestStream = request.GetRequestStream();

            requestStream.Write(buffer, 0, buffer.Length);

#pragma warning disable S2589
            if (requestStream != null)
                requestStream.Close();

            return uri;
        }

        [ExcludeFromCodeCoverage]
        public Responses ReadDocument(ClaimsIdentity identity, string ipAddress, decimal id)
        {
            SupportDocuments docuemnto = new SupportDocuments();
            var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            string usuraio = "";
            string clave = "";
            foreach (var c in query1)
            {
                switch (c.A008descripcion)
                {
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }

            var resp = from s in _context.AdmintT006PlantillaDocumentos
                       where s.PkT006codigo == id
                       select new
                       {
                           s.A006plantillaUrl,
                           s.A006nombre
                       };

            string url = "";
            string name = "";

            if (resp != null)
            {
                var consulta = resp.FirstOrDefault();

                if (consulta != null)
                {
                    url = consulta.A006plantillaUrl;
                    name = consulta.A006nombre ?? "";

                    if (!String.IsNullOrEmpty(url))
                    {
                        bool esDocx = url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esXlsx = url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

                        #pragma warning disable SYSLIB0014
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        request.Credentials = new NetworkCredential(usuraio, clave);

                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        Stream responseStream = response.GetResponseStream();

                        if (esDocx)
                        {
                            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(responseStream, false);
                            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
                            string contentXML = body.InnerXml;
                            var textoFinal = new StringBuilder();
                            textoFinal.Append("<p>");
                            bool encontro = true;
                            bool encontrofila = false;
                            bool table = false;

                            while (encontro)
                            {
                                int Fin = contentXML.IndexOf(">");
                                string valTabla = contentXML.Substring(0, Fin);
                                string cadenaTemp = "";

                                if (valTabla.Contains("<w:tbl"))
                                {
                                    table = true;
                                    encontrofila = true;
                                    textoFinal.Append("<table border='2'>");
                                }

                                if (table)
                                {
                                    int posIniT = contentXML.IndexOf("<w:tr");
                                    if (posIniT == -1)
                                    {
                                        textoFinal.Append("</table>");
                                        table = false;
                                        contentXML = contentXML.Substring(contentXML.IndexOf("</w:tbl>") + 8);
                                    }
                                    else
                                    {
                                        cadenaTemp = contentXML.Substring(posIniT);
                                        int posFinT = cadenaTemp.IndexOf(">");
                                        if (cadenaTemp[posFinT - 1] != '/')
                                        {
                                            posFinT = cadenaTemp.IndexOf("</w:tr>");
                                            cadenaTemp = cadenaTemp.Substring(0, posFinT);
                                            textoFinal.Append("<tr>");
                                            bool encontrocelda = true;

                                            while (encontrocelda)
                                            {
                                                int posIniC = cadenaTemp.IndexOf("<w:tc");
                                                if (posIniC == -1)
                                                    encontrocelda = false;
                                                else
                                                {
                                                    cadenaTemp = cadenaTemp.Substring(posIniC);
                                                    int posFinC = cadenaTemp.IndexOf(">");
                                                    if (cadenaTemp[posFinC - 1] != '/')
                                                    {
                                                        posFinC = cadenaTemp.IndexOf("</w:tc>");
                                                        string cadenaTempCelda = cadenaTemp.Substring(0, posFinC);
                                                        int inicioSpan = cadenaTempCelda.IndexOf("<w:gridSpan");

                                                        if (inicioSpan == -1)
                                                        {
                                                            textoFinal.Append("<td>");
                                                        }
                                                        else
                                                        {
                                                            string valorSpan = cadenaTempCelda.Substring(inicioSpan);
                                                            int finSpan = valorSpan.IndexOf(">");
                                                            string valSp = valorSpan.Substring(0, finSpan);
                                                            int valSpan = valSp.IndexOf("w:val=");
                                                            string spanGrid = valSp.Substring(valSpan + 7);
                                                            int valSpanFin = spanGrid.IndexOf('"');
                                                            string valorSpanGrid = spanGrid.Substring(0, valSpanFin);
                                                            textoFinal.Append("<td colspan='" + valorSpanGrid + "'>");
                                                        }

                                                        bool encontroText = true;

                                                        while (encontroText)
                                                        {
                                                            int posIni = cadenaTempCelda.IndexOf("<w:p");
                                                            if (posIni == -1)
                                                                encontroText = false;
                                                            else
                                                            {
                                                                cadenaTempCelda = cadenaTempCelda.Substring(posIni);
                                                                int posFin = cadenaTempCelda.IndexOf(">");
                                                                string estiloParrafo = "";
                                                                string cadenaTempCeldaP = "";

                                                                if (cadenaTempCelda[posFin - 1] != '/')
                                                                {
                                                                    posFin = cadenaTempCelda.IndexOf("</w:p>");
                                                                    if (posFin == -1)
                                                                    {
                                                                        encontroText = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        cadenaTempCeldaP = cadenaTempCelda.Substring(0, posFin);
                                                                        cadenaTempCeldaP = cadenaTempCeldaP.Replace(" xml:space=\"preserve\"", "");
                                                                        cadenaTempCeldaP = cadenaTempCeldaP.Replace("<w:rPr>", "");
                                                                        cadenaTempCeldaP = cadenaTempCeldaP.Replace("</w:rPr>", "");
                                                                        if (cadenaTempCeldaP.Contains("<w:pPr>"))
                                                                        {
                                                                            estiloParrafo = cadenaTempCeldaP.Substring(cadenaTempCeldaP.IndexOf("<w:pPr"));
                                                                            estiloParrafo = cadenaTempCeldaP.Substring(0, cadenaTempCeldaP.IndexOf("</w:pPr>"));
                                                                        }
                                                                        else
                                                                        {
                                                                            estiloParrafo = "";
                                                                        }
                                                                        while (cadenaTempCeldaP.IndexOf("<w:r") != -1)
                                                                        {
                                                                            int posini1 = cadenaTempCeldaP.IndexOf("<w:t>");
                                                                            if (posini1 != -1)
                                                                            {
                                                                                string texto = cadenaTempCeldaP.Substring(cadenaTempCeldaP.IndexOf("<w:t>") + 5);
                                                                                int posfin1 = texto.IndexOf("</w:t>");
                                                                                texto = texto.Substring(0, posfin1);

                                                                                string estilo = cadenaTempCeldaP.Substring(cadenaTempCeldaP.IndexOf("<w:r"), cadenaTempCeldaP.IndexOf("<w:t>") - cadenaTempCeldaP.IndexOf("<w:r")) + estiloParrafo;
                                                                                cadenaTempCeldaP = cadenaTempCeldaP.Replace("<w:t>" + texto + "</w:t>", "");
                                                                                if (estilo.Contains("<w:i />"))
                                                                                    texto = "<span style='font-style:italic'>" + texto + "</span>";
                                                                                if (estilo.Contains("<w:b />"))
                                                                                    texto = "<b>" + texto + "</b>";
                                                                                if (estilo.Contains("<w:jc w:val=\"center\" />"))
                                                                                    texto = "<center>" + texto + "</center>";
                                                                                textoFinal.Append("" + texto);
                                                                            }
                                                                            cadenaTempCeldaP = cadenaTempCeldaP.Substring(cadenaTempCeldaP.IndexOf("<w:r") + 4);
                                                                        }
                                                                        cadenaTempCelda = cadenaTempCelda.Substring(posFin);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    cadenaTempCelda = cadenaTempCelda.Substring(0, posIni) + cadenaTempCelda.Substring(posFin);
                                                                }
                                                                int valParrafo = cadenaTempCelda.IndexOf("<w:p ");
                                                                if (valParrafo != -1)
                                                                {
                                                                    textoFinal.Append("<br/>");
                                                                }
                                                            }
                                                        }
                                                        textoFinal.Append("</td>");
                                                        cadenaTemp = cadenaTemp.Substring(cadenaTemp.IndexOf("</w:tc>") + 7);
                                                    }
                                                    else
                                                    {
                                                        cadenaTemp = cadenaTemp.Substring(0, posIniC) + cadenaTemp.Substring(posIniC + posFinC);
                                                    }
                                                }
                                            }
                                            textoFinal.Append("</tr>");
                                            contentXML = contentXML.Substring(posFinT) + 7;
                                        }
                                        else
                                        {
                                            contentXML = contentXML.Substring(posFinT) + 7;
                                        }
                                    }
                                }
                                else
                                {
                                    int posIni = contentXML.IndexOf("<w:p");
                                    if (posIni == -1)
                                        encontro = false;
                                    else
                                    {
                                        cadenaTemp = contentXML.Substring(posIni);
                                        int posFin = cadenaTemp.IndexOf(">");
                                        string estiloParrafo = "";
                                        if (cadenaTemp[posFin - 1] != '/')
                                        {
                                            posFin = cadenaTemp.IndexOf("</w:p>");
                                            cadenaTemp = cadenaTemp.Substring(0, posFin);
                                            cadenaTemp = cadenaTemp.Replace(" xml:space=\"preserve\"", "");
                                            cadenaTemp = cadenaTemp.Replace("<w:rPr>", "");
                                            cadenaTemp = cadenaTemp.Replace("</w:rPr>", "");
                                            if (cadenaTemp.Contains("<w:pPr>"))
                                            {
                                                estiloParrafo = cadenaTemp.Substring(cadenaTemp.IndexOf("<w:pPr>"));
                                                estiloParrafo = cadenaTemp.Substring(0, cadenaTemp.IndexOf("</w:pPr>"));
                                            }
                                            else
                                            {
                                                estiloParrafo = "";
                                            }
                                            while (cadenaTemp.IndexOf("<w:r") != -1)
                                            {
                                                int posini1 = cadenaTemp.IndexOf("<w:t>");
                                                if (posini1 != -1)
                                                {
                                                    string texto = cadenaTemp.Substring(cadenaTemp.IndexOf("<w:t>") + 5);
                                                    int posfin1 = texto.IndexOf("</w:t>");
                                                    texto = texto.Substring(0, posfin1);

                                                    string estilo = cadenaTemp.Substring(cadenaTemp.IndexOf("<w:r"), cadenaTemp.IndexOf("<w:t>") - cadenaTemp.IndexOf("<w:r")) + estiloParrafo;
                                                    cadenaTemp = cadenaTemp.Replace("<w:t>" + texto + "</w:t>", "");
                                                    if (estilo.Contains("<w:i />"))
                                                        texto = "<span style='font-style:italic'>" + texto + "</span>";
                                                    if (estilo.Contains("<w:b />"))
                                                        texto = "<b>" + texto + "</b>";
                                                    if (estilo.Contains("<w:jc w:val=\"center\" />"))
                                                        texto = "<center>" + texto + "</center>";
                                                    textoFinal.Append("" + texto);

                                                }
                                                cadenaTemp = cadenaTemp.Substring(cadenaTemp.IndexOf("<w:r") + 4);
                                            }
                                            textoFinal = textoFinal.Append("<br/>");
                                            contentXML = contentXML.Substring(0, posIni) + contentXML.Substring(contentXML.IndexOf("</w:p>") + 6);
                                        }
                                        else
                                        {
                                            contentXML = contentXML.Substring(0, posIni) + contentXML.Substring(posIni + posFin);
                                            textoFinal.Append("<br/>");
                                        }
                                    }
                                }
                            }
                            textoFinal = textoFinal.Append("</p>");
                            return ResponseManager.generaRespuestaGenerica("", textoFinal, "", false);
                        }
                        if (esXlsx)
                        {
                            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(responseStream, false);
                            WorkbookPart wbPart = spreadsheetDocument.WorkbookPart;
                            WorksheetPart worksheetPart = wbPart.WorksheetParts.First();
                            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                            int filamax = 1;
                            int columnamax = 1;
                            foreach (Row r in sheetData.Elements<Row>())
                            {
                                foreach (DocumentFormat.OpenXml.Spreadsheet.Cell celda in r.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                                {

                                    string referencia = celda.CellReference;
                                    int fila = 0;
                                    int columna = 0;
                                    for (int h = 0; h < referencia.Length; h++)
                                    {
                                        if (referencia[h] >= '0' && referencia[h] <= '9')
                                            fila = fila * 10 + (referencia[h] - 48);
                                        if (referencia[h] >= 'A' && referencia[h] <= 'Z')
                                            columna = (h * 26) + ((referencia[h] - 'A') + 1);
                                    }
                                    if (fila > filamax)
                                        filamax = fila;
                                    if (columna > columnamax)
                                        columnamax = columna;
                                }
                            }

                            string[,] info = new string[filamax, columnamax];
                            for (int i = 0; i < filamax; i++)
                            {
                                for (int j = 0; j < columnamax; j++)
                                {
                                    info[i, j] = "";
                                }
                            }

                            string xmlsheet = sheetData.InnerXml;
                            string text;
                            foreach (Row r in sheetData.Elements<Row>())
                            {
                                string xml = r.InnerXml;
                                foreach (DocumentFormat.OpenXml.Spreadsheet.Cell celda in r.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                                {
                                    string referencia = celda.CellReference;
                                    int fila = 0;
                                    int columna = 0;
                                    for (int j = 0; j < referencia.Length; j++)
                                    {
                                        if (referencia[j] >= '0' && referencia[j] <= '9')
                                            fila = fila * 10 + (referencia[j] - 48);
                                        if (referencia[j] >= 'A' && referencia[j] <= 'Z')
                                            columna = (j * 26) + ((referencia[j] - 'A') + 1);
                                    }
                                    fila--;
                                    columna--;
                                    if (columna < filamax)
                                    {
                                        text = GetCellValue(spreadsheetDocument, celda);
                                        info[fila, columna] = text;
                                    }
                                }
                            }

                            spreadsheetDocument.Close();
                        }

                        return ResponseManager.generaRespuestaGenerica("", docuemnto, "", false);
                    }
                }
            }

            return ResponseManager.generaRespuestaGenerica("No existe ningun documento", docuemnto, "", true);
        }

        public static string GetCellValue(SpreadsheetDocument document, DocumentFormat.OpenXml.Spreadsheet.Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        [ExcludeFromCodeCoverage]
        public Responses updateDocument(ClaimsIdentity identity, string ipAddress, UpdateDocument datosDoc)
        {
            var query1 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
            string usuraio = "";
            string clave = "";
            foreach (var c in query1)
            {
                switch (c.A008descripcion)
                {
                    case "USUARIO":
                        usuraio = c.A008valor;
                        break;
                    case "CONTRASEÑA":
                        clave = c.A008valor;
                        break;
                }
            }

            var resp = from s in _context.AdmintT006PlantillaDocumentos
                       where s.PkT006codigo == datosDoc.id
                       select new
                       {
                           s.A006plantillaUrl,
                           s.A006nombre
                       };

            string url = "";
            string name = "";

            if (resp != null)
            {
                var consulta = resp.FirstOrDefault();
                if (consulta != null)
                {
                    url = consulta.A006plantillaUrl;
                    name = consulta.A006nombre;

                    if (!string.IsNullOrEmpty(url))
                    {
                        bool esDocx = url.Contains(".docx", System.StringComparison.CurrentCultureIgnoreCase);
                        bool esXlsx = url.Contains(".xlsx", System.StringComparison.CurrentCultureIgnoreCase);

#pragma warning disable SYSLIB0014
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        request.Credentials = new NetworkCredential(usuraio, clave);

                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        Stream responseStream = response.GetResponseStream();


                        if (esDocx)
                        {
                            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(responseStream, false);

                            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
                            string contentXML = body.InnerXml;
                            var textoNuevo = new StringBuilder();
                            bool encontro = true;
                            bool table = false;
                            string Cambios = datosDoc.documentChanges ?? "";

                            while (encontro)
                            {
                                int Fin = contentXML.IndexOf(">") + 1;
                                string valTabla = contentXML.Substring(0, Fin);
                                string cadenaTemp = "";
                                string cadTemp = "";

                                if (valTabla.Contains("<w:tbl"))
                                {
                                    table = true;
                                    textoNuevo.Append(valTabla);
                                    contentXML = contentXML.Substring(Fin);

                                    int FinEstTabla = contentXML.IndexOf("</w:tblGrid>") + 12;
                                    string estTabla = contentXML.Substring(0, FinEstTabla);
                                    textoNuevo.Append(estTabla);
                                    contentXML = contentXML.Substring(FinEstTabla);

                                    int TablaHTML = Cambios.IndexOf("<table");
                                    Cambios = Cambios.Substring(TablaHTML);
                                    int finTablaHTML = Cambios.IndexOf(">") + 1;
                                    Cambios = Cambios.Substring(finTablaHTML);
                                }

                                if (table)
                                {
                                    int posIniT = contentXML.IndexOf("<w:tr");
                                    int posiniT = Cambios.IndexOf("<tr");

                                    if (posIniT == -1)
                                    {
                                        textoNuevo.Append("</w:tbl>");
                                        table = false;
                                        contentXML = contentXML.Substring(contentXML.IndexOf("</w:tbl>") + 8);
                                    }
                                    else
                                    {
                                        cadTemp = Cambios.Substring(posiniT);
                                        int posfinT = Cambios.IndexOf("</tr>") + 6;
                                        cadTemp = Cambios.Substring(0, posfinT);
                                        Cambios = Cambios.Substring(posfinT);

                                        cadenaTemp = contentXML.Substring(posIniT);
                                        int posFinT = cadenaTemp.IndexOf(">");
                                        if (cadenaTemp[posFinT - 1] != '/')
                                        {
                                            textoNuevo.Append(cadenaTemp.Substring(0, posFinT + 1));
                                            cadenaTemp += cadenaTemp.Substring(posFinT + 1);
                                            posFinT = cadenaTemp.IndexOf("</w:tr>") + 7;
                                            cadenaTemp = cadenaTemp.Substring(0, posFinT);
                                            int prIniColumna = cadenaTemp.IndexOf("<w:trPr>");
                                            int prFinColumna = cadenaTemp.IndexOf("</w:trPr>") + 9;
                                            textoNuevo.Append(cadenaTemp.Substring(prIniColumna, prFinColumna - prIniColumna));
                                            bool encontrocelda = true;
                                            while (encontrocelda)
                                            {
                                                int posIniC = cadenaTemp.IndexOf("<w:tc");
                                                int posiniC = cadTemp.IndexOf("<td");
                                                if (posIniC == -1)
                                                    encontrocelda = false;
                                                else
                                                {
                                                    cadenaTemp = cadenaTemp.Substring(posIniC);
                                                    int posFinC = cadenaTemp.IndexOf(">");
                                                    if (cadenaTemp[posFinC - 1] != '/')
                                                    {
                                                        posFinC = cadenaTemp.IndexOf("</w:tc>") + 7;
                                                        int posfinC = cadTemp.IndexOf("</td>") + 5;
                                                        string cadenaTempCelda = cadenaTemp.Substring(0, posFinC);
                                                        string cadTempCelda = cadTemp.Substring(0, posfinC);
                                                        int prFinCelda = cadenaTempCelda.IndexOf("</w:tcPr>") + 9;
                                                        textoNuevo.Append(cadenaTempCelda.Substring(0, prFinCelda));
                                                        bool encontroText = true;

                                                        while (encontroText)
                                                        {
                                                            int posIni = cadenaTempCelda.IndexOf("<w:p");
                                                            if (posIni == -1)
                                                                encontroText = false;
                                                            else
                                                            {
                                                                cadenaTempCelda = cadenaTempCelda.Substring(posIni);
                                                                int posFin = cadenaTempCelda.IndexOf(">");
                                                                string cadenaTempCeldaP = "";

                                                                if (cadenaTempCelda[posFin - 1] != '/')
                                                                {
                                                                    posFin = cadenaTempCelda.IndexOf("</w:p>");
                                                                    if (posFin == -1)
                                                                    {
                                                                        encontroText = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        int iniText = cadTemp.IndexOf(">") + 1;
                                                                        int finText = cadTemp.IndexOf("<br/>");

                                                                        if (finText == -1)
                                                                            finText = cadTemp.IndexOf("</td>");

                                                                        string textTemp = cadTemp.Substring(iniText, finText);
                                                                        cadenaTempCeldaP = cadenaTempCelda.Substring(0, posFin);
                                                                        int IniEstilo = cadenaTempCeldaP.IndexOf("<w:rPr");
                                                                        string nuevoP = cadenaTempCeldaP.Substring(0, cadenaTempCeldaP.IndexOf("<w:pPr>") + 7);
                                                                        int FinEstilo = cadenaTempCeldaP.IndexOf("</w:rPr>") + 8;
                                                                        string Estilo = cadenaTempCeldaP.Substring(IniEstilo, FinEstilo - IniEstilo);
                                                                        string estiloParrafo = cadenaTempCeldaP.Substring(cadenaTempCeldaP.IndexOf("<w:rPr>"), (cadenaTempCeldaP.IndexOf("</w:rPr>") + 8) - cadenaTempCeldaP.IndexOf("<w:rPr>"));
                                                                        _ = estiloParrafo.Replace("<w:i />", "");
                                                                        _ = estiloParrafo.Replace("<w:b /><w:bCs />", "");
                                                                        _ = Estilo.Replace("<w:i />", "");
                                                                        _ = Estilo.Replace("<w:b /><w:bCs />", "");
                                                                        nuevoP += estiloParrafo + "</w:pPr>";
                                                                        cadenaTempCeldaP = cadenaTempCeldaP.Substring(FinEstilo);

                                                                        while (cadenaTempCeldaP.IndexOf("<w:r") != -1 /*|| cadenaTempCeldaP.IndexOf("<w:r>") != -1*/)
                                                                        {
                                                                            int iniR = cadenaTempCeldaP.IndexOf("<w:r");

                                                                            if (cadenaTempCeldaP[iniR + 4] == ' ' || cadenaTempCeldaP[iniR + 4] == '>')
                                                                            {
                                                                                string cadenaR = cadenaTempCeldaP.Substring(iniR);
                                                                                int finR = cadenaR.IndexOf(">") + 1;
                                                                                string R = cadenaR.Substring(0, finR);

                                                                                nuevoP += R;

                                                                                int posini1 = cadenaTempCeldaP.IndexOf("<w:t>");
                                                                                if (posini1 != -1)
                                                                                {
                                                                                    if (textTemp.Contains("<span style='font-style:italic'>"))
                                                                                        Estilo = Estilo.Substring(0, Estilo.IndexOf("/>") + 2) + "<w:i />" + Estilo.Substring(Estilo.IndexOf("/>") + 2);
                                                                                    if (textTemp.Contains("<b"))
                                                                                        Estilo = Estilo.Substring(0, Estilo.IndexOf("/>") + 2) + "<w:b /><w:bCs />" + Estilo.Substring(Estilo.IndexOf("/>") + 2);
                                                                                    if (textTemp.Contains("<center>"))
                                                                                        nuevoP = nuevoP.Substring(0, nuevoP.IndexOf("<w:pPr>") + 7) + "<w:jc w:val=\"center\" />" + nuevoP.Substring(nuevoP.IndexOf("<w:pPr>") + 7);

                                                                                    textTemp = textTemp.Replace("<span style='font-style:italic'>", "");
                                                                                    textTemp = textTemp.Replace("<b>", "");
                                                                                    textTemp = textTemp.Replace("<center>", "");
                                                                                    textTemp = textTemp.Replace("</b>", "");
                                                                                    textTemp = textTemp.Replace("</center>", "");

                                                                                    int iniTextT = textTemp.IndexOf("<td");
                                                                                    textTemp = textTemp.Substring(iniTextT + 1);
                                                                                    int finTextT = textTemp.IndexOf(">");
                                                                                    textTemp = textTemp.Substring(finTextT + 1);
                                                                                    int TextT = textTemp.IndexOf("<");

                                                                                    if (TextT != -1)
                                                                                        textTemp = textTemp.Substring(0, TextT);

                                                                                    string parrafoTemp = nuevoP + Estilo;
                                                                                    if (textTemp == "&nbsp;")
                                                                                        textTemp = " ";
                                                                                    nuevoP = parrafoTemp + "<w:t>" + textTemp + "</w:t>";
                                                                                    textTemp = "";
                                                                                }
                                                                                nuevoP += "</w:r>";
                                                                            }
                                                                            cadenaTempCeldaP = cadenaTempCeldaP.Substring(cadenaTempCeldaP.IndexOf("<w:r") + 4);
                                                                        }
                                                                        textoNuevo.Append(nuevoP + "</w:p>");
                                                                        cadenaTempCelda = cadenaTempCelda.Substring(posFin);
                                                                        cadTemp = cadTemp.Substring(finText);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    cadenaTempCelda = cadenaTempCelda.Substring(0, posIni) + cadenaTempCelda.Substring(posFin);
                                                                }
                                                            }
                                                        }
                                                        textoNuevo.Append("</w:tc>");
                                                        cadenaTemp = cadenaTemp.Substring(cadenaTemp.IndexOf("</w:tc>") + 7);
                                                    }
                                                    else
                                                    {
                                                        cadenaTemp = cadenaTemp.Substring(0, posIniC) + cadenaTemp.Substring(posIniC + posFinC);
                                                    }
                                                }
                                            }
                                            textoNuevo.Append("</w:tr>");
                                            contentXML = contentXML.Substring(posFinT);
                                        }
                                        else
                                        {
                                            contentXML = contentXML.Substring(posFinT);
                                        }
                                    }
                                }
                                else
                                {
                                    int posIni = contentXML.IndexOf("<w:p ");
                                    if (posIni == -1)
                                        encontro = false;
                                    else
                                    {
                                        string Temp = contentXML.Substring(posIni);
                                        int fin = Temp.IndexOf(">");

                                        if (Temp[fin - 1] != '/')
                                        {
                                            int posFin = contentXML.IndexOf("</w:p>");

                                            if (posFin == -1)
                                                encontro = false;
                                            else
                                            {
                                                textoNuevo.Append(contentXML.Substring(posIni, posFin + 6));
                                                contentXML = contentXML.Substring(posFin + 6);
                                            }
                                        }
                                        else
                                        {
                                            textoNuevo.Append(contentXML.Substring(posIni, (fin + 1) - posIni));
                                            contentXML = contentXML.Substring(fin + 1);
                                        }
                                    }
                                }
                            }

                            int iniSect = contentXML.IndexOf("<w:sectPr");

                            if (iniSect != -1)
                            {
                                string sect = contentXML.Substring(iniSect);
                                int finSect = contentXML.IndexOf("</w:sectPr>");

                                if (finSect != -1)
                                {
                                    textoNuevo.Append(contentXML.Substring(iniSect, (finSect + 11) - iniSect));
                                }
                            }

                            wordprocessingDocument.Close();

                            var query3 = _context.AdmintT008Parametricas.Where(s => s.A008parametrica == "CARPETA").FirstOrDefault();

                            if (query3 != null)
                            {
                                CreateWordprocessingDocument(query3.A008valor, Convert.ToString(textoNuevo));

                                var query2 = _context.AdmintT008Parametricas.Where(p => p.A008parametrica == "SERVIDOR FTP").ToList();
                                string usuraio1 = "";
                                string clave1 = "";
                                foreach (var c in query2)
                                {
                                    switch (c.A008descripcion)
                                    {
                                        case "USUARIO":
                                            usuraio1 = c.A008valor;
                                            break;
                                        case "CONTRASEÑA":
                                            clave1 = c.A008valor;
                                            break;
                                    }
                                }

                                string uri = url;

                                #pragma warning disable SYSLIB0014
                                FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create(uri);
                                request1.Method = WebRequestMethods.Ftp.UploadFile;
                                request1.EnableSsl = false;
                                request1.Credentials = new NetworkCredential(usuraio1, clave1);

                                FileStream stream = File.OpenRead(query3.A008valor);
                                byte[] buffer = new byte[stream.Length];
                                stream.Read(buffer, 0, buffer.Length);
                                stream.Close();
                                Stream reqStream = request1.GetRequestStream();
                                reqStream.Write(buffer, 0, buffer.Length);
                                reqStream.Flush();
                                reqStream.Close();

                                return ResponseManager.generaRespuestaGenerica("El documento fue actualizado correctamente", "", "", false);
                            }
                            else
                            {
                                return ResponseManager.generaRespuestaGenerica("El documento no fue encontrado", "", "", true);
                            }
                        }
                        if (esXlsx)
                        {
                            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(responseStream, false);
                            WorkbookPart wbPart = spreadsheetDocument.WorkbookPart;
                            WorksheetPart worksheetPart = wbPart.WorksheetParts.First();
                            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                            int filamax = 1;
                            foreach (Row r in sheetData.Elements<Row>())
                            {
                                foreach (DocumentFormat.OpenXml.Spreadsheet.Cell celda in r.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                                {

                                    string referencia = celda.CellReference;
                                    int fila = 0;
                                    for (int h = 0; h < referencia.Length; h++)
                                    {
                                        if (referencia[h] >= '0' && referencia[h] <= '9')
                                            fila = fila * 10 + (referencia[h] - 48);
                                    }
                                    if (fila > filamax)
                                        filamax = fila;
                                }
                            }
                        }
                    }
                }
            }
            return ResponseManager.generaRespuestaGenerica("No existe ningun documento", "", "", true);
        }

        public static void CreateWordprocessingDocument(string filepath, string texto)
        {
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                body.InnerXml = texto;
            }
        }
    }
}