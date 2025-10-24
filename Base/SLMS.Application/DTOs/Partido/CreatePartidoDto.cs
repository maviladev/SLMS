using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Partido
{
    public class CreatePartidoDto
    {
        [Required]
        public int LocalId { get; set; }

        [Required]
        public int VisitanteId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int TorneoId { get; set; }
    }
}
