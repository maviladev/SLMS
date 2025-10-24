using SLMS.Domain.Entities.Base;

namespace SLMS.Domain.Entities
{
    /// Liga deportiva (ej: Liga MX, MLS, La Liga)
    /// </summary>
    public class Liga : BaseEntityWithState
    {
        /// <summary>
        /// Nombre de la liga
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// URL del logo de la liga
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Descripción de la liga
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// País de la liga
        /// </summary>
        public string Pais { get; set; }

        // Navegación
        /// <summary>
        /// Torneos de esta liga
        /// </summary>
        public virtual ICollection<Torneo> Torneos { get; set; }

        /// <summary>
        /// Equipos que pertenecen a esta liga
        /// </summary>
        public virtual ICollection<Equipo> Equipos { get; set; }

        public Liga()
        {
            Torneos = new HashSet<Torneo>();
            Equipos = new HashSet<Equipo>();
        }
    }
}
