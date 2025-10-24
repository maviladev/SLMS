using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Liga
{
    // Liga DTOs
    public class LigaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
    }
}
