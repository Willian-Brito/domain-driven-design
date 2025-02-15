using NerdStore.Modules.Core.DomainObjects.DTO;

namespace NerdStore.Modules.Core.Messages.CommonMessages.IntegrationEvent;
public class PedidoProcessamentoCanceladoEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public ListaProdutosPedido ProdutosPedido { get; private set; }

    public PedidoProcessamentoCanceladoEvent(Guid pedidoId, Guid clienteId, ListaProdutosPedido produtosPedido)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
        ProdutosPedido = produtosPedido;
    }
}
