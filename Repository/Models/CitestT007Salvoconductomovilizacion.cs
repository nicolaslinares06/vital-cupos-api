using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CitestT007Salvoconductomovilizacion
    {
        public decimal PkT007codigo { get; set; }
        public decimal A007codigoCertificado { get; set; }
        public decimal A007codigoPersonaTransportador { get; set; }
        public decimal A007codigoPersonaSalvoconducto { get; set; }
        public decimal A007codigoParametricaTipoVehiculo { get; set; }
        public decimal A007codigoParametricaTipoRuta { get; set; }
        public string A007codigoParametricaMedioTransporte { get; set; } = null!;
        public decimal A007codigoUsuarioCreacion { get; set; }
        public decimal? A007codigoUsuarioModificacion { get; set; }
        public decimal A007codigoCiudad { get; set; }
        public decimal A007estadoRegistro { get; set; } 
        public string A007empresaTransportadora { get; set; } = null!;
        public DateTime A007fechaCreacion { get; set; }
        public DateTime? A007fechaModificacion { get; set; }
        public DateTime A007fechaVencimientoSalvoconducto { get; set; }
        public string A007finalidadMovilizacion { get; set; } = null!;
        public decimal A007numeroSalvoconducto { get; set; }
        public string A007placaVehiculo { get; set; } = null!;

        public virtual CitestT001Certificado A007codigoCertificadoNavigation { get; set; } = null!;
        public virtual AdmintT004Ciudad A007codigoCiudadNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A007codigoParametricaTipoRutaNavigation { get; set; } = null!;
        public virtual AdmintT008Parametrica A007codigoParametricaTipoVehiculoNavigation { get; set; } = null!;
        public virtual CitestT003Persona A007codigoPersonaSalvoconductoNavigation { get; set; } = null!;
        public virtual CitestT003Persona A007codigoPersonaTransportadorNavigation { get; set; } = null!;
    }
}
