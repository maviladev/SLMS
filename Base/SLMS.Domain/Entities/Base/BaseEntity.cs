namespace SLMS.Domain.Entities.Base
{
    /// <summary>
    /// Entidad base con auditoría completa y soft delete
    /// Principio SOLID: DRY (Don't Repeat Yourself)
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador único de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha y hora de creación (UTC)
        /// </summary>
        public DateTime Creado { get; set; }

        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string CreadoPor { get; set; }

        /// <summary>
        /// Fecha y hora de última modificación (UTC)
        /// </summary>
        public DateTime Modificado { get; set; }

        /// <summary>
        /// Usuario que modificó el registro por última vez
        /// </summary>
        public string ModificadoPor { get; set; }

        /// <summary>
        /// Indica si el registro está eliminado (Soft Delete)
        /// </summary>
        public bool Eliminado { get; set; }

        /// <summary>
        /// Fecha y hora de eliminación (UTC)
        /// </summary>
        public DateTime? FechaEliminacion { get; set; }

        /// <summary>
        /// Usuario que eliminó el registro
        /// </summary>
        public string EliminadoPor { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        protected BaseEntity()
        {
            Creado = DateTime.UtcNow;
            Modificado = DateTime.UtcNow;
            Eliminado = false;
            CreadoPor = "Sistema";
            ModificadoPor = "Sistema";
        }
    }

    /// <summary>
    /// Entidad base con campo Estado adicional
    /// </summary>
    public abstract class BaseEntityWithState : BaseEntity
    {
        /// <summary>
        /// Estado de la entidad (Activo, Inactivo, Suspendido)
        /// </summary>
        public bool Estado { get; set; }

        protected BaseEntityWithState()
        {
            Estado = true;
        }
    }
}
