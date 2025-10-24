using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface IEstadisticaService
    {
        Task<ApiResponse<IEnumerable<EstadisticaDto>>> GetAllAsync();
        Task<ApiResponse<EstadisticaDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<EstadisticaDto>>> GetByPartidoIdAsync(int partidoId);
        Task<ApiResponse<IEnumerable<EstadisticaDto>>> GetByJugadorIdAsync(int jugadorId);
        Task<ApiResponse<EstadisticaDto>> CreateAsync(CreateEstadisticaDto dto);
        Task<ApiResponse<EstadisticaDto>> UpdateAsync(int id, UpdateEstadisticaDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
