using SLMS.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Services
{
    /// <summary>
    /// Servicio de autenticación con Google OAuth 2.0
    /// Clean Code: Nombres descriptivos, métodos pequeños
    /// SOLID: Single Responsibility - solo maneja autenticación Google
    /// </summary>
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolUsuarioRepository _rolUsuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GoogleAuthService> _logger;

        public GoogleAuthService(
            IUsuarioRepository usuarioRepository,
            IRolUsuarioRepository rolUsuarioRepository,
            IConfiguration configuration,
            ILogger<GoogleAuthService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _rolUsuarioRepository = rolUsuarioRepository;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Autentica usuario con token de Google
        /// </summary>
        public async Task<ApiResponse<GoogleAuthResponseDto>> LoginWithGoogleAsync(GoogleLoginDto dto)
        {
            var response = new ApiResponse<GoogleAuthResponseDto>();

            try
            {
                // 1. Validar token de Google
                var googleUser = await ValidateGoogleTokenAsync(dto.IdToken);

                if (googleUser == null)
                {
                    response.Success = false;
                    response.Message = "Token de Google inválido";
                    response.Errors.Add("No se pudo validar el token");
                    return response;
                }

                // 2. Buscar o crear usuario
                var (usuario, esNuevo) = await GetOrCreateUserAsync(googleUser);

                // 3. Generar JWT propio
                var jwtToken = GenerateJwtToken(usuario);
                var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"]);

                response.Success = true;
                response.Message = esNuevo ? "Usuario registrado exitosamente" : "Inicio de sesión exitoso";
                response.Data = new GoogleAuthResponseDto
                {
                    Token = jwtToken,
                    Email = usuario.Email,
                    NombreCompleto = usuario.NombreCompleto,
                    FotoPerfil = usuario.FotoPerfil,
                    Rol = usuario.RolUsuario.Nombre,
                    EsNuevoUsuario = esNuevo,
                    Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
                };

                _logger.LogInformation($"Usuario {usuario.Email} autenticado exitosamente");
            }
            catch (InvalidJwtException ex)
            {
                _logger.LogWarning($"Token JWT inválido: {ex.Message}");
                response.Success = false;
                response.Message = "Token de Google inválido";
                response.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar con Google");
                response.Success = false;
                response.Message = "Error al iniciar sesión";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Valida el token de Google y extrae información del usuario
        /// </summary>
        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["GoogleAuth:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error al validar token de Google: {ex.Message}");
                throw new InvalidJwtException("Token de Google inválido", ex);
            }
        }

        /// <summary>
        /// Obtiene usuario existente o crea uno nuevo
        /// Clean Code: Método hace UNA cosa
        /// </summary>
        private async Task<(Usuario usuario, bool esNuevo)> GetOrCreateUserAsync(
            GoogleJsonWebSignature.Payload googleUser)
        {
            // Buscar por GoogleId primero
            var usuarioExistente = await _usuarioRepository
                .FindAsync(u => u.GoogleId == googleUser.Subject && !u.Eliminado);

            if (usuarioExistente.Any())
            {
                var usuario = usuarioExistente.First();

                // Actualizar foto de perfil si cambió
                if (usuario.FotoPerfil != googleUser.Picture)
                {
                    usuario.FotoPerfil = googleUser.Picture;
                    usuario.Modificado = DateTime.UtcNow;
                    await _usuarioRepository.UpdateAsync(usuario);
                }

                return (usuario, false);
            }

            // Buscar por email (por si ya existía antes de Google)
            var usuarioPorEmail = await _usuarioRepository
                .FindAsync(u => u.Email == googleUser.Email && !u.Eliminado);

            if (usuarioPorEmail.Any())
            {
                var usuario = usuarioPorEmail.First();
                usuario.GoogleId = googleUser.Subject;
                usuario.FotoPerfil = googleUser.Picture;
                usuario.Modificado = DateTime.UtcNow;
                await _usuarioRepository.UpdateAsync(usuario);

                return (usuario, false);
            }

            // Crear nuevo usuario
            return await CreateNewUserAsync(googleUser);
        }

        /// <summary>
        /// Crea un nuevo usuario desde Google
        /// </summary>
        private async Task<(Usuario usuario, bool esNuevo)> CreateNewUserAsync(
            GoogleJsonWebSignature.Payload googleUser)
        {
            // Obtener rol por defecto (Consultor)
            var rolConsultor = await _rolUsuarioRepository
                .FindAsync(r => r.Tipo == RolUsuarioEnum.Consultor);

            var rol = rolConsultor.FirstOrDefault()
                ?? throw new InvalidOperationException("Rol Consultor no encontrado en la base de datos");

            var nuevoUsuario = new Usuario
            {
                Email = googleUser.Email,
                GoogleId = googleUser.Subject,
                NombreCompleto = googleUser.Name,
                FotoPerfil = googleUser.Picture,
                RolUsuarioId = rol.Id,
                Estado = EstadoEntidad.Activo,
                Creado = DateTime.UtcNow,
                CreadoPor = "Sistema",
                Modificado = DateTime.UtcNow,
                ModificadoPor = "Sistema",
                Eliminado = false
            };

            await _usuarioRepository.AddAsync(nuevoUsuario);

            // Recargar con navegación
            var usuarioCompleto = await _usuarioRepository.GetByIdAsync(nuevoUsuario.Id);

            _logger.LogInformation($"Nuevo usuario creado: {nuevoUsuario.Email}");

            return (usuarioCompleto, true);
        }

        /// <summary>
        /// Genera JWT propio de la aplicación
        /// </summary>
        private string GenerateJwtToken(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Role, usuario.RolUsuario.Tipo.ToString()),
                new Claim("GoogleId", usuario.GoogleId),
                new Claim("FotoPerfil", usuario.FotoPerfil ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Refresca el token JWT
        /// </summary>
        public async Task<ApiResponse<GoogleAuthResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var response = new ApiResponse<GoogleAuthResponseDto>();

            try
            {
                // Validar y extraer claims del token expirado
                var principal = GetPrincipalFromExpiredToken(refreshToken);
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    response.Success = false;
                    response.Message = "Token inválido";
                    return response;
                }

                var usuario = await _usuarioRepository.GetByIdAsync(int.Parse(userId));

                if (usuario == null || usuario.Eliminado || usuario.Estado != EstadoEntidad.Activo)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado o inactivo";
                    return response;
                }

                // Generar nuevo token
                var newToken = GenerateJwtToken(usuario);
                var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"]);

                response.Success = true;
                response.Message = "Token refrescado exitosamente";
                response.Data = new GoogleAuthResponseDto
                {
                    Token = newToken,
                    Email = usuario.Email,
                    NombreCompleto = usuario.NombreCompleto,
                    FotoPerfil = usuario.FotoPerfil,
                    Rol = usuario.RolUsuario.Nombre,
                    EsNuevoUsuario = false,
                    Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al refrescar token");
                response.Success = false;
                response.Message = "Error al refrescar token";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Extrae claims de un token expirado (para refresh)
        /// </summary>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // No validar expiración
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token inválido");
            }

            return principal;
        }
    }

    // ==================== REPOSITORIO ADICIONAL ====================

    public interface IRolUsuarioRepository : IRepository<RolUsuario>
    {
        Task<RolUsuario> GetByTipoAsync(RolUsuarioEnum tipo);
    }
}
