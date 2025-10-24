using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Enums
{
    /// <summary>
    /// Tipos de estadísticas/eventos en un partido
    /// </summary>
    public enum TipoEstadisticaEnum
    {
        /// <summary>
        /// Gol anotado
        /// </summary>
        Gol = 1,

        /// <summary>
        /// Tarjeta amarilla (amonestación)
        /// </summary>
        TarjetaAmarilla = 2,

        /// <summary>
        /// Tarjeta roja (expulsión)
        /// </summary>
        TarjetaRoja = 3,

        /// <summary>
        /// Asistencia para gol
        /// </summary>
        Asistencia = 4,

        /// <summary>
        /// Gol en propia meta
        /// </summary>
        AutoGol = 5,

        /// <summary>
        /// Falta cometida
        /// </summary>
        Falta = 6
    }
}
