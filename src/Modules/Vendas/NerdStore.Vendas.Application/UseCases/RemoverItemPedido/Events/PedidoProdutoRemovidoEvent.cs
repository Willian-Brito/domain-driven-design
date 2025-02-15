using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.RemoverItemPedido.Events;
public class PedidoProdutoRemovidoEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public PedidoProdutoRemovidoEvent(Guid clientId, Guid pedidoId, Guid produtoId)
    {
        AggregateId = pedidoId;
        ClientId = clientId;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
    }
}