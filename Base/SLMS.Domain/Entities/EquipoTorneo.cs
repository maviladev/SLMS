using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Inscripción de un equipo en un torneo específico
    /// Tabla intermedia: Equipo (*) ←→ (*) Torneo
    /// </summary>
    public class EquipoTorneo : BaseEntity
    {
        /// <summary>
        /// ID del equipo inscrito
        /// </summary>
        public int EquipoId { get; set; }

        /// <summary>
        /// ID del torneo
        /// </summary>
        public int TorneoId { get; set; }

        /// <summary>
        /// Fecha de inscripción
        /// </summary>
        public DateTime FechaInscripcion { get; set; }

        /// <summary>
        /// Nombre del director técnico para este torneo
        /// </summary>
        public string DirectorTecnico { get; set; }

        // Navegación
        /// <summary>
        /// Equipo inscrito
        /// </summary>
        public virtual Equipo Equipo { get; set; }

        /// <summary>
        /// Torneo en el que participa
        /// </summary>
        public virtual Torneo Torneo { get; set; }

        /// <summary>
        /// Jugadores del equipo en este torneo
        /// </summary>
        public virtual ICollection<Jugador> Jugadores { get; set; }

        /// <summary>
        /// Partidos como equipo local
        /// </summary>
        public virtual ICollection<Partido> PartidosLocal { get; set; }

        /// <summary>
        /// Partidos como equipo visitante
        /// </summary>
        public virtual ICollection<Partido> PartidosVisitante { get; set; }

        public EquipoTorneo()
        {
            FechaInscripcion = DateTime.UtcNow;
            Jugadores = new HashSet<Jugador>();
            PartidosLocal = new HashSet<Partido>();
            PartidosVisitante = new HashSet<Partido>();
        }
    }
}
