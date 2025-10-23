using Base.DTOs;
using Base.Services.Interfaces;
using Base.Models;
using Base.Repositories.Interfaces;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Base.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _configuration = configuration;
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var response = new ApiResponse<AuthResponseDto>();

            try
            {
                var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);

                if (usuario == null)
                {
                    response.Success = false;
                    response.Message = "Credenciales inválidas";
                    response.Errors.Add("Usuario no encontrado");
                    return response;
                }

                if (!usuario.Estado)
                {
                    response.Success = false;
                    response.Message = "Usuario inactivo";
                    response.Errors.Add("El usuario está deshabilitado");
                    return response;
                }

                if (!VerifyPassword(loginDto.Password, usuario.PasswordHash))
                {
                    response.Success = false;
                    response.Message = "Credenciales inválidas";
                    response.Errors.Add("Contraseña incorrecta");
                    return response;
                }

                var token = GenerateJwtToken(usuario);
                var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"]);

                response.Success = true;
                response.Message = "Inicio de sesión exitoso";
                response.Data = new AuthResponseDto
                {
                    Token = token,
                    Email = usuario.Email,
                    Rol = usuario.Rol.Nombre,
                    Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al iniciar sesión";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            var response = new ApiResponse<AuthResponseDto>();

            try
            {
                var existingUser = await _usuarioRepository.GetByEmailAsync(registerDto.Email);
                if (existingUser != null)
                {
                    response.Success = false;
                    response.Message = "El usuario ya existe";
                    response.Errors.Add("El email ya está registrado");
                    return response;
                }

                var rol = await _rolRepository.GetByIdAsync(registerDto.RolId);
                if (rol == null)
                {
                    response.Success = false;
                    response.Message = "Rol no encontrado";
                    response.Errors.Add("El rol especificado no existe");
                    return response;
                }

                var usuario = new Usuario
                {
                    Email = registerDto.Email,
                    PasswordHash = HashPassword(registerDto.Password),
                    RolId = registerDto.RolId,
                    Estado = true,
                    Creado = DateTime.UtcNow,
                    Modificado = DateTime.UtcNow
                };

                await _usuarioRepository.AddAsync(usuario);
                usuario = await _usuarioRepository.GetByEmailAsync(usuario.Email);

                var token = GenerateJwtToken(usuario);
                var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"]);

                response.Success = true;
                response.Message = "Usuario registrado exitosamente";
                response.Data = new AuthResponseDto
                {
                    Token = token,
                    Email = usuario.Email,
                    Rol = usuario.Rol.Nombre,
                    Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al registrar usuario";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.Tipo)
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

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            var hash = HashPassword(password);
            return hash == passwordHash;
        }
    }
}