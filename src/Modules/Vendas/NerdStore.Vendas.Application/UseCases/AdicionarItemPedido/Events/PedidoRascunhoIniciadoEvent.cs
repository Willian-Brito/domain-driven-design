using NerdStore.Modules.Core.Messages;

namespace NerdStore.Modules.Vendas.Application.UseCases.AdicionarItemPedido.Events;

public class PedidoRascunhoIniciadoEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid PedidoId { get; private set; }

    public PedidoRascunhoIniciadoEvent(Guid clientId, Guid pedidoId)
    {
        AggregateId = pedidoId;
        ClientId = clientId;
        PedidoId = pedidoId;
    }
}