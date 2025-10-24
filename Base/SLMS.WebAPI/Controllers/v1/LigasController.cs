using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SLMS.WebAPI.Controllers
{
    /// <summary>
    /// Gestión de Ligas - Patrón CQRS
    /// </summary>
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiVersion("1.0")]
    public class LigasController : BaseController
    {
        private readonly ICommandHandler<CreateLigaCommand, ApiResponse<LigaDto>> _createHandler;
        private readonly ICommandHandler<UpdateLigaCommand, ApiResponse<LigaDto>> _updateHandler;
        private readonly ICommandHandler<DeleteLigaCommand, ApiResponse<bool>> _deleteHandler;
        private readonly IQueryHandler<GetLigaByIdQuery, ApiResponse<LigaDto>> _getByIdHandler;
        private readonly IQueryHandler<GetAllLigasQuery, ApiResponse<IEnumerable<LigaDto>>> _getAllHandler;

        public LigasController(
            ICommandHandler<CreateLigaCommand, ApiResponse<LigaDto>> createHandler,
            ICommandHandler<UpdateLigaCommand, ApiResponse<LigaDto>> updateHandler,
            ICommandHandler<DeleteLigaCommand, ApiResponse<bool>> deleteHandler,
            IQueryHandler<GetLigaByIdQuery, ApiResponse<LigaDto>> getByIdHandler,
            IQueryHandler<GetAllLigasQuery, ApiResponse<IEnumerable<LigaDto>>> getAllHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        /// <summary>
        /// Obtiene todas las ligas
        /// </summary>
        /// <param name="soloActivas">Filtrar solo ligas activas</param>
        /// <param name="pais">Filtrar por país</param>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<LigaDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] bool? soloActivas = null, [FromQuery] string pais = null)
        {
            var query = new GetAllLigasQuery
            {
                SoloActivas = soloActivas,
                Pais = pais
            };

            var response = await _getAllHandler.Handle(query);
            return HandleResponse(response);
        }

        /// <summary>
        /// Obtiene una liga por ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<LigaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<LigaDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetLigaByIdQuery { Id = id };
            var response = await _getByIdHandler.Handle(query);
            return HandleResponse(response);
        }

        /// <summary>
        /// Crea una nueva liga
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(typeof(ApiResponse<LigaDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<LigaDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateLigaDto dto)
        {
            var command = new CreateLigaCommand
            {
                Nombre = dto.Nombre,
                Logo = dto.Logo,
                Descripcion = dto.Descripcion,
                Pais = dto.Pais,
                UsuarioId = GetUserId()
            };

            var response = await _createHandler.Handle(command);

            if (response.Success)
                return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);

            return HandleResponse(response);
        }

        /// <summary>
        /// Actualiza una liga existente
        /// </summary>
        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(typeof(ApiResponse<LigaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<LigaDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLigaDto dto)
        {
            var command = new UpdateLigaCommand
            {
                Id = id,
                Nombre = dto.Nombre,
                Logo = dto.Logo,
                Descripcion = dto.Descripcion,
                Pais = dto.Pais,
                Estado = dto.Estado,
                UsuarioId = GetUserId()
            };

            var response = await _updateHandler.Handle(command);
            return HandleResponse(response);
        }

        /// <summary>
        /// Elimina una liga (soft delete)
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLigaCommand
            {
                Id = id,
                UsuarioId = GetUserId()
            };

            var response = await _deleteHandler.Handle(command);
            return HandleResponse(response);
        }
    }
}
