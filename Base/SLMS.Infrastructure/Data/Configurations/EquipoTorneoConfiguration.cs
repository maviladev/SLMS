using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuración de Entity Framework para EquipoTorneo
    /// </summary>
    public class EquipoTorneoConfiguration : IEntityTypeConfiguration<EquipoTorneo>
    {
        public void Configure(EntityTypeBuilder<EquipoTorneo> builder)
        {
            builder.ToTable("EquipoTorneo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FechaInscripcion).IsRequired();

            builder.Property(e => e.DirectorTecnico)
                .HasMaxLength(200);

            // Auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);

            // Índices
            builder.HasIndex(e => e.EquipoId);
            builder.HasIndex(e => e.TorneoId);

            // Índice único compuesto (un equipo no puede estar inscrito dos veces en el mismo torneo)
            builder.HasIndex(e => new { e.EquipoId, e.TorneoId })
                .IsUnique()
                .HasDatabaseName("UQ_EquipoTorneo_EquipoTorneo");

            // Relaciones
            builder.HasOne(e => e.Equipo)
                .WithMany(eq => eq.EquiposTorneo)
                .HasForeignKey(e => e.EquipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Torneo)
                .WithMany(t => t.EquiposTorneo)
                .HasForeignKey(e => e.TorneoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Jugadores)
                .WithOne(j => j.EquipoTorneo)
                .HasForeignKey(j => j.EquipoTorneoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
