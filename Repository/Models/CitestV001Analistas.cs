using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class CitestV001Analistas
    {
        public decimal PkV001Codigo { get; set; }  
        public string? A001PrimerNombre { get; set; }
        public string? A001PrimerApellido { get; set; }
        public string? A001Descripcion { get; set; }
        public int A001Asignados { get; set; }  
    }
}
