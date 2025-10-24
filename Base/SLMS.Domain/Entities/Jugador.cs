using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Jugador inscrito en un equipo para un torneo específico
    /// </summary>
    public class Jugador : BaseEntityWithState
    {
        /// <summary>
        /// ID de la inscripción equipo-torneo
        /// </summary>
        public int EquipoTorneoId { get; set; }

        /// <summary>
        /// Nombre(s) del jugador
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos del jugador
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Nombre completo (calculado)
        /// </summary>
        public string NombreCompleto => $"{Nombre} {Apellidos}";

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Edad calculada automáticamente
        /// </summary>
        public int Edad
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        /// <summary>
        /// Nacionalidad del jugador
        /// </summary>
        public string Nacionalidad { get; set; }

        /// <summary>
        /// Número de camiseta
        /// </summary>
        public int NumeroCamiseta { get; set; }

        /// <summary>
        /// Posición en el campo
        /// </summary>
        public string Posicion { get; set; }

        /// <summary>
        /// URL de la foto del jugador
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Altura en metros (ej: 1.75)
        /// </summary>
        public decimal? Altura { get; set; }

        /// <summary>
        /// Peso en kilogramos (ej: 75.5)
        /// </summary>
        public decimal? Peso { get; set; }

        // Navegación
        /// <summary>
        /// Inscripción del equipo en el torneo
        /// </summary>
        public virtual EquipoTorneo EquipoTorneo { get; set; }

        /// <summary>
        /// Estadísticas del jugador
        /// </summary>
        public virtual ICollection<Estadistica> Estadisticas { get; set; }

        /// <summary>
        /// Castigos del jugador
        /// </summary>
        public virtual ICollection<Castigo> Castigos { get; set; }

        /// <summary>
        /// Alineaciones en las que participó
        /// </summary>
        public virtual ICollection<Alineacion> Alineaciones { get; set; }

        public Jugador()
        {
            Estadisticas = new HashSet<Estadistica>();
            Castigos = new HashSet<Castigo>();
            Alineaciones = new HashSet<Alineacion>();
        }
    }
}
