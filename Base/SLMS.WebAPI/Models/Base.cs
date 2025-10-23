namespace SLMS.WebAPI.Models.Base
{
    /// <summary>
    /// Entidad base con auditoría completa y soft delete
    /// Principio: DRY (Don't Repeat Yourself)
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        // Auditoría
        public DateTime Creado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime Modificado { get; set; }
        public string ModificadoPor { get; set; }

        // Soft Delete
        public bool Eliminado { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public string EliminadoPor { get; set; }
    }

    /// <summary>
    /// Entidad base con estado
    /// </summary>
    public abstract class BaseEntityWithState : BaseEntity
    {
        public EstadoEntidad Estado { get; set; }
    }

    public enum EstadoEntidad
    {
        Activo = 1,
        Inactivo = 2,
        Suspendido = 3
    }

    public enum EstadoPartido
    {
        Programado = 1,
        EnJuego = 2,
        Finalizado = 3,
        Cancelado = 4,
        Pospuesto = 5
    }

    public enum PosicionJugador
    {
        Portero = 1,
        Defensa = 2,
        Mediocampista = 3,
        Delantero = 4,
        DirectorTecnico = 5,
        Arbitro = 6
    }

    public enum RolUsuarioEnum
    {
        Administrador = 1,
        Operador = 2,
        Consultor = 3
    }

    public enum TipoEstadisticaEnum
    {
        Gol = 1,
        TarjetaAmarilla = 2,
        TarjetaRoja = 3,
        Asistencia = 4,
        AutoGol = 5,
        Falta = 6
    }
}