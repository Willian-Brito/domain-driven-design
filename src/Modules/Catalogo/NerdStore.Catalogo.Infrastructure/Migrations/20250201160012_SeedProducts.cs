using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NerdStore.Catalogo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "CategoriaId", "DataCadastro", "Descricao", "Imagem", "Nome", "QuantidadeEstoque", "Valor", "Altura", "Largura", "Profundidade" },
                values: new object[,]
                {
                    { new Guid("7ad3b789-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modelagem moderna e estilosa, perfeita para o dia a dia", "camiseta4.jpg", "Camiseta Urban Fit", 23, 110m, 5, 5, 5 },
                    { new Guid("9a4e9ab0-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muda de cor com líquidos quentes, um toque de magia no seu dia", "caneca1.jpg", "Caneca Magic Color", 5, 20m , 12, 8, 5},
                    { new Guid("a48db94e-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clássica, estilosa e versátil, ideal para qualquer ocasião", "camiseta1.jpg", "Camiseta Classic Print", 8, 100m, 5, 5, 5 },
                    { new Guid("a9f3c872-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tecido leve e macio para quem busca conforto absoluto", "camiseta3.jpg", "Camiseta SoftTouch", 15, 80m, 5, 5, 5 },
                    { new Guid("b21e5a57-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feita com 100% algodão para máximo conforto e durabilidade", "camiseta2.jpg", "Camiseta Premium", 3, 90m, 5, 5, 5 },
                    { new Guid("c7d8e512-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Capacidade maior para quem precisa de mais energia", "caneca2.jpg", "Caneca Gamer XL", 8, 15m , 12, 8, 5},
                    { new Guid("d6b4f3a0-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Personalize com sua estampa favorita e torne-a única", "caneca3.jpg", "Caneca Personal Print", 5, 20m , 12, 8, 5},
                    { new Guid("f4d3a756-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resistente e perfeita para café, chá ou chocolate quente", "caneca4.jpg", "Caneca Cerâmica Master", 5, 10m, 12, 8, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("7ad3b789-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("9a4e9ab0-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("a48db94e-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("a9f3c872-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("b21e5a57-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("c7d8e512-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("d6b4f3a0-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("f4d3a756-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Codigo", "Nome" },
                values: new object[,]
                {
                    { new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), 101, "Canecas" },
                    { new Guid("29950c46-5565-404f-9266-2069d7dc56bc"), 102, "Adesivos" },
                    { new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), 100, "Camisetas" }
                });
        }
    }
}
