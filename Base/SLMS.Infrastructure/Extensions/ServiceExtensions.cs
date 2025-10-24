using Base.Services;
using Base.Services.Interfaces;
using SLMS.Infrastructure.Services;
using SLMS.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Extensions
{
    /// <summary>
    /// Extension Methods para registro de servicios
    /// SOLID: Single Responsibility - cada método registra un grupo específico
    /// Clean Code: Organización clara, fácil de mantener
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registra todos los repositorios
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Patrón Repository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolUsuarioRepository, RolUsuarioRepository>();
            services.AddScoped<ILigaRepository, LigaRepository>();
            services.AddScoped<ITorneoRepository, TorneoRepository>();
            services.AddScoped<IEquipoRepository, EquipoRepository>();
            services.AddScoped<IEquipoTorneoRepository, EquipoTorneoRepository>();
            services.AddScoped<IJugadorRepository, JugadorRepository>();
            services.AddScoped<IPartidoRepository, PartidoRepository>();
            services.AddScoped<IEstadisticaRepository, EstadisticaRepository>();
            services.AddScoped<ICastigoRepository, CastigoRepository>();
            services.AddScoped<IAlineacionRepository, AlineacionRepository>();

            return services;
        }

        /// <summary>
        /// Registra servicios de aplicación
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Servicios de autenticación
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            // Servicios de dominio
            services.AddScoped<ILigaService, LigaService>();
            services.AddScoped<ITorneoService, TorneoService>();
            services.AddScoped<IEquipoService, EquipoService>();
            services.AddScoped<IJugadorService, JugadorService>();
            services.AddScoped<IPartidoService, PartidoService>();
            services.AddScoped<IEstadisticaService, EstadisticaService>();

            return services;
        }

        /// <summary>
        /// Registra Command Handlers (CQRS - Write)
        /// </summary>
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            // Liga Commands
            services.AddScoped<ICommandHandler<CreateLigaCommand, ApiResponse<LigaDto>>,
                CreateLigaCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateLigaCommand, ApiResponse<LigaDto>>,
                UpdateLigaCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteLigaCommand, ApiResponse<bool>>,
                DeleteLigaCommandHandler>();

            // Torneo Commands
            services.AddScoped<ICommandHandler<CreateTorneoCommand, ApiResponse<TorneoDto>>,
                CreateTorneoCommandHandler>();

            // Equipo Commands
            services.AddScoped<ICommandHandler<InscribirEquipoTorneoCommand, ApiResponse<EquipoTorneoDto>>,
                InscribirEquipoTorneoCommandHandler>();

            // Partido Commands
            services.AddScoped<ICommandHandler<RegistrarGolCommand, ApiResponse<EstadisticaDto>>,
                RegistrarGolCommandHandler>();
            services.AddScoped<ICommandHandler<RegistrarTarjetaCommand, ApiResponse<EstadisticaDto>>,
                RegistrarTarjetaCommandHandler>();
            services.AddScoped<ICommandHandler<FinalizarPartidoCommand, ApiResponse<PartidoDto>>,
                FinalizarPartidoCommandHandler>();

            return services;
        }

        /// <summary>
        /// Registra Query Handlers (CQRS - Read)
        /// </summary>
        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            // Liga Queries
            services.AddScoped<IQueryHandler<GetLigaByIdQuery, ApiResponse<LigaDto>>,
                GetLigaByIdQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllLigasQuery, ApiResponse<IEnumerable<LigaDto>>>,
                GetAllLigasQueryHandler>();

            // Torneo Queries
            services.AddScoped<IQueryHandler<GetTorneosByLigaQuery, ApiResponse<IEnumerable<TorneoDto>>>,
                GetTorneosByLigaQueryHandler>();
            services.AddScoped<IQueryHandler<GetTorneoActualQuery, ApiResponse<TorneoDto>>,
                GetTorneoActualQueryHandler>();

            // Estadísticas Queries
            services.AddScoped<IQueryHandler<GetEstadisticasPartidoQuery, ApiResponse<IEnumerable<EstadisticaDto>>>,
                GetEstadisticasPartidoQueryHandler>();
            services.AddScoped<IQueryHandler<GetGolesJugadorQuery, ApiResponse<EstadisticasJugadorDto>>,
                GetGolesJugadorQueryHandler>();
            services.AddScoped<IQueryHandler<GetTablaPosicionesQuery, ApiResponse<IEnumerable<TablaPosicionDto>>>,
                GetTablaPosicionesQueryHandler>();

            return services;
        }
    }
}
