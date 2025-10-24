using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{

    public interface IGoogleAuthService
    {
        Task<ApiResponse<GoogleAuthResponseDto>> LoginWithGoogleAsync(GoogleLoginDto dto);
        Task<ApiResponse<GoogleAuthResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}
