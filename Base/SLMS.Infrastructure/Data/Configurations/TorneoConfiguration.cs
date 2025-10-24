using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuración de Entity Framework para Torneo
    /// </summary>
    public class TorneoConfiguration : IEntityTypeConfiguration<Torneo>
    {
        public void Configure(EntityTypeBuilder<Torneo> builder)
        {
            builder.ToTable("Torneo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Logo)
                .HasMaxLength(500);

            builder.Property(e => e.FechaInicio).IsRequired();
            builder.Property(e => e.FechaFin).IsRequired();
            builder.Property(e => e.NumeroJornadas).IsRequired();

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasConversion<int>();

            // Auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);

            // Índices
            builder.HasIndex(e => e.LigaId);

            // Relaciones
            builder.HasOne(e => e.Liga)
                .WithMany(l => l.Torneos)
                .HasForeignKey(e => e.LigaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.EquiposTorneo)
                .WithOne(et => et.Torneo)
                .HasForeignKey(et => et.TorneoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Partidos)
                .WithOne(p => p.Torneo)
                .HasForeignKey(p => p.TorneoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
