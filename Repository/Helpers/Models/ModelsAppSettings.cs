using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers.Models
{
    public class ModelsAppSettings
    {
        public class EstadoCupo
        {
            public decimal IdEstado { get; set; } = 0;
            public string ValorEstado { get; set; } = "";


        }

        public class EstadosCuposSettings
        {
            public EstadoCupo Enviada { get; set; } = new EstadoCupo();
            public EstadoCupo Evaluacion { get; set; } = new EstadoCupo();
            public EstadoCupo EnEstudio { get; set; } = new EstadoCupo();
            public EstadoCupo PreAprobado { get; set; } = new EstadoCupo();
            public EstadoCupo PreNegado { get; set; } = new EstadoCupo();
            public EstadoCupo Aprobado { get; set; } = new EstadoCupo();
            public EstadoCupo Negado { get; set; } = new EstadoCupo();
            public EstadoCupo Desistido { get; set; } = new EstadoCupo();
            public EstadoCupo Radicada { get; set; } = new EstadoCupo();
            public EstadoCupo AprobadoParaFirma { get; set; } = new EstadoCupo();
            public EstadoCupo EnRequerimiento { get; set; } = new EstadoCupo();

        }
    }
}
