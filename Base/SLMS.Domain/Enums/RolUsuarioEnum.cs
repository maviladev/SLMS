

namespace SLMS.Domain.Enums
{
    /// <summary>
    /// Roles de usuarios del sistema
    /// </summary>
    public enum RolUsuarioEnum
    {
        /// <summary>
        /// Administrador con acceso completo
        /// </summary>
        Administrador = 1,

        /// <summary>
        /// Operador que gestiona ligas y torneos
        /// </summary>
        Operador = 2,

        /// <summary>
        /// Consultor con acceso de solo lectura
        /// </summary>
        Consultor = 3
    }
}
