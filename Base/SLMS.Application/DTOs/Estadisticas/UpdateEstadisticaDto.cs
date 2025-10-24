using System.ComponentModel.DataAnnotations;


namespace SLMS.Application.DTOs.Estadisticas
{
    public class UpdateEstadisticaDto
    {
        public int? TipoId { get; set; }

        [Range(0, 120)]
        public int? Minuto { get; set; }
        public int? JugadorId { get; set; }
        public int? PartidoId { get; set; }
    }
}
