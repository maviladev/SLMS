using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Partido entre dos equipos
    /// Combina programación y resultado
    /// </summary>
    public class Partido : BaseEntity
    {
        /// <summary>
        /// ID del torneo
        /// </summary>
        public int TorneoId { get; set; }

        /// <summary>
        /// ID del equipo local (EquipoTorneo)
        /// </summary>
        public int LocalId { get; set; }

        /// <summary>
        /// ID del equipo visitante (EquipoTorneo)
        /// </summary>
        public int VisitanteId { get; set; }

        /// <summary>
        /// Número de jornada
        /// </summary>
        public int Jornada { get; set; }

        /// <summary>
        /// Fecha y hora del partido
        /// </summary>
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Estadio donde se juega
        /// </summary>
        public string Estadio { get; set; }

        /// <summary>
        /// Estado del partido
        /// </summary>
        public bool Estado { get; set; }

        // Resultado
        /// <summary>
        /// Goles del equipo local
        /// </summary>
        public int? GolesLocal { get; set; }

        /// <summary>
        /// Goles del equipo visitante
        /// </summary>
        public int? GolesVisitante { get; set; }

        /// <summary>
        /// Asistencia de público
        /// </summary>
        public int? AsistenciaPublico { get; set; }

        // Árbitros
        public string ArbitroPrincipal { get; set; }
        public string Arbitro1 { get; set; }
        public string Arbitro2 { get; set; }
        public string CuartoArbitro { get; set; }

        // Navegación
        public virtual Torneo Torneo { get; set; }
        public virtual EquipoTorneo Local { get; set; }
        public virtual EquipoTorneo Visitante { get; set; }
        public virtual ICollection<Estadistica> Estadisticas { get; set; }
        public virtual ICollection<Alineacion> Alineaciones { get; set; }

        public Partido()
        {
            Estado = true;//EstadoPartido.Programado;
            Estadisticas = new HashSet<Estadistica>();
            Alineaciones = new HashSet<Alineacion>();
        }
    }
}
