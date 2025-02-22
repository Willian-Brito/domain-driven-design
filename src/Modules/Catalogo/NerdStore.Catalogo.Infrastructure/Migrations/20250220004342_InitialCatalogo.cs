using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NerdStore.Catalogo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", maxLength: 100, nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(250)", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Largura = table.Column<int>(type: "int", nullable: false),
                    Profundidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Codigo", "Nome" },
                values: new object[,]
                {
                    { new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), 101, "Canecas" },
                    { new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), 100, "Camisetas" },
                    { new Guid("42d46079-089e-4e29-b8bd-ea1ef2b00da3"), 102, "Adesivos" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "CategoriaId", "DataCadastro", "Descricao", "Imagem", "Nome", "QuantidadeEstoque", "Valor", "Altura", "Largura", "Profundidade" },
                values: new object[,]
                {
                    { new Guid("7ad3b789-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modelagem moderna e estilosa, perfeita para o dia a dia", "camiseta4.jpg", "Camiseta Urban Fit", 23, 110m, 5, 5, 5 },
                    { new Guid("9a4e9ab0-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muda de cor com líquidos quentes, um toque de magia no seu dia", "caneca1.jpg", "Caneca Magic Color", 5, 20m , 12, 8, 5},
                    { new Guid("a48db94e-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clássica, estilosa e versátil, ideal para qualquer ocasião", "camiseta1.jpg", "Camiseta Classic Print", 8, 100m, 5, 5, 5 },
                    { new Guid("a9f3c872-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tecido leve e macio para quem busca conforto absoluto", "camiseta3.jpg", "Camiseta SoftTouch", 15, 80m, 5, 5, 5 },
                    { new Guid("b21e5a57-89ab-cdef-0123-456789abcdef"), true, new Guid("eb43126c-d516-4032-907a-2b578ccbcd61"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feita com 100% algodão para máximo conforto e durabilidade", "camiseta2.jpg", "Camiseta Premium", 3, 90m, 5, 5, 5 },
                    { new Guid("c7d8e512-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Capacidade maior para quem precisa de mais energia", "caneca2.jpg", "Caneca Gamer XL", 8, 15m , 12, 8, 5},
                    { new Guid("d6b4f3a0-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Personalize com sua estampa favorita e torne-a única", "caneca3.jpg", "Caneca Personal Print", 5, 20m , 12, 8, 5},
                    { new Guid("f4d3a756-89ab-cdef-0123-456789abcdef"), true, new Guid("1b8a1e23-5a9d-42c2-a632-798e3a4a88a2"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resistente e perfeita para café, chá ou chocolate quente", "caneca4.jpg", "Caneca Cerâmica Master", 5, 10m, 12, 8, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
