using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface IEquipoService
    {
        Task<ApiResponse<IEnumerable<EquipoDto>>> GetAllAsync();
        Task<ApiResponse<EquipoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<EquipoDto>>> GetByTorneoIdAsync(int torneoId);
        Task<ApiResponse<IEnumerable<EquipoDto>>> GetByLigaIdAsync(int ligaId);
        Task<ApiResponse<EquipoDto>> CreateAsync(CreateEquipoDto dto);
        Task<ApiResponse<EquipoDto>> UpdateAsync(int id, UpdateEquipoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
