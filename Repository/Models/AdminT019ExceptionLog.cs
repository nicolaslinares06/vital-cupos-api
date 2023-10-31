using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class AdminT019ExceptionLog
    {
        public decimal PkT019codigo { get; set; }
        public DateTime A019Timestamp { get; set; }
        public string? A019Mensaje { get; set; }
        public string? A019Detalles { get; set; }
        public string? A019Fuente { get; set; }
        public string? A019Tipo { get; set; }
        public string? A019StackTrace { get; set; }
        public string? A019Modulo { get; set; }
    }
}