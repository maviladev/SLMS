using Base.DTOs;
using SLMS.Application.Queries.Estadisticas;
using SLMS.Application.Queries.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Handlers.Queries
{
    /// <summary>
    /// Handler para obtener tabla de posiciones
    /// Query compleja con cálculos
    /// </summary>
    public class GetTablaPosicionesQueryHandler : IQueryHandler<GetTablaPosicionesQuery, ApiResponse<IEnumerable<TablaPosicionDto>>>
    {
        private readonly IPartidoRepository _partidoRepository;

        public GetTablaPosicionesQueryHandler(IPartidoRepository partidoRepository)
        {
            _partidoRepository = partidoRepository;
        }

        public async Task<ApiResponse<IEnumerable<TablaPosicionDto>>> Handle(
            GetTablaPosicionesQuery query,
            CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<IEnumerable<TablaPosicionDto>>();

            try
            {
                // Obtener todos los partidos finalizados del torneo
                var partidos = await _partidoRepository
                    .FindAsync(p => p.TorneoId == query.TorneoId &&
                                   p.Estado == EstadoPartido.Finalizado);

                var partidos List = partidos.ToList();

                // Obtener equipos únicos
                var equiposIds = partidosList
                    .SelectMany(p => new[] { p.LocalId, p.VisitanteId })
                    .Distinct();

                var tabla = new List<TablaPosicionDto>();

                foreach (var equipoId in equiposIds)
                {
                    var partidosLocal = partidosList.Where(p => p.LocalId == equipoId).ToList();
                    var partidosVisitante = partidosList.Where(p => p.VisitanteId == equipoId).ToList();

                    var ganados = partidosLocal.Count(p => p.GolesLocal > p.GolesVisitante) +
                                  partidosVisitante.Count(p => p.GolesVisitante > p.GolesLocal);

                    var empatados = partidosLocal.Count(p => p.GolesLocal == p.GolesVisitante) +
                                    partidosVisitante.Count(p => p.GolesVisitante == p.GolesLocal);

                    var perdidos = partidosLocal.Count(p => p.GolesLocal < p.GolesVisitante) +
                                   partidosVisitante.Count(p => p.GolesVisitante < p.GolesLocal);

                    var golesFavor = partidosLocal.Sum(p => p.GolesLocal ?? 0) +
                                     partidosVisitante.Sum(p => p.GolesVisitante ?? 0);

                    var golesContra = partidosLocal.Sum(p => p.GolesVisitante ?? 0) +
                                      partidosVisitante.Sum(p => p.GolesLocal ?? 0);

                    var puntos = ganados * 3 + empatados;

                    // Obtener datos del equipo
                    var primerPartido = partidosLocal.FirstOrDefault() ?? partidosVisitante.FirstOrDefault();
                    var equipo = equipoId == primerPartido?.LocalId ? primerPartido.Local : primerPartido?.Visitante;

                    tabla.Add(new TablaPosicionDto
                    {
                        Equipo = equipo?.Equipo?.Nombre,
                        Logo = equipo?.Equipo?.Logo,
                        PartidosJugados = ganados + empatados + perdidos,
                        Ganados = ganados,
                        Empatados = empatados,
                        Perdidos = perdidos,
                        GolesFavor = golesFavor,
                        GolesContra = golesContra,
                        DiferenciaGoles = golesFavor - golesContra,
                        Puntos = puntos
                    });
                }

                // Ordenar por puntos, diferencia de goles, goles a favor
                var tablaOrdenada = tabla
                    .OrderByDescending(t => t.Puntos)
                    .ThenByDescending(t => t.DiferenciaGoles)
                    .ThenByDescending(t => t.GolesFavor)
                    .Select((t, index) => { t.Posicion = index + 1; return t; })
                    .ToList();

                response.Success = true;
                response.Message = "Tabla de posiciones obtenida exitosamente";
                response.Data = tablaOrdenada;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener tabla de posiciones";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
