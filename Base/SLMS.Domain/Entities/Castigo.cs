using SLMS.Domain.Enums;
using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Castigo/Sanción a un jugador
    /// </summary>
    public class Castigo : BaseEntity
    {
        public int JugadorId { get; set; }

        /// <summary>
        /// Tipo de castigo
        /// </summary>
        public TipoCastigoEnum Tipo { get; set; }

        /// <summary>
        /// Número de partidos de suspensión
        /// </summary>
        public int PartidosSuspension { get; set; }

        /// <summary>
        /// Motivo del castigo
        /// </summary>
        public string Motivo { get; set; }

        /// <summary>
        /// Fecha de inicio del castigo
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Indica si el castigo está activo
        /// </summary>
        public bool Activo { get; set; }

        // Navegación
        public virtual Jugador Jugador { get; set; }

        public Castigo()
        {
            Activo = true;
            FechaInicio = DateTime.UtcNow;
        }
    }
}
