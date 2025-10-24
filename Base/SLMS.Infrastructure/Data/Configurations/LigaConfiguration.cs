using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuración de Entity Framework para Liga
    /// </summary>
    public class LigaConfiguration : IEntityTypeConfiguration<Liga>
    {
        public void Configure(EntityTypeBuilder<Liga> builder)
        {
            builder.ToTable("Liga");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Logo)
                .HasMaxLength(500);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(1000);

            builder.Property(e => e.Pais)
                .HasMaxLength(100);

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

            // Relaciones
            builder.HasMany(e => e.Torneos)
                .WithOne(t => t.Liga)
                .HasForeignKey(t => t.LigaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Equipos)
                .WithOne(eq => eq.Liga)
                .HasForeignKey(eq => eq.LigaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
