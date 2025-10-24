using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    public class EstadisticaConfiguration : IEntityTypeConfiguration<Estadistica>
    {
        public void Configure(EntityTypeBuilder<Estadistica> builder)
        {
            builder.ToTable("Estadistica");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Minuto).IsRequired();

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(e => e.Descripcion)
                .HasMaxLength(500);

            // Auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);

            // Índices
            builder.HasIndex(e => e.PartidoId);
            builder.HasIndex(e => e.JugadorId);

            // Relaciones
            builder.HasOne(e => e.Partido)
                .WithMany(p => p.Estadisticas)
                .HasForeignKey(e => e.PartidoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Jugador)
                .WithMany(j => j.Estadisticas)
                .HasForeignKey(e => e.JugadorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.JugadorAsistente)
                .WithMany()
                .HasForeignKey(e => e.JugadorAsistenteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
