using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.NomeUsuario).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Senha).IsRequired().HasMaxLength(255);

            // Seed: Cria automaticamente o usuário "admin" com a senha criptografada "Kaxoxa_123!"
            builder.HasData(new Usuario
            {
                Id = 1,
                NomeUsuario = "admin",
                Senha = BCrypt.Net.BCrypt.HashPassword("Kaxoxa_123!")
            });
        }
    }
}