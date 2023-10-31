using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class AdmintT006PlantillaDocumento
    {
        public AdmintT006PlantillaDocumento()
        {
            AdmintT009Documentos = new HashSet<AdmintT009Documento>();
        }

        public decimal PkT006codigo { get; set; }
        public decimal? A006codigoUsuarioCreacion { get; set; }
        public decimal? A006codigoUsuarioModificacion { get; set; }
        public decimal? A006estadoRegistro { get; set; }
        public DateTime? A006fechaCreacion { get; set; }
        public DateTime? A006fechaModificacion { get; set; }
        public string A006nombre { get; set; } = "";
        public string? A006descripcion { get; set; }
        public string? A006plantillaUrl { get; set; }

        public virtual ICollection<AdmintT009Documento> AdmintT009Documentos { get; set; }
    }
}
