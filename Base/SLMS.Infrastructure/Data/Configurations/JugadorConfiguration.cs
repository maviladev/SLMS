using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuración de Entity Framework para Jugador
    /// </summary>
    public class JugadorConfiguration : IEntityTypeConfiguration<Jugador>
    {
        public void Configure(EntityTypeBuilder<Jugador> builder)
        {
            builder.ToTable("Jugador");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaNacimiento).IsRequired();

            builder.Property(e => e.Nacionalidad)
                .HasMaxLength(100);

            builder.Property(e => e.Foto)
                .HasMaxLength(500);

            builder.Property(e => e.Posicion)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(e => e.Altura)
                .HasColumnType("decimal(5,2)");

            builder.Property(e => e.Peso)
                .HasColumnType("decimal(5,2)");

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

            // Propiedades calculadas ignoradas
            builder.Ignore(e => e.NombreCompleto);
            builder.Ignore(e => e.Edad);

            // Índices
            builder.HasIndex(e => e.EquipoTorneoId);

            // Relaciones
            builder.HasOne(e => e.EquipoTorneo)
                .WithMany(et => et.Jugadores)
                .HasForeignKey(e => e.EquipoTorneoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Estadisticas)
                .WithOne(est => est.Jugador)
                .HasForeignKey(est => est.JugadorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Castigos)
                .WithOne(c => c.Jugador)
                .HasForeignKey(c => c.JugadorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Alineaciones)
                .WithOne(a => a.Jugador)
                .HasForeignKey(a => a.JugadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
