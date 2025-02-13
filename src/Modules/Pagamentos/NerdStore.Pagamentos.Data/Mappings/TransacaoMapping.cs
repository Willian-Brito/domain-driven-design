using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Modules.Pagamentos.Business.Entities;

namespace NerdStore.Modules.Pagamentos.Data.Mappings;
public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ToTable("Transacoes");
    }
}