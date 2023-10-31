using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CupostT012RlNovedadDocumento
    {
        public decimal PkT012codigo { get; set; }
        public decimal A012codigoUsuarioCreacion { get; set; }
        public decimal? A012codigoUsuarioModificacion { get; set; }
        public decimal A012codigoNovedad { get; set; }
        public decimal A012codigoDocumento { get; set; }
        public decimal A012estadoRegistro { get; set; } 
        public DateTime A012fechaCreacion { get; set; }
        public DateTime? A012fechaModificacion { get; set; }
    }
}
