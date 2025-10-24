using SLMS.Domain.Entities.Base;
using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Evento/Estadística durante un partido
    /// (Gol, tarjeta, asistencia, etc.)
    /// </summary>
    public class Estadistica : BaseEntity
    {
        public int PartidoId { get; set; }
        public int JugadorId { get; set; }

        /// <summary>
        /// Tipo de evento
        /// </summary>
        public TipoEstadisticaEnum Tipo { get; set; }

        /// <summary>
        /// Minuto en que ocurrió
        /// </summary>
        public int Minuto { get; set; }

        /// <summary>
        /// Descripción adicional del evento
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// ID del jugador que dio la asistencia (para goles)
        /// </summary>
        public int? JugadorAsistenteId { get; set; }

        // Navegación
        public virtual Partido Partido { get; set; }
        public virtual Jugador Jugador { get; set; }
        public virtual Jugador JugadorAsistente { get; set; }
    }
}
