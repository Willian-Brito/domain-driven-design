using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Modules.Vendas.Domain.Entities;
using NerdStore.Modules.Vendas.Domain.Enum;

namespace NerdStore.Modules.Vendas.Infrastructure.EntitiesConfiguration;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasData(
            new
            {
                Id = new Guid("d4e1f2a3-b567-c890-1234-56789abcdef0"),
                Codigo = "WILL-V30",                  
                ValorDesconto = 30M,
                Quantidade = 1,
                TipoDescontoVoucher = TipoDescontoVoucher.Valor,
                DataCriacao = new DateTime(2025, 2, 1),
                DataValidade = new DateTime(2025, 3, 20),
                Ativo = true,
                Utilizado = false
            },
            new
            {
                Id = new Guid("a1b2c3d4-e5f6-7890-1234-abcdef987654"),
                Codigo = "WILL-P10",
                Percentual = 10M,
                Quantidade = 1,
                TipoDescontoVoucher = TipoDescontoVoucher.Porcentagem,
                DataCriacao = new DateTime(2025, 2, 1),
                DataValidade = new DateTime(2025, 3, 20),
                Ativo = true,
                Utilizado = false
            }
        );
    }
}