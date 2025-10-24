using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Torneo/Temporada (ej: Apertura 2025, Clausura 2025)
    /// </summary>
    public class Torneo : BaseEntityWithState
    {
        /// <summary>
        /// ID de la liga a la que pertenece
        /// </summary>
        public int LigaId { get; set; }

        /// <summary>
        /// Nombre del torneo
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// URL del logo del torneo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Fecha de inicio del torneo
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de finalización del torneo
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Número de jornadas del torneo
        /// </summary>
        public int NumeroJornadas { get; set; }

        // Navegación
        /// <summary>
        /// Liga a la que pertenece el torneo
        /// </summary>
        public virtual Liga Liga { get; set; }

        /// <summary>
        /// Equipos inscritos en este torneo
        /// </summary>
        public virtual ICollection<EquipoTorneo> EquiposTorneo { get; set; }

        /// <summary>
        /// Partidos de este torneo
        /// </summary>
        public virtual ICollection<Partido> Partidos { get; set; }

        public Torneo()
        {
            EquiposTorneo = new HashSet<EquipoTorneo>();
            Partidos = new HashSet<Partido>();
            NumeroJornadas = 17; // Valor por defecto
        }
    }
}
