using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Modules.Catalogo.Domain.Entity;

namespace NerdStore.Modules.Catalogo.Infrastructure.EntitiesConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Categoria>
{

    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        builder.HasData(
            new Categoria("Adesivos", 102),
            new Categoria("Camisetas", 100),
            new Categoria("Canecas", 101)
        );
    }
}
