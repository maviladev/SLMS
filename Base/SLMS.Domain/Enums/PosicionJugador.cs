using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Enums
{
    /// <summary>
    /// Posiciones de jugadores en el campo
    /// </summary>
    public enum PosicionJugador
    {
        /// <summary>
        /// Portero / Arquero
        /// </summary>
        Portero = 1,

        /// <summary>
        /// Defensa (lateral, central, líbero)
        /// </summary>
        Defensa = 2,

        /// <summary>
        /// Mediocampista / Medio
        /// </summary>
        Mediocampista = 3,

        /// <summary>
        /// Delantero / Atacante
        /// </summary>
        Delantero = 4
    }
}
