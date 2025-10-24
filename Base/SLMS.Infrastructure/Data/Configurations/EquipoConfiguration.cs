using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuración de Entity Framework para Equipo
    /// </summary>
    public class EquipoConfiguration : IEntityTypeConfiguration<Equipo>
    {
        public void Configure(EntityTypeBuilder<Equipo> builder)
        {
            builder.ToTable("Equipo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.NombreCorto)
                .HasMaxLength(50);

            builder.Property(e => e.Logo)
                .HasMaxLength(500);

            builder.Property(e => e.Estadio)
                .HasMaxLength(200);

            builder.Property(e => e.Ciudad)
                .HasMaxLength(100);

            builder.Property(e => e.ColorPrincipal)
                .HasMaxLength(50);

            builder.Property(e => e.ColorSecundario)
                .HasMaxLength(50);

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
                .WithMany(l => l.Equipos)
                .HasForeignKey(e => e.LigaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.EquiposTorneo)
                .WithOne(et => et.Equipo)
                .HasForeignKey(et => et.EquipoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
