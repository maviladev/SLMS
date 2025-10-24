using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface ITorneoService
    {
        Task<ApiResponse<IEnumerable<TorneoDto>>> GetAllAsync();
        Task<ApiResponse<TorneoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<TorneoDto>>> GetByLigaIdAsync(int ligaId);
        Task<ApiResponse<TorneoDto>> CreateAsync(CreateTorneoDto dto);
        Task<ApiResponse<TorneoDto>> UpdateAsync(int id, UpdateTorneoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
