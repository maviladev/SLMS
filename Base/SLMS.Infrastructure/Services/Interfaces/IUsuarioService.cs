using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ApiResponse<IEnumerable<UsuarioDto>>> GetAllAsync();
        Task<ApiResponse<UsuarioDto>> GetByIdAsync(int id);
        Task<ApiResponse<UsuarioDto>> CreateAsync(CreateUsuarioDto dto);
        Task<ApiResponse<UsuarioDto>> UpdateAsync(int id, UpdateUsuarioDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
