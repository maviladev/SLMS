using Base.DTOs;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLMS.WebAPI.Controllers;

namespace Base.Controllers
{
    /// <summary>
    /// Controlador de autenticación con Google OAuth 2.0
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : BaseController
    {
        private readonly IGoogleAuthService _googleAuthService;

        public AuthController(IGoogleAuthService googleAuthService)
        {
            _googleAuthService = googleAuthService;
        }

        /// <summary>
        /// Autenticación con Google
        /// </summary>
        /// <remarks>
        /// Flujo:
        /// 1. El cliente obtiene un ID Token de Google (frontend)
        /// 2. Envía el token a este endpoint
        /// 3. El API valida el token con Google
        /// 4. Crea/actualiza el usuario en la BD
        /// 5. Retorna un JWT propio del API
        /// 
        /// Ejemplo de request:
        /// 
        ///     POST /api/v1/auth/google
        ///     {
        ///         "idToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjI3..."
        ///     }
        /// 
        /// </remarks>
        /// <param name="dto">Token de Google obtenido en el cliente</param>
        /// <returns>JWT propio del API y datos del usuario</returns>
        [HttpPost("google")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<GoogleAuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<GoogleAuthResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            var response = await _googleAuthService.LoginWithGoogleAsync(dto);
            return HandleResponse(response);
        }

        /// <summary>
        /// Refresca el token JWT
        /// </summary>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<GoogleAuthResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var response = await _googleAuthService.RefreshTokenAsync(refreshToken);
            return HandleResponse(response);
        }

        /// <summary>
        /// Obtiene información del usuario actual
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetCurrentUser()
        {
            return Ok(new
            {
                userId = GetUserId(),
                email = GetUserEmail(),
                role = GetUserRole(),
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}