

using NerdStore.Modules.Core.DomainObjects;
using NerdStore.Modules.Pagamentos.Business.Aggregates;
using NerdStore.Modules.Pagamentos.Business.Enum;

namespace NerdStore.Modules.Pagamentos.Business.Entities;

public class Transacao : Entity
{
    public Guid PedidoId { get; set; }
    public Guid PagamentoId { get; set; }
    public decimal Total { get; set; }
    public StatusTransacao StatusTransacao { get; set; }

    // EF. Rel.
    public Pagamento Pagamento { get; set; }
}