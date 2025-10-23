using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Entities.Base
{
    internal interface IAuditable
    {
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
