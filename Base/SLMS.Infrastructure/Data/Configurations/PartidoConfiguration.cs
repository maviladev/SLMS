using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    public class PartidoConfiguration : IEntityTypeConfiguration<Partido>
    {
        public void Configure(EntityTypeBuilder<Partido> builder)
        {
            builder.ToTable("Partido");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FechaHora).IsRequired();
            builder.Property(e => e.Jornada).IsRequired();

            builder.Property(e => e.Estadio)
                .HasMaxLength(200);

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(e => e.ArbitroPrincipal).HasMaxLength(200);
            builder.Property(e => e.Arbitro1).HasMaxLength(200);
            builder.Property(e => e.Arbitro2).HasMaxLength(200);
            builder.Property(e => e.CuartoArbitro).HasMaxLength(200);

            // Auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);

            // Índices
            builder.HasIndex(e => e.TorneoId);
            builder.HasIndex(e => e.LocalId);
            builder.HasIndex(e => e.VisitanteId);
            builder.HasIndex(e => e.FechaHora);

            // Relaciones
            builder.HasOne(e => e.Torneo)
                .WithMany(t => t.Partidos)
                .HasForeignKey(e => e.TorneoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Local)
                .WithMany(et => et.PartidosLocal)
                .HasForeignKey(e => e.LocalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Visitante)
                .WithMany(et => et.PartidosVisitante)
                .HasForeignKey(e => e.VisitanteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
