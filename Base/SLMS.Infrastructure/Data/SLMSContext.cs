using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data
{
    /// <summary>
    /// Contexto de base de datos para Liga Fútbol
    /// Database First Approach
    /// </summary>
    public class LigaFutbolContext : DbContext
    {
        public LigaFutbolContext(DbContextOptions<LigaFutbolContext> options) : base(options)
        {
        }

        // DbSets (Tablas)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolUsuario> RolesUsuario { get; set; }
        public DbSet<Liga> Ligas { get; set; }
        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<EquipoTorneo> EquiposTorneo { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Estadistica> Estadisticas { get; set; }
        public DbSet<Alineacion> Alineaciones { get; set; }
        public DbSet<Castigo> Castigos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new RolUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new LigaConfiguration());
            modelBuilder.ApplyConfiguration(new TorneoConfiguration());
            modelBuilder.ApplyConfiguration(new EquipoConfiguration());
            modelBuilder.ApplyConfiguration(new EquipoTorneoConfiguration());
            modelBuilder.ApplyConfiguration(new JugadorConfiguration());
            modelBuilder.ApplyConfiguration(new PartidoConfiguration());
            modelBuilder.ApplyConfiguration(new EstadisticaConfiguration());
            modelBuilder.ApplyConfiguration(new AlineacionConfiguration());
            modelBuilder.ApplyConfiguration(new CastigoConfiguration());

            // Configuración global de queries (filtro de soft delete)
            ConfigureGlobalQueryFilters(modelBuilder);
        }

        /// <summary>
        /// Configura filtros globales para todas las entidades
        /// Automáticamente excluye registros eliminados (soft delete)
        /// </summary>
        private void ConfigureGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            // Filtro global: No mostrar entidades eliminadas
            modelBuilder.Entity<Usuario>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<RolUsuario>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Liga>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Torneo>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Equipo>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<EquipoTorneo>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Jugador>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Partido>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Estadistica>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Alineacion>().HasQueryFilter(e => !e.Eliminado);
            modelBuilder.Entity<Castigo>().HasQueryFilter(e => !e.Eliminado);
        }

        /// <summary>
        /// Sobrescribe SaveChanges para auditoría automática
        /// </summary>
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        /// <summary>
        /// Sobrescribe SaveChangesAsync para auditoría automática
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Actualiza automáticamente los campos de auditoría
        /// </summary>
        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Domain.Entities.Base.BaseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Domain.Entities.Base.BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.Creado = DateTime.UtcNow;
                    entity.Modificado = DateTime.UtcNow;
                    // CreadoPor y ModificadoPor se establecen desde el servicio
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.Modificado = DateTime.UtcNow;
                    // ModificadoPor se establece desde el servicio
                }
            }
        }
    }
}
