using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Modules.Catalogo.Domain.Entity;

namespace NerdStore.Catalogo.Infra.Mappings
{
    public partial class ProdutoMapping
    {
        public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
        {
            public void Configure(EntityTypeBuilder<Categoria> builder)
            {
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                // 1 : N => Categorias : Produtos

                builder.HasMany(c => c.Produtos)
                    .WithOne(p => p.Categoria)
                    .HasForeignKey(p => p.CategoriaId);

                builder.ToTable("Categorias");
            }
        }
    }
}
