using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Estadisticas
{
    public class CreateEstadisticaDto
    {
        [Required]
        public int TipoId { get; set; }

        [Required]
        [Range(0, 120)]
        public int Minuto { get; set; }

        [Required]
        public int JugadorId { get; set; }

        [Required]
        public int PartidoId { get; set; }
    }
}
