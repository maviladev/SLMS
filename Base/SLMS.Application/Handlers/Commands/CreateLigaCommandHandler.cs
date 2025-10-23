using Base.DTOs;
using SLMS.Application.Commands.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Handlers.Commands
{
    /// <summary>
    /// Handler para crear liga
    /// Principio: Single Responsibility - cada handler hace UNA cosa
    /// </summary>
    public class CreateLigaCommandHandler : ICommandHandler<CreateLigaCommand, ApiResponse<LigaDto>>
    {
        private readonly ILigaRepository _ligaRepository;

        public CreateLigaCommandHandler(ILigaRepository ligaRepository)
        {
            _ligaRepository = ligaRepository;
        }

        public async Task<ApiResponse<LigaDto>> Handle(
            CreateLigaCommand command,
            CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<LigaDto>();

            try
            {
                // Validación de negocio
                var ligaExistente = await _ligaRepository
                    .FindAsync(l => l.Nombre == command.Nombre && !l.Eliminado);

                if (ligaExistente.Any())
                {
                    response.Success = false;
                    response.Message = "Ya existe una liga con ese nombre";
                    return response;
                }

                // Crear entidad
                var liga = new Liga
                {
                    Nombre = command.Nombre,
                    Logo = command.Logo,
                    Descripcion = command.Descripcion,
                    Pais = command.Pais,
                    Estado = EstadoEntidad.Activo,
                    Creado = DateTime.UtcNow,
                    CreadoPor = command.UsuarioId,
                    Modificado = DateTime.UtcNow,
                    ModificadoPor = command.UsuarioId,
                    Eliminado = false
                };

                await _ligaRepository.AddAsync(liga);

                response.Success = true;
                response.Message = "Liga creada exitosamente";
                response.Data = MapToDto(liga);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la liga";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        private LigaDto MapToDto(Liga liga)
        {
            return new LigaDto
            {
                Id = liga.Id,
                Nombre = liga.Nombre,
                Logo = liga.Logo,
                Descripcion = liga.Descripcion,
                Pais = liga.Pais,
                Estado = liga.Estado.ToString(),
                Creado = liga.Creado,
                Modificado = liga.Modificado
            };
        }
    }
}
