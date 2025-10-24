using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Equipo de fútbol (ej: América, Chivas, Cruz Azul)
    /// Un equipo puede participar en múltiples torneos
    /// </summary>
    public class Equipo : BaseEntityWithState
    {
        /// <summary>
        /// ID de la liga a la que pertenece el equipo
        /// </summary>
        public int LigaId { get; set; }

        /// <summary>
        /// Nombre completo del equipo
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Nombre corto o abreviatura
        /// </summary>
        public string NombreCorto { get; set; }

        /// <summary>
        /// URL del logo del equipo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Nombre del estadio
        /// </summary>
        public string Estadio { get; set; }

        /// <summary>
        /// Ciudad del equipo
        /// </summary>
        public string Ciudad { get; set; }

        /// <summary>
        /// Año de fundación del equipo
        /// </summary>
        public int AñoFundacion { get; set; }

        /// <summary>
        /// Color principal del uniforme
        /// </summary>
        public string ColorPrincipal { get; set; }

        /// <summary>
        /// Color secundario del uniforme
        /// </summary>
        public string ColorSecundario { get; set; }

        // Navegación
        /// <summary>
        /// Liga a la que pertenece
        /// </summary>
        public virtual Liga Liga { get; set; }

        /// <summary>
        /// Inscripciones del equipo en torneos
        /// </summary>
        public virtual ICollection<EquipoTorneo> EquiposTorneo { get; set; }

        public Equipo()
        {
            EquiposTorneo = new HashSet<EquipoTorneo>();
        }
    }
}
