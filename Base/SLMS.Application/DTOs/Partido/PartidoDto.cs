using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Partido
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public string LocalNombre { get; set; }
        public int VisitanteId { get; set; }
        public string VisitanteNombre { get; set; }
        public int Numero { get; set; }
        public int TorneoId { get; set; }
        public string TorneoNombre { get; set; }
    }
}
