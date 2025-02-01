using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Modules.Catalogo.Domain.Aggregates;

namespace NerdStore.Modules.Catalogo.Infrastructure.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

        // builder.OwnsOne(p => p.Dimensoes, dim =>
        // {
        //     dim.Property(d => d.Altura).HasColumnName("Altura");
        //     dim.Property(d => d.Largura).HasColumnName("Largura");
        //     dim.Property(d => d.Profundidade).HasColumnName("Profundidade");
        // });

        builder.HasOne(e => e.Categoria).WithMany(e => e.Produtos)
            .HasForeignKey(e => e.CategoriaId);

        builder.HasData(
            new 
            {
                Id = new Guid("b21e5a57-89ab-cdef-0123-456789abcdef"),
                Nome = "Camiseta Premium",
                Descricao = "Feita com 100% algodão para máximo conforto e durabilidade",
                Ativo = true,
                Valor = 90M,
                CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "camiseta2.jpg",
                QuantidadeEstoque = 3,
                Altura = 5M,
                Largura = 5M, 
                Profundidade = 5M
            },
            new
            {
                Id = new Guid("f4d3a756-89ab-cdef-0123-456789abcdef"),
                Nome = "Caneca Cerâmica Master",
                Descricao = "Resistente e perfeita para café, chá ou chocolate quente",
                Ativo = true,
                Valor = 10M,
                CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "caneca4.jpg",
                QuantidadeEstoque = 5,
                Altura = 12M,
                Largura = 8M,
                Profundidade = 5M
            },
            new 
            {
                Id = new Guid("7ad3b789-89ab-cdef-0123-456789abcdef"),
                Nome = "Camiseta Urban Fit",
                Descricao = "Modelagem moderna e estilosa, perfeita para o dia a dia",
                Ativo = true,
                Valor = 110M,
                CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "camiseta4.jpg",
                QuantidadeEstoque = 23,
                Altura = 5M,
                Largura = 5M,
                Profundidade = 5M
            },
            new
            {
                Id = new Guid("a9f3c872-89ab-cdef-0123-456789abcdef"),
                Nome = "Camiseta SoftTouch",
                Descricao = "Tecido leve e macio para quem busca conforto absoluto",
                Ativo = true,
                Valor = 80M,
                CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "camiseta3.jpg",
                QuantidadeEstoque = 15,
                Altura = 5M,
                Largura = 5M,
                Profundidade = 5M
            },
            new
            {
                Id = new Guid("9a4e9ab0-89ab-cdef-0123-456789abcdef"),
                Nome = "Caneca Magic Color",
                Descricao = "Muda de cor com líquidos quentes, um toque de magia no seu dia",
                Ativo = true,
                Valor = 20M,
                CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "caneca1.jpg",
                QuantidadeEstoque = 5,
                Altura = 12M,
                Largura = 8M,
                Profundidade = 5M
            },
            new
            {
                Id = new Guid("c7d8e512-89ab-cdef-0123-456789abcdef"),
                Nome = "Caneca Gamer XL",
                Descricao = "Capacidade maior para quem precisa de mais energia",
                Ativo = true,
                Valor = 15M,
                CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "caneca2.jpg",
                QuantidadeEstoque = 8,
                Altura = 12M,
                Largura = 8M,
                Profundidade = 5M
            },
            new
            {
                Id = new Guid("a48db94e-89ab-cdef-0123-456789abcdef"),
                Nome = "Camiseta Classic Print",
                Descricao = "Clássica, estilosa e versátil, ideal para qualquer ocasião",
                Ativo = true,
                Valor = 100M,
                CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "camiseta1.jpg",
                QuantidadeEstoque = 8,
                Altura = 5M,
                Largura = 5M,
                Profundidade = 5M
            },
            new
            {
                Id = new Guid("d6b4f3a0-89ab-cdef-0123-456789abcdef"),
                Nome = "Caneca Personal Print",
                Descricao = "Personalize com sua estampa favorita e torne-a única",
                Ativo = true,
                Valor = 20M,
                CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                DataCadastro = new DateTime(2024, 2, 1),
                Imagem = "caneca3.jpg",
                QuantidadeEstoque = 5,
                Altura = 12M,
                Largura = 8M,
                Profundidade = 5M
            }
        );
    }
}