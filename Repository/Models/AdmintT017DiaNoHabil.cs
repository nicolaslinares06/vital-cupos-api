using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT017DiaNoHabil
    {
        public int PkT017Codigo { get; set; }
        public int A017anio { get; set; }
        public DateTime A017fechaNoHabil { get; set; }
        public decimal A017codigoUsuarioCreacion { get; set; }
        public decimal? A017codigoUsuarioModificacion { get; set; }
        public DateTime A017fechaCreacion { get; set; }
        public DateTime? A017fechaModificacion { get; set; }
    }
}
