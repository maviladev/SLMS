using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface ILigaService
    {
        Task<ApiResponse<IEnumerable<LigaDto>>> GetAllAsync();
        Task<ApiResponse<LigaDto>> GetByIdAsync(int id);
        Task<ApiResponse<LigaDto>> CreateAsync(CreateLigaDto dto);
        Task<ApiResponse<LigaDto>> UpdateAsync(int id, UpdateLigaDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
