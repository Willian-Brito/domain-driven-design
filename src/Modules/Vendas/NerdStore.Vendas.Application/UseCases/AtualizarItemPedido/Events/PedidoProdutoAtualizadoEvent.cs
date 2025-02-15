using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.AtualizarItemPedido.Events;
public class PedidoProdutoAtualizadoEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }

    public PedidoProdutoAtualizadoEvent(Guid clientId, Guid pedidoId, Guid produtoId, int quantidade)
    {
        AggregateId = pedidoId;
        ClientId = clientId;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }
}
