using Base.DTOs;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Inicia sesión de un usuario
        /// </summary>
        /// <param name="loginDto">Credenciales de inicio de sesión</param>
        /// <returns>Token JWT y datos del usuario</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            var response = await _authService.LoginAsync(loginDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        /// <param name="registerDto">Datos del nuevo usuario</param>
        /// <returns>Token JWT y datos del usuario</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            var response = await _authService.RegisterAsync(registerDto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(Register), response);
        }
    }
}