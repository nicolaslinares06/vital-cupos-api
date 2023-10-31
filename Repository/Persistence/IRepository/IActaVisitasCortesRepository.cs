using Repository.Helpers;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Repository.Helpers.Models.ActaVisitaCortesModel;

namespace Repository.Persistence.IRepository
{
    public interface IActaVisitasCortesRepository
    {
        Task<Responses> CrearActaVisita(ClaimsIdentity identity, CupostT007ActaVisitum actaVisitaCorte, string ipAddress);
        Task<Responses> CrearActaVisitaIrregular(ClaimsIdentity identity, CupostT007ActaVisitum actaVisitaCorteIrregular);
        Task<Responses> InsertarTipoPielIdentificable(ClaimsIdentity identity, CupostT008CortePiel tipoCortePielIdentificable, string ipAddress);
        Task<Responses> InsertarTipoPielIrregular(ClaimsIdentity identity, CupostT008CortePiel tipoPielIrregular, string ipAddress);
        Task<Responses> InsertarTipoParteIrregular(ClaimsIdentity identity, CupostT008CortePiel tipoParteIrregular, string ipAddress);
        Task<Responses> InsertTipoParteIdentificable(ClaimsIdentity identity, CupostT008CortePiel tipoParteIdentificable, string ipAddress);
        Task<Responses> ConsultarEstablecimientoPorTipo(ClaimsIdentity identity);
        Task<Responses> ConsultarEmpresas(ClaimsIdentity identity);
        Task<Responses> InsertarActaVisitaDocumentoorigenPiel(ClaimsIdentity identity, CupostT015ActaVisitaDocumentoOrigenPiel documentoOrigenPiel, string ipAddress);
        Task<Responses> InsertarActaVisitaResolucionNumero(ClaimsIdentity identity, CupostT016ActaVisitaResolucion documentoResolucion, string ipAddress);
        Task<Responses> InsertarActaVisitaSAlvoConductoNumero(ClaimsIdentity identity, CupostT017ActaVisitaSalvoConducto documentoSalvoConducto, string ipAddress);
        Task<Responses> ConsultarActasEstablecimientosPorId(ClaimsIdentity identity, decimal idEstablecimento);
        Task<Responses> ConsultarActaVisitaporId(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarDocumentosOrigenPiel(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarDocumentosSavoConductos(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarDocumentosResolucion(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarTipoPielidentificablelActaVisita(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarTipoParteIdentificable(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarTipoPielIrregularActaVisita(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarTipoParteIrregular(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> EliminarDocumentosOrigenPiel(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<Responses> EliminarDocResolucionActaVisita(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<Responses> EliminarDocsSalvoConductos(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<Responses> EliminarTiposPielIdentificables(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<Responses> EliminarTiposPartesIdentificables(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<Responses> ActualizarActaVisita(ClaimsIdentity identity, CupostT007ActaVisitum actaVisitaIdentificable, string ipAddress);
        Task<Responses> DesHabilitarActaVisita(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<List<AdmintT008Parametrica>> ObtenerDatosFTP();
        Task<Responses> InsertarActaVisitaDocumento(ClaimsIdentity identity, CupostT018ActaVisitaDocumentos archivoActaVisita, string ipAddress);
        Task<Responses> EliminarArchivosActaVisita(ClaimsIdentity identity, decimal idActaVisita, string ipAddress);
        Task<Responses> ConsultarArchivosActaVisita(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarActasEstablecimientosPorTipo(ClaimsIdentity identity, VisitRecordsSearch criterios);
        Task<Responses> ConsultarCiudadesPorDepartamento(ClaimsIdentity identity, decimal departamentoId);
        Task<Responses> ConsultarDepartamentos(ClaimsIdentity identity);
        Task<Responses> ConsultarEmpresaPorNit(ClaimsIdentity identity, decimal nit);
        Task<Responses> ConsultarArchivoPrecintoActaVisita(ClaimsIdentity identity, decimal idActaVisita);
        Task<Responses> ConsultarEstablecimientosPorTipo(ClaimsIdentity identity, decimal tipoEstablecimiento);
        Task<Responses> ValidarDatosExcelPrecintos(ClaimsIdentity identity, string base64Excel, decimal nitEmpresa);
        Task<bool> ModificarCantidadCupoPrecintos(ClaimsIdentity identity, string excelPrecintosBase64, decimal idEmpresa);
        Task<bool> IncrementarCantidadCupoPrecintos(decimal idActavisita);
        Task<Responses> ActualizarNumeroPrecinto(ClaimsIdentity identity, string excelPrecintosBase64, decimal idEmpresa, decimal estado);
        Task<Responses> ConsultarTiposPartesLista(ClaimsIdentity identity);
        Task<Responses> IngresarSalvoConductosExcel(ClaimsIdentity identity, string excelPrecintosBase64, decimal idActaVisita);
    }
}
