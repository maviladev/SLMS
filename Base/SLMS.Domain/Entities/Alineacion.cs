using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Alineación: Jugadores que participaron en un partido
    /// </summary>
    public class Alineacion : BaseEntity
    {
        public int PartidoId { get; set; }
        public int JugadorId { get; set; }

        /// <summary>
        /// Indica si fue titular o suplente
        /// </summary>
        public bool EsTitular { get; set; }

        /// <summary>
        /// Minuto en que entró (si es suplente)
        /// </summary>
        public int? MinutoEntrada { get; set; }

        /// <summary>
        /// Minuto en que salió (si fue sustituido)
        /// </summary>
        public int? MinutoSalida { get; set; }

        // Navegación
        public virtual Partido Partido { get; set; }
        public virtual Jugador Jugador { get; set; }
    }
}
