using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services.Interfaces
{
    public class GoogleLoginDto
    {
        public string IdToken { get; set; }
    }

    public class GoogleAuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string FotoPerfil { get; set; }
        public string Rol { get; set; }
        public bool EsNuevoUsuario { get; set; }
        public DateTime Expiration { get; set; }
    }

    // ==================== INTERFACE ====================

    public interface IGoogleAuthService
    {
        Task<ApiResponse<GoogleAuthResponseDto>> LoginWithGoogleAsync(GoogleLoginDto dto);
        Task<ApiResponse<GoogleAuthResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}
