using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    public class CastigoConfiguration : IEntityTypeConfiguration<Castigo>
    {
        public void Configure(EntityTypeBuilder<Castigo> builder)
        {
            builder.ToTable("Castigo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(e => e.PartidosSuspension).IsRequired();

            builder.Property(e => e.Motivo)
                .HasMaxLength(1000);

            builder.Property(e => e.FechaInicio).IsRequired();
            builder.Property(e => e.Activo).IsRequired();

            // Auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);

            // Índices
            builder.HasIndex(e => e.JugadorId);

            // Relaciones
            builder.HasOne(e => e.Jugador)
                .WithMany(j => j.Castigos)
                .HasForeignKey(e => e.JugadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
