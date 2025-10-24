using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Jugador
{
    public class CreateJugadorDto
    {
        [Required]
        public int EquipoId { get; set; }

        [Required]
        public int TorneoId { get; set; }

        [Required]
        public int LigaId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public DateTime Nacimiento { get; set; }

        [Required]
        public int RolId { get; set; }
    }
}
