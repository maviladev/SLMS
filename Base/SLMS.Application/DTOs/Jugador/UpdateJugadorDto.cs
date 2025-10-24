using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Jugador
{
    public class UpdateJugadorDto
    {
        public int? EquipoId { get; set; }
        public int? TorneoId { get; set; }
        public int? LigaId { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public DateTime? Nacimiento { get; set; }
        public bool? Estado { get; set; }
        public int? RolId { get; set; }
    }
}
