using SLMS.Domain.Entities.Base;
using SLMS.Domain.Enums;

namespace SLMS.Domain.Entities
{
    /// <summary>
    /// Usuario del sistema (autenticación con Google OAuth)
    /// </summary>
    public class Usuario : BaseEntityWithState
    {
        /// <summary>
        /// Email del usuario (único)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// ID de Google del usuario (único)
        /// </summary>
        public string GoogleId { get; set; }

        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>
        /// URL de la foto de perfil
        /// </summary>
        public string FotoPerfil { get; set; }

        /// <summary>
        /// ID del rol del usuario
        /// </summary>
        public int RolUsuarioId { get; set; }

        // Navegación
        /// <summary>
        /// Rol asignado al usuario
        /// </summary>
        public virtual RolUsuario RolUsuario { get; set; }
    }

    /// <summary>
    /// Roles del sistema (Admin, Operador, Consultor)
    /// </summary>
    public class RolUsuario : BaseEntity
    {
        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción del rol
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Tipo de rol (enum)
        /// </summary>
        public RolUsuarioEnum Tipo { get; set; }

        // Navegación
        /// <summary>
        /// Usuarios con este rol
        /// </summary>
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public RolUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }
    }
}
