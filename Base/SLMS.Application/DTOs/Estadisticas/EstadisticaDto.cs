using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Estadisticas
{
    public class EstadisticaDto
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int Minuto { get; set; }
        public int JugadorId { get; set; }
        public string JugadorNombre { get; set; }
        public int PartidoId { get; set; }
    }
}
