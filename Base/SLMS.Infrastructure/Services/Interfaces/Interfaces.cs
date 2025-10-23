using Base.DTOs;

namespace Base.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);
    }

    public interface IUsuarioService
    {
        Task<ApiResponse<IEnumerable<UsuarioDto>>> GetAllAsync();
        Task<ApiResponse<UsuarioDto>> GetByIdAsync(int id);
        Task<ApiResponse<UsuarioDto>> CreateAsync(CreateUsuarioDto dto);
        Task<ApiResponse<UsuarioDto>> UpdateAsync(int id, UpdateUsuarioDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public interface IRolService
    {
        Task<ApiResponse<IEnumerable<Models.Rol>>> GetAllAsync();
        Task<ApiResponse<Models.Rol>> GetByIdAsync(int id);
    }

    public interface ILigaService
    {
        Task<ApiResponse<IEnumerable<LigaDto>>> GetAllAsync();
        Task<ApiResponse<LigaDto>> GetByIdAsync(int id);
        Task<ApiResponse<LigaDto>> CreateAsync(CreateLigaDto dto);
        Task<ApiResponse<LigaDto>> UpdateAsync(int id, UpdateLigaDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public interface ITorneoService
    {
        Task<ApiResponse<IEnumerable<TorneoDto>>> GetAllAsync();
        Task<ApiResponse<TorneoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<TorneoDto>>> GetByLigaIdAsync(int ligaId);
        Task<ApiResponse<TorneoDto>> CreateAsync(CreateTorneoDto dto);
        Task<ApiResponse<TorneoDto>> UpdateAsync(int id, UpdateTorneoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public interface IEquipoService
    {
        Task<ApiResponse<IEnumerable<EquipoDto>>> GetAllAsync();
        Task<ApiResponse<EquipoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<EquipoDto>>> GetByTorneoIdAsync(int torneoId);
        Task<ApiResponse<IEnumerable<EquipoDto>>> GetByLigaIdAsync(int ligaId);
        Task<ApiResponse<EquipoDto>> CreateAsync(CreateEquipoDto dto);
        Task<ApiResponse<EquipoDto>> UpdateAsync(int id, UpdateEquipoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

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

    public interface IPartidoService
    {
        Task<ApiResponse<IEnumerable<PartidoDto>>> GetAllAsync();
        Task<ApiResponse<PartidoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<PartidoDto>>> GetByTorneoIdAsync(int torneoId);
        Task<ApiResponse<IEnumerable<PartidoDto>>> GetByEquipoIdAsync(int equipoId);
        Task<ApiResponse<PartidoDto>> CreateAsync(CreatePartidoDto dto);
        Task<ApiResponse<PartidoDto>> UpdateAsync(int id, UpdatePartidoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public interface IEstadisticaService
    {
        Task<ApiResponse<IEnumerable<EstadisticaDto>>> GetAllAsync();
        Task<ApiResponse<EstadisticaDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<EstadisticaDto>>> GetByPartidoIdAsync(int partidoId);
        Task<ApiResponse<IEnumerable<EstadisticaDto>>> GetByJugadorIdAsync(int jugadorId);
        Task<ApiResponse<EstadisticaDto>> CreateAsync(CreateEstadisticaDto dto);
        Task<ApiResponse<EstadisticaDto>> UpdateAsync(int id, UpdateEstadisticaDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public interface ICastigoService
    {
        Task<ApiResponse<IEnumerable<CastigoDto>>> GetAllAsync();
        Task<ApiResponse<CastigoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<CastigoDto>>> GetByJugadorIdAsync(int jugadorId);
        Task<ApiResponse<CastigoDto>> CreateAsync(CreateCastigoDto dto);
        Task<ApiResponse<CastigoDto>> UpdateAsync(int id, UpdateCastigoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public interface IProgramacionJuegosService
    {
        Task<ApiResponse<IEnumerable<ProgramacionJuegoDto>>> GetAllAsync();
        Task<ApiResponse<ProgramacionJuegoDto>> GetByIdAsync(int id);
        Task<ApiResponse<IEnumerable<ProgramacionJuegoDto>>> GetByEquipoIdAsync(int equipoId);
        Task<ApiResponse<IEnumerable<ProgramacionJuegoDto>>> GetByFechaAsync(DateTime fecha);
        Task<ApiResponse<ProgramacionJuegoDto>> CreateAsync(CreateProgramacionJuegoDto dto);
        Task<ApiResponse<ProgramacionJuegoDto>> UpdateAsync(int id, UpdateProgramacionJuegoDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}