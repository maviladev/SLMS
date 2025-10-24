using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface IPartidoService
    {
        Task<ApiResponse<IEnumerable<PartidoDto>>> GetAllAsync();
        Task<ApiResponse<PartidoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<PartidoDto>>> GetByTorneoIdAsync(int torneoId);
        Task<ApiResponse<IEnumerable<PartidoDto>>> GetByEquipoIdAsync(int equipoId);
        Task<ApiResponse<PartidoDto>> CreateAsync(CreatePartidoDto dto);
        Task<ApiResponse<PartidoDto>> UpdateAsync(int id, UpdatePartidoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

}
