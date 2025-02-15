using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NerdStore.Vendas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedVouchers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Ativo", "Codigo", "DataCriacao", "DataUtilizacao", "DataValidade", "Percentual", "Quantidade", "TipoDescontoVoucher", "Utilizado", "ValorDesconto" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-abcdef987654"), true, "WILL-P10", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, 1, 1, false, null },
                    { new Guid("d4e1f2a3-b567-c890-1234-56789abcdef0"), true, "WILL-V30", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 0, false, 30m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-abcdef987654"));

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: new Guid("d4e1f2a3-b567-c890-1234-56789abcdef0"));
        }
    }
}
