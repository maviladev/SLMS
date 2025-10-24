using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Enums
{
    /// <summary>
    /// Tipos de castigos/sanciones
    /// </summary>
    public enum TipoCastigoEnum
    {
        /// <summary>
        /// Suspensión por partidos
        /// </summary>
        Suspension = 1,

        /// <summary>
        /// Multa económica
        /// </summary>
        Multa = 2,

        /// <summary>
        /// Amonestación verbal/escrita
        /// </summary>
        Amonestacion = 3
    }
}
