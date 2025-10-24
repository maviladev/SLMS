using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuración de Entity Framework para Usuario
    /// </summary>
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Tabla
            builder.ToTable("Usuario");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.GoogleId)
                .HasMaxLength(100);

            builder.Property(e => e.NombreCompleto)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.FotoPerfil)
                .HasMaxLength(500);

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasConversion<int>(); // Enum a int

            // Campos de auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);

            // Índices
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.GoogleId).IsUnique();

            // Relaciones
            builder.HasOne(e => e.RolUsuario)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(e => e.RolUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    /// <summary>
    /// Configuración de Entity Framework para RolUsuario
    /// </summary>
    public class RolUsuarioConfiguration : IEntityTypeConfiguration<RolUsuario>
    {
        public void Configure(EntityTypeBuilder<RolUsuario> builder)
        {
            builder.ToTable("RolUsuario");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(500);

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasConversion<int>();

            // Auditoría
            builder.Property(e => e.Creado).IsRequired();
            builder.Property(e => e.CreadoPor).HasMaxLength(100);
            builder.Property(e => e.Modificado).IsRequired();
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.Eliminado).IsRequired();
            builder.Property(e => e.EliminadoPor).HasMaxLength(100);
        }
    }
}
