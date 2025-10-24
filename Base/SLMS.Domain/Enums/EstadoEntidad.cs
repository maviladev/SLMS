using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Enums
{
    /// <summary>
    /// Estado general de las entidades
    /// </summary>
    public enum EstadoEntidad
    {
        /// <summary>
        /// Entidad activa y disponible
        /// </summary>
        Activo = 1,

        /// <summary>
        /// Entidad inactiva temporalmente
        /// </summary>
        Inactivo = 2,

        /// <summary>
        /// Entidad suspendida por alguna razón
        /// </summary>
        Suspendido = 3
    }
}
