using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Partido
{
    public class UpdatePartidoDto
    {
        public int? LocalId { get; set; }
        public int? VisitanteId { get; set; }
        public int? Numero { get; set; }
        public int? TorneoId { get; set; }
    }
}
