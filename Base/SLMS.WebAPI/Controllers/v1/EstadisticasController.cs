using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SLMS.WebAPI.Controllers
{
    /// <summary>
    /// Estadísticas y reportes
    /// </summary>
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiVersion("1.0")]
    public class EstadisticasController : BaseController
    {
        private readonly IQueryHandler<GetTablaPosicionesQuery, ApiResponse<IEnumerable<TablaPosicionDto>>> _tablaPosicionesHandler;
        private readonly IQueryHandler<GetGolesJugadorQuery, ApiResponse<EstadisticasJugadorDto>> _golesJugadorHandler;

        public EstadisticasController(
            IQueryHandler<GetTablaPosicionesQuery, ApiResponse<IEnumerable<TablaPosicionDto>>> tablaPosicionesHandler,
            IQueryHandler<GetGolesJugadorQuery, ApiResponse<EstadisticasJugadorDto>> golesJugadorHandler)
        {
            _tablaPosicionesHandler = tablaPosicionesHandler;
            _golesJugadorHandler = golesJugadorHandler;
        }

        /// <summary>
        /// Obtiene la tabla de posiciones de un torneo
        /// </summary>
        /// <param name="torneoId">ID del torneo</param>
        [HttpGet("tabla-posiciones/{torneoId:int}")]
        [AllowAnonymous] // Estadísticas públicas
        [ResponseCache(Duration = 300)] // Cache de 5 minutos
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TablaPosicionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTablaPosiciones(int torneoId)
        {
            var query = new GetTablaPosicionesQuery { TorneoId = torneoId };
            var response = await _tablaPosicionesHandler.Handle(query);
            return HandleResponse(response);
        }

        /// <summary>
        /// Obtiene estadísticas de goles de un jugador
        /// </summary>
        [HttpGet("goles-jugador/{jugadorId:int}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 180)]
        [ProducesResponseType(typeof(ApiResponse<EstadisticasJugadorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGolesJugador(int jugadorId, [FromQuery] int? torneoId = null)
        {
            var query = new GetGolesJugadorQuery
            {
                JugadorId = jugadorId,
                TorneoId = torneoId
            };

            var response = await _golesJugadorHandler.Handle(query);
            return HandleResponse(response);
        }
    }
}
