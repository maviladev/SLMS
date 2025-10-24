using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SLMS.Infrastructure.Data.Configurations
{
    public class AlineacionConfiguration : IEntityTypeConfiguration<Alineacion>
    {
        public void Configure(EntityTypeBuilder<Alineacion> builder)
        {
            // Nombre de la tabla en la base de datos
            builder.ToTable("Alineacion");

            // Configuración de la clave primaria
            builder.HasKey(e => e.Id);

            // Configuración de propiedades
            builder.Property(e => e.EsTitular)
                .IsRequired(); // Campo obligatorio

            builder.Property(e => e.MinutoEntrada)
                .IsRequired(false); // Campo opcional (nullable)

            builder.Property(e => e.MinutoSalida)
                .IsRequired(false); // Campo opcional (nullable)

            // Configuración de campos de auditoría
            builder.Property(e => e.Creado)
                .IsRequired();

            builder.Property(e => e.CreadoPor)
                .HasMaxLength(100);

            builder.Property(e => e.Modificado)
                .IsRequired();

            builder.Property(e => e.ModificadoPor)
                .HasMaxLength(100);

            builder.Property(e => e.Eliminado)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.FechaEliminacion)
                .IsRequired(false);

            builder.Property(e => e.EliminadoPor)
                .HasMaxLength(100);

            // Configuración de índices para mejorar el rendimiento de las consultas
            builder.HasIndex(e => e.PartidoId)
                .HasDatabaseName("IX_Alineacion_PartidoId");

            builder.HasIndex(e => e.JugadorId)
                .HasDatabaseName("IX_Alineacion_JugadorId");

            // Índice compuesto para evitar duplicados (un jugador no puede estar dos veces en la misma alineación)
            builder.HasIndex(e => new { e.PartidoId, e.JugadorId })
                .IsUnique()
                .HasDatabaseName("IX_Alineacion_PartidoJugador");

            // Configuración de relaciones (Foreign Keys)

            // Relación con Partido
            builder.HasOne(e => e.Partido)
                .WithMany(p => p.Alineaciones)
                .HasForeignKey(e => e.PartidoId)
                .OnDelete(DeleteBehavior.Restrict) // No eliminar en cascada
                .HasConstraintName("FK_Alineacion_Partido");

            // Relación con Jugador
            builder.HasOne(e => e.Jugador)
                .WithMany(j => j.Alineaciones)
                .HasForeignKey(e => e.JugadorId)
                .OnDelete(DeleteBehavior.Restrict) // No eliminar en cascada
                .HasConstraintName("FK_Alineacion_Jugador");
        }
    }
}
