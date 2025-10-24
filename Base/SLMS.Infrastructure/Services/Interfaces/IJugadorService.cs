using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface IJugadorService
    {
        Task<ApiResponse<IEnumerable<JugadorDto>>> GetAllAsync();
        Task<ApiResponse<JugadorDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<JugadorDto>>> GetByEquipoIdAsync(int equipoId);
        Task<ApiResponse<IEnumerable<JugadorDto>>> GetByTorneoIdAsync(int torneoId);
        Task<ApiResponse<IEnumerable<JugadorDto>>> GetByLigaIdAsync(int ligaId);
        Task<ApiResponse<JugadorDto>> CreateAsync(CreateJugadorDto dto);
        Task<ApiResponse<JugadorDto>> UpdateAsync(int id, UpdateJugadorDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
