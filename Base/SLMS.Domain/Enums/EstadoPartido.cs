using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Enums
{
    /// <summary>
    /// Estados de un partido
    /// </summary>
    public enum EstadoPartido
    {
        /// <summary>
        /// Partido programado, aún no iniciado
        /// </summary>
        Programado = 1,

        /// <summary>
        /// Partido en curso
        /// </summary>
        EnJuego = 2,

        /// <summary>
        /// Partido finalizado
        /// </summary>
        Finalizado = 3,

        /// <summary>
        /// Partido cancelado
        /// </summary>
        Cancelado = 4,

        /// <summary>
        /// Partido pospuesto para otra fecha
        /// </summary>
        Pospuesto = 5
    }
}
