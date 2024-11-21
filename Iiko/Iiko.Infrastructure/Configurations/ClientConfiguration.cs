using Iiko.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iiko.Infrastructure.Configurations;

/// <summary>
/// Конфигурация таблицы клиентов
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.ClientId);
        builder.HasIndex(c => c.ClientId).IsUnique();
        builder.Property(c => c.ClientId).IsRequired();
        builder.Property(c => c.Username).IsRequired();
        builder.Property(c => c.SystemId).IsRequired();
    }
}