using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public interface IRolService
    {
        Task<ApiResponse<IEnumerable<Models.Rol>>> GetAllAsync();
        Task<ApiResponse<Models.Rol>> GetByIdAsync(int id);
    }
}
