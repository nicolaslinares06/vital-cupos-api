using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Repository.Persistence.IRepository
{
    public interface INonTimberFloraCertificate
    {
        /// <summary>
        /// consultar autoridades
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultAuthority(ClaimsIdentity identity);

        /// <summary>
        /// consultar tipos productos
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEspecimensProductsType(ClaimsIdentity identity);

        /// <summary>
        /// consultar tipos de documentos
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultDocumentsTypes(ClaimsIdentity identity);

        /// <summary>
        /// consultar certificados
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultCertificates(ClaimsIdentity identity);

        /// <summary>
        /// consultar datos de entidad
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <returns></returns>
        public Responses ConsultEntityDates(ClaimsIdentity identity, decimal documentType, decimal nitBussines);

        /// <summary>
        /// consultar certificados por nit, numero resolucion o ambas
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="documentType"></param>
        /// <param name="nitBussines"></param>
        /// <param name="CertificateNumber"></param>
        /// <returns></returns>
        public Responses ConsultCertificatesForNit(ClaimsIdentity identity, decimal documentType, decimal nitBussines, string CertificateNumber);

        /// <summary>
        /// consultar tipos de especimenes
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Responses ConsultEspecimensTypes(ClaimsIdentity identity);

        /// <summary>
        /// guardar certificado nuevo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datosGuardar"></param>
        /// <returns></returns>
        public Responses SaveCertificate(ClaimsIdentity identity, CertificateData datosGuardar, string ipAddress);

        /// <summary>
        /// consultar un certificado por codigo
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeCertificate"></param>
        /// <returns></returns>
        public Responses ConsultDatasCertificate(ClaimsIdentity identity, decimal codeCertificate);

        /// <summary>
        /// editar certificado
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="datosGuardar"></param>
        /// <returns></returns>
        public Responses SaveEditCertificate(ClaimsIdentity identity, CertificateData datosGuardar);

        /// <summary>
        /// geshabilitar certificado
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="codeCertificate"></param>
        /// <returns></returns>
        public Responses DeleteCertificate(ClaimsIdentity identity, decimal codeCertificate);
    }
}
