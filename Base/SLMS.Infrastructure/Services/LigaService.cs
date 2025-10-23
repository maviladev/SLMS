using Base.DTOs;
using Base.Services.Interfaces;
using Base.Models;
using Base.Repositories.Interfaces;
using Base.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Base.Services
{
    public class LigaService : ILigaService
    {
        private readonly ILigaRepository _ligaRepository;

        public LigaService(ILigaRepository ligaRepository)
        {
            _ligaRepository = ligaRepository;
        }

        public async Task<ApiResponse<IEnumerable<LigaDto>>> GetAllAsync()
        {
            var response = new ApiResponse<IEnumerable<LigaDto>>();

            try
            {
                var ligas = await _ligaRepository.GetAllAsync();
                response.Success = true;
                response.Message = "Ligas obtenidas exitosamente";
                response.Data = ligas.Select(MapToDto);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener las ligas";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<LigaDto>> GetByIdAsync(int id)
        {
            var response = new ApiResponse<LigaDto>();

            try
            {
                var liga = await _ligaRepository.GetByIdAsync(id);

                if (liga == null)
                {
                    response.Success = false;
                    response.Message = "Liga no encontrada";
                    response.Errors.Add($"No existe una liga con el ID {id}");
                    return response;
                }

                response.Success = true;
                response.Message = "Liga obtenida exitosamente";
                response.Data = MapToDto(liga);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener la liga";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<LigaDto>> CreateAsync(CreateLigaDto dto)
        {
            var response = new ApiResponse<LigaDto>();

            try
            {
                var liga = new Liga
                {
                    Nombre = dto.Nombre,
                    Logo = dto.Logo,
                    Estado = true,
                    Creado = DateTime.UtcNow,
                    Modificado = DateTime.UtcNow
                };

                await _ligaRepository.AddAsync(liga);

                response.Success = true;
                response.Message = "Liga creada exitosamente";
                response.Data = MapToDto(liga);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la liga";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<LigaDto>> UpdateAsync(int id, UpdateLigaDto dto)
        {
            var response = new ApiResponse<LigaDto>();

            try
            {
                var liga = await _ligaRepository.GetByIdAsync(id);

                if (liga == null)
                {
                    response.Success = false;
                    response.Message = "Liga no encontrada";
                    response.Errors.Add($"No existe una liga con el ID {id}");
                    return response;
                }

                if (!string.IsNullOrEmpty(dto.Nombre))
                    liga.Nombre = dto.Nombre;

                if (!string.IsNullOrEmpty(dto.Logo))
                    liga.Logo = dto.Logo;

                if (dto.Estado.HasValue)
                    liga.Estado = dto.Estado.Value;

                liga.Modificado = DateTime.UtcNow;

                await _ligaRepository.UpdateAsync(liga);

                response.Success = true;
                response.Message = "Liga actualizada exitosamente";
                response.Data = MapToDto(liga);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar la liga";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var exists = await _ligaRepository.ExistsAsync(id);

                if (!exists)
                {
                    response.Success = false;
                    response.Message = "Liga no encontrada";
                    response.Errors.Add($"No existe una liga con el ID {id}");
                    return response;
                }

                var deleted = await _ligaRepository.DeleteAsync(id);

                response.Success = deleted;
                response.Message = deleted ? "Liga eliminada exitosamente" : "No se pudo eliminar la liga";
                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar la liga";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        private LigaDto MapToDto(Liga liga)
        {
            return new LigaDto
            {
                Id = liga.Id,
                Nombre = liga.Nombre,
                Logo = liga.Logo,
                Estado = liga.Estado,
                Creado = liga.Creado,
                Modificado = liga.Modificado
            };
        }
    }

    // Implementación similar para otros servicios
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ApiResponse<IEnumerable<UsuarioDto>>> GetAllAsync()
        {
            var response = new ApiResponse<IEnumerable<UsuarioDto>>();
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                response.Success = true;
                response.Message = "Usuarios obtenidos exitosamente";
                response.Data = usuarios.Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Estado = u.Estado,
                    Creado = u.Creado,
                    Modificado = u.Modificado,
                    RolId = u.RolId,
                    RolNombre = u.Rol?.Nombre
                });
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener usuarios";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<UsuarioDto>> GetByIdAsync(int id)
        {
            var response = new ApiResponse<UsuarioDto>();
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado";
                    return response;
                }
                response.Success = true;
                response.Data = new UsuarioDto
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Estado = usuario.Estado,
                    Creado = usuario.Creado,
                    Modificado = usuario.Modificado,
                    RolId = usuario.RolId,
                    RolNombre = usuario.Rol?.Nombre
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener usuario";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<UsuarioDto>> CreateAsync(CreateUsuarioDto dto)
        {
            var response = new ApiResponse<UsuarioDto>();
            try
            {
                var usuario = new Usuario
                {
                    Email = dto.Email,
                    PasswordHash = HashPassword(dto.Password),
                    RolId = dto.RolId,
                    Estado = true,
                    Creado = DateTime.UtcNow,
                    Modificado = DateTime.UtcNow
                };
                await _usuarioRepository.AddAsync(usuario);
                response.Success = true;
                response.Message = "Usuario creado exitosamente";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear usuario";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<UsuarioDto>> UpdateAsync(int id, UpdateUsuarioDto dto)
        {
            var response = new ApiResponse<UsuarioDto>();
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado";
                    return response;
                }
                if (!string.IsNullOrEmpty(dto.Email)) usuario.Email = dto.Email;
                if (dto.Estado.HasValue) usuario.Estado = dto.Estado.Value;
                if (dto.RolId.HasValue) usuario.RolId = dto.RolId.Value;
                usuario.Modificado = DateTime.UtcNow;
                await _usuarioRepository.UpdateAsync(usuario);
                response.Success = true;
                response.Message = "Usuario actualizado exitosamente";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar usuario";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var deleted = await _usuarioRepository.DeleteAsync(id);
                response.Success = deleted;
                response.Message = deleted ? "Usuario eliminado exitosamente" : "Usuario no encontrado";
                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar usuario";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }

    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<ApiResponse<IEnumerable<Rol>>> GetAllAsync()
        {
            var response = new ApiResponse<IEnumerable<Rol>>();
            try
            {
                var roles = await _rolRepository.GetAllAsync();
                response.Success = true;
                response.Message = "Roles obtenidos exitosamente";
                response.Data = roles;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener roles";
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<Rol>> GetByIdAsync(int id)
        {
            var response = new ApiResponse<Rol>();
            try
            {
                var rol = await _rolRepository.GetByIdAsync(id);
                if (rol == null)
                {
                    response.Success = false;
                    response.Message = "Rol no encontrado";
                    return response;
                }
                response.Success = true;
                response.Data = rol;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener rol";
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}