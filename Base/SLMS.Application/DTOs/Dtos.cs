using System.ComponentModel.DataAnnotations;

namespace Base.DTOs
{
    // Auth DTOs
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public int RolId { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public DateTime Expiration { get; set; }
    }

    // Usuario DTOs
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; }
    }

    public class CreateUsuarioDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public int RolId { get; set; }
    }

    public class UpdateUsuarioDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public bool? Estado { get; set; }
        public int? RolId { get; set; }
    }

    // Liga DTOs
    public class LigaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
    }

    public class CreateLigaDto
    {
        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
    }

    public class UpdateLigaDto
    {
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
        public bool? Estado { get; set; }
    }

    // Torneo DTOs
    public class TorneoDto
    {
        public int Id { get; set; }
        public int LigaId { get; set; }
        public string LigaNombre { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
    }

    public class CreateTorneoDto
    {
        [Required]
        public int LigaId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
    }

    public class UpdateTorneoDto
    {
        public int? LigaId { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
        public bool? Estado { get; set; }
    }

    // Equipo DTOs
    public class EquipoDto
    {
        public int Id { get; set; }
        public int TorneoId { get; set; }
        public string TorneoNombre { get; set; }
        public int LigaId { get; set; }
        public string LigaNombre { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
    }

    public class CreateEquipoDto
    {
        [Required]
        public int TorneoId { get; set; }

        [Required]
        public int LigaId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
    }

    public class UpdateEquipoDto
    {
        public int? TorneoId { get; set; }
        public int? LigaId { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
        public bool? Estado { get; set; }
    }

    // Jugador DTOs
    public class JugadorDto
    {
        public int Id { get; set; }
        public int EquipoId { get; set; }
        public string EquipoNombre { get; set; }
        public int TorneoId { get; set; }
        public string TorneoNombre { get; set; }
        public int LigaId { get; set; }
        public string LigaNombre { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Nacimiento { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; }
    }

    public class CreateJugadorDto
    {
        [Required]
        public int EquipoId { get; set; }

        [Required]
        public int TorneoId { get; set; }

        [Required]
        public int LigaId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public DateTime Nacimiento { get; set; }

        [Required]
        public int RolId { get; set; }
    }

    public class UpdateJugadorDto
    {
        public int? EquipoId { get; set; }
        public int? TorneoId { get; set; }
        public int? LigaId { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public DateTime? Nacimiento { get; set; }
        public bool? Estado { get; set; }
        public int? RolId { get; set; }
    }

    // Partido DTOs
    public class PartidoDto
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public string LocalNombre { get; set; }
        public int VisitanteId { get; set; }
        public string VisitanteNombre { get; set; }
        public int Numero { get; set; }
        public int TorneoId { get; set; }
        public string TorneoNombre { get; set; }
    }

    public class CreatePartidoDto
    {
        [Required]
        public int LocalId { get; set; }

        [Required]
        public int VisitanteId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int TorneoId { get; set; }
    }

    public class UpdatePartidoDto
    {
        public int? LocalId { get; set; }
        public int? VisitanteId { get; set; }
        public int? Numero { get; set; }
        public int? TorneoId { get; set; }
    }

    // Estadistica DTOs
    public class EstadisticaDto
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int Minuto { get; set; }
        public int JugadorId { get; set; }
        public string JugadorNombre { get; set; }
        public int PartidoId { get; set; }
    }

    public class CreateEstadisticaDto
    {
        [Required]
        public int TipoId { get; set; }

        [Required]
        [Range(0, 120)]
        public int Minuto { get; set; }

        [Required]
        public int JugadorId { get; set; }

        [Required]
        public int PartidoId { get; set; }
    }

    public class UpdateEstadisticaDto
    {
        public int? TipoId { get; set; }

        [Range(0, 120)]
        public int? Minuto { get; set; }
        public int? JugadorId { get; set; }
        public int? PartidoId { get; set; }
    }

    // Castigo DTOs
    public class CastigoDto
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int JugadorId { get; set; }
        public string JugadorNombre { get; set; }
        public int Partidos { get; set; }
    }

    public class CreateCastigoDto
    {
        [Required]
        public int TipoId { get; set; }

        [Required]
        public int JugadorId { get; set; }

        [Required]
        [Range(1, 99)]
        public int Partidos { get; set; }
    }

    public class UpdateCastigoDto
    {
        public int? TipoId { get; set; }
        public int? JugadorId { get; set; }

        [Range(1, 99)]
        public int? Partidos { get; set; }
    }

    // ProgramacionJuego DTOs
    public class ProgramacionJuegoDto
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public string LocalNombre { get; set; }
        public int VisitanteId { get; set; }
        public string VisitanteNombre { get; set; }
        public DateTime FechaHora { get; set; }
    }

    public class CreateProgramacionJuegoDto
    {
        [Required]
        public int LocalId { get; set; }

        [Required]
        public int VisitanteId { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }
    }

    public class UpdateProgramacionJuegoDto
    {
        public int? LocalId { get; set; }
        public int? VisitanteId { get; set; }
        public DateTime? FechaHora { get; set; }
    }

    // Response DTOs
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }
    }