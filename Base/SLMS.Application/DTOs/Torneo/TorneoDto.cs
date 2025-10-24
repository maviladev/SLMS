using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Torneo
{
    public class TorneoDto
    {
        public int Id { get; set; }
        public int LigaId { get; set; }
        public string LigaNombre { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
    }
}
