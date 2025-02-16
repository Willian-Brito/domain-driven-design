﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdStore.Modules.Catalogo.Infrastructure.Context;

#nullable disable

namespace NerdStore.Catalogo.Infrastructure.Migrations
{
    [DbContext(typeof(CatalogoContext))]
    [Migration("20250201160012_SeedProducts")]
    partial class SeedProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NerdStore.Modules.Catalogo.Domain.Aggregates.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("QuantidadeEstoque")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b21e5a57-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Feita com 100% algodão para máximo conforto e durabilidade",
                            Imagem = "camiseta2.jpg",
                            Nome = "Camiseta Algodão Premium",
                            QuantidadeEstoque = 3,
                            Valor = 90m
                        },
                        new
                        {
                            Id = new Guid("f4d3a756-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Resistente e perfeita para café, chá ou chocolate quente",
                            Imagem = "caneca4.jpg",
                            Nome = "Caneca Cerâmica Master",
                            QuantidadeEstoque = 5,
                            Valor = 10m
                        },
                        new
                        {
                            Id = new Guid("7ad3b789-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Modelagem moderna e estilosa, perfeita para o dia a dia",
                            Imagem = "camiseta4.jpg",
                            Nome = "Camiseta Urban Fit",
                            QuantidadeEstoque = 23,
                            Valor = 110m
                        },
                        new
                        {
                            Id = new Guid("a9f3c872-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Tecido leve e macio para quem busca conforto absoluto",
                            Imagem = "camiseta3.jpg",
                            Nome = "Camiseta SoftTouch",
                            QuantidadeEstoque = 15,
                            Valor = 80m
                        },
                        new
                        {
                            Id = new Guid("9a4e9ab0-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Muda de cor com líquidos quentes, um toque de magia no seu dia",
                            Imagem = "caneca1.jpg",
                            Nome = "Caneca Magic Color",
                            QuantidadeEstoque = 5,
                            Valor = 20m
                        },
                        new
                        {
                            Id = new Guid("c7d8e512-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Capacidade maior para quem precisa de mais energia",
                            Imagem = "caneca2.jpg",
                            Nome = "Caneca Gamer XL",
                            QuantidadeEstoque = 8,
                            Valor = 15m
                        },
                        new
                        {
                            Id = new Guid("a48db94e-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Clássica e versátil, ideal para qualquer ocasião",
                            Imagem = "camiseta1.jpg",
                            Nome = "Camiseta Classic Print",
                            QuantidadeEstoque = 8,
                            Valor = 100m
                        },
                        new
                        {
                            Id = new Guid("d6b4f3a0-89ab-cdef-0123-456789abcdef"),
                            Ativo = true,
                            CategoriaId = new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"),
                            DataCadastro = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Personalize com sua estampa favorita e torne-a única",
                            Imagem = "caneca3.jpg",
                            Nome = "Caneca Personal Print",
                            QuantidadeEstoque = 5,
                            Valor = 20m
                        });
                });

            modelBuilder.Entity("NerdStore.Modules.Catalogo.Domain.Entity.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Categorias", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5d2074ca-9181-440b-bad9-77d4d5f67904"),
                            Codigo = 102,
                            Nome = "Adesivos"
                        },
                        new
                        {
                            Id = new Guid("1f4d2cb1-1869-4816-8c29-204e2effec3e"),
                            Codigo = 100,
                            Nome = "Camisetas"
                        },
                        new
                        {
                            Id = new Guid("194ef033-9e92-4be4-b6e7-a8f74c711d25"),
                            Codigo = 101,
                            Nome = "Canecas"
                        });
                });

            modelBuilder.Entity("NerdStore.Modules.Catalogo.Domain.Aggregates.Produto", b =>
                {
                    b.HasOne("NerdStore.Modules.Catalogo.Domain.Entity.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Dimensoes", "Dimensoes", b1 =>
                        {
                            b1.Property<Guid>("ProdutoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Altura")
                                .HasColumnType("int")
                                .HasColumnName("Altura");

                            b1.Property<int>("Largura")
                                .HasColumnType("int")
                                .HasColumnName("Largura");

                            b1.Property<int>("Profundidade")
                                .HasColumnType("int")
                                .HasColumnName("Profundidade");

                            b1.HasKey("ProdutoId");

                            b1.ToTable("Produtos");

                            b1.WithOwner()
                                .HasForeignKey("ProdutoId");
                        });

                    b.Navigation("Categoria");

                    b.Navigation("Dimensoes")
                        .IsRequired();
                });

            modelBuilder.Entity("NerdStore.Modules.Catalogo.Domain.Entity.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
