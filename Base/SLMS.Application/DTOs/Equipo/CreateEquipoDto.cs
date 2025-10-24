using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Equipo
{
    public class CreateEquipoDto
    {
        [Required]
        public int TorneoId { get; set; }

        [Required]
        public int LigaId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
    }
}
